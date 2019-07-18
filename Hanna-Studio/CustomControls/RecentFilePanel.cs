using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hanna_Studio
{
    public class RecentFilePanel: Panel
    {

 
        public string projectName { get; set; }
        public string filePath { get; set; }
        frmHub hub;
        public RecentFilePanel(string projectName,string filePath,frmHub hub) {
            this.projectName = projectName;
            this.filePath = filePath;
            this.hub = hub;

            this.Tag = this.filePath; 

            FlatBoiButton filePathLabel = new FlatBoiButton(); // filePathLabel should also have an tag i think
            filePathLabel.Text = this.filePath;
            filePathLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#9b9b9b");
            filePathLabel.BackColor = Color.Transparent;
            filePathLabel.Dock = DockStyle.Left;
            filePathLabel.Font = new Font(filePathLabel.Font.FontFamily, 9, FontStyle.Regular);
            filePathLabel.Size = new System.Drawing.Size(filePathLabel.Size.Width, 50);
            filePathLabel.AutoSize = true;
            filePathLabel.TabStop = false;
            filePathLabel.FlatStyle = FlatStyle.Flat;
            filePathLabel.FlatAppearance.BorderSize = 0;
            filePathLabel.FlatAppearance.MouseDownBackColor = Color.Transparent;
            filePathLabel.FlatAppearance.MouseOverBackColor = Color.Transparent;
            filePathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            filePathLabel.Cursor = Cursors.Hand;
            filePathLabel.Click += new EventHandler(ControlEventWrapper_Click);
            filePathLabel.MouseEnter += new EventHandler(Panel_MouseEnter);
            filePathLabel.MouseLeave += new EventHandler(Panel_MouseLeave);


            FlatBoiButton projectNameLabel = new FlatBoiButton(); // projectNameLabel should also have an tag i think
            projectNameLabel.Text = this.projectName;
            projectNameLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            projectNameLabel.BackColor = Color.Transparent;
            projectNameLabel.Dock = DockStyle.Left;
            projectNameLabel.Font = new Font(projectNameLabel.Font.FontFamily, 10, FontStyle.Bold);
            projectNameLabel.Size = new System.Drawing.Size(projectNameLabel.Size.Width, 50);
            projectNameLabel.AutoSize = true;
            projectNameLabel.TabStop = false;
            projectNameLabel.FlatStyle = FlatStyle.Flat;
            projectNameLabel.FlatAppearance.BorderSize = 0;
            projectNameLabel.FlatAppearance.MouseDownBackColor = Color.Transparent;
            projectNameLabel.FlatAppearance.MouseOverBackColor = Color.Transparent;
            projectNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            projectNameLabel.Cursor = Cursors.Hand;
            projectNameLabel.Click += new EventHandler(ControlEventWrapper_Click);
            projectNameLabel.MouseEnter += new EventHandler(Panel_MouseEnter);
            projectNameLabel.MouseLeave += new EventHandler(Panel_MouseLeave);
   

            this.Controls.Add(filePathLabel);
            this.Controls.Add(projectNameLabel);

            this.Height = 50;
            this.Dock = DockStyle.Top;
            //this.Width = parentWidth;
            //this.MaximumSize = new Size(5000,50);
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#000000");
            this.Cursor = Cursors.Hand;
            this.Click += new EventHandler(ControlEventWrapper_Click);
            this.MouseEnter += new EventHandler(Panel_MouseEnter);
            this.MouseLeave += new EventHandler(Panel_MouseLeave);

        }

        void ControlEventWrapper_Click(object sender, EventArgs e)
        {
            // check if the project file exists first. If it doesn't exist... then delete it from the recent projects
            if (System.IO.File.Exists(this.filePath))
            {
                // open the project
                hub.openProjectFile(this.filePath);
            }
            else {
                // it doesn't exist.... delete it please abeg!
                MessageBox.Show("This project doesn't exist");
                // how do I delete this recent file now ???? Hmmmmmmmmmm (insert Buzz Lightyear looking down confusingly meme)
                hub.deleteRecentFile(this.filePath);
            }
        }


        private void Panel_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#5b5b5b");
        }
        private void Panel_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#000000");
        }


    }

    public class FlatBoiButton : Button
    {
        public override void NotifyDefault(bool value)
        {
            base.NotifyDefault(false);
        }
    }
}
