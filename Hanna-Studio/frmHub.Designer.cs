namespace Hanna_Studio
{
    partial class frmHub
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHub));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panelRecentProjects = new System.Windows.Forms.Panel();
            this.btnNewProject = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.labelNoRecents = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.8F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(41, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(327, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Welcome to Hanna Studio";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(44, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Recent Projects";
            // 
            // panelRecentProjects
            // 
            this.panelRecentProjects.AutoScroll = true;
            this.panelRecentProjects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.panelRecentProjects.Location = new System.Drawing.Point(47, 160);
            this.panelRecentProjects.Name = "panelRecentProjects";
            this.panelRecentProjects.Size = new System.Drawing.Size(797, 277);
            this.panelRecentProjects.TabIndex = 3;
            // 
            // btnNewProject
            // 
            this.btnNewProject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btnNewProject.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnNewProject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewProject.ForeColor = System.Drawing.Color.White;
            this.btnNewProject.Image = global::Hanna_Studio.Properties.Resources.btnimage_plus_white;
            this.btnNewProject.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNewProject.Location = new System.Drawing.Point(594, 40);
            this.btnNewProject.Name = "btnNewProject";
            this.btnNewProject.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnNewProject.Size = new System.Drawing.Size(250, 46);
            this.btnNewProject.TabIndex = 2;
            this.btnNewProject.Text = "Create New Project";
            this.btnNewProject.UseVisualStyleBackColor = false;
            this.btnNewProject.Click += new System.EventHandler(this.BtnNewProject_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btnOpenFile.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnOpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenFile.ForeColor = System.Drawing.Color.White;
            this.btnOpenFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOpenFile.Location = new System.Drawing.Point(594, 92);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(250, 46);
            this.btnOpenFile.TabIndex = 5;
            this.btnOpenFile.Text = "Open Existing Project";
            this.btnOpenFile.UseVisualStyleBackColor = false;
            this.btnOpenFile.Click += new System.EventHandler(this.BtnOpenFile_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "hprj";
            this.openFileDialog.Filter = "Hanna Project (*.hprj)|*.hprj";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenFileDialog_FileOk);
            // 
            // labelNoRecents
            // 
            this.labelNoRecents.AutoSize = true;
            this.labelNoRecents.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.labelNoRecents.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.labelNoRecents.ForeColor = System.Drawing.Color.White;
            this.labelNoRecents.Location = new System.Drawing.Point(361, 276);
            this.labelNoRecents.Name = "labelNoRecents";
            this.labelNoRecents.Size = new System.Drawing.Size(172, 24);
            this.labelNoRecents.TabIndex = 0;
            this.labelNoRecents.Text = "No Recent Projects";
            // 
            // frmHub
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(909, 496);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.labelNoRecents);
            this.Controls.Add(this.panelRecentProjects);
            this.Controls.Add(this.btnNewProject);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmHub";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hanna Studio";
            this.Load += new System.EventHandler(this.FrmHub_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnNewProject;
        private System.Windows.Forms.Panel panelRecentProjects;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label labelNoRecents;
    }
}