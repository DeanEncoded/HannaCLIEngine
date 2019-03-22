namespace Hanna_Studio
{
    partial class dlgEditProperty
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgEditProperty));
            this.richTextBoxUpdateText = new System.Windows.Forms.RichTextBox();
            this.btnCommit = new System.Windows.Forms.Button();
            this.labelEditProp = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // richTextBoxUpdateText
            // 
            this.richTextBoxUpdateText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.richTextBoxUpdateText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxUpdateText.ForeColor = System.Drawing.Color.White;
            this.richTextBoxUpdateText.Location = new System.Drawing.Point(41, 94);
            this.richTextBoxUpdateText.Name = "richTextBoxUpdateText";
            this.richTextBoxUpdateText.Size = new System.Drawing.Size(688, 238);
            this.richTextBoxUpdateText.TabIndex = 1;
            this.richTextBoxUpdateText.Text = "";
            // 
            // btnCommit
            // 
            this.btnCommit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btnCommit.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnCommit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCommit.ForeColor = System.Drawing.Color.White;
            this.btnCommit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCommit.Location = new System.Drawing.Point(576, 371);
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Size = new System.Drawing.Size(153, 46);
            this.btnCommit.TabIndex = 6;
            this.btnCommit.Text = "Commit Text";
            this.btnCommit.UseVisualStyleBackColor = false;
            this.btnCommit.Click += new System.EventHandler(this.BtnCommit_Click);
            // 
            // labelEditProp
            // 
            this.labelEditProp.AutoSize = true;
            this.labelEditProp.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.8F);
            this.labelEditProp.ForeColor = System.Drawing.Color.White;
            this.labelEditProp.Location = new System.Drawing.Point(41, 53);
            this.labelEditProp.Name = "labelEditProp";
            this.labelEditProp.Size = new System.Drawing.Size(110, 25);
            this.labelEditProp.TabIndex = 7;
            this.labelEditProp.Text = "editing_this";
            // 
            // dlgEditProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelEditProp);
            this.Controls.Add(this.btnCommit);
            this.Controls.Add(this.richTextBoxUpdateText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "dlgEditProperty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Sequence Property";
            this.Load += new System.EventHandler(this.DlgEditProperty_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox richTextBoxUpdateText;
        private System.Windows.Forms.Button btnCommit;
        private System.Windows.Forms.Label labelEditProp;
    }
}