using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hanna_Studio.UI;

namespace Hanna_Studio
{
    class ChoicePanel : Panel
    {
        public string sequenceid { get; set; }
        public string choiceLetter { get; set; }

        frmWorkspace refworkspace;
        private Panel innerPanel;
        private Label letterLabel;
        private Label textLabel;
        private Button deleteButton;

        public ChoicePanel(string sqid, string letter, frmWorkspace workspace)
        {
            this.DoubleBuffered = true;
            refworkspace = workspace;
            sequenceid = sqid;
            choiceLetter = letter;
            this.Tag = sqid + letter;

            // Main panel styling
            this.Height = 52;
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
            innerPanel.MouseEnter += (s, e) => innerPanel.BackColor = UIStyles.BackgroundHover;
            innerPanel.MouseLeave += (s, e) => innerPanel.BackColor = UIStyles.BackgroundPanel;

            // Choice letter badge
            letterLabel = new Label
            {
                Text = letter,
                Font = new Font("Segoe UI Semibold", 10f, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = UIStyles.AccentBlue,
                Size = new Size(28, 28),
                Location = new Point(10, 10),
                TextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand
            };
            letterLabel.Click += ControlEventWrapper_Click;

            // Get choice text for preview
            string choiceText = "Choice " + letter;
            try
            {
                var choice = workspace.getMySequences()[sqid].choices[letter];
                if (!string.IsNullOrEmpty(choice.text))
                {
                    choiceText = choice.text.Length > 35 ? choice.text.Substring(0, 32) + "..." : choice.text;
                }
            }
            catch { }

            // Choice text label
            textLabel = new Label
            {
                Text = choiceText,
                Font = UIStyles.FontRegular,
                ForeColor = UIStyles.TextPrimary,
                AutoSize = false,
                Location = new Point(48, 8),
                Size = new Size(innerPanel.Width - 100, 32),
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleLeft
            };
            textLabel.Click += ControlEventWrapper_Click;
            textLabel.MouseEnter += (s, e) => innerPanel.BackColor = UIStyles.BackgroundHover;
            textLabel.MouseLeave += (s, e) => innerPanel.BackColor = UIStyles.BackgroundPanel;

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

            innerPanel.Controls.Add(letterLabel);
            innerPanel.Controls.Add(textLabel);
            innerPanel.Controls.Add(deleteButton);
            this.Controls.Add(innerPanel);
        }

        void ControlEventWrapper_Click(object sender, EventArgs e)
        {
            Choice thisChoice = refworkspace.getCurrentSequence().choices[choiceLetter];
            refworkspace.startChoiceEdit(this.choiceLetter, thisChoice);
        }

        void DeleteBtn_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                $"Are you sure you want to delete choice '{choiceLetter}'?",
                "Delete Choice",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                foreach (ChoicePanel cPanel in refworkspace.getChoicesPanel().Controls)
                {
                    if (cPanel.Tag.ToString() == this.sequenceid + this.choiceLetter)
                    {
                        refworkspace.getChoicesPanel().Controls.Remove(cPanel);
                        refworkspace.deleteChoice(this.sequenceid, this.choiceLetter);
                        break;
                    }
                }
            }
        }
    }
}
