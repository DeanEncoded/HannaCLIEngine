using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Hanna_Studio.NodeEditor
{
    /// <summary>
    /// High-performance infinite canvas for node-based sequence editing.
    /// Supports pan, zoom, node dragging, and connection management.
    /// </summary>
    public class NodeEditorCanvas : Control
    {
        #region Fields

        private List<SequenceNode> _nodes = new List<SequenceNode>();
        private List<NodeConnection> _connections = new List<NodeConnection>();
        
        // Canvas transformation
        private PointF _panOffset = PointF.Empty;
        private float _zoom = 1.0f;
        private const float MinZoom = 0.25f;
        private const float MaxZoom = 2.0f;
        private const float ZoomStep = 0.1f;

        // Interaction state
        private bool _isPanning = false;
        private Point _lastMousePosition;
        private SequenceNode _selectedNode = null;
        private SequenceNode _draggedNode = null;
        private PointF _dragOffset;
        private NodePort _connectionStartPort = null;
        private Point _connectionEndPoint;
        private bool _isConnecting = false;

        // Selection
        private List<SequenceNode> _selectedNodes = new List<SequenceNode>();
        private bool _isBoxSelecting = false;
        private Point _boxSelectStart;
        private Rectangle _boxSelectRect;

        // Grid settings
        private bool _showGrid = true;
        private int _gridSize = 20;
        private int _majorGridSize = 100;

        // Colors - Professional dark theme
        private Color _backgroundColor = Color.FromArgb(30, 30, 30);
        private Color _gridColor = Color.FromArgb(45, 45, 45);
        private Color _majorGridColor = Color.FromArgb(55, 55, 55);
        private Color _connectionColor = Color.FromArgb(100, 180, 255);
        private Color _connectionHighlightColor = Color.FromArgb(255, 200, 100);
        private Color _selectionBoxColor = Color.FromArgb(50, 100, 180, 255);
        private Color _selectionBoxBorderColor = Color.FromArgb(100, 180, 255);

        // Performance
        private BufferedGraphicsContext _graphicsContext;
        private BufferedGraphics _bufferedGraphics;

        // Events
        public event EventHandler<SequenceNode> NodeSelected;
        public event EventHandler<SequenceNode> NodeDoubleClicked;
        public event EventHandler<NodeConnection> ConnectionCreated;
        public event EventHandler<NodeConnection> ConnectionDeleted;
        public event EventHandler<SequenceNode> NodeDeleted;
        public event EventHandler CanvasChanged;

        #endregion

        #region Properties

        public float Zoom
        {
            get => _zoom;
            set
            {
                _zoom = Math.Max(MinZoom, Math.Min(MaxZoom, value));
                Invalidate();
            }
        }

        public PointF PanOffset
        {
            get => _panOffset;
            set
            {
                _panOffset = value;
                Invalidate();
            }
        }

        public bool ShowGrid
        {
            get => _showGrid;
            set
            {
                _showGrid = value;
                Invalidate();
            }
        }

        public SequenceNode SelectedNode => _selectedNode;

        public IReadOnlyList<SequenceNode> Nodes => _nodes.AsReadOnly();
        public IReadOnlyList<NodeConnection> Connections => _connections.AsReadOnly();

        #endregion

        #region Constructor

        public NodeEditorCanvas()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);

            BackColor = _backgroundColor;
            _graphicsContext = BufferedGraphicsManager.Current;

            // Enable mouse wheel
            MouseWheel += OnMouseWheel;
        }

        #endregion

        #region Node Management

        public SequenceNode AddNode(string sequenceId, PointF position, bool isEndSequence = false)
        {
            var node = new SequenceNode(sequenceId, position, isEndSequence);
            _nodes.Add(node);
            Invalidate();
            CanvasChanged?.Invoke(this, EventArgs.Empty);
            return node;
        }

        public void RemoveNode(SequenceNode node)
        {
            if (node == null) return;

            // Remove all connections to/from this node
            _connections.RemoveAll(c => c.SourceNode == node || c.TargetNode == node);

            _nodes.Remove(node);
            if (_selectedNode == node)
            {
                _selectedNode = null;
            }
            _selectedNodes.Remove(node);

            NodeDeleted?.Invoke(this, node);
            Invalidate();
            CanvasChanged?.Invoke(this, EventArgs.Empty);
        }

        public SequenceNode FindNodeBySequenceId(string sequenceId)
        {
            return _nodes.FirstOrDefault(n => n.SequenceId == sequenceId);
        }

        public void ClearAll()
        {
            _nodes.Clear();
            _connections.Clear();
            _selectedNode = null;
            _selectedNodes.Clear();
            Invalidate();
            CanvasChanged?.Invoke(this, EventArgs.Empty);
        }

        public void UpdateNodeFromSequence(string sequenceId, Sequence sequence)
        {
            var node = FindNodeBySequenceId(sequenceId);
            if (node != null)
            {
                node.UpdateFromSequence(sequence);
                Invalidate();
            }
        }

        #endregion

        #region Connection Management

        public NodeConnection AddConnection(SequenceNode sourceNode, string choiceLetter, SequenceNode targetNode)
        {
            if (sourceNode == null || targetNode == null) return null;
            if (sourceNode == targetNode) return null;

            // Check if connection already exists
            var existing = _connections.FirstOrDefault(c =>
                c.SourceNode == sourceNode &&
                c.ChoiceLetter == choiceLetter &&
                c.TargetNode == targetNode);

            if (existing != null) return existing;

            var connection = new NodeConnection(sourceNode, choiceLetter, targetNode);
            _connections.Add(connection);
            ConnectionCreated?.Invoke(this, connection);
            Invalidate();
            CanvasChanged?.Invoke(this, EventArgs.Empty);
            return connection;
        }

        public void RemoveConnection(NodeConnection connection)
        {
            if (connection == null) return;
            _connections.Remove(connection);
            ConnectionDeleted?.Invoke(this, connection);
            Invalidate();
            CanvasChanged?.Invoke(this, EventArgs.Empty);
        }

        public void RemoveConnectionsForChoice(SequenceNode sourceNode, string choiceLetter)
        {
            var toRemove = _connections.Where(c =>
                c.SourceNode == sourceNode && c.ChoiceLetter == choiceLetter).ToList();

            foreach (var conn in toRemove)
            {
                RemoveConnection(conn);
            }
        }

        #endregion

        #region Coordinate Transformation

        public PointF ScreenToCanvas(Point screenPoint)
        {
            return new PointF(
                (screenPoint.X - _panOffset.X) / _zoom,
                (screenPoint.Y - _panOffset.Y) / _zoom
            );
        }

        private PointF CanvasToScreen(PointF canvasPoint)
        {
            return new PointF(
                canvasPoint.X * _zoom + _panOffset.X,
                canvasPoint.Y * _zoom + _panOffset.Y
            );
        }

        private RectangleF CanvasToScreen(RectangleF canvasRect)
        {
            var topLeft = CanvasToScreen(canvasRect.Location);
            return new RectangleF(
                topLeft.X,
                topLeft.Y,
                canvasRect.Width * _zoom,
                canvasRect.Height * _zoom
            );
        }

        #endregion

        #region Painting

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            // Clear background
            g.Clear(_backgroundColor);

            // Draw grid
            if (_showGrid)
            {
                DrawGrid(g);
            }

            // Apply transformation for canvas elements
            g.TranslateTransform(_panOffset.X, _panOffset.Y);
            g.ScaleTransform(_zoom, _zoom);

            // Draw connections
            DrawConnections(g);

            // Draw temporary connection line
            if (_isConnecting && _connectionStartPort != null)
            {
                DrawTemporaryConnection(g);
            }

            // Draw nodes
            DrawNodes(g);

            // Reset transformation for UI elements
            g.ResetTransform();

            // Draw selection box
            if (_isBoxSelecting)
            {
                DrawSelectionBox(g);
            }

            // Draw zoom level indicator
            DrawZoomIndicator(g);

            // Draw minimap
            DrawMinimap(g);
        }

        private void DrawGrid(Graphics g)
        {
            using (var gridPen = new Pen(_gridColor, 1))
            using (var majorGridPen = new Pen(_majorGridColor, 1))
            {
                // Calculate visible area in canvas coordinates
                var topLeft = ScreenToCanvas(Point.Empty);
                var bottomRight = ScreenToCanvas(new Point(Width, Height));

                // Draw minor grid
                int startX = (int)(Math.Floor(topLeft.X / _gridSize) * _gridSize);
                int startY = (int)(Math.Floor(topLeft.Y / _gridSize) * _gridSize);

                for (float x = startX; x <= bottomRight.X; x += _gridSize)
                {
                    var screenX = x * _zoom + _panOffset.X;
                    bool isMajor = Math.Abs(x % _majorGridSize) < 0.01f;
                    g.DrawLine(isMajor ? majorGridPen : gridPen, screenX, 0, screenX, Height);
                }

                for (float y = startY; y <= bottomRight.Y; y += _gridSize)
                {
                    var screenY = y * _zoom + _panOffset.Y;
                    bool isMajor = Math.Abs(y % _majorGridSize) < 0.01f;
                    g.DrawLine(isMajor ? majorGridPen : gridPen, 0, screenY, Width, screenY);
                }
            }
        }

        private void DrawConnections(Graphics g)
        {
            foreach (var connection in _connections)
            {
                DrawConnection(g, connection);
            }
        }

        private void DrawConnection(Graphics g, NodeConnection connection)
        {
            var sourcePort = connection.SourceNode.GetOutputPort(connection.ChoiceLetter);
            var targetPort = connection.TargetNode.InputPort;

            if (sourcePort == null) return;

            var start = sourcePort.Center;
            var end = targetPort.Center;

            // Draw bezier curve
            var controlPointOffset = Math.Max(50, Math.Abs(end.X - start.X) / 2);

            using (var pen = new Pen(connection.IsHighlighted ? _connectionHighlightColor : _connectionColor, 2.5f / _zoom))
            {
                pen.CustomEndCap = new AdjustableArrowCap(4, 4);

                var controlPoint1 = new PointF(start.X + controlPointOffset, start.Y);
                var controlPoint2 = new PointF(end.X - controlPointOffset, end.Y);

                g.DrawBezier(pen, start, controlPoint1, controlPoint2, end);
            }

            // Draw choice letter on connection
            var midPoint = GetBezierMidpoint(start, 
                new PointF(start.X + controlPointOffset, start.Y),
                new PointF(end.X - controlPointOffset, end.Y), 
                end);

            using (var font = new Font("Segoe UI", 8f / _zoom, FontStyle.Bold))
            using (var brush = new SolidBrush(_connectionColor))
            using (var bgBrush = new SolidBrush(Color.FromArgb(200, 30, 30, 30)))
            {
                var text = connection.ChoiceLetter;
                var textSize = g.MeasureString(text, font);
                var textRect = new RectangleF(
                    midPoint.X - textSize.Width / 2 - 3,
                    midPoint.Y - textSize.Height / 2 - 2,
                    textSize.Width + 6,
                    textSize.Height + 4);

                g.FillRectangle(bgBrush, textRect);
                g.DrawString(text, font, brush, midPoint.X - textSize.Width / 2, midPoint.Y - textSize.Height / 2);
            }
        }

        private PointF GetBezierMidpoint(PointF p0, PointF p1, PointF p2, PointF p3)
        {
            // t = 0.5 for midpoint
            float t = 0.5f;
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            float x = uuu * p0.X + 3 * uu * t * p1.X + 3 * u * tt * p2.X + ttt * p3.X;
            float y = uuu * p0.Y + 3 * uu * t * p1.Y + 3 * u * tt * p2.Y + ttt * p3.Y;

            return new PointF(x, y);
        }

        private void DrawTemporaryConnection(Graphics g)
        {
            var start = _connectionStartPort.Center;
            var end = ScreenToCanvas(_connectionEndPoint);

            var controlPointOffset = Math.Max(50, Math.Abs(end.X - start.X) / 2);

            using (var pen = new Pen(_connectionHighlightColor, 2f / _zoom))
            {
                pen.DashStyle = DashStyle.Dash;

                var controlPoint1 = new PointF(start.X + controlPointOffset, start.Y);
                var controlPoint2 = new PointF(end.X - controlPointOffset, end.Y);

                g.DrawBezier(pen, start, controlPoint1, controlPoint2, end);
            }
        }

        private void DrawNodes(Graphics g)
        {
            foreach (var node in _nodes)
            {
                node.Draw(g, _zoom, _selectedNodes.Contains(node));
            }
        }

        private void DrawSelectionBox(Graphics g)
        {
            using (var fillBrush = new SolidBrush(_selectionBoxColor))
            using (var borderPen = new Pen(_selectionBoxBorderColor, 1))
            {
                borderPen.DashStyle = DashStyle.Dash;
                g.FillRectangle(fillBrush, _boxSelectRect);
                g.DrawRectangle(borderPen, _boxSelectRect);
            }
        }

        private void DrawZoomIndicator(Graphics g)
        {
            using (var font = new Font("Segoe UI", 9f, FontStyle.Regular))
            using (var brush = new SolidBrush(Color.FromArgb(150, 255, 255, 255)))
            {
                var text = $"{(int)(_zoom * 100)}%";
                g.DrawString(text, font, brush, 10, Height - 25);
            }
        }

        private void DrawMinimap(Graphics g)
        {
            if (_nodes.Count == 0) return;

            var minimapSize = new Size(150, 100);
            var minimapRect = new Rectangle(Width - minimapSize.Width - 10, Height - minimapSize.Height - 10,
                minimapSize.Width, minimapSize.Height);

            // Background
            using (var bgBrush = new SolidBrush(Color.FromArgb(180, 20, 20, 20)))
            using (var borderPen = new Pen(Color.FromArgb(100, 100, 100, 100), 1))
            {
                g.FillRectangle(bgBrush, minimapRect);
                g.DrawRectangle(borderPen, minimapRect);
            }

            // Calculate bounds of all nodes
            float minX = float.MaxValue, minY = float.MaxValue;
            float maxX = float.MinValue, maxY = float.MinValue;

            foreach (var node in _nodes)
            {
                minX = Math.Min(minX, node.Bounds.Left);
                minY = Math.Min(minY, node.Bounds.Top);
                maxX = Math.Max(maxX, node.Bounds.Right);
                maxY = Math.Max(maxY, node.Bounds.Bottom);
            }

            // Add padding
            float padding = 50;
            minX -= padding;
            minY -= padding;
            maxX += padding;
            maxY += padding;

            float contentWidth = maxX - minX;
            float contentHeight = maxY - minY;

            if (contentWidth <= 0 || contentHeight <= 0) return;

            // Calculate scale to fit content in minimap
            float scaleX = (minimapRect.Width - 10) / contentWidth;
            float scaleY = (minimapRect.Height - 10) / contentHeight;
            float scale = Math.Min(scaleX, scaleY);

            // Draw nodes in minimap
            using (var nodeBrush = new SolidBrush(Color.FromArgb(150, 100, 180, 255)))
            using (var endNodeBrush = new SolidBrush(Color.FromArgb(150, 255, 100, 100)))
            {
                foreach (var node in _nodes)
                {
                    float x = minimapRect.X + 5 + (node.Bounds.X - minX) * scale;
                    float y = minimapRect.Y + 5 + (node.Bounds.Y - minY) * scale;
                    float w = Math.Max(3, node.Bounds.Width * scale);
                    float h = Math.Max(2, node.Bounds.Height * scale);

                    g.FillRectangle(node.IsEndSequence ? endNodeBrush : nodeBrush, x, y, w, h);
                }
            }

            // Draw viewport rectangle
            var viewTopLeft = ScreenToCanvas(Point.Empty);
            var viewBottomRight = ScreenToCanvas(new Point(Width, Height));

            float viewX = minimapRect.X + 5 + (viewTopLeft.X - minX) * scale;
            float viewY = minimapRect.Y + 5 + (viewTopLeft.Y - minY) * scale;
            float viewW = (viewBottomRight.X - viewTopLeft.X) * scale;
            float viewH = (viewBottomRight.Y - viewTopLeft.Y) * scale;

            using (var viewPen = new Pen(Color.FromArgb(200, 255, 255, 255), 1))
            {
                g.DrawRectangle(viewPen, viewX, viewY, viewW, viewH);
            }
        }

        #endregion

        #region Mouse Handling

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Focus();

            _lastMousePosition = e.Location;
            var canvasPos = ScreenToCanvas(e.Location);

            if (e.Button == MouseButtons.Middle || (e.Button == MouseButtons.Left && ModifierKeys.HasFlag(Keys.Space)))
            {
                // Start panning
                _isPanning = true;
                Cursor = Cursors.SizeAll;
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                // Check for port click (start connection)
                foreach (var node in _nodes)
                {
                    var port = node.GetPortAtPoint(canvasPos);
                    if (port != null && port.IsOutput)
                    {
                        _isConnecting = true;
                        _connectionStartPort = port;
                        _connectionEndPoint = e.Location;
                        return;
                    }
                }

                // Check for node click
                var clickedNode = _nodes.LastOrDefault(n => n.Bounds.Contains(canvasPos));
                if (clickedNode != null)
                {
                    if (ModifierKeys.HasFlag(Keys.Control))
                    {
                        // Toggle selection
                        if (_selectedNodes.Contains(clickedNode))
                            _selectedNodes.Remove(clickedNode);
                        else
                            _selectedNodes.Add(clickedNode);
                    }
                    else if (!_selectedNodes.Contains(clickedNode))
                    {
                        _selectedNodes.Clear();
                        _selectedNodes.Add(clickedNode);
                    }

                    _selectedNode = clickedNode;
                    _draggedNode = clickedNode;
                    _dragOffset = new PointF(canvasPos.X - clickedNode.Position.X, canvasPos.Y - clickedNode.Position.Y);

                    NodeSelected?.Invoke(this, clickedNode);
                    Invalidate();
                    return;
                }

                // Start box selection
                _selectedNodes.Clear();
                _selectedNode = null;
                _isBoxSelecting = true;
                _boxSelectStart = e.Location;
                _boxSelectRect = Rectangle.Empty;
                NodeSelected?.Invoke(this, null);
                Invalidate();
            }

            if (e.Button == MouseButtons.Right)
            {
                // Right-click on node
                var clickedNode = _nodes.LastOrDefault(n => n.Bounds.Contains(canvasPos));
                if (clickedNode != null)
                {
                    _selectedNode = clickedNode;
                    if (!_selectedNodes.Contains(clickedNode))
                    {
                        _selectedNodes.Clear();
                        _selectedNodes.Add(clickedNode);
                    }
                    NodeSelected?.Invoke(this, clickedNode);
                    Invalidate();
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (_isPanning)
            {
                var delta = new Point(e.X - _lastMousePosition.X, e.Y - _lastMousePosition.Y);
                _panOffset.X += delta.X;
                _panOffset.Y += delta.Y;
                _lastMousePosition = e.Location;
                Invalidate();
                return;
            }

            if (_isConnecting)
            {
                _connectionEndPoint = e.Location;
                Invalidate();
                return;
            }

            if (_draggedNode != null)
            {
                var canvasPos = ScreenToCanvas(e.Location);
                var newPos = new PointF(canvasPos.X - _dragOffset.X, canvasPos.Y - _dragOffset.Y);

                // Snap to grid if Shift is not held
                if (!ModifierKeys.HasFlag(Keys.Shift))
                {
                    newPos.X = (float)Math.Round(newPos.X / _gridSize) * _gridSize;
                    newPos.Y = (float)Math.Round(newPos.Y / _gridSize) * _gridSize;
                }

                // Move all selected nodes
                var delta = new PointF(newPos.X - _draggedNode.Position.X, newPos.Y - _draggedNode.Position.Y);
                foreach (var node in _selectedNodes)
                {
                    node.Position = new PointF(node.Position.X + delta.X, node.Position.Y + delta.Y);
                }

                Invalidate();
                return;
            }

            if (_isBoxSelecting)
            {
                int x = Math.Min(_boxSelectStart.X, e.X);
                int y = Math.Min(_boxSelectStart.Y, e.Y);
                int w = Math.Abs(e.X - _boxSelectStart.X);
                int h = Math.Abs(e.Y - _boxSelectStart.Y);
                _boxSelectRect = new Rectangle(x, y, w, h);

                // Update selection
                _selectedNodes.Clear();
                foreach (var node in _nodes)
                {
                    var screenBounds = CanvasToScreen(node.Bounds);
                    if (_boxSelectRect.IntersectsWith(Rectangle.Round(screenBounds)))
                    {
                        _selectedNodes.Add(node);
                    }
                }

                Invalidate();
                return;
            }

            // Hover effects
            var pos = ScreenToCanvas(e.Location);
            bool needsRepaint = false;

            foreach (var node in _nodes)
            {
                var wasHovered = node.IsHovered;
                node.IsHovered = node.Bounds.Contains(pos);
                if (wasHovered != node.IsHovered) needsRepaint = true;
            }

            if (needsRepaint) Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (_isPanning)
            {
                _isPanning = false;
                Cursor = Cursors.Default;
            }

            if (_isConnecting && _connectionStartPort != null)
            {
                var canvasPos = ScreenToCanvas(e.Location);

                // Find target node's input port
                foreach (var node in _nodes)
                {
                    if (node == _connectionStartPort.ParentNode) continue;

                    if (node.InputPort.Bounds.Contains(canvasPos))
                    {
                        AddConnection(_connectionStartPort.ParentNode, _connectionStartPort.ChoiceLetter, node);
                        break;
                    }
                }

                _isConnecting = false;
                _connectionStartPort = null;
                Invalidate();
            }

            if (_draggedNode != null)
            {
                _draggedNode = null;
                CanvasChanged?.Invoke(this, EventArgs.Empty);
            }

            if (_isBoxSelecting)
            {
                _isBoxSelecting = false;
                if (_selectedNodes.Count == 1)
                {
                    _selectedNode = _selectedNodes[0];
                    NodeSelected?.Invoke(this, _selectedNode);
                }
                Invalidate();
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            if (e.Button == MouseButtons.Left)
            {
                var canvasPos = ScreenToCanvas(e.Location);
                var clickedNode = _nodes.LastOrDefault(n => n.Bounds.Contains(canvasPos));

                if (clickedNode != null)
                {
                    NodeDoubleClicked?.Invoke(this, clickedNode);
                }
            }
        }

        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            // Zoom towards mouse position
            var mouseCanvasBefore = ScreenToCanvas(e.Location);

            if (e.Delta > 0)
                _zoom = Math.Min(MaxZoom, _zoom + ZoomStep);
            else
                _zoom = Math.Max(MinZoom, _zoom - ZoomStep);

            var mouseCanvasAfter = ScreenToCanvas(e.Location);

            // Adjust pan to keep mouse position fixed
            _panOffset.X += (mouseCanvasAfter.X - mouseCanvasBefore.X) * _zoom;
            _panOffset.Y += (mouseCanvasAfter.Y - mouseCanvasBefore.Y) * _zoom;

            Invalidate();
        }

        #endregion

        #region Keyboard Handling

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Delete:
                    DeleteSelectedNodes();
                    return true;

                case Keys.Control | Keys.A:
                    SelectAllNodes();
                    return true;

                case Keys.Escape:
                    ClearSelection();
                    return true;

                case Keys.F:
                    FitAllNodes();
                    return true;

                case Keys.Home:
                    ResetView();
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void DeleteSelectedNodes()
        {
            var nodesToDelete = _selectedNodes.ToList();
            foreach (var node in nodesToDelete)
            {
                RemoveNode(node);
            }
            _selectedNodes.Clear();
            _selectedNode = null;
        }

        private void SelectAllNodes()
        {
            _selectedNodes.Clear();
            _selectedNodes.AddRange(_nodes);
            Invalidate();
        }

        private void ClearSelection()
        {
            _selectedNodes.Clear();
            _selectedNode = null;
            _isConnecting = false;
            _connectionStartPort = null;
            NodeSelected?.Invoke(this, null);
            Invalidate();
        }

        #endregion

        #region View Control

        public void ResetView()
        {
            _zoom = 1.0f;
            _panOffset = new PointF(Width / 2, Height / 2);
            Invalidate();
        }

        public void FitAllNodes()
        {
            if (_nodes.Count == 0)
            {
                ResetView();
                return;
            }

            // Calculate bounds of all nodes
            float minX = float.MaxValue, minY = float.MaxValue;
            float maxX = float.MinValue, maxY = float.MinValue;

            foreach (var node in _nodes)
            {
                minX = Math.Min(minX, node.Bounds.Left);
                minY = Math.Min(minY, node.Bounds.Top);
                maxX = Math.Max(maxX, node.Bounds.Right);
                maxY = Math.Max(maxY, node.Bounds.Bottom);
            }

            // Add padding
            float padding = 50;
            minX -= padding;
            minY -= padding;
            maxX += padding;
            maxY += padding;

            float contentWidth = maxX - minX;
            float contentHeight = maxY - minY;

            // Calculate zoom to fit
            float zoomX = Width / contentWidth;
            float zoomY = Height / contentHeight;
            _zoom = Math.Max(MinZoom, Math.Min(MaxZoom, Math.Min(zoomX, zoomY)));

            // Center content
            float centerX = (minX + maxX) / 2;
            float centerY = (minY + maxY) / 2;

            _panOffset.X = Width / 2 - centerX * _zoom;
            _panOffset.Y = Height / 2 - centerY * _zoom;

            Invalidate();
        }

        public void CenterOnNode(SequenceNode node)
        {
            if (node == null) return;

            var nodeCenter = new PointF(
                node.Bounds.X + node.Bounds.Width / 2,
                node.Bounds.Y + node.Bounds.Height / 2);

            _panOffset.X = Width / 2 - nodeCenter.X * _zoom;
            _panOffset.Y = Height / 2 - nodeCenter.Y * _zoom;

            Invalidate();
        }

        #endregion

        #region Serialization

        public NodeEditorState SaveState()
        {
            var state = new NodeEditorState
            {
                PanOffsetX = _panOffset.X,
                PanOffsetY = _panOffset.Y,
                Zoom = _zoom,
                NodePositions = new Dictionary<string, PointF>()
            };

            foreach (var node in _nodes)
            {
                state.NodePositions[node.SequenceId] = node.Position;
            }

            return state;
        }

        public void LoadState(NodeEditorState state)
        {
            if (state == null) return;

            _panOffset = new PointF(state.PanOffsetX, state.PanOffsetY);
            _zoom = state.Zoom;

            foreach (var kvp in state.NodePositions)
            {
                var node = FindNodeBySequenceId(kvp.Key);
                if (node != null)
                {
                    node.Position = kvp.Value;
                }
            }

            Invalidate();
        }

        #endregion
    }

    /// <summary>
    /// Serializable state for the node editor canvas.
    /// </summary>
    [Serializable]
    public class NodeEditorState
    {
        public float PanOffsetX { get; set; }
        public float PanOffsetY { get; set; }
        public float Zoom { get; set; }
        public Dictionary<string, PointF> NodePositions { get; set; }
    }
}
