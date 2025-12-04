using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace Hanna_Studio.NodeEditor
{
    /// <summary>
    /// Represents a sequence node on the canvas with input/output ports.
    /// </summary>
    public class SequenceNode
    {
        #region Constants

        private const float NodeWidth = 200;
        private const float NodeHeight = 80;
        private const float HeaderHeight = 28;
        private const float PortRadius = 8;
        private const float PortSpacing = 24;
        private const float CornerRadius = 8;

        #endregion

        #region Fields

        private PointF _position;
        private List<NodePort> _outputPorts = new List<NodePort>();
        private NodePort _inputPort;
        private string _mainText;
        private string _secondaryText;

        #endregion

        #region Properties

        public string SequenceId { get; private set; }
        public bool IsEndSequence { get; set; }
        public bool IsHovered { get; set; }
        public bool IsStartSequence { get; set; }

        public PointF Position
        {
            get => _position;
            set
            {
                _position = value;
                UpdatePortPositions();
            }
        }

        public RectangleF Bounds => new RectangleF(_position.X, _position.Y, NodeWidth, NodeHeight);

        public NodePort InputPort => _inputPort;
        public IReadOnlyList<NodePort> OutputPorts => _outputPorts.AsReadOnly();

        public string MainText
        {
            get => _mainText;
            set => _mainText = value ?? "";
        }

        public string SecondaryText
        {
            get => _secondaryText;
            set => _secondaryText = value ?? "";
        }

        #endregion

        #region Constructor

        public SequenceNode(string sequenceId, PointF position, bool isEndSequence = false)
        {
            SequenceId = sequenceId;
            IsEndSequence = isEndSequence;
            _position = position;
            _mainText = "Main Text";
            _secondaryText = "Secondary Text";

            // Create input port
            _inputPort = new NodePort(this, null, false);
            UpdatePortPositions();
        }

        #endregion

        #region Port Management

        public void SetChoices(IEnumerable<string> choiceLetters)
        {
            _outputPorts.Clear();

            foreach (var letter in choiceLetters.OrderBy(l => l))
            {
                _outputPorts.Add(new NodePort(this, letter, true));
            }

            UpdatePortPositions();
        }

        public void AddChoice(string choiceLetter)
        {
            if (_outputPorts.Any(p => p.ChoiceLetter == choiceLetter)) return;

            _outputPorts.Add(new NodePort(this, choiceLetter, true));
            _outputPorts = _outputPorts.OrderBy(p => p.ChoiceLetter).ToList();
            UpdatePortPositions();
        }

        public void RemoveChoice(string choiceLetter)
        {
            _outputPorts.RemoveAll(p => p.ChoiceLetter == choiceLetter);
            UpdatePortPositions();
        }

        public NodePort GetOutputPort(string choiceLetter)
        {
            return _outputPorts.FirstOrDefault(p => p.ChoiceLetter == choiceLetter);
        }

        public NodePort GetPortAtPoint(PointF point)
        {
            if (_inputPort.Bounds.Contains(point)) return _inputPort;

            foreach (var port in _outputPorts)
            {
                if (port.Bounds.Contains(point)) return port;
            }

            return null;
        }

        private void UpdatePortPositions()
        {
            // Input port on left side, centered vertically
            _inputPort.Center = new PointF(_position.X, _position.Y + NodeHeight / 2);

            // Output ports on right side
            if (_outputPorts.Count > 0)
            {
                float totalHeight = (_outputPorts.Count - 1) * PortSpacing;
                float startY = _position.Y + NodeHeight / 2 - totalHeight / 2;

                for (int i = 0; i < _outputPorts.Count; i++)
                {
                    _outputPorts[i].Center = new PointF(
                        _position.X + NodeWidth,
                        startY + i * PortSpacing);
                }
            }
        }

        #endregion

        #region Update

        public void UpdateFromSequence(Sequence sequence)
        {
            if (sequence == null) return;

            MainText = sequence.mainText;
            SecondaryText = sequence.secondaryText;
            IsEndSequence = sequence.type == "end";

            // Update choices
            if (sequence.choices != null)
            {
                SetChoices(sequence.choices.Keys);
            }
            else
            {
                _outputPorts.Clear();
                UpdatePortPositions();
            }
        }

        #endregion

        #region Drawing

        public void Draw(Graphics g, float zoom, bool isSelected)
        {
            // Colors
            var headerColor = IsEndSequence
                ? Color.FromArgb(180, 80, 80)
                : (IsStartSequence ? Color.FromArgb(80, 180, 80) : Color.FromArgb(70, 130, 180));

            var bodyColor = Color.FromArgb(50, 50, 55);
            var bodyColorHover = Color.FromArgb(60, 60, 65);
            var borderColor = isSelected ? Color.FromArgb(100, 180, 255) : Color.FromArgb(80, 80, 85);
            var selectedBorderColor = Color.FromArgb(100, 180, 255);

            using (var path = CreateRoundedRectangle(Bounds, CornerRadius))
            using (var headerPath = CreateHeaderPath(CornerRadius))
            {
                // Shadow
                using (var shadowBrush = new SolidBrush(Color.FromArgb(50, 0, 0, 0)))
                {
                    var shadowPath = CreateRoundedRectangle(
                        new RectangleF(_position.X + 3, _position.Y + 3, NodeWidth, NodeHeight),
                        CornerRadius);
                    g.FillPath(shadowBrush, shadowPath);
                    shadowPath.Dispose();
                }

                // Body
                using (var bodyBrush = new SolidBrush(IsHovered ? bodyColorHover : bodyColor))
                {
                    g.FillPath(bodyBrush, path);
                }

                // Header
                using (var headerBrush = new LinearGradientBrush(
                    new PointF(_position.X, _position.Y),
                    new PointF(_position.X, _position.Y + HeaderHeight),
                    headerColor,
                    Color.FromArgb(headerColor.R - 20, headerColor.G - 20, headerColor.B - 20)))
                {
                    g.FillPath(headerBrush, headerPath);
                }

                // Border
                using (var borderPen = new Pen(isSelected ? selectedBorderColor : borderColor, isSelected ? 2f : 1f))
                {
                    g.DrawPath(borderPen, path);
                }

                // Sequence ID (header text)
                using (var font = new Font("Segoe UI Semibold", 10f, FontStyle.Bold))
                using (var brush = new SolidBrush(Color.White))
                {
                    var textRect = new RectangleF(_position.X + 10, _position.Y + 5, NodeWidth - 20, HeaderHeight - 5);
                    var format = new StringFormat
                    {
                        Alignment = StringAlignment.Near,
                        LineAlignment = StringAlignment.Center,
                        Trimming = StringTrimming.EllipsisCharacter
                    };
                    g.DrawString(SequenceId, font, brush, textRect, format);
                }

                // Type badge
                if (IsEndSequence || IsStartSequence)
                {
                    var badgeText = IsEndSequence ? "END" : "START";
                    using (var font = new Font("Segoe UI", 7f, FontStyle.Bold))
                    using (var brush = new SolidBrush(Color.FromArgb(200, 255, 255, 255)))
                    {
                        var textSize = g.MeasureString(badgeText, font);
                        var badgeX = _position.X + NodeWidth - textSize.Width - 8;
                        g.DrawString(badgeText, font, brush, badgeX, _position.Y + 8);
                    }
                }

                // Main text preview
                using (var font = new Font("Segoe UI", 8f))
                using (var brush = new SolidBrush(Color.FromArgb(200, 200, 200)))
                {
                    var textRect = new RectangleF(_position.X + 10, _position.Y + HeaderHeight + 5, NodeWidth - 20, 20);
                    var format = new StringFormat
                    {
                        Alignment = StringAlignment.Near,
                        LineAlignment = StringAlignment.Near,
                        Trimming = StringTrimming.EllipsisCharacter
                    };

                    var displayText = string.IsNullOrEmpty(_mainText) ? "(no text)" : _mainText;
                    if (displayText.Length > 30) displayText = displayText.Substring(0, 27) + "...";
                    g.DrawString(displayText, font, brush, textRect, format);
                }

                // Choices indicator
                if (_outputPorts.Count > 0)
                {
                    using (var font = new Font("Segoe UI", 7f))
                    using (var brush = new SolidBrush(Color.FromArgb(150, 150, 150)))
                    {
                        var choicesText = $"{_outputPorts.Count} choice{(_outputPorts.Count > 1 ? "s" : "")}";
                        g.DrawString(choicesText, font, brush, _position.X + 10, _position.Y + NodeHeight - 18);
                    }
                }

                // Draw ports
                DrawPort(g, _inputPort, false);
                foreach (var port in _outputPorts)
                {
                    DrawPort(g, port, true);
                }
            }
        }

        private void DrawPort(Graphics g, NodePort port, bool isOutput)
        {
            var center = port.Center;
            var radius = PortRadius;

            // Port colors
            var portColor = isOutput ? Color.FromArgb(100, 180, 255) : Color.FromArgb(180, 100, 255);
            var portBorderColor = Color.FromArgb(80, 80, 85);

            using (var brush = new SolidBrush(portColor))
            using (var borderPen = new Pen(portBorderColor, 1.5f))
            {
                g.FillEllipse(brush, center.X - radius, center.Y - radius, radius * 2, radius * 2);
                g.DrawEllipse(borderPen, center.X - radius, center.Y - radius, radius * 2, radius * 2);
            }

            // Draw choice letter for output ports
            if (isOutput && !string.IsNullOrEmpty(port.ChoiceLetter))
            {
                using (var font = new Font("Segoe UI", 7f, FontStyle.Bold))
                using (var brush = new SolidBrush(Color.White))
                {
                    var textSize = g.MeasureString(port.ChoiceLetter, font);
                    g.DrawString(port.ChoiceLetter, font, brush,
                        center.X - textSize.Width / 2,
                        center.Y - textSize.Height / 2);
                }
            }
        }

        private GraphicsPath CreateRoundedRectangle(RectangleF rect, float radius)
        {
            var path = new GraphicsPath();
            float diameter = radius * 2;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }

        private GraphicsPath CreateHeaderPath(float radius)
        {
            var path = new GraphicsPath();
            float diameter = radius * 2;
            var rect = new RectangleF(_position.X, _position.Y, NodeWidth, HeaderHeight);

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddLine(rect.Right, rect.Bottom, rect.X, rect.Bottom);
            path.CloseFigure();

            return path;
        }

        #endregion
    }

    /// <summary>
    /// Represents a connection port on a node.
    /// </summary>
    public class NodePort
    {
        private const float PortRadius = 8;

        public SequenceNode ParentNode { get; }
        public string ChoiceLetter { get; }
        public bool IsOutput { get; }
        public PointF Center { get; set; }

        public RectangleF Bounds => new RectangleF(
            Center.X - PortRadius - 2,
            Center.Y - PortRadius - 2,
            (PortRadius + 2) * 2,
            (PortRadius + 2) * 2);

        public NodePort(SequenceNode parentNode, string choiceLetter, bool isOutput)
        {
            ParentNode = parentNode;
            ChoiceLetter = choiceLetter;
            IsOutput = isOutput;
        }
    }

    /// <summary>
    /// Represents a connection between two nodes.
    /// </summary>
    public class NodeConnection
    {
        public SequenceNode SourceNode { get; }
        public string ChoiceLetter { get; }
        public SequenceNode TargetNode { get; }
        public bool IsHighlighted { get; set; }

        public NodeConnection(SequenceNode sourceNode, string choiceLetter, SequenceNode targetNode)
        {
            SourceNode = sourceNode;
            ChoiceLetter = choiceLetter;
            TargetNode = targetNode;
        }
    }
}
