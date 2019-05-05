namespace StokBarangApp
{
    partial class PrintStruk
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
            this.StrukViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // StrukViewer
            // 
            this.StrukViewer.ActiveViewIndex = -1;
            this.StrukViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StrukViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.StrukViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StrukViewer.Location = new System.Drawing.Point(20, 60);
            this.StrukViewer.Name = "StrukViewer";
            this.StrukViewer.Size = new System.Drawing.Size(723, 412);
            this.StrukViewer.TabIndex = 0;
            // 
            // PrintStruk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 492);
            this.Controls.Add(this.StrukViewer);
            this.Name = "PrintStruk";
            this.Text = "PrintStruk";
            this.Load += new System.EventHandler(this.PrintStruk_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer StrukViewer;
    }
}