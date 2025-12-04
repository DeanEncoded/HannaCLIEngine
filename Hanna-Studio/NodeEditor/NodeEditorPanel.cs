using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Hanna_Studio.NodeEditor
{
    /// <summary>
    /// A complete node-based editor panel for managing sequences.
    /// Integrates the canvas with a toolbar and context menus.
    /// </summary>
    public class NodeEditorPanel : Panel
    {
        #region Fields

        private NodeEditorCanvas _canvas;
        private Panel _toolbar;
        private ContextMenuStrip _nodeContextMenu;
        private ContextMenuStrip _canvasContextMenu;
        private SequenceNode _contextMenuNode;
        private Point _contextMenuLocation;

        // Reference to workspace
        private frmWorkspace _workspace;

        #endregion

        #region Events

        public event EventHandler<string> SequenceSelected;
        public event EventHandler<string> SequenceDoubleClicked;
        public event EventHandler<string> RequestDeleteSequence;
        public event EventHandler<Point> RequestNewSequence;

        #endregion

        #region Properties

        public NodeEditorCanvas Canvas => _canvas;

        #endregion

        #region Constructor

        public NodeEditorPanel()
        {
            InitializeComponent();
        }

        public NodeEditorPanel(frmWorkspace workspace)
        {
            _workspace = workspace;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            SuspendLayout();

            // Configure panel
            BackColor = Color.FromArgb(30, 30, 30);
            Dock = DockStyle.Fill;

            // Create toolbar
            CreateToolbar();

            // Create canvas
            _canvas = new NodeEditorCanvas
            {
                Dock = DockStyle.Fill
            };

            _canvas.NodeSelected += OnNodeSelected;
            _canvas.NodeDoubleClicked += OnNodeDoubleClicked;
            _canvas.NodeDeleted += OnNodeDeleted;
            _canvas.ConnectionCreated += OnConnectionCreated;
            _canvas.ConnectionDeleted += OnConnectionDeleted;

            // Create context menus
            CreateContextMenus();

            // Add controls
            Controls.Add(_canvas);
            Controls.Add(_toolbar);

            ResumeLayout(false);
        }

        private void CreateToolbar()
        {
            _toolbar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.FromArgb(45, 45, 48),
                Padding = new Padding(5)
            };

            // Add Node button
            var btnAddNode = CreateToolbarButton("Add Sequence", "+", 0);
            btnAddNode.Font = new Font("Segoe UI", 14f, FontStyle.Bold);
            btnAddNode.Click += (s, e) =>
            {
                var center = _canvas.ScreenToCanvas(new Point(_canvas.Width / 2, _canvas.Height / 2));
                RequestNewSequence?.Invoke(this, new Point((int)center.X, (int)center.Y));
            };

            // Fit All button
            var btnFitAll = CreateToolbarButton("Fit All (F)", "[ ]", 1);
            btnFitAll.Font = new Font("Consolas", 9f, FontStyle.Bold);
            btnFitAll.Click += (s, e) => _canvas.FitAllNodes();

            // Reset View button
            var btnReset = CreateToolbarButton("Reset View (Home)", "H", 2);
            btnReset.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            btnReset.Click += (s, e) => _canvas.ResetView();

            // Zoom In button
            var btnZoomIn = CreateToolbarButton("Zoom In", "+", 3);
            btnZoomIn.Font = new Font("Segoe UI", 12f, FontStyle.Regular);
            btnZoomIn.Click += (s, e) => _canvas.Zoom += 0.1f;

            // Zoom Out button
            var btnZoomOut = CreateToolbarButton("Zoom Out", "?", 4);
            btnZoomOut.Font = new Font("Segoe UI", 12f, FontStyle.Regular);
            btnZoomOut.Click += (s, e) => _canvas.Zoom -= 0.1f;

            // Toggle Grid button
            var btnGrid = CreateToolbarButton("Toggle Grid", "#", 5);
            btnGrid.Font = new Font("Consolas", 12f, FontStyle.Bold);
            btnGrid.Click += (s, e) => _canvas.ShowGrid = !_canvas.ShowGrid;

            // Separator
            var separator = new Panel
            {
                Width = 2,
                Height = 24,
                BackColor = Color.FromArgb(80, 80, 85),
                Location = new Point(290, 8)
            };

            // Help label
            var lblHelp = new Label
            {
                Text = "Pan: Middle Mouse | Zoom: Scroll | Select: Click | Multi-select: Ctrl+Click | Delete: Del",
                ForeColor = Color.FromArgb(150, 150, 150),
                AutoSize = true,
                Location = new Point(305, 12),
                Font = new Font("Segoe UI", 8f)
            };

            _toolbar.Controls.Add(btnAddNode);
            _toolbar.Controls.Add(btnFitAll);
            _toolbar.Controls.Add(btnReset);
            _toolbar.Controls.Add(btnZoomIn);
            _toolbar.Controls.Add(btnZoomOut);
            _toolbar.Controls.Add(btnGrid);
            _toolbar.Controls.Add(separator);
            _toolbar.Controls.Add(lblHelp);
        }

        private Button CreateToolbarButton(string tooltip, string text, int index)
        {
            var btn = new Button
            {
                Text = text,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(36, 30),
                Location = new Point(5 + index * 42, 5),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(60, 60, 65),
                Font = new Font("Segoe UI", 10f),
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter
            };

            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(80, 80, 85);
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(100, 100, 105);

            var toolTip = new ToolTip();
            toolTip.SetToolTip(btn, tooltip);

            return btn;
        }

        private void CreateContextMenus()
        {
            // Node context menu
            _nodeContextMenu = new ContextMenuStrip();
            _nodeContextMenu.BackColor = Color.FromArgb(45, 45, 48);
            _nodeContextMenu.ForeColor = Color.White;
            _nodeContextMenu.RenderMode = ToolStripRenderMode.Professional;
            _nodeContextMenu.Renderer = new DarkToolStripRenderer();

            var editItem = new ToolStripMenuItem("Edit Properties");
            editItem.Click += (s, e) =>
            {
                if (_contextMenuNode != null)
                {
                    SequenceDoubleClicked?.Invoke(this, _contextMenuNode.SequenceId);
                }
            };

            var centerItem = new ToolStripMenuItem("Center on Node");
            centerItem.Click += (s, e) =>
            {
                if (_contextMenuNode != null)
                {
                    _canvas.CenterOnNode(_contextMenuNode);
                }
            };

            var setStartItem = new ToolStripMenuItem("Set as Start Sequence");
            setStartItem.Click += (s, e) =>
            {
                if (_contextMenuNode != null)
                {
                    foreach (var node in _canvas.Nodes)
                    {
                        node.IsStartSequence = false;
                    }
                    _contextMenuNode.IsStartSequence = true;
                    _canvas.Invalidate();
                }
            };

            var deleteItem = new ToolStripMenuItem("Delete Sequence");
            deleteItem.Click += (s, e) =>
            {
                if (_contextMenuNode != null)
                {
                    RequestDeleteSequence?.Invoke(this, _contextMenuNode.SequenceId);
                }
            };

            _nodeContextMenu.Items.AddRange(new ToolStripItem[]
            {
                editItem,
                centerItem,
                new ToolStripSeparator(),
                setStartItem,
                new ToolStripSeparator(),
                deleteItem
            });

            // Canvas context menu
            _canvasContextMenu = new ContextMenuStrip();
            _canvasContextMenu.BackColor = Color.FromArgb(45, 45, 48);
            _canvasContextMenu.ForeColor = Color.White;
            _canvasContextMenu.RenderMode = ToolStripRenderMode.Professional;
            _canvasContextMenu.Renderer = new DarkToolStripRenderer();

            var addNodeItem = new ToolStripMenuItem("Add Sequence Here");
            addNodeItem.Click += (s, e) =>
            {
                var canvasPos = _canvas.ScreenToCanvas(_contextMenuLocation);
                RequestNewSequence?.Invoke(this, new Point((int)canvasPos.X, (int)canvasPos.Y));
            };

            var fitAllItem = new ToolStripMenuItem("Fit All Nodes");
            fitAllItem.Click += (s, e) => _canvas.FitAllNodes();

            var resetViewItem = new ToolStripMenuItem("Reset View");
            resetViewItem.Click += (s, e) => _canvas.ResetView();

            _canvasContextMenu.Items.AddRange(new ToolStripItem[]
            {
                addNodeItem,
                new ToolStripSeparator(),
                fitAllItem,
                resetViewItem
            });

            // Attach context menus
            _canvas.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    _contextMenuLocation = e.Location;
                    var canvasPos = _canvas.ScreenToCanvas(e.Location);
                    _contextMenuNode = null;

                    foreach (var node in _canvas.Nodes)
                    {
                        if (node.Bounds.Contains(canvasPos))
                        {
                            _contextMenuNode = node;
                            break;
                        }
                    }

                    if (_contextMenuNode != null)
                    {
                        _nodeContextMenu.Show(_canvas, e.Location);
                    }
                    else
                    {
                        _canvasContextMenu.Show(_canvas, e.Location);
                    }
                }
            };
        }

        #endregion

        #region Event Handlers

        private void OnNodeSelected(object sender, SequenceNode node)
        {
            SequenceSelected?.Invoke(this, node?.SequenceId);
        }

        private void OnNodeDoubleClicked(object sender, SequenceNode node)
        {
            SequenceDoubleClicked?.Invoke(this, node?.SequenceId);
        }

        private void OnNodeDeleted(object sender, SequenceNode node)
        {
            RequestDeleteSequence?.Invoke(this, node?.SequenceId);
        }

        private void OnConnectionCreated(object sender, NodeConnection connection)
        {
            if (_workspace != null && connection != null)
            {
                // Update the choice's nextSq in the workspace
                var sourceSequence = _workspace.getMySequences()[connection.SourceNode.SequenceId];
                if (sourceSequence.choices.ContainsKey(connection.ChoiceLetter))
                {
                    sourceSequence.choices[connection.ChoiceLetter].nextSq = connection.TargetNode.SequenceId;
                }
            }
        }

        private void OnConnectionDeleted(object sender, NodeConnection connection)
        {
            if (_workspace != null && connection != null)
            {
                // Clear the choice's nextSq in the workspace
                var sourceSequence = _workspace.getMySequences()[connection.SourceNode.SequenceId];
                if (sourceSequence.choices.ContainsKey(connection.ChoiceLetter))
                {
                    sourceSequence.choices[connection.ChoiceLetter].nextSq = null;
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Synchronizes the node editor with the current sequences in the workspace.
        /// </summary>
        public void SyncWithSequences(Dictionary<string, Sequence> sequences, string startSequenceId = null)
        {
            // Store existing positions
            var existingPositions = new Dictionary<string, PointF>();
            foreach (var node in _canvas.Nodes)
            {
                existingPositions[node.SequenceId] = node.Position;
            }

            _canvas.ClearAll();

            if (sequences == null || sequences.Count == 0) return;

            // Calculate layout positions for new nodes
            int col = 0, row = 0;
            int maxCols = 4;
            float nodeSpacingX = 250;
            float nodeSpacingY = 120;
            float startX = 100;
            float startY = 100;

            foreach (var kvp in sequences)
            {
                var sequenceId = kvp.Key;
                var sequence = kvp.Value;

                // Use existing position or calculate new one
                PointF position;
                if (existingPositions.ContainsKey(sequenceId))
                {
                    position = existingPositions[sequenceId];
                }
                else
                {
                    position = new PointF(startX + col * nodeSpacingX, startY + row * nodeSpacingY);
                    col++;
                    if (col >= maxCols)
                    {
                        col = 0;
                        row++;
                    }
                }

                var node = _canvas.AddNode(sequenceId, position, sequence.type == "end");
                node.UpdateFromSequence(sequence);
                node.IsStartSequence = sequenceId == startSequenceId;
            }

            // Create connections based on choice.nextSq
            foreach (var kvp in sequences)
            {
                var sequenceId = kvp.Key;
                var sequence = kvp.Value;
                var sourceNode = _canvas.FindNodeBySequenceId(sequenceId);

                if (sequence.choices != null && sourceNode != null)
                {
                    foreach (var choiceKvp in sequence.choices)
                    {
                        var choiceLetter = choiceKvp.Key;
                        var choice = choiceKvp.Value;

                        if (!string.IsNullOrEmpty(choice.nextSq))
                        {
                            var targetNode = _canvas.FindNodeBySequenceId(choice.nextSq);
                            if (targetNode != null)
                            {
                                _canvas.AddConnection(sourceNode, choiceLetter, targetNode);
                            }
                        }
                    }
                }
            }

            _canvas.Invalidate();
        }

        /// <summary>
        /// Adds a new node for a sequence.
        /// </summary>
        public SequenceNode AddSequenceNode(string sequenceId, Sequence sequence, PointF? position = null)
        {
            var pos = position ?? new PointF(100, 100);
            var node = _canvas.AddNode(sequenceId, pos, sequence?.type == "end");

            if (sequence != null)
            {
                node.UpdateFromSequence(sequence);
            }

            return node;
        }

        /// <summary>
        /// Removes a node by sequence ID.
        /// </summary>
        public void RemoveSequenceNode(string sequenceId)
        {
            var node = _canvas.FindNodeBySequenceId(sequenceId);
            if (node != null)
            {
                _canvas.RemoveNode(node);
            }
        }

        /// <summary>
        /// Updates a node from its sequence data.
        /// </summary>
        public void UpdateSequenceNode(string sequenceId, Sequence sequence)
        {
            _canvas.UpdateNodeFromSequence(sequenceId, sequence);
        }

        /// <summary>
        /// Selects a node by sequence ID.
        /// </summary>
        public void SelectNode(string sequenceId)
        {
            var node = _canvas.FindNodeBySequenceId(sequenceId);
            if (node != null)
            {
                _canvas.CenterOnNode(node);
            }
        }

        /// <summary>
        /// Gets the node editor state for saving.
        /// </summary>
        public NodeEditorState GetState()
        {
            return _canvas.SaveState();
        }

        /// <summary>
        /// Loads the node editor state.
        /// </summary>
        public void LoadState(NodeEditorState state)
        {
            _canvas.LoadState(state);
        }

        #endregion
    }

    /// <summary>
    /// Custom renderer for dark-themed context menus.
    /// </summary>
    public class DarkToolStripRenderer : ToolStripProfessionalRenderer
    {
        public DarkToolStripRenderer() : base(new DarkColorTable()) { }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Selected)
            {
                using (var brush = new SolidBrush(Color.FromArgb(70, 70, 75)))
                {
                    e.Graphics.FillRectangle(brush, e.Item.ContentRectangle);
                }
            }
            else
            {
                base.OnRenderMenuItemBackground(e);
            }
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            using (var pen = new Pen(Color.FromArgb(70, 70, 75)))
            {
                int y = e.Item.Height / 2;
                e.Graphics.DrawLine(pen, 0, y, e.Item.Width, y);
            }
        }
    }

    /// <summary>
    /// Custom color table for dark theme.
    /// </summary>
    public class DarkColorTable : ProfessionalColorTable
    {
        public override Color MenuItemSelected => Color.FromArgb(70, 70, 75);
        public override Color MenuItemSelectedGradientBegin => Color.FromArgb(70, 70, 75);
        public override Color MenuItemSelectedGradientEnd => Color.FromArgb(70, 70, 75);
        public override Color MenuBorder => Color.FromArgb(60, 60, 65);
        public override Color MenuItemBorder => Color.FromArgb(70, 70, 75);
        public override Color ToolStripDropDownBackground => Color.FromArgb(45, 45, 48);
        public override Color ImageMarginGradientBegin => Color.FromArgb(45, 45, 48);
        public override Color ImageMarginGradientMiddle => Color.FromArgb(45, 45, 48);
        public override Color ImageMarginGradientEnd => Color.FromArgb(45, 45, 48);
        public override Color SeparatorDark => Color.FromArgb(70, 70, 75);
        public override Color SeparatorLight => Color.FromArgb(70, 70, 75);
    }
}
