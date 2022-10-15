namespace VivaBillingDesktopApplication
{
    partial class CopyForm
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
            this.CustomerNameDisplayPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.lblChangeCustomerLabel = new System.Windows.Forms.LinkLabel();
            this.InputComboBox = new System.Windows.Forms.TextBox();
            this.CustomersGridPanel = new System.Windows.Forms.Panel();
            this.CustomerGrid = new System.Windows.Forms.DataGridView();
            this.dropPanel = new System.Windows.Forms.Panel();
            this.dropImageLabel = new System.Windows.Forms.Label();
            this.CustomerNameDisplayPanel.SuspendLayout();
            this.CustomersGridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerGrid)).BeginInit();
            this.dropPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // CustomerNameDisplayPanel
            // 
            this.CustomerNameDisplayPanel.AutoSize = true;
            this.CustomerNameDisplayPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CustomerNameDisplayPanel.Controls.Add(this.lblCustomerName);
            this.CustomerNameDisplayPanel.Controls.Add(this.lblChangeCustomerLabel);
            this.CustomerNameDisplayPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.CustomerNameDisplayPanel.Location = new System.Drawing.Point(0, 22);
            this.CustomerNameDisplayPanel.Name = "CustomerNameDisplayPanel";
            this.CustomerNameDisplayPanel.Padding = new System.Windows.Forms.Padding(3);
            this.CustomerNameDisplayPanel.Size = new System.Drawing.Size(800, 23);
            this.CustomerNameDisplayPanel.TabIndex = 3;
            this.CustomerNameDisplayPanel.Visible = false;
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCustomerName.Location = new System.Drawing.Point(6, 3);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(46, 17);
            this.lblCustomerName.TabIndex = 0;
            this.lblCustomerName.Text = "label1";
            // 
            // lblChangeCustomerLabel
            // 
            this.lblChangeCustomerLabel.AutoSize = true;
            this.lblChangeCustomerLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblChangeCustomerLabel.Location = new System.Drawing.Point(58, 3);
            this.lblChangeCustomerLabel.Name = "lblChangeCustomerLabel";
            this.lblChangeCustomerLabel.Size = new System.Drawing.Size(121, 17);
            this.lblChangeCustomerLabel.TabIndex = 1;
            this.lblChangeCustomerLabel.TabStop = true;
            this.lblChangeCustomerLabel.Text = "Change Customer";
            this.lblChangeCustomerLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblChangeCustomerLabel_LinkClicked);
            // 
            // InputComboBox
            // 
            this.InputComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.InputComboBox.Location = new System.Drawing.Point(0, 0);
            this.InputComboBox.Name = "InputComboBox";
            this.InputComboBox.Size = new System.Drawing.Size(800, 22);
            this.InputComboBox.TabIndex = 2;
            this.InputComboBox.TextChanged += new System.EventHandler(this.InputComboBox_TextChanged);
            // 
            // CustomersGridPanel
            // 
            this.CustomersGridPanel.AutoSize = true;
            this.CustomersGridPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CustomersGridPanel.Controls.Add(this.CustomerGrid);
            this.CustomersGridPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.CustomersGridPanel.Location = new System.Drawing.Point(0, 45);
            this.CustomersGridPanel.Name = "CustomersGridPanel";
            this.CustomersGridPanel.Size = new System.Drawing.Size(800, 0);
            this.CustomersGridPanel.TabIndex = 4;
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
            this.CustomerGrid.Size = new System.Drawing.Size(800, 0);
            this.CustomerGrid.TabIndex = 0;
            this.CustomerGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CustomerGrid_CellClick);
            // 
            // dropPanel
            // 
            this.dropPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dropPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dropPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dropPanel.Controls.Add(this.dropImageLabel);
            this.dropPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dropPanel.Location = new System.Drawing.Point(0, 45);
            this.dropPanel.Name = "dropPanel";
            this.dropPanel.Size = new System.Drawing.Size(800, 405);
            this.dropPanel.TabIndex = 5;
            this.dropPanel.Visible = false;
            // 
            // dropImageLabel
            // 
            this.dropImageLabel.AllowDrop = true;
            this.dropImageLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dropImageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dropImageLabel.Location = new System.Drawing.Point(0, 0);
            this.dropImageLabel.Name = "dropImageLabel";
            this.dropImageLabel.Size = new System.Drawing.Size(796, 401);
            this.dropImageLabel.TabIndex = 0;
            this.dropImageLabel.Text = "Please drop all images in this panel to copy.";
            this.dropImageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dropImageLabel.DragDrop += new System.Windows.Forms.DragEventHandler(this.TempForm_DragDrop);
            this.dropImageLabel.DragEnter += new System.Windows.Forms.DragEventHandler(this.TempForm_DragEnter);
            this.dropImageLabel.DragOver += new System.Windows.Forms.DragEventHandler(this.TempForm_DragOver);
            this.dropImageLabel.DragLeave += new System.EventHandler(this.TempForm_DragLeave);
            // 
            // CopyForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dropPanel);
            this.Controls.Add(this.CustomersGridPanel);
            this.Controls.Add(this.CustomerNameDisplayPanel);
            this.Controls.Add(this.InputComboBox);
            this.Name = "CopyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Copy Form";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.CustomerNameDisplayPanel.ResumeLayout(false);
            this.CustomerNameDisplayPanel.PerformLayout();
            this.CustomersGridPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CustomerGrid)).EndInit();
            this.dropPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel CustomerNameDisplayPanel;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.LinkLabel lblChangeCustomerLabel;
        private System.Windows.Forms.TextBox InputComboBox;
        private System.Windows.Forms.Panel CustomersGridPanel;
        private System.Windows.Forms.DataGridView CustomerGrid;
        private System.Windows.Forms.Panel dropPanel;
        private System.Windows.Forms.Label dropImageLabel;
    }
}