using System;
using System.Windows.Forms;
using assignment_6;
using OrderService;

namespace OrderManagementUI
{
    public partial class OrderEditForm : Form
    {
        public Order EditedOrder { get; private set; }
        private BindingSource detailBindingSource = new BindingSource();

        public OrderEditForm(Order orderToEdit)
        {
            InitializeComponent();

            EditedOrder = orderToEdit == null ?
                new Order() { OrderDate = DateTime.Now } :
                new Order(orderToEdit); // 使用拷贝构造函数避免直接修改原对象

            InitializeDataBinding();
            InitializeControls();
        }

        private void InitializeDataBinding()
        {
            // 绑定主订单信息
            txtOrderId.DataBindings.Add("Text", EditedOrder, "OrderId");
            txtCustomer.DataBindings.Add("Text", EditedOrder, "Customer");
            dtpOrderDate.DataBindings.Add("Value", EditedOrder, "OrderDate");

            // 绑定订单明细
            detailBindingSource.DataSource = EditedOrder.OrderDetails;
            dgvDetails.DataSource = detailBindingSource;

            // 配置明细DataGridView
            dgvDetails.AutoGenerateColumns = false;
            dgvDetails.Columns.Clear();

            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "ProductId",
                HeaderText = "产品ID",
                Name = "colProductId"
            });

            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "ProductName",
                HeaderText = "产品名称",
                Name = "colProductName"
            });

            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "UnitPrice",
                HeaderText = "单价",
                Name = "colUnitPrice"
            });

            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Quantity",
                HeaderText = "数量",
                Name = "colQuantity"
            });
        }

        private void InitializeControls()
        {
            txtOrderId.Enabled = EditedOrder.OrderId == 0; // 新建订单时可编辑ID
            btnAddDetail.Click += BtnAddDetail_Click;
            btnRemoveDetail.Click += BtnRemoveDetail_Click;
        }

        private void BtnAddDetail_Click(object sender, EventArgs e)
        {
            var detailForm = new OrderDetailEditForm(null);
            if (detailForm.ShowDialog() == DialogResult.OK)
            {
                EditedOrder.AddDetail(detailForm.OrderDetail);
                detailBindingSource.ResetBindings(false);
            }
        }

        private void BtnRemoveDetail_Click(object sender, EventArgs e)
        {
            if (dgvDetails.CurrentRow != null)
            {
                var selectedDetail = (OrderDetail)dgvDetails.CurrentRow.DataBoundItem;
                EditedOrder.RemoveDetail(selectedDetail);
                detailBindingSource.ResetBindings(false);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidateOrder())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private bool ValidateOrder()
        {
            if (string.IsNullOrWhiteSpace(txtCustomer.Text))
            {
                MessageBox.Show("客户名称不能为空", "验证错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (EditedOrder.OrderDetails.Count == 0)
            {
                MessageBox.Show("订单必须包含至少一个明细项", "验证错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}