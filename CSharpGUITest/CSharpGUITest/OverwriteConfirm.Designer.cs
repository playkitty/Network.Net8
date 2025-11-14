namespace NOBAgentClient
{
    partial class OverwriteConfirm
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
        private void InitializeComponent(int count)
        {
            this.Overwrite = new System.Windows.Forms.Button();
            this.Skip = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.AllApply = new System.Windows.Forms.CheckBox();
            this.FileCollisionExplain = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Overwrite
            // 
            this.Overwrite.Location = new System.Drawing.Point(24, 71);
            this.Overwrite.Name = "Overwrite";
            this.Overwrite.Size = new System.Drawing.Size(75, 23);
            this.Overwrite.TabIndex = 0;
            this.Overwrite.Text = "덮어쓰기";
            this.Overwrite.UseVisualStyleBackColor = true;
            this.Overwrite.Click += new System.EventHandler(this.Overwrite_Click);
            // 
            // Skip
            // 
            this.Skip.Location = new System.Drawing.Point(116, 71);
            this.Skip.Name = "Skip";
            this.Skip.Size = new System.Drawing.Size(75, 23);
            this.Skip.TabIndex = 1;
            this.Skip.Text = "건너뛰기";
            this.Skip.UseVisualStyleBackColor = true;
            this.Skip.Click += new System.EventHandler(this.Skip_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(207, 71);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 2;
            this.Cancel.Text = "취소";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // AllApply
            // 
            this.AllApply.AutoSize = true;
            this.AllApply.Location = new System.Drawing.Point(24, 52);
            this.AllApply.Name = "AllApply";
            this.AllApply.Size = new System.Drawing.Size(76, 16);
            this.AllApply.TabIndex = 3;
            this.AllApply.Text = "모두 적용";
            this.AllApply.UseVisualStyleBackColor = true;
            // 
            // FileCollisionExplain
            // 
            this.FileCollisionExplain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FileCollisionExplain.Location = new System.Drawing.Point(12, 12);
            this.FileCollisionExplain.Multiline = true;
            this.FileCollisionExplain.Name = "FileCollisionExplain";
            this.FileCollisionExplain.ReadOnly = true;
            this.FileCollisionExplain.Size = new System.Drawing.Size(278, 34);
            this.FileCollisionExplain.TabIndex = 4;
            this.FileCollisionExplain.Text = "파일을 덮어쓰겠습니까?";
            // 
            // OverwriteConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 111);
            this.Controls.Add(this.FileCollisionExplain);
            if(count > 0)
            {
                this.Controls.Add(this.AllApply);
            }
                
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Skip);
            this.Controls.Add(this.Overwrite);
            this.Name = "OverwriteConfirm";
            this.Text = "OverwriteConfirm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Overwrite;
        private System.Windows.Forms.Button Skip;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.CheckBox AllApply;
        private System.Windows.Forms.TextBox FileCollisionExplain;
    }
}