using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SteamCMD_GUI
{
    public partial class AboutWindow : Form
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(142, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "SteamCMD GUI";

            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(15, 34);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(46, 15);
            this.lblVersion.TabIndex = 1;
            this.lblVersion.Text = "C# Port";

            this.btnOk.Location = new System.Drawing.Point(12, 60);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 101);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblTitle);
            this.Name = "AboutWindow";
            this.Text = "About SteamCMD GUI";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Button btnOk;

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
