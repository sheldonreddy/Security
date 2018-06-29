namespace SecureStringTextBox
{
    partial class DemoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DemoForm));
            this.BlackWaspLink = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.ShowButton = new System.Windows.Forms.Button();
            this.SecureInput = new SecureStringTextBox.SSTextBox();
            this.SuspendLayout();
            // 
            // BlackWaspLink
            // 
            this.BlackWaspLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BlackWaspLink.AutoSize = true;
            this.BlackWaspLink.Location = new System.Drawing.Point(12, 64);
            this.BlackWaspLink.Name = "BlackWaspLink";
            this.BlackWaspLink.Size = new System.Drawing.Size(151, 13);
            this.BlackWaspLink.TabIndex = 0;
            this.BlackWaspLink.TabStop = true;
            this.BlackWaspLink.Text = "http://www.blackwasp.co.uk/";
            this.BlackWaspLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.BlackWaspLink_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Secure";
            // 
            // ShowButton
            // 
            this.ShowButton.Location = new System.Drawing.Point(205, 32);
            this.ShowButton.Name = "ShowButton";
            this.ShowButton.Size = new System.Drawing.Size(75, 23);
            this.ShowButton.TabIndex = 3;
            this.ShowButton.Text = "Show";
            this.ShowButton.UseVisualStyleBackColor = true;
            this.ShowButton.Click += new System.EventHandler(this.ShowStringButton_Click);
            // 
            // SecureInput
            // 
            this.SecureInput.Location = new System.Drawing.Point(63, 6);
            this.SecureInput.Name = "SecureInput";
            this.SecureInput.PasswordChar = '●';
            this.SecureInput.Size = new System.Drawing.Size(217, 20);
            this.SecureInput.TabIndex = 2;
            // 
            // DemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 86);
            this.Controls.Add(this.ShowButton);
            this.Controls.Add(this.SecureInput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BlackWaspLink);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DemoForm";
            this.Text = "SecureStringTextBox Demo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel BlackWaspLink;
        private System.Windows.Forms.Label label1;
        private SSTextBox SecureInput;
        private System.Windows.Forms.Button ShowButton;
    }
}

