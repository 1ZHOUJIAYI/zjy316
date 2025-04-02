using System;
using System.Windows.Forms;

namespace OrderManagementUI
{
    public partial class QueryForm : Form
    {
        public int? OrderId { get; private set; }
        public string CustomerName { get; private set; }
        public string ProductName { get; private set; }
        public decimal? MinTotalAmount { get; private set; }

        public QueryForm()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            // 解析查询条件
            OrderId = string.IsNullOrWhiteSpace(txtOrderId.Text) ?
                null : (int?)int.Parse(txtOrderId.Text);

            CustomerName = string.IsNullOrWhiteSpace(txtCustomer.Text) ?
                null : txtCustomer.Text.Trim();

            ProductName = string.IsNullOrWhiteSpace(txtProduct.Text) ?
                null : txtProduct.Text.Trim();

            MinTotalAmount = string.IsNullOrWhiteSpace(txtMinAmount.Text) ?
                null : (decimal?)decimal.Parse(txtMinAmount.Text);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}