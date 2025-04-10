using System;
using System.Windows.Forms;
using OrderService.Data;
using static System.Net.Mime.MediaTypeNames;

namespace OrderManagementUI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // 初始化数据库
            try
            {
                using (var context = new OrderContext())
                {
                    context.Database.EnsureCreated();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"数据库初始化失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}