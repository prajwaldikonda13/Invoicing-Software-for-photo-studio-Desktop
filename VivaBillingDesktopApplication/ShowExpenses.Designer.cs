namespace VivaBillingDesktopApplication
{
    partial class ShowExpenses
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
            this.components = new System.ComponentModel.Container();
            this.viewForPirintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InvoicesGridContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewForPaymentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpForStartDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.InvoicesGridPanel = new System.Windows.Forms.Panel();
            this.ExpenseGrid = new System.Windows.Forms.DataGridView();
            this.dtpForEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblTotalPaid = new System.Windows.Forms.Label();
            this.lblTotalDiscount = new System.Windows.Forms.Label();
            this.FilterByDate = new System.Windows.Forms.CheckBox();
            this.topPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTotalDue = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.InvoicesGridContextMenu.SuspendLayout();
            this.InvoicesGridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ExpenseGrid)).BeginInit();
            this.topPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // viewForPirintToolStripMenuItem
            // 
            this.viewForPirintToolStripMenuItem.Name = "viewForPirintToolStripMenuItem";
            this.viewForPirintToolStripMenuItem.Size = new System.Drawing.Size(195, 24);
            this.viewForPirintToolStripMenuItem.Text = "View for print";
            // 
            // InvoicesGridContextMenu
            // 
            this.InvoicesGridContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.InvoicesGridContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewForPaymentToolStripMenuItem,
            this.viewForPirintToolStripMenuItem});
            this.InvoicesGridContextMenu.Name = "InvoicesGridContextMenu";
            this.InvoicesGridContextMenu.Size = new System.Drawing.Size(196, 52);
            // 
            // viewForPaymentToolStripMenuItem
            // 
            this.viewForPaymentToolStripMenuItem.Name = "viewForPaymentToolStripMenuItem";
            this.viewForPaymentToolStripMenuItem.Size = new System.Drawing.Size(195, 24);
            this.viewForPaymentToolStripMenuItem.Text = "View for payment";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(496, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 23);
            this.label1.TabIndex = 6;
            this.label1.Text = "From:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpForStartDate
            // 
            this.dtpForStartDate.Location = new System.Drawing.Point(554, 3);
            this.dtpForStartDate.Name = "dtpForStartDate";
            this.dtpForStartDate.Size = new System.Drawing.Size(118, 22);
            this.dtpForStartDate.TabIndex = 4;
            this.dtpForStartDate.ValueChanged += new System.EventHandler(this.dtpForStartDate_ValueChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(678, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 24);
            this.label2.TabIndex = 7;
            this.label2.Text = "To:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // InvoicesGridPanel
            // 
            this.InvoicesGridPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.InvoicesGridPanel.Controls.Add(this.ExpenseGrid);
            this.InvoicesGridPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InvoicesGridPanel.Location = new System.Drawing.Point(0, 28);
            this.InvoicesGridPanel.Name = "InvoicesGridPanel";
            this.InvoicesGridPanel.Size = new System.Drawing.Size(1456, 422);
            this.InvoicesGridPanel.TabIndex = 5;
            // 
            // ExpenseGrid
            // 
            this.ExpenseGrid.AllowUserToAddRows = false;
            this.ExpenseGrid.AllowUserToDeleteRows = false;
            this.ExpenseGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.ExpenseGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ExpenseGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.ExpenseGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ExpenseGrid.ContextMenuStrip = this.InvoicesGridContextMenu;
            this.ExpenseGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExpenseGrid.Location = new System.Drawing.Point(0, 0);
            this.ExpenseGrid.Name = "ExpenseGrid";
            this.ExpenseGrid.RowTemplate.Height = 24;
            this.ExpenseGrid.Size = new System.Drawing.Size(1456, 422);
            this.ExpenseGrid.TabIndex = 0;
            // 
            // dtpForEndDate
            // 
            this.dtpForEndDate.Location = new System.Drawing.Point(720, 3);
            this.dtpForEndDate.Name = "dtpForEndDate";
            this.dtpForEndDate.Size = new System.Drawing.Size(118, 22);
            this.dtpForEndDate.TabIndex = 5;
            this.dtpForEndDate.ValueChanged += new System.EventHandler(this.dtpForStartDate_ValueChanged);
            // 
            // lblTotalPaid
            // 
            this.lblTotalPaid.AutoSize = true;
            this.lblTotalPaid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalPaid.Location = new System.Drawing.Point(844, 0);
            this.lblTotalPaid.Name = "lblTotalPaid";
            this.lblTotalPaid.Size = new System.Drawing.Size(46, 28);
            this.lblTotalPaid.TabIndex = 8;
            this.lblTotalPaid.Text = "label3";
            this.lblTotalPaid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalDiscount
            // 
            this.lblTotalDiscount.AutoSize = true;
            this.lblTotalDiscount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalDiscount.Location = new System.Drawing.Point(948, 0);
            this.lblTotalDiscount.Name = "lblTotalDiscount";
            this.lblTotalDiscount.Size = new System.Drawing.Size(46, 28);
            this.lblTotalDiscount.TabIndex = 10;
            this.lblTotalDiscount.Text = "label3";
            this.lblTotalDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FilterByDate
            // 
            this.FilterByDate.AutoSize = true;
            this.FilterByDate.Checked = true;
            this.FilterByDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FilterByDate.Location = new System.Drawing.Point(1000, 3);
            this.FilterByDate.Name = "FilterByDate";
            this.FilterByDate.Size = new System.Drawing.Size(115, 21);
            this.FilterByDate.TabIndex = 11;
            this.FilterByDate.Text = "Filter By Date";
            this.FilterByDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.FilterByDate.UseVisualStyleBackColor = true;
            this.FilterByDate.CheckedChanged += new System.EventHandler(this.FilterByDate_CheckedChanged);
            // 
            // topPanel
            // 
            this.topPanel.AutoSize = true;
            this.topPanel.Controls.Add(this.textBox1);
            this.topPanel.Controls.Add(this.label1);
            this.topPanel.Controls.Add(this.dtpForStartDate);
            this.topPanel.Controls.Add(this.label2);
            this.topPanel.Controls.Add(this.dtpForEndDate);
            this.topPanel.Controls.Add(this.lblTotalPaid);
            this.topPanel.Controls.Add(this.lblTotalDue);
            this.topPanel.Controls.Add(this.lblTotalDiscount);
            this.topPanel.Controls.Add(this.FilterByDate);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(1456, 28);
            this.topPanel.TabIndex = 3;
            // 
            // lblTotalDue
            // 
            this.lblTotalDue.AutoSize = true;
            this.lblTotalDue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalDue.Location = new System.Drawing.Point(896, 0);
            this.lblTotalDue.Name = "lblTotalDue";
            this.lblTotalDue.Size = new System.Drawing.Size(46, 28);
            this.lblTotalDue.TabIndex = 9;
            this.lblTotalDue.Text = "label3";
            this.lblTotalDue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(487, 22);
            this.textBox1.TabIndex = 12;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // ShowExpenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1456, 450);
            this.Controls.Add(this.InvoicesGridPanel);
            this.Controls.Add(this.topPanel);
            this.Name = "ShowExpenses";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ShowExpenses";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.InvoicesGridContextMenu.ResumeLayout(false);
            this.InvoicesGridPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ExpenseGrid)).EndInit();
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem viewForPirintToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip InvoicesGridContextMenu;
        private System.Windows.Forms.ToolStripMenuItem viewForPaymentToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpForStartDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel InvoicesGridPanel;
        private System.Windows.Forms.DataGridView ExpenseGrid;
        private System.Windows.Forms.DateTimePicker dtpForEndDate;
        private System.Windows.Forms.Label lblTotalPaid;
        private System.Windows.Forms.Label lblTotalDiscount;
        private System.Windows.Forms.CheckBox FilterByDate;
        private System.Windows.Forms.FlowLayoutPanel topPanel;
        private System.Windows.Forms.Label lblTotalDue;
        private System.Windows.Forms.TextBox textBox1;
    }
}