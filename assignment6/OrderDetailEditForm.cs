using System;
using System.Windows.Forms;
using OrderService;

namespace OrderManagementUI
{
    public partial class OrderDetailEditForm : Form
    {
        public OrderDetail OrderDetail { get; private set; }

        public OrderDetailEditForm(OrderDetail detailToEdit)
        {
            InitializeComponent();

            OrderDetail = detailToEdit == null ?
                new OrderDetail() :
                new OrderDetail(detailToEdit); // 使用拷贝构造函数

            InitializeDataBinding();
        }

        private void InitializeDataBinding()
        {
            txtProductId.DataBindings.Add("Text", OrderDetail, "ProductId");
            txtProductName.DataBindings.Add("Text", OrderDetail, "ProductName");
            numUnitPrice.DataBindings.Add("Value", OrderDetail, "UnitPrice");
            numQuantity.DataBindings.Add("Value", OrderDetail, "Quantity");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidateDetail())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private bool ValidateDetail()
        {
            if (string.IsNullOrWhiteSpace(txtProductId.Text))
            {
                MessageBox.Show("产品ID不能为空", "验证错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("产品名称不能为空", "验证错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (numUnitPrice.Value <= 0)
            {
                MessageBox.Show("单价必须大于0", "验证错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (numQuantity.Value <= 0)
            {
                MessageBox.Show("数量必须大于0", "验证错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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