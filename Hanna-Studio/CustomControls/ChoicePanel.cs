using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hanna_Studio
{
    class ChoicePanel: GradientPanel
    {
        public string sequenceid { get; set; }
        public string choiceLetter { get; set; }

        frmWorkspace refworkspace;
        public ChoicePanel(string sqid, string letter,frmWorkspace workspace) {
            refworkspace = workspace;
            sequenceid = sqid;
            choiceLetter = letter;
            this.Tag = sqid+letter; // should probably generate unique tags according to sqid or something? And store those tags somewhere??? I've been coding plenty just a bit confused tbh
            // this panel should also have some text on it yeah???
            Label sqText = new Label(); // sqText should also have an tag i think
            sqText.Text = "Choice " + letter;
            sqText.Tag = sqid + letter + "labelText";
            sqText.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            sqText.Click += new EventHandler(ControlEventWrapper_Click);
            sqText.Cursor = Cursors.Hand;
            sqText.BackColor = Color.Transparent;
            //sqText.Dock = DockStyle.Left;
            sqText.Font = new Font(sqText.Font.FontFamily, 14, FontStyle.Regular);
            sqText.Size = new System.Drawing.Size(sqText.Size.Width, 50);
            sqText.AutoSize = true;
            sqText.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8); // trying to center this label

            // create a delete button
            Button sqDelete = new Button();
            sqDelete.Dock = DockStyle.Right;
            sqDelete.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            sqDelete.FlatAppearance.BorderSize = 0;
            sqDelete.Image = global::Hanna_Studio.Properties.Resources.ic_delete;
            sqDelete.UseVisualStyleBackColor = false;
            sqDelete.BackColor = System.Drawing.Color.Transparent;
            sqDelete.Click += new EventHandler(DeleteBtn_Click);
            sqDelete.Width = 50;
            sqDelete.FlatStyle = FlatStyle.Flat;
            sqDelete.Cursor = Cursors.Hand;


            this.ColorTop = System.Drawing.ColorTranslator.FromHtml("#1b3744");
            this.ColorBottom = System.Drawing.ColorTranslator.FromHtml("#0d1c23");
            this.Controls.Add(sqText);
            this.Controls.Add(sqDelete);
            //this.Controls.Add(sqDelete);
            this.Height = 50;
            this.Dock = DockStyle.Top;
            //this.MaximumSize = new Size(5000,50);
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#000000");
            this.Cursor = Cursors.Hand;
            this.Click += new EventHandler(ControlEventWrapper_Click);

        }
        void ControlEventWrapper_Click(object sender, EventArgs e)
        {
            //refworkspace.showSequenceProps(this.sequenceid);
            // function for showing edits to a choice
            Choice thisChoice = refworkspace.getCurrentSequence().choices[choiceLetter];
            // lets edit this choice
            refworkspace.startChoiceEdit(this.choiceLetter,thisChoice);
        }

        void DeleteBtn_Click(object sender, EventArgs e)
        {
            // delete this control!
            // we need to get the flowpanel from workspace and delete this specific control
            foreach (ChoicePanel cPanel in refworkspace.getChoicesPanel().Controls)
            {
                if (cPanel.Tag.ToString() == this.sequenceid+this.choiceLetter)
                {
                    // delete this
                    refworkspace.getChoicesPanel().Controls.Remove(cPanel);
                    // we need to also delete the choice from the dictionary.
                    refworkspace.deleteChoice(this.sequenceid,this.choiceLetter);
                }
            }
        }

    }
}
