namespace DoAn
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.TLPTab = new System.Windows.Forms.TableLayoutPanel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnMaxsize = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMinisize = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlHome = new System.Windows.Forms.Panel();
            this.TLPTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // TLPTab
            // 
            this.TLPTab.ColumnCount = 8;
            this.TLPTab.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.TLPTab.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.TLPTab.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.TLPTab.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.TLPTab.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.TLPTab.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.TLPTab.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.TLPTab.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.TLPTab.Controls.Add(this.btnExit, 7, 0);
            this.TLPTab.Controls.Add(this.btnMaxsize, 6, 0);
            this.TLPTab.Controls.Add(this.label1, 2, 0);
            this.TLPTab.Controls.Add(this.btnMinisize, 5, 0);
            this.TLPTab.Controls.Add(this.pictureBox1, 1, 0);
            this.TLPTab.Dock = System.Windows.Forms.DockStyle.Top;
            this.TLPTab.Location = new System.Drawing.Point(0, 0);
            this.TLPTab.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TLPTab.Name = "TLPTab";
            this.TLPTab.RowCount = 1;
            this.TLPTab.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLPTab.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TLPTab.Size = new System.Drawing.Size(1100, 30);
            this.TLPTab.TabIndex = 0;
            this.TLPTab.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TLPTab_MouseDown);
            // 
            // btnExit
            // 
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ForeColor = System.Drawing.Color.LimeGreen;
            this.btnExit.Image = global::DoAn.Properties.Resources.close_24dp_FFFFFF;
            this.btnExit.Location = new System.Drawing.Point(1050, 0);
            this.btnExit.Margin = new System.Windows.Forms.Padding(0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(50, 30);
            this.btnExit.TabIndex = 0;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnMaxsize
            // 
            this.btnMaxsize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMaxsize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaxsize.ForeColor = System.Drawing.Color.LimeGreen;
            this.btnMaxsize.Image = global::DoAn.Properties.Resources.fullscreen_32dp_FFFFFF;
            this.btnMaxsize.Location = new System.Drawing.Point(1000, 0);
            this.btnMaxsize.Margin = new System.Windows.Forms.Padding(0);
            this.btnMaxsize.Name = "btnMaxsize";
            this.btnMaxsize.Size = new System.Drawing.Size(50, 30);
            this.btnMaxsize.TabIndex = 1;
            this.btnMaxsize.UseVisualStyleBackColor = true;
            this.btnMaxsize.Click += new System.EventHandler(this.Maxsize_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(143, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(222, 30);
            this.label1.TabIndex = 4;
            this.label1.Text = "DANH BA DIEN THOAI";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnMinisize
            // 
            this.btnMinisize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMinisize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinisize.ForeColor = System.Drawing.Color.LimeGreen;
            this.btnMinisize.Image = global::DoAn.Properties.Resources.minimize_24dp_FFFFFF;
            this.btnMinisize.Location = new System.Drawing.Point(950, 0);
            this.btnMinisize.Margin = new System.Windows.Forms.Padding(0);
            this.btnMinisize.Name = "btnMinisize";
            this.btnMinisize.Size = new System.Drawing.Size(50, 30);
            this.btnMinisize.TabIndex = 5;
            this.btnMinisize.UseVisualStyleBackColor = true;
            this.btnMinisize.Click += new System.EventHandler(this.btnMinisize_Click_1);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DoAn.Properties.Resources.contact_phone_42dp_FFFFFF;
            this.pictureBox1.Location = new System.Drawing.Point(93, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(42, 24);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // pnlHome
            // 
            this.pnlHome.BackColor = System.Drawing.Color.MintCream;
            this.pnlHome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHome.Location = new System.Drawing.Point(0, 30);
            this.pnlHome.Name = "pnlHome";
            this.pnlHome.Size = new System.Drawing.Size(1100, 670);
            this.pnlHome.TabIndex = 2;
            // 
            // Main
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.pnlHome);
            this.Controls.Add(this.TLPTab);
            this.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TLPTab.ResumeLayout(false);
            this.TLPTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TLPTab;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlHome;
        public System.Windows.Forms.Button btnExit;
        public System.Windows.Forms.Button btnMaxsize;
        public System.Windows.Forms.Button btnMinisize;
    }
}