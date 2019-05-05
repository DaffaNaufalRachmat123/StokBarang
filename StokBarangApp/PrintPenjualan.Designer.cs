namespace StokBarangApp
{
    partial class PrintPenjualan
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
            this.ReportViewerJual = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // ReportViewerJual
            // 
            this.ReportViewerJual.ActiveViewIndex = -1;
            this.ReportViewerJual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReportViewerJual.Cursor = System.Windows.Forms.Cursors.Default;
            this.ReportViewerJual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportViewerJual.Location = new System.Drawing.Point(20, 60);
            this.ReportViewerJual.Name = "ReportViewerJual";
            this.ReportViewerJual.Size = new System.Drawing.Size(893, 417);
            this.ReportViewerJual.TabIndex = 0;
            // 
            // PrintPenjualan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 497);
            this.Controls.Add(this.ReportViewerJual);
            this.Name = "PrintPenjualan";
            this.Text = "PrintPenjualan";
            this.Load += new System.EventHandler(this.PrintPenjualan_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer ReportViewerJual;
    }
}