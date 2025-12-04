using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hanna_Studio.UI;

namespace Hanna_Studio
{
    public class SequencePanel : Panel
    {
        public string sequenceid { get; set; }
        frmWorkspace refworkspace;
        private Panel innerPanel;
        private Label nameLabel;
        private Button deleteButton;
        private bool isActive = false;

        public SequencePanel(string sqid, frmWorkspace workspace)
        {
            this.DoubleBuffered = true;
            refworkspace = workspace;
            sequenceid = sqid;
            this.Tag = sqid;

            // Main panel styling
            this.Height = 44;
            this.Dock = DockStyle.Top;
            this.BackColor = Color.Transparent;
            this.Padding = new Padding(4, 2, 4, 2);
            this.Cursor = Cursors.Hand;

            // Inner panel for content
            innerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = UIStyles.BackgroundPanel,
                Cursor = Cursors.Hand
            };
            innerPanel.Click += ControlEventWrapper_Click;
            innerPanel.MouseEnter += (s, e) => OnMouseEnterPanel();
            innerPanel.MouseLeave += (s, e) => OnMouseLeavePanel();

            // Sequence name label
            nameLabel = new Label
            {
                Text = sqid,
                Font = UIStyles.FontMedium,
                ForeColor = UIStyles.TextPrimary,
                AutoSize = false,
                Location = new Point(12, 10),
                Size = new Size(innerPanel.Width - 60, 20),
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };
            nameLabel.Click += ControlEventWrapper_Click;
            nameLabel.MouseEnter += (s, e) => OnMouseEnterPanel();
            nameLabel.MouseLeave += (s, e) => OnMouseLeavePanel();

            // Delete button
            deleteButton = new Button
            {
                Text = "×",
                Font = new Font("Segoe UI", 12f, FontStyle.Regular),
                ForeColor = UIStyles.TextMuted,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(32, 32),
                Dock = DockStyle.Right,
                Cursor = Cursors.Hand,
                BackColor = Color.Transparent
            };
            deleteButton.FlatAppearance.BorderSize = 0;
            deleteButton.FlatAppearance.MouseOverBackColor = UIStyles.AccentRed;
            deleteButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(150, 50, 50);
            deleteButton.Click += DeleteBtn_Click;
            deleteButton.MouseEnter += (s, e) => deleteButton.ForeColor = Color.White;
            deleteButton.MouseLeave += (s, e) => deleteButton.ForeColor = UIStyles.TextMuted;

            innerPanel.Controls.Add(nameLabel);
            innerPanel.Controls.Add(deleteButton);
            this.Controls.Add(innerPanel);

            this.ContextMenuStrip = refworkspace.sequenceContextMenuStrip;

            setInActive();
        }

        private void OnMouseEnterPanel()
        {
            if (!isActive)
            {
                innerPanel.BackColor = UIStyles.BackgroundHover;
            }
        }

        private void OnMouseLeavePanel()
        {
            if (!isActive)
            {
                innerPanel.BackColor = UIStyles.BackgroundPanel;
            }
        }

        void ControlEventWrapper_Click(object sender, EventArgs e)
        {
            refworkspace.showSequenceProps(this.sequenceid);
        }

        void DeleteBtn_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                $"Are you sure you want to delete sequence '{sequenceid}'?",
                "Delete Sequence",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                refworkspace.deleteSequence(this.sequenceid);
            }
        }

        public void setActive()
        {
            isActive = true;
            innerPanel.BackColor = UIStyles.AccentBlue;
            nameLabel.ForeColor = Color.White;
        }

        public void setInActive()
        {
            isActive = false;
            innerPanel.BackColor = UIStyles.BackgroundPanel;
            nameLabel.ForeColor = UIStyles.TextPrimary;
        }
    }
}
