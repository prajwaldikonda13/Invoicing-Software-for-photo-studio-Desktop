namespace VivaBillingDesktopApplication
{
    partial class FindInvoices
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
            this.topPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.findById = new System.Windows.Forms.RadioButton();
            this.findByMobile = new System.Windows.Forms.RadioButton();
            this.findByEmailId = new System.Windows.Forms.RadioButton();
            this.findByDate = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpForStartDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpForEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblTotalPaid = new System.Windows.Forms.Label();
            this.lblTotalDue = new System.Windows.Forms.Label();
            this.lblTotalDiscount = new System.Windows.Forms.Label();
            this.FilterByDate = new System.Windows.Forms.CheckBox();
            this.InputBox = new System.Windows.Forms.TextBox();
            this.InvoicesGridPanel = new System.Windows.Forms.Panel();
            this.InvoicesGrid = new System.Windows.Forms.DataGridView();
            this.InvoicesGridContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewForPaymentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewForPirintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topPanel.SuspendLayout();
            this.InvoicesGridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InvoicesGrid)).BeginInit();
            this.InvoicesGridContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.AutoSize = true;
            this.topPanel.Controls.Add(this.findById);
            this.topPanel.Controls.Add(this.findByMobile);
            this.topPanel.Controls.Add(this.findByEmailId);
            this.topPanel.Controls.Add(this.findByDate);
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
            this.topPanel.Size = new System.Drawing.Size(1785, 28);
            this.topPanel.TabIndex = 0;
            // 
            // findById
            // 
            this.findById.AutoSize = true;
            this.findById.Location = new System.Drawing.Point(3, 3);
            this.findById.Name = "findById";
            this.findById.Size = new System.Drawing.Size(101, 21);
            this.findById.TabIndex = 0;
            this.findById.TabStop = true;
            this.findById.Text = "Find By Id";
            this.findById.UseVisualStyleBackColor = true;
            this.findById.CheckedChanged += new System.EventHandler(this.findById_CheckedChanged);
            // 
            // findByMobile
            // 
            this.findByMobile.AutoSize = true;
            this.findByMobile.Location = new System.Drawing.Point(110, 3);
            this.findByMobile.Name = "findByMobile";
            this.findByMobile.Size = new System.Drawing.Size(196, 21);
            this.findByMobile.TabIndex = 1;
            this.findByMobile.TabStop = true;
            this.findByMobile.Text = "Find By Mobile Number";
            this.findByMobile.UseVisualStyleBackColor = true;
            this.findByMobile.CheckedChanged += new System.EventHandler(this.findById_CheckedChanged);
            // 
            // findByEmailId
            // 
            this.findByEmailId.AutoSize = true;
            this.findByEmailId.Location = new System.Drawing.Point(312, 3);
            this.findByEmailId.Name = "findByEmailId";
            this.findByEmailId.Size = new System.Drawing.Size(147, 21);
            this.findByEmailId.TabIndex = 2;
            this.findByEmailId.TabStop = true;
            this.findByEmailId.Text = "Find By Email ID";
            this.findByEmailId.UseVisualStyleBackColor = true;
            this.findByEmailId.CheckedChanged += new System.EventHandler(this.findById_CheckedChanged);
            // 
            // findByDate
            // 
            this.findByDate.AutoSize = true;
            this.findByDate.Location = new System.Drawing.Point(465, 3);
            this.findByDate.Name = "findByDate";
            this.findByDate.Size = new System.Drawing.Size(122, 21);
            this.findByDate.TabIndex = 3;
            this.findByDate.TabStop = true;
            this.findByDate.Text = "Find By Date";
            this.findByDate.UseVisualStyleBackColor = true;
            this.findByDate.CheckedChanged += new System.EventHandler(this.findById_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(593, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 23);
            this.label1.TabIndex = 6;
            this.label1.Text = "From:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpForStartDate
            // 
            this.dtpForStartDate.Location = new System.Drawing.Point(651, 3);
            this.dtpForStartDate.Name = "dtpForStartDate";
            this.dtpForStartDate.Size = new System.Drawing.Size(118, 22);
            this.dtpForStartDate.TabIndex = 4;
            this.dtpForStartDate.ValueChanged += new System.EventHandler(this.dtpForStartDate_ValueChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(775, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 24);
            this.label2.TabIndex = 7;
            this.label2.Text = "To:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpForEndDate
            // 
            this.dtpForEndDate.Location = new System.Drawing.Point(817, 3);
            this.dtpForEndDate.Name = "dtpForEndDate";
            this.dtpForEndDate.Size = new System.Drawing.Size(118, 22);
            this.dtpForEndDate.TabIndex = 5;
            this.dtpForEndDate.ValueChanged += new System.EventHandler(this.dtpForStartDate_ValueChanged);
            // 
            // lblTotalPaid
            // 
            this.lblTotalPaid.AutoSize = true;
            this.lblTotalPaid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalPaid.Location = new System.Drawing.Point(941, 0);
            this.lblTotalPaid.Name = "lblTotalPaid";
            this.lblTotalPaid.Size = new System.Drawing.Size(52, 28);
            this.lblTotalPaid.TabIndex = 8;
            this.lblTotalPaid.Text = "label3";
            this.lblTotalPaid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalDue
            // 
            this.lblTotalDue.AutoSize = true;
            this.lblTotalDue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalDue.Location = new System.Drawing.Point(999, 0);
            this.lblTotalDue.Name = "lblTotalDue";
            this.lblTotalDue.Size = new System.Drawing.Size(52, 28);
            this.lblTotalDue.TabIndex = 9;
            this.lblTotalDue.Text = "label3";
            this.lblTotalDue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalDiscount
            // 
            this.lblTotalDiscount.AutoSize = true;
            this.lblTotalDiscount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalDiscount.Location = new System.Drawing.Point(1057, 0);
            this.lblTotalDiscount.Name = "lblTotalDiscount";
            this.lblTotalDiscount.Size = new System.Drawing.Size(52, 28);
            this.lblTotalDiscount.TabIndex = 10;
            this.lblTotalDiscount.Text = "label3";
            this.lblTotalDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FilterByDate
            // 
            this.FilterByDate.AutoSize = true;
            this.FilterByDate.Location = new System.Drawing.Point(1115, 3);
            this.FilterByDate.Name = "FilterByDate";
            this.FilterByDate.Size = new System.Drawing.Size(129, 21);
            this.FilterByDate.TabIndex = 11;
            this.FilterByDate.Text = "Filter By Date";
            this.FilterByDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.FilterByDate.UseVisualStyleBackColor = true;
            // 
            // InputBox
            // 
            this.InputBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.InputBox.Location = new System.Drawing.Point(0, 28);
            this.InputBox.Name = "InputBox";
            this.InputBox.Size = new System.Drawing.Size(1785, 22);
            this.InputBox.TabIndex = 1;
            this.InputBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.InputBox_KeyUp);
            // 
            // InvoicesGridPanel
            // 
            this.InvoicesGridPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.InvoicesGridPanel.Controls.Add(this.InvoicesGrid);
            this.InvoicesGridPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InvoicesGridPanel.Location = new System.Drawing.Point(0, 50);
            this.InvoicesGridPanel.Name = "InvoicesGridPanel";
            this.InvoicesGridPanel.Size = new System.Drawing.Size(1785, 400);
            this.InvoicesGridPanel.TabIndex = 2;
            // 
            // InvoicesGrid
            // 
            this.InvoicesGrid.AllowUserToAddRows = false;
            this.InvoicesGrid.AllowUserToDeleteRows = false;
            this.InvoicesGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.InvoicesGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.InvoicesGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.InvoicesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.InvoicesGrid.ContextMenuStrip = this.InvoicesGridContextMenu;
            this.InvoicesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InvoicesGrid.Location = new System.Drawing.Point(0, 0);
            this.InvoicesGrid.Name = "InvoicesGrid";
            this.InvoicesGrid.RowTemplate.Height = 24;
            this.InvoicesGrid.Size = new System.Drawing.Size(1785, 400);
            this.InvoicesGrid.TabIndex = 0;
            this.InvoicesGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.InvoicesGrid_CellClick);
            this.InvoicesGrid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.InvoicesGrid_KeyUp);
            this.InvoicesGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.InvoicesGrid_MouseDown);
            // 
            // InvoicesGridContextMenu
            // 
            this.InvoicesGridContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.InvoicesGridContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewForPaymentToolStripMenuItem,
            this.viewForPirintToolStripMenuItem});
            this.InvoicesGridContextMenu.Name = "InvoicesGridContextMenu";
            this.InvoicesGridContextMenu.Size = new System.Drawing.Size(196, 52);
            this.InvoicesGridContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.InvoicesGridContextMenu_Opening);
            this.InvoicesGridContextMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.InvoicesGridContextMenu_ItemClicked);
            // 
            // viewForPaymentToolStripMenuItem
            // 
            this.viewForPaymentToolStripMenuItem.Name = "viewForPaymentToolStripMenuItem";
            this.viewForPaymentToolStripMenuItem.Size = new System.Drawing.Size(195, 24);
            this.viewForPaymentToolStripMenuItem.Text = "View for payment";
            // 
            // viewForPirintToolStripMenuItem
            // 
            this.viewForPirintToolStripMenuItem.Name = "viewForPirintToolStripMenuItem";
            this.viewForPirintToolStripMenuItem.Size = new System.Drawing.Size(195, 24);
            this.viewForPirintToolStripMenuItem.Text = "View for print";
            // 
            // FindInvoices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1785, 450);
            this.Controls.Add(this.InvoicesGridPanel);
            this.Controls.Add(this.InputBox);
            this.Controls.Add(this.topPanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FindInvoices";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FindInvoices";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.InvoicesGridPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.InvoicesGrid)).EndInit();
            this.InvoicesGridContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel topPanel;
        private System.Windows.Forms.RadioButton findById;
        private System.Windows.Forms.RadioButton findByMobile;
        private System.Windows.Forms.RadioButton findByEmailId;
        private System.Windows.Forms.RadioButton findByDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpForStartDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpForEndDate;
        private System.Windows.Forms.TextBox InputBox;
        private System.Windows.Forms.Panel InvoicesGridPanel;
        private System.Windows.Forms.DataGridView InvoicesGrid;
        private System.Windows.Forms.Label lblTotalPaid;
        private System.Windows.Forms.Label lblTotalDue;
        private System.Windows.Forms.Label lblTotalDiscount;
        private System.Windows.Forms.ContextMenuStrip InvoicesGridContextMenu;
        private System.Windows.Forms.ToolStripMenuItem viewForPaymentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewForPirintToolStripMenuItem;
        private System.Windows.Forms.CheckBox FilterByDate;
    }
}