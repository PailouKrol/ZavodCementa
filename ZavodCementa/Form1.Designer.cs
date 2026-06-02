namespace ZavodCementa
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox listBoxTabs;
        private System.Windows.Forms.Panel panelRight;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listBoxTabs = new System.Windows.Forms.ListBox();
            this.panelRight = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listBoxTabs);
            this.splitContainer1.Panel1MinSize = 180;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelRight);
            this.splitContainer1.Size = new System.Drawing.Size(984, 561);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.TabIndex = 0;
            // 
            // listBoxTabs
            // 
            this.listBoxTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxTabs.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.listBoxTabs.FormattingEnabled = true;
            this.listBoxTabs.ItemHeight = 21;
            this.listBoxTabs.Items.AddRange(new object[] {
            "Склад",
            "Доставка",
            "Клиенты",
            "Сотрудники",
            "Поставщики",
            "Оборудование"});
            this.listBoxTabs.Location = new System.Drawing.Point(0, 0);
            this.listBoxTabs.Name = "listBoxTabs";
            this.listBoxTabs.Size = new System.Drawing.Size(200, 561);
            this.listBoxTabs.TabIndex = 0;
            // 
            // panelRight
            // 
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(0, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(780, 561);
            this.panelRight.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Управление цементным заводом";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}