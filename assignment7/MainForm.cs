using System;
using System.Windows.Forms;
using OrderService.Data;
using OrderService.Models;
using OrderService.Services;

namespace OrderManagementUI
{
    public partial class MainForm : Form
    {
        private readonly OrderService _orderService;

        public MainForm()
        {
            InitializeComponent();

            // 创建数据库上下文和订单服务
            var context = new OrderContext();
            _orderService = new OrderService(context);

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

        // ... 其他方法与之前相同，只需修改数据访问部分使用_orderService ...

        private void LoadOrders()
        {
            try
            {
                orderBindingSource.DataSource = _orderService.GetAllOrders();
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
                _orderService.AddOrder(editForm.EditedOrder);
                LoadOrders();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (orderBindingSource.Current == null) return;

            var selectedOrder = (Order)orderBindingSource.Current;
            var originalOrder = _orderService.GetOrderById(selectedOrder.OrderId);

            var editForm = new OrderEditForm(originalOrder);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                _orderService.UpdateOrder(editForm.EditedOrder);
                LoadOrders();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (orderBindingSource.Current == null) return;

            if (MessageBox.Show("确定要删除选中的订单吗?", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var selectedOrder = (Order)orderBindingSource.Current;
                _orderService.RemoveOrder(selectedOrder.OrderId);
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
                    orderBindingSource.DataSource = _orderService.QueryOrders(
                        queryForm.OrderId,
                        queryForm.CustomerName,
                        queryForm.ProductName,
                        queryForm.MinTotalAmount);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"查询失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}