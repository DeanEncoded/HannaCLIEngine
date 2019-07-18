namespace Hanna_Studio
{
    partial class dlgNewSequence
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgNewSequence));
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSequenceId = new System.Windows.Forms.TextBox();
            this.checkBoxEndsq = new MaterialSkin.Controls.MaterialCheckBox();
            this.mbtnCreate = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(31, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "Sequence Id";
            // 
            // textBoxSequenceId
            // 
            this.textBoxSequenceId.BackColor = System.Drawing.Color.Black;
            this.textBoxSequenceId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxSequenceId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.8F);
            this.textBoxSequenceId.ForeColor = System.Drawing.Color.White;
            this.textBoxSequenceId.Location = new System.Drawing.Point(172, 97);
            this.textBoxSequenceId.Name = "textBoxSequenceId";
            this.textBoxSequenceId.Size = new System.Drawing.Size(223, 25);
            this.textBoxSequenceId.TabIndex = 2;
            // 
            // checkBoxEndsq
            // 
            this.checkBoxEndsq.AutoSize = true;
            this.checkBoxEndsq.Depth = 0;
            this.checkBoxEndsq.Font = new System.Drawing.Font("Roboto", 10F);
            this.checkBoxEndsq.Location = new System.Drawing.Point(35, 159);
            this.checkBoxEndsq.Margin = new System.Windows.Forms.Padding(0);
            this.checkBoxEndsq.MouseLocation = new System.Drawing.Point(-1, -1);
            this.checkBoxEndsq.MouseState = MaterialSkin.MouseState.HOVER;
            this.checkBoxEndsq.Name = "checkBoxEndsq";
            this.checkBoxEndsq.Ripple = true;
            this.checkBoxEndsq.Size = new System.Drawing.Size(140, 30);
            this.checkBoxEndsq.TabIndex = 7;
            this.checkBoxEndsq.Text = "End Sequence";
            this.checkBoxEndsq.UseVisualStyleBackColor = true;
            // 
            // mbtnCreate
            // 
            this.mbtnCreate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.mbtnCreate.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.mbtnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnCreate.ForeColor = System.Drawing.Color.White;
            this.mbtnCreate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mbtnCreate.Location = new System.Drawing.Point(438, 236);
            this.mbtnCreate.Name = "mbtnCreate";
            this.mbtnCreate.Size = new System.Drawing.Size(162, 41);
            this.mbtnCreate.TabIndex = 9;
            this.mbtnCreate.Text = "Create Sequence";
            this.mbtnCreate.UseVisualStyleBackColor = false;
            this.mbtnCreate.Click += new System.EventHandler(this.BtnCreate_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(31, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(202, 24);
            this.label2.TabIndex = 10;
            this.label2.Text = "Create New Sequence";
            // 
            // dlgNewSequence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(644, 307);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mbtnCreate);
            this.Controls.Add(this.checkBoxEndsq);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxSequenceId);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "dlgNewSequence";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Sequence";
            this.Load += new System.EventHandler(this.DlgNewSequence_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSequenceId;
        private MaterialSkin.Controls.MaterialCheckBox checkBoxEndsq;
        private System.Windows.Forms.Button mbtnCreate;
        private System.Windows.Forms.Label label2;
    }
}