using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Hanna_Studio.UI
{
    /// <summary>
    /// Centralized UI styling for a professional, modern dark theme.
    /// </summary>
    public static class UIStyles
    {
        #region Colors

        // Background colors
        public static readonly Color BackgroundDark = Color.FromArgb(25, 25, 28);
        public static readonly Color BackgroundMedium = Color.FromArgb(30, 30, 33);
        public static readonly Color BackgroundLight = Color.FromArgb(37, 37, 40);
        public static readonly Color BackgroundPanel = Color.FromArgb(42, 42, 45);
        public static readonly Color BackgroundHover = Color.FromArgb(50, 50, 55);
        public static readonly Color BackgroundSelected = Color.FromArgb(60, 60, 65);

        // Border colors
        public static readonly Color BorderDark = Color.FromArgb(50, 50, 55);
        public static readonly Color BorderLight = Color.FromArgb(70, 70, 75);
        public static readonly Color BorderAccent = Color.FromArgb(70, 130, 180);

        // Text colors
        public static readonly Color TextPrimary = Color.FromArgb(230, 230, 230);
        public static readonly Color TextSecondary = Color.FromArgb(160, 160, 165);
        public static readonly Color TextMuted = Color.FromArgb(120, 120, 125);
        public static readonly Color TextDisabled = Color.FromArgb(90, 90, 95);

        // Accent colors
        public static readonly Color AccentBlue = Color.FromArgb(70, 130, 180);
        public static readonly Color AccentBlueDark = Color.FromArgb(50, 100, 150);
        public static readonly Color AccentBlueLight = Color.FromArgb(100, 160, 210);
        public static readonly Color AccentGreen = Color.FromArgb(80, 160, 80);
        public static readonly Color AccentRed = Color.FromArgb(180, 70, 70);
        public static readonly Color AccentOrange = Color.FromArgb(220, 140, 50);

        // Status colors
        public static readonly Color StatusReady = Color.FromArgb(70, 130, 180);
        public static readonly Color StatusRunning = Color.FromArgb(220, 140, 50);
        public static readonly Color StatusError = Color.FromArgb(180, 70, 70);
        public static readonly Color StatusSuccess = Color.FromArgb(80, 160, 80);

        #endregion

        #region Fonts

        public static readonly Font FontRegular = new Font("Segoe UI", 9f, FontStyle.Regular);
        public static readonly Font FontMedium = new Font("Segoe UI Semibold", 9f, FontStyle.Regular);
        public static readonly Font FontBold = new Font("Segoe UI", 9f, FontStyle.Bold);
        public static readonly Font FontSmall = new Font("Segoe UI", 8f, FontStyle.Regular);
        public static readonly Font FontLarge = new Font("Segoe UI Semibold", 11f, FontStyle.Regular);
        public static readonly Font FontHeader = new Font("Segoe UI Semibold", 10f, FontStyle.Regular);
        public static readonly Font FontTitle = new Font("Segoe UI Light", 14f, FontStyle.Regular);
        public static readonly Font FontMonospace = new Font("Consolas", 9f, FontStyle.Regular);

        #endregion

        #region Styling Methods

        /// <summary>
        /// Applies professional dark styling to a Form.
        /// </summary>
        public static void StyleForm(Form form)
        {
            form.BackColor = BackgroundDark;
            form.ForeColor = TextPrimary;
            form.Font = FontRegular;
        }

        /// <summary>
        /// Applies professional dark styling to a MenuStrip.
        /// </summary>
        public static void StyleMenuStrip(MenuStrip menuStrip)
        {
            menuStrip.BackColor = BackgroundMedium;
            menuStrip.ForeColor = TextPrimary;
            menuStrip.Font = FontRegular;
            menuStrip.Renderer = new DarkMenuRenderer();
            menuStrip.Padding = new Padding(6, 3, 0, 3);
        }

        /// <summary>
        /// Applies professional dark styling to a Panel as a header.
        /// </summary>
        public static void StyleHeaderPanel(Panel panel, string title = null)
        {
            panel.BackColor = BackgroundLight;
            panel.Padding = new Padding(12, 8, 12, 8);

            if (!string.IsNullOrEmpty(title))
            {
                var label = new Label
                {
                    Text = title,
                    Font = FontHeader,
                    ForeColor = TextPrimary,
                    AutoSize = true,
                    Location = new Point(12, 10)
                };
                panel.Controls.Add(label);
            }
        }

        /// <summary>
        /// Creates a styled flat button.
        /// </summary>
        public static Button CreateButton(string text, EventHandler onClick = null, bool isPrimary = false)
        {
            var btn = new Button
            {
                Text = text,
                FlatStyle = FlatStyle.Flat,
                Font = FontMedium,
                ForeColor = TextPrimary,
                BackColor = isPrimary ? AccentBlue : BackgroundPanel,
                Cursor = Cursors.Hand,
                Padding = new Padding(12, 6, 12, 6),
                Height = 32
            };

            btn.FlatAppearance.BorderSize = isPrimary ? 0 : 1;
            btn.FlatAppearance.BorderColor = BorderDark;
            btn.FlatAppearance.MouseOverBackColor = isPrimary ? AccentBlueLight : BackgroundHover;
            btn.FlatAppearance.MouseDownBackColor = isPrimary ? AccentBlueDark : BackgroundSelected;

            if (onClick != null)
                btn.Click += onClick;

            return btn;
        }

        /// <summary>
        /// Creates a styled icon button.
        /// </summary>
        public static Button CreateIconButton(string text, string tooltip, EventHandler onClick = null)
        {
            var btn = new Button
            {
                Text = text,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11f, FontStyle.Bold),
                ForeColor = TextPrimary,
                BackColor = BackgroundPanel,
                Cursor = Cursors.Hand,
                Size = new Size(36, 32),
                TextAlign = ContentAlignment.MiddleCenter
            };

            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = BackgroundHover;
            btn.FlatAppearance.MouseDownBackColor = BackgroundSelected;

            if (onClick != null)
                btn.Click += onClick;

            if (!string.IsNullOrEmpty(tooltip))
            {
                var tt = new ToolTip();
                tt.SetToolTip(btn, tooltip);
            }

            return btn;
        }

        /// <summary>
        /// Styles a TextBox with dark theme.
        /// </summary>
        public static void StyleTextBox(TextBox textBox)
        {
            textBox.BackColor = BackgroundDark;
            textBox.ForeColor = TextPrimary;
            textBox.Font = FontRegular;
            textBox.BorderStyle = BorderStyle.FixedSingle;
        }

        /// <summary>
        /// Styles a ComboBox with dark theme.
        /// </summary>
        public static void StyleComboBox(ComboBox comboBox)
        {
            comboBox.BackColor = BackgroundDark;
            comboBox.ForeColor = TextPrimary;
            comboBox.Font = FontRegular;
            comboBox.FlatStyle = FlatStyle.Flat;
        }

        /// <summary>
        /// Styles a Label.
        /// </summary>
        public static void StyleLabel(Label label, bool isHeader = false, bool isMuted = false)
        {
            label.ForeColor = isMuted ? TextMuted : TextPrimary;
            label.Font = isHeader ? FontHeader : FontRegular;
            label.BackColor = Color.Transparent;
        }

        /// <summary>
        /// Styles a Panel as a content area.
        /// </summary>
        public static void StyleContentPanel(Panel panel)
        {
            panel.BackColor = BackgroundMedium;
            panel.Padding = new Padding(1);
        }

        /// <summary>
        /// Styles a SplitContainer.
        /// </summary>
        public static void StyleSplitContainer(SplitContainer splitContainer)
        {
            splitContainer.BackColor = BackgroundDark;
            splitContainer.Panel1.BackColor = BackgroundMedium;
            splitContainer.Panel2.BackColor = BackgroundMedium;
        }

        /// <summary>
        /// Creates a styled separator line.
        /// </summary>
        public static Panel CreateSeparator(bool horizontal = true)
        {
            return new Panel
            {
                BackColor = BorderDark,
                Height = horizontal ? 1 : 0,
                Width = horizontal ? 0 : 1,
                Dock = horizontal ? DockStyle.Top : DockStyle.Left
            };
        }

        /// <summary>
        /// Creates a styled section header.
        /// </summary>
        public static Panel CreateSectionHeader(string title, Button[] buttons = null)
        {
            var header = new Panel
            {
                Height = 40,
                Dock = DockStyle.Top,
                BackColor = BackgroundLight,
                Padding = new Padding(12, 0, 8, 0)
            };

            var label = new Label
            {
                Text = title,
                Font = FontHeader,
                ForeColor = TextPrimary,
                AutoSize = true,
                Location = new Point(12, 11)
            };
            header.Controls.Add(label);

            if (buttons != null)
            {
                int rightOffset = 8;
                for (int i = buttons.Length - 1; i >= 0; i--)
                {
                    var btn = buttons[i];
                    btn.Anchor = AnchorStyles.Right | AnchorStyles.Top;
                    btn.Location = new Point(header.Width - btn.Width - rightOffset, 4);
                    header.Controls.Add(btn);
                    rightOffset += btn.Width + 4;
                }
            }

            return header;
        }

        #endregion
    }

    /// <summary>
    /// Custom renderer for dark-themed menus.
    /// </summary>
    public class DarkMenuRenderer : ToolStripProfessionalRenderer
    {
        public DarkMenuRenderer() : base(new DarkMenuColorTable()) { }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            var rect = new Rectangle(Point.Empty, e.Item.Size);

            if (e.Item.Selected || e.Item.Pressed)
            {
                using (var brush = new SolidBrush(UIStyles.BackgroundHover))
                {
                    e.Graphics.FillRectangle(brush, rect);
                }
            }
            else
            {
                using (var brush = new SolidBrush(UIStyles.BackgroundMedium))
                {
                    e.Graphics.FillRectangle(brush, rect);
                }
            }
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            using (var pen = new Pen(UIStyles.BorderDark))
            {
                int y = e.Item.Height / 2;
                e.Graphics.DrawLine(pen, 30, y, e.Item.Width - 4, y);
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = e.Item.Enabled ? UIStyles.TextPrimary : UIStyles.TextDisabled;
            base.OnRenderItemText(e);
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            using (var brush = new SolidBrush(UIStyles.BackgroundMedium))
            {
                e.Graphics.FillRectangle(brush, e.AffectedBounds);
            }
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            using (var pen = new Pen(UIStyles.BorderDark))
            {
                var rect = new Rectangle(0, 0, e.AffectedBounds.Width - 1, e.AffectedBounds.Height - 1);
                e.Graphics.DrawRectangle(pen, rect);
            }
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            e.ArrowColor = UIStyles.TextSecondary;
            base.OnRenderArrow(e);
        }
    }

    /// <summary>
    /// Color table for dark menu theme.
    /// </summary>
    public class DarkMenuColorTable : ProfessionalColorTable
    {
        public override Color MenuItemSelected => UIStyles.BackgroundHover;
        public override Color MenuItemSelectedGradientBegin => UIStyles.BackgroundHover;
        public override Color MenuItemSelectedGradientEnd => UIStyles.BackgroundHover;
        public override Color MenuItemPressedGradientBegin => UIStyles.BackgroundSelected;
        public override Color MenuItemPressedGradientEnd => UIStyles.BackgroundSelected;
        public override Color MenuBorder => UIStyles.BorderDark;
        public override Color MenuItemBorder => Color.Transparent;
        public override Color ToolStripDropDownBackground => UIStyles.BackgroundMedium;
        public override Color ImageMarginGradientBegin => UIStyles.BackgroundMedium;
        public override Color ImageMarginGradientMiddle => UIStyles.BackgroundMedium;
        public override Color ImageMarginGradientEnd => UIStyles.BackgroundMedium;
        public override Color SeparatorDark => UIStyles.BorderDark;
        public override Color SeparatorLight => UIStyles.BorderDark;
    }

    /// <summary>
    /// A modern styled panel with optional header.
    /// </summary>
    public class StyledPanel : Panel
    {
        private string _title;
        private Panel _headerPanel;
        private Panel _contentPanel;
        private bool _showBorder = true;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                UpdateHeader();
            }
        }

        public bool ShowBorder
        {
            get => _showBorder;
            set
            {
                _showBorder = value;
                Invalidate();
            }
        }

        public Panel ContentPanel => _contentPanel;

        public StyledPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = UIStyles.BackgroundMedium;
            Padding = new Padding(1);

            _contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = UIStyles.BackgroundMedium,
                Padding = new Padding(8)
            };
            Controls.Add(_contentPanel);
        }

        private void UpdateHeader()
        {
            if (_headerPanel != null)
            {
                Controls.Remove(_headerPanel);
                _headerPanel.Dispose();
            }

            if (!string.IsNullOrEmpty(_title))
            {
                _headerPanel = new Panel
                {
                    Dock = DockStyle.Top,
                    Height = 36,
                    BackColor = UIStyles.BackgroundLight
                };

                var label = new Label
                {
                    Text = _title,
                    Font = UIStyles.FontHeader,
                    ForeColor = UIStyles.TextPrimary,
                    AutoSize = true,
                    Location = new Point(10, 9)
                };
                _headerPanel.Controls.Add(label);

                Controls.Add(_headerPanel);
                _headerPanel.BringToFront();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_showBorder)
            {
                using (var pen = new Pen(UIStyles.BorderDark))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
                }
            }
        }
    }

    /// <summary>
    /// Modern styled property row for displaying label-value pairs.
    /// </summary>
    public class PropertyRow : Panel
    {
        private Label _labelControl;
        private Control _valueControl;

        public string Label
        {
            get => _labelControl.Text;
            set => _labelControl.Text = value;
        }

        public Control ValueControl => _valueControl;

        public PropertyRow(string label, Control valueControl)
        {
            Height = 36;
            Dock = DockStyle.Top;
            BackColor = UIStyles.BackgroundMedium;
            Padding = new Padding(12, 6, 12, 6);

            _labelControl = new Label
            {
                Text = label,
                Font = UIStyles.FontRegular,
                ForeColor = UIStyles.TextSecondary,
                AutoSize = true,
                Location = new Point(12, 10)
            };

            _valueControl = valueControl;
            _valueControl.Font = UIStyles.FontRegular;
            _valueControl.Location = new Point(100, 6);

            if (_valueControl is TextBox tb)
            {
                UIStyles.StyleTextBox(tb);
                tb.Width = 180;
            }

            Controls.Add(_labelControl);
            Controls.Add(_valueControl);
        }
    }

    /// <summary>
    /// Modern toast notification panel.
    /// </summary>
    public class ToastNotification : Panel
    {
        private Label _messageLabel;
        private Timer _hideTimer;
        private Timer _fadeTimer;
        private float _opacity = 1.0f;

        public enum ToastType { Info, Success, Warning, Error }

        public ToastNotification()
        {
            Size = new Size(320, 48);
            BackColor = UIStyles.AccentBlue;
            Visible = false;
            Padding = new Padding(16, 0, 16, 0);

            _messageLabel = new Label
            {
                Font = UIStyles.FontMedium,
                ForeColor = Color.White,
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            Controls.Add(_messageLabel);

            _hideTimer = new Timer { Interval = 3000 };
            _hideTimer.Tick += (s, e) =>
            {
                _hideTimer.Stop();
                _fadeTimer.Start();
            };

            _fadeTimer = new Timer { Interval = 30 };
            _fadeTimer.Tick += (s, e) =>
            {
                _opacity -= 0.1f;
                if (_opacity <= 0)
                {
                    _fadeTimer.Stop();
                    Visible = false;
                    _opacity = 1.0f;
                }
                Invalidate();
            };
        }

        public void Show(string message, ToastType type = ToastType.Info)
        {
            _messageLabel.Text = message;

            switch (type)
            {
                case ToastType.Success:
                    BackColor = UIStyles.AccentGreen;
                    break;
                case ToastType.Warning:
                    BackColor = UIStyles.AccentOrange;
                    break;
                case ToastType.Error:
                    BackColor = UIStyles.AccentRed;
                    break;
                default:
                    BackColor = UIStyles.AccentBlue;
                    break;
            }

            _opacity = 1.0f;
            Visible = true;
            BringToFront();
            _hideTimer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (var brush = new SolidBrush(Color.FromArgb((int)(255 * _opacity), BackColor)))
            {
                e.Graphics.FillRectangle(brush, ClientRectangle);
            }
        }
    }
}
