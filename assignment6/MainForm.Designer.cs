partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

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
        this.dgvOrders = new System.Windows.Forms.DataGridView();
        this.dgvOrderDetails = new System.Windows.Forms.DataGridView();
        this.btnAdd = new System.Windows.Forms.Button();
        this.btnEdit = new System.Windows.Forms.Button();
        this.btnDelete = new System.Windows.Forms.Button();
        this.btnQuery = new System.Windows.Forms.Button();
        this.btnRefresh = new System.Windows.Forms.Button();
        this.splitContainer1 = new System.Windows.Forms.SplitContainer();
        this.panel1 = new System.Windows.Forms.Panel();
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetails)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
        this.splitContainer1.Panel1.SuspendInit();
        this.splitContainer1.Panel2.SuspendInit();
        this.splitContainer1.SuspendInit();
        this.panel1.SuspendInit();
        this.SuspendLayout();

        // dgvOrders
        this.dgvOrders.Dock = System.Windows.Forms.DockStyle.Fill;
        this.dgvOrders.Location = new System.Drawing.Point(0, 0);
        this.dgvOrders.Name = "dgvOrders";
        this.dgvOrders.Size = new System.Drawing.Size(800, 250);
        this.dgvOrders.TabIndex = 0;

        // dgvOrderDetails
        this.dgvOrderDetails.Dock = System.Windows.Forms.DockStyle.Fill;
        this.dgvOrderDetails.Location = new System.Drawing.Point(0, 0);
        this.dgvOrderDetails.Name = "dgvOrderDetails";
        this.dgvOrderDetails.Size = new System.Drawing.Size(800, 250);
        this.dgvOrderDetails.TabIndex = 1;

        // btnAdd
        this.btnAdd.Location = new System.Drawing.Point(12, 12);
        this.btnAdd.Name = "btnAdd";
        this.btnAdd.Size = new System.Drawing.Size(75, 23);
        this.btnAdd.TabIndex = 2;
        this.btnAdd.Text = "添加订单";
        this.btnAdd.UseVisualStyleBackColor = true;

        // btnEdit
        this.btnEdit.Location = new System.Drawing.Point(93, 12);
        this.btnEdit.Name = "btnEdit";
        this.btnEdit.Size = new System.Drawing.Size(75, 23);
        this.btnEdit.TabIndex = 3;
        this.btnEdit.Text = "修改订单";
        this.btnEdit.UseVisualStyleBackColor = true;

        // btnDelete
        this.btnDelete.Location = new System.Drawing.Point(174, 12);
        this.btnDelete.Name = "btnDelete";
        this.btnDelete.Size = new System.Drawing.Size(75, 23);
        this.btnDelete.TabIndex = 4;
        this.btnDelete.Text = "删除订单";
        this.btnDelete.UseVisualStyleBackColor = true;

        // btnQuery
        this.btnQuery.Location = new System.Drawing.Point(255, 12);
        this.btnQuery.Name = "btnQuery";
        this.btnQuery.Size = new System.Drawing.Size(75, 23);
        this.btnQuery.TabIndex = 5;
        this.btnQuery.Text = "查询订单";
        this.btnQuery.UseVisualStyleBackColor = true;

        // btnRefresh
        this.btnRefresh.Location = new System.Drawing.Point(336, 12);
        this.btnRefresh.Name = "btnRefresh";
        this.btnRefresh.Size = new System.Drawing.Size(75, 23);
        this.btnRefresh.TabIndex = 6;
        this.btnRefresh.Text = "刷新";
        this.btnRefresh.UseVisualStyleBackColor = true;

        // splitContainer1
        this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.splitContainer1.Location = new System.Drawing.Point(0, 50);
        this.splitContainer1.Name = "splitContainer1";
        this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;

        // splitContainer1.Panel1
        this.splitContainer1.Panel1.Controls.Add(this.dgvOrders);
        this.splitContainer1.Panel1.Controls.Add(this.label1);

        // splitContainer1.Panel2
        this.splitContainer1.Panel2.Controls.Add(this.dgvOrderDetails);
        this.splitContainer1.Panel2.Controls.Add(this.label2);
        this.splitContainer1.Size = new System.Drawing.Size(800, 500);
        this.splitContainer1.SplitterDistance = 250;
        this.splitContainer1.TabIndex = 7;

        // panel1
        this.panel1.Controls.Add(this.btnAdd);
        this.panel1.Controls.Add(this.btnEdit);
        this.panel1.Controls.Add(this.btnDelete);
        this.panel1.Controls.Add(this.btnQuery);
        this.panel1.Controls.Add(this.btnRefresh);
        this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
        this.panel1.Location = new System.Drawing.Point(0, 0);
        this.panel1.Name = "panel1";
        this.panel1.Size = new System.Drawing.Size(800, 50);
        this.panel1.TabIndex = 8;

        // label1
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(3, 3);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(53, 12);
        this.label1.TabIndex = 2;
        this.label1.Text = "订单列表";

        // label2
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(3, 3);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(53, 12);
        this.label2.TabIndex = 3;
        this.label2.Text = "订单明细";

        // MainForm
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 550);
        this.Controls.Add(this.splitContainer1);
        this.Controls.Add(this.panel1);
        this.Name = "MainForm";
        this.Text = "订单管理系统";
        ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetails)).EndInit();
        this.splitContainer1.Panel1.ResumeInit(false);
        this.splitContainer1.Panel2.ResumeInit(false);
        ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
        this.splitContainer1.ResumeInit(false);
        this.panel1.ResumeInit(false);
        this.ResumeInit(false);
    }

    private System.Windows.Forms.DataGridView dgvOrders;
    private System.Windows.Forms.DataGridView dgvOrderDetails;
    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.Button btnEdit;
    private System.Windows.Forms.Button btnDelete;
    private System.Windows.Forms.Button btnQuery;
    private System.Windows.Forms.Button btnRefresh;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
}