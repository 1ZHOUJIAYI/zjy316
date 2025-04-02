using System;
using System.Windows.Forms;
using assignment_6;
using OrderService; // 原有订单服务的命名空间

namespace OrderManagementUI
{
    public partial class MainForm : Form
    {
        private OrderService.OrderService orderService = new OrderService.OrderService();
        private BindingSource orderBindingSource = new BindingSource();
        private BindingSource orderDetailBindingSource = new BindingSource();

        public MainForm()
        {
            InitializeComponent();
            InitializeDataBinding();
            LoadOrders();
        }

        private void InitializeDataBinding()
        {
            // 主订单数据绑定
            orderBindingSource.DataSource = typeof(Order);
            dgvOrders.DataSource = orderBindingSource;

            // 订单明细数据绑定
            orderDetailBindingSource.DataSource = orderBindingSource;
            orderDetailBindingSource.DataMember = "OrderDetails";
            dgvOrderDetails.DataSource = orderDetailBindingSource;

            // 设置DataGridView列
            ConfigureOrderGridColumns();
            ConfigureOrderDetailGridColumns();
        }

        private void ConfigureOrderGridColumns()
        {
            dgvOrders.AutoGenerateColumns = false;
            dgvOrders.Columns.Clear();

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "OrderId",
                HeaderText = "订单号",
                Name = "colOrderId"
            });

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Customer",
                HeaderText = "客户",
                Name = "colCustomer"
            });

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "OrderDate",
                HeaderText = "日期",
                Name = "colOrderDate",
                DefaultCellStyle = new DataGridViewCellStyle() { Format = "yyyy-MM-dd" }
            });

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "TotalAmount",
                HeaderText = "总金额",
                Name = "colTotalAmount",
                DefaultCellStyle = new DataGridViewCellStyle() { Format = "C2" }
            });
        }

        private void ConfigureOrderDetailGridColumns()
        {
            dgvOrderDetails.AutoGenerateColumns = false;
            dgvOrderDetails.Columns.Clear();

            dgvOrderDetails.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "ProductId",
                HeaderText = "产品ID",
                Name = "colProductId"
            });

            dgvOrderDetails.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "ProductName",
                HeaderText = "产品名称",
                Name = "colProductName"
            });

            dgvOrderDetails.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "UnitPrice",
                HeaderText = "单价",
                Name = "colUnitPrice",
                DefaultCellStyle = new DataGridViewCellStyle() { Format = "C2" }
            });

            dgvOrderDetails.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Quantity",
                HeaderText = "数量",
                Name = "colQuantity"
            });

            dgvOrderDetails.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Amount",
                HeaderText = "金额",
                Name = "colAmount",
                DefaultCellStyle = new DataGridViewCellStyle() { Format = "C2" }
            });
        }

        private void LoadOrders()
        {
            try
            {
                orderBindingSource.DataSource = orderService.GetAllOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载订单失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var editForm = new OrderEditForm(null);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                orderService.AddOrder(editForm.EditedOrder);
                LoadOrders();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (orderBindingSource.Current == null)
            {
                MessageBox.Show("请先选择要修改的订单", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedOrder = (Order)orderBindingSource.Current;
            var editForm = new OrderEditForm(selectedOrder);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                orderService.UpdateOrder(editForm.EditedOrder);
                LoadOrders();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (orderBindingSource.Current == null)
            {
                MessageBox.Show("请先选择要删除的订单", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("确定要删除选中的订单吗?", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var selectedOrder = (Order)orderBindingSource.Current;
                orderService.RemoveOrder(selectedOrder.OrderId);
                LoadOrders();
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            var queryForm = new QueryForm();
            if (queryForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var orders = orderService.QueryOrders(
                        queryForm.OrderId,
                        queryForm.CustomerName,
                        queryForm.ProductName,
                        queryForm.MinTotalAmount);

                    orderBindingSource.DataSource = orders;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"查询失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadOrders();
        }
    }
}