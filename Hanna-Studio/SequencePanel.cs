using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hanna_Studio
{
    public class SequencePanel: Panel
    {
        public string sequenceid { get; set; }
        frmWorkspace refworkspace;
        public SequencePanel(string sqid,frmWorkspace workspace) {
            this.DoubleBuffered = true; // prevent flickering? I guess it did though
            refworkspace = workspace;
            sequenceid = sqid;
            this.Tag = sqid; // should probably generate unique tags according to sqid or something? And store those tags somewhere???
            // this panel should also have some text on it yeah???
            Label sqText = new Label(); // sqText should also have an tag i think
            sqText.Text = sqid;
            sqText.Name = "label"+sqid;
            sqText.Tag = sqid + "sqText";
            sqText.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            sqText.BackColor = Color.Transparent;
            //sqText.Dock = DockStyle.Left;
            sqText.Font = new Font(sqText.Font.FontFamily, 14, FontStyle.Regular);
            sqText.Click += new EventHandler(ControlEventWrapper_Click);
            sqText.Cursor = Cursors.Hand;
            sqText.Size = new System.Drawing.Size(sqText.Size.Width, 50);
            sqText.AutoSize = true;
            sqText.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8); // trying to center this label


            // create a delete button
            Button sqDelete = new Button();
            sqDelete.Dock = DockStyle.Right;
            sqDelete.Name = "btnDelete" + sqid;
            sqDelete.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            sqDelete.FlatAppearance.BorderSize = 0;
            sqDelete.Image = global::Hanna_Studio.Properties.Resources.ic_delete;
            sqDelete.UseVisualStyleBackColor = false;
            sqDelete.BackColor = System.Drawing.Color.Transparent;
            sqDelete.Click += new EventHandler(DeleteBtn_Click);
            sqDelete.Width = 50;
            sqDelete.FlatStyle = FlatStyle.Flat;
            sqDelete.Cursor = Cursors.Hand;
            sqDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            sqDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));


            

            // TESTING INSIDE PANEL TO ADD SOME KIND OF SPACING BETWEEN MULTIPLE SEQUENCE PANELS
            Panel parentPanel = new Panel();
            parentPanel.Name = "insidePanel" + sqid;
            parentPanel.Dock = DockStyle.Fill;
            parentPanel.BackColor = Color.Red;
            parentPanel.Controls.Add(sqText);
            parentPanel.Controls.Add(sqDelete);
            parentPanel.Click += new EventHandler(ControlEventWrapper_Click);
            this.Controls.Add(parentPanel);
            this.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);


            setInActive();
            //this.Controls.Add(sqText);
            //this.Controls.Add(sqDelete);
            this.Height = 50;
            this.Dock = DockStyle.Top;
            //this.Width = parentWidth;
            //this.MaximumSize = new Size(5000,50);
            //this.BackColor = System.Drawing.ColorTranslator.FromHtml("#000000");
            this.BackColor = Color.Transparent;
            this.Cursor = Cursors.Hand;
            //this.Click += new EventHandler(ControlEventWrapper_Click);

            this.ContextMenuStrip = refworkspace.sequenceContextMenuStrip;

        }
        void ControlEventWrapper_Click(object sender, EventArgs e)
        {
            refworkspace.showSequenceProps(this.sequenceid);
        }

        void DeleteBtn_Click(object sender, EventArgs e)
        {
            refworkspace.deleteSequence(this.sequenceid);
        }

        public void setActive() {
            this.Controls[0].BackColor = System.Drawing.ColorTranslator.FromHtml("#0091db");
            /*
            this.ColorTop = System.Drawing.ColorTranslator.FromHtml("#36aac1");
            this.ColorBottom = System.Drawing.ColorTranslator.FromHtml("#216977");
            this.Refresh();
            */
        }
        public void setInActive()
        {
            this.Controls[0].BackColor = System.Drawing.ColorTranslator.FromHtml("#000000");
            /*
            this.ColorTop = System.Drawing.ColorTranslator.FromHtml("#303030");
            this.ColorBottom = System.Drawing.ColorTranslator.FromHtml("#000000");
            this.Refresh();
            */
        }

    }
}
