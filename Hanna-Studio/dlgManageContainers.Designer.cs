namespace Hanna_Studio
{
    partial class dlgManageContainers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgManageContainers));
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxContainerName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxContainers = new System.Windows.Forms.ListBox();
            this.btnAddContainer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.8F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(43, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(413, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "Project containers are used to contain any strings that\r\ncan be used with conditi" +
    "onal choices.";
            // 
            // textBoxContainerName
            // 
            this.textBoxContainerName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.textBoxContainerName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxContainerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.8F);
            this.textBoxContainerName.ForeColor = System.Drawing.Color.White;
            this.textBoxContainerName.Location = new System.Drawing.Point(433, 141);
            this.textBoxContainerName.Name = "textBoxContainerName";
            this.textBoxContainerName.Size = new System.Drawing.Size(252, 25);
            this.textBoxContainerName.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(430, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "Add a container here";
            // 
            // listBoxContainers
            // 
            this.listBoxContainers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.listBoxContainers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxContainers.ForeColor = System.Drawing.Color.White;
            this.listBoxContainers.FormattingEnabled = true;
            this.listBoxContainers.ItemHeight = 16;
            this.listBoxContainers.Location = new System.Drawing.Point(47, 107);
            this.listBoxContainers.Name = "listBoxContainers";
            this.listBoxContainers.Size = new System.Drawing.Size(250, 192);
            this.listBoxContainers.TabIndex = 12;
            // 
            // btnAddContainer
            // 
            this.btnAddContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btnAddContainer.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnAddContainer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddContainer.ForeColor = System.Drawing.Color.White;
            this.btnAddContainer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddContainer.Location = new System.Drawing.Point(433, 181);
            this.btnAddContainer.Name = "btnAddContainer";
            this.btnAddContainer.Size = new System.Drawing.Size(155, 41);
            this.btnAddContainer.TabIndex = 13;
            this.btnAddContainer.Text = "Add Container";
            this.btnAddContainer.UseVisualStyleBackColor = false;
            this.btnAddContainer.Click += new System.EventHandler(this.BtnAddContainer_Click);
            // 
            // dlgManageContainers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(800, 376);
            this.Controls.Add(this.btnAddContainer);
            this.Controls.Add(this.listBoxContainers);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxContainerName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "dlgManageContainers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage Project Containers";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxContainerName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBoxContainers;
        private System.Windows.Forms.Button btnAddContainer;
    }
}