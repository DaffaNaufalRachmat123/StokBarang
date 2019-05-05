namespace StokBarangApp
{
    partial class PrintPembelian
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
            this.ReportViewerBeli = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // ReportViewerBeli
            // 
            this.ReportViewerBeli.ActiveViewIndex = -1;
            this.ReportViewerBeli.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReportViewerBeli.Cursor = System.Windows.Forms.Cursors.Default;
            this.ReportViewerBeli.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportViewerBeli.Location = new System.Drawing.Point(20, 60);
            this.ReportViewerBeli.Name = "ReportViewerBeli";
            this.ReportViewerBeli.Size = new System.Drawing.Size(895, 460);
            this.ReportViewerBeli.TabIndex = 0;
            // 
            // PrintPembelian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 540);
            this.Controls.Add(this.ReportViewerBeli);
            this.Name = "PrintPembelian";
            this.Text = "PrintBeli";
            this.Load += new System.EventHandler(this.PrintPembelian_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer ReportViewerBeli;
    }
}