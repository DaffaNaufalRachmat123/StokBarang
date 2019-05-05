namespace StokBarangApp
{
    partial class PrintSupplier
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
            this.SupplierReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // SupplierReport
            // 
            this.SupplierReport.ActiveViewIndex = -1;
            this.SupplierReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SupplierReport.Cursor = System.Windows.Forms.Cursors.Default;
            this.SupplierReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SupplierReport.Location = new System.Drawing.Point(20, 60);
            this.SupplierReport.Name = "SupplierReport";
            this.SupplierReport.Size = new System.Drawing.Size(757, 427);
            this.SupplierReport.TabIndex = 0;
            // 
            // PrintSupplier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 507);
            this.Controls.Add(this.SupplierReport);
            this.Name = "PrintSupplier";
            this.Text = "PrintSupplier";
            this.Load += new System.EventHandler(this.PrintSupplier_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer SupplierReport;
    }
}