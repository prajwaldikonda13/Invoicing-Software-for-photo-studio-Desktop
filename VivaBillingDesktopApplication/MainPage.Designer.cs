namespace VivaBillingDesktopApplication
{
    partial class MainPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainPage));
            this.InputComboBox = new System.Windows.Forms.TextBox();
            this.CustomerNameDisplayPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.lblChangeCustomerLabel = new System.Windows.Forms.LinkLabel();
            this.CustomersGridPanel = new System.Windows.Forms.Panel();
            this.CustomerGrid = new System.Windows.Forms.DataGridView();
            this.JobsGridPanel = new System.Windows.Forms.Panel();
            this.JobsGrid = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpForInvoiceWithPayment = new System.Windows.Forms.DateTimePicker();
            this.DueLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCurrentDue = new System.Windows.Forms.TextBox();
            this.txtFinalPrice = new System.Windows.Forms.TextBox();
            this.txtGrandTotal = new System.Windows.Forms.TextBox();
            this.txtPaid = new System.Windows.Forms.TextBox();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.txtPrevBal = new System.Windows.Forms.TextBox();
            this.lblPrevDue = new System.Windows.Forms.Label();
            this.dtpForInvoice = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnInvoiceSave = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPaymentSave = new System.Windows.Forms.Button();
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.CustomerNameDisplayPanel.SuspendLayout();
            this.CustomersGridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerGrid)).BeginInit();
            this.JobsGridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.JobsGrid)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.BottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // InputComboBox
            // 
            this.InputComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.InputComboBox.Location = new System.Drawing.Point(0, 0);
            this.InputComboBox.Name = "InputComboBox";
            this.InputComboBox.Size = new System.Drawing.Size(1057, 22);
            this.InputComboBox.TabIndex = 0;
            this.InputComboBox.TextChanged += new System.EventHandler(this.InputComboBox_TextChanged);
            // 
            // CustomerNameDisplayPanel
            // 
            this.CustomerNameDisplayPanel.AutoSize = true;
            this.CustomerNameDisplayPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CustomerNameDisplayPanel.Controls.Add(this.lblCustomerName);
            this.CustomerNameDisplayPanel.Controls.Add(this.lblChangeCustomerLabel);
            this.CustomerNameDisplayPanel.Controls.Add(this.linkLabel1);
            this.CustomerNameDisplayPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.CustomerNameDisplayPanel.Location = new System.Drawing.Point(0, 22);
            this.CustomerNameDisplayPanel.Name = "CustomerNameDisplayPanel";
            this.CustomerNameDisplayPanel.Padding = new System.Windows.Forms.Padding(3);
            this.CustomerNameDisplayPanel.Size = new System.Drawing.Size(1057, 23);
            this.CustomerNameDisplayPanel.TabIndex = 1;
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCustomerName.Location = new System.Drawing.Point(6, 3);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(52, 17);
            this.lblCustomerName.TabIndex = 0;
            this.lblCustomerName.Text = "label1";
            // 
            // lblChangeCustomerLabel
            // 
            this.lblChangeCustomerLabel.AutoSize = true;
            this.lblChangeCustomerLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblChangeCustomerLabel.Location = new System.Drawing.Point(64, 3);
            this.lblChangeCustomerLabel.Name = "lblChangeCustomerLabel";
            this.lblChangeCustomerLabel.Size = new System.Drawing.Size(136, 17);
            this.lblChangeCustomerLabel.TabIndex = 1;
            this.lblChangeCustomerLabel.TabStop = true;
            this.lblChangeCustomerLabel.Text = "Change Customer";
            this.lblChangeCustomerLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblChangeCustomer_LinkClicked);
            // 
            // CustomersGridPanel
            // 
            this.CustomersGridPanel.AutoSize = true;
            this.CustomersGridPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CustomersGridPanel.Controls.Add(this.CustomerGrid);
            this.CustomersGridPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.CustomersGridPanel.Location = new System.Drawing.Point(0, 45);
            this.CustomersGridPanel.Name = "CustomersGridPanel";
            this.CustomersGridPanel.Size = new System.Drawing.Size(1057, 0);
            this.CustomersGridPanel.TabIndex = 2;
            // 
            // CustomerGrid
            // 
            this.CustomerGrid.AllowUserToAddRows = false;
            this.CustomerGrid.AllowUserToDeleteRows = false;
            this.CustomerGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.CustomerGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.CustomerGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CustomerGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CustomerGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CustomerGrid.Location = new System.Drawing.Point(0, 0);
            this.CustomerGrid.Margin = new System.Windows.Forms.Padding(0);
            this.CustomerGrid.Name = "CustomerGrid";
            this.CustomerGrid.ReadOnly = true;
            this.CustomerGrid.RowTemplate.Height = 24;
            this.CustomerGrid.Size = new System.Drawing.Size(1057, 0);
            this.CustomerGrid.TabIndex = 0;
            this.CustomerGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CustomerGrid_CellClick);
            // 
            // JobsGridPanel
            // 
            this.JobsGridPanel.AutoSize = true;
            this.JobsGridPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.JobsGridPanel.Controls.Add(this.JobsGrid);
            this.JobsGridPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.JobsGridPanel.Location = new System.Drawing.Point(0, 45);
            this.JobsGridPanel.Name = "JobsGridPanel";
            this.JobsGridPanel.Size = new System.Drawing.Size(1057, 0);
            this.JobsGridPanel.TabIndex = 3;
            // 
            // JobsGrid
            // 
            this.JobsGrid.AllowUserToAddRows = false;
            this.JobsGrid.AllowUserToDeleteRows = false;
            this.JobsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.JobsGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.JobsGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.JobsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.JobsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.JobsGrid.Location = new System.Drawing.Point(0, 0);
            this.JobsGrid.Name = "JobsGrid";
            this.JobsGrid.RowTemplate.Height = 24;
            this.JobsGrid.Size = new System.Drawing.Size(1057, 0);
            this.JobsGrid.TabIndex = 0;
            this.JobsGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.JobsGrid_CellEndEdit);
            this.JobsGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.JobsGrid_CellEnter);
            this.JobsGrid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.JobsGrid_KeyUp);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 9;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.227466F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.227468F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.227468F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.227468F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.227468F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.227468F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.227468F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.70386F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.70386F));
            this.tableLayoutPanel1.Controls.Add(this.label6, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.dtpForInvoiceWithPayment, 7, 1);
            this.tableLayoutPanel1.Controls.Add(this.DueLabel, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtCurrentDue, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtFinalPrice, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtGrandTotal, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtPaid, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtDiscount, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtPrevBal, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblPrevDue, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpForInvoice, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 8, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 8, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1057, 70);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(585, 35);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label6.Size = new System.Drawing.Size(91, 35);
            this.label6.TabIndex = 22;
            this.label6.Text = "Payment Date:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // dtpForInvoiceWithPayment
            // 
            this.dtpForInvoiceWithPayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpForInvoiceWithPayment.Location = new System.Drawing.Point(682, 38);
            this.dtpForInvoiceWithPayment.Name = "dtpForInvoiceWithPayment";
            this.dtpForInvoiceWithPayment.Size = new System.Drawing.Size(181, 22);
            this.dtpForInvoiceWithPayment.TabIndex = 19;
            this.dtpForInvoiceWithPayment.Value = new System.DateTime(1960, 1, 1, 0, 0, 0, 0);
            this.dtpForInvoiceWithPayment.ValueChanged += new System.EventHandler(this.dtpForInvoice_ValueChanged);
            // 
            // DueLabel
            // 
            this.DueLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DueLabel.Location = new System.Drawing.Point(391, 35);
            this.DueLabel.Name = "DueLabel";
            this.DueLabel.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.DueLabel.Size = new System.Drawing.Size(91, 35);
            this.DueLabel.TabIndex = 17;
            this.DueLabel.Text = "label6";
            this.DueLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(197, 35);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label5.Size = new System.Drawing.Size(91, 35);
            this.label5.TabIndex = 16;
            this.label5.Text = "Final Price:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 35);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label4.Size = new System.Drawing.Size(91, 35);
            this.label4.TabIndex = 15;
            this.label4.Text = "Grand Total:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(391, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label3.Size = new System.Drawing.Size(91, 35);
            this.label3.TabIndex = 14;
            this.label3.Text = "Paid:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(197, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label2.Size = new System.Drawing.Size(91, 35);
            this.label2.TabIndex = 13;
            this.label2.Text = "Discount:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtCurrentDue
            // 
            this.txtCurrentDue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCurrentDue.Location = new System.Drawing.Point(488, 38);
            this.txtCurrentDue.Name = "txtCurrentDue";
            this.txtCurrentDue.ReadOnly = true;
            this.txtCurrentDue.Size = new System.Drawing.Size(91, 22);
            this.txtCurrentDue.TabIndex = 11;
            this.txtCurrentDue.TextChanged += new System.EventHandler(this.PrevBal_TextChanged);
            // 
            // txtFinalPrice
            // 
            this.txtFinalPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFinalPrice.Location = new System.Drawing.Point(294, 38);
            this.txtFinalPrice.Name = "txtFinalPrice";
            this.txtFinalPrice.ReadOnly = true;
            this.txtFinalPrice.Size = new System.Drawing.Size(91, 22);
            this.txtFinalPrice.TabIndex = 9;
            this.txtFinalPrice.TextChanged += new System.EventHandler(this.PrevBal_TextChanged);
            // 
            // txtGrandTotal
            // 
            this.txtGrandTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGrandTotal.Location = new System.Drawing.Point(100, 38);
            this.txtGrandTotal.Name = "txtGrandTotal";
            this.txtGrandTotal.ReadOnly = true;
            this.txtGrandTotal.Size = new System.Drawing.Size(91, 22);
            this.txtGrandTotal.TabIndex = 7;
            this.txtGrandTotal.TextChanged += new System.EventHandler(this.PrevBal_TextChanged);
            // 
            // txtPaid
            // 
            this.txtPaid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPaid.Location = new System.Drawing.Point(488, 3);
            this.txtPaid.Name = "txtPaid";
            this.txtPaid.Size = new System.Drawing.Size(91, 22);
            this.txtPaid.TabIndex = 5;
            this.txtPaid.TextChanged += new System.EventHandler(this.PrevBal_TextChanged);
            // 
            // txtDiscount
            // 
            this.txtDiscount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDiscount.Location = new System.Drawing.Point(294, 3);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(91, 22);
            this.txtDiscount.TabIndex = 3;
            this.txtDiscount.TextChanged += new System.EventHandler(this.PrevBal_TextChanged);
            // 
            // txtPrevBal
            // 
            this.txtPrevBal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPrevBal.Location = new System.Drawing.Point(100, 3);
            this.txtPrevBal.Name = "txtPrevBal";
            this.txtPrevBal.ReadOnly = true;
            this.txtPrevBal.Size = new System.Drawing.Size(91, 22);
            this.txtPrevBal.TabIndex = 0;
            this.txtPrevBal.TextChanged += new System.EventHandler(this.PrevBal_TextChanged);
            // 
            // lblPrevDue
            // 
            this.lblPrevDue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPrevDue.Location = new System.Drawing.Point(3, 0);
            this.lblPrevDue.Name = "lblPrevDue";
            this.lblPrevDue.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblPrevDue.Size = new System.Drawing.Size(91, 35);
            this.lblPrevDue.TabIndex = 12;
            this.lblPrevDue.Text = "label1";
            this.lblPrevDue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // dtpForInvoice
            // 
            this.dtpForInvoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpForInvoice.Location = new System.Drawing.Point(682, 3);
            this.dtpForInvoice.Name = "dtpForInvoice";
            this.dtpForInvoice.Size = new System.Drawing.Size(181, 22);
            this.dtpForInvoice.TabIndex = 18;
            this.dtpForInvoice.Value = new System.DateTime(1960, 1, 1, 0, 0, 0, 0);
            this.dtpForInvoice.ValueChanged += new System.EventHandler(this.dtpForInvoice_ValueChanged);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(585, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label1.Size = new System.Drawing.Size(91, 35);
            this.label1.TabIndex = 21;
            this.label1.Text = "Invoice Date:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnInvoiceSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(868, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(187, 31);
            this.panel1.TabIndex = 24;
            // 
            // btnInvoiceSave
            // 
            this.btnInvoiceSave.AutoSize = true;
            this.btnInvoiceSave.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnInvoiceSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInvoiceSave.Location = new System.Drawing.Point(0, 0);
            this.btnInvoiceSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnInvoiceSave.Name = "btnInvoiceSave";
            this.btnInvoiceSave.Size = new System.Drawing.Size(187, 30);
            this.btnInvoiceSave.TabIndex = 21;
            this.btnInvoiceSave.Text = "Save Invoice";
            this.btnInvoiceSave.UseVisualStyleBackColor = true;
            this.btnInvoiceSave.Click += new System.EventHandler(this.btnInvoiceSave_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnPaymentSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(868, 37);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(187, 31);
            this.panel2.TabIndex = 25;
            // 
            // btnPaymentSave
            // 
            this.btnPaymentSave.AutoSize = true;
            this.btnPaymentSave.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPaymentSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPaymentSave.Location = new System.Drawing.Point(0, 0);
            this.btnPaymentSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnPaymentSave.Name = "btnPaymentSave";
            this.btnPaymentSave.Size = new System.Drawing.Size(187, 30);
            this.btnPaymentSave.TabIndex = 24;
            this.btnPaymentSave.Text = "Save Invoice With Payment";
            this.btnPaymentSave.UseVisualStyleBackColor = true;
            this.btnPaymentSave.Click += new System.EventHandler(this.btnPaymentSave_Click);
            // 
            // BottomPanel
            // 
            this.BottomPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BottomPanel.Controls.Add(this.tableLayoutPanel1);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 380);
            this.BottomPanel.Margin = new System.Windows.Forms.Padding(0);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(1057, 70);
            this.BottomPanel.TabIndex = 4;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(206, 3);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(100, 17);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Copy Images";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 450);
            this.Controls.Add(this.BottomPanel);
            this.Controls.Add(this.JobsGridPanel);
            this.Controls.Add(this.CustomersGridPanel);
            this.Controls.Add(this.CustomerNameDisplayPanel);
            this.Controls.Add(this.InputComboBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MainPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainPage";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.CustomerNameDisplayPanel.ResumeLayout(false);
            this.CustomerNameDisplayPanel.PerformLayout();
            this.CustomersGridPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CustomerGrid)).EndInit();
            this.JobsGridPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.JobsGrid)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.BottomPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox InputComboBox;
        private System.Windows.Forms.FlowLayoutPanel CustomerNameDisplayPanel;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.LinkLabel lblChangeCustomerLabel;
        private System.Windows.Forms.Panel CustomersGridPanel;
        private System.Windows.Forms.DataGridView CustomerGrid;
        private System.Windows.Forms.Panel JobsGridPanel;
        private System.Windows.Forms.DataGridView JobsGrid;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtPaid;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.TextBox txtPrevBal;
        private System.Windows.Forms.Panel BottomPanel;
        private System.Windows.Forms.Label lblPrevDue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label DueLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCurrentDue;
        private System.Windows.Forms.TextBox txtFinalPrice;
        private System.Windows.Forms.TextBox txtGrandTotal;
        private System.Windows.Forms.DateTimePicker dtpForInvoiceWithPayment;
        private System.Windows.Forms.DateTimePicker dtpForInvoice;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnInvoiceSave;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnPaymentSave;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}