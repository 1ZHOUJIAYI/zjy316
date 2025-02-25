using System;
using System.Windows.Forms;

namespace CalculatorApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            double num1, num2;
            if (!double.TryParse(txtNum1.Text, out num1) || !double.TryParse(txtNum2.Text, out num2))
            {
                lblResult.Text = "请输入有效的数字！";
                return;
            }

            char op = cmbOperator.SelectedItem.ToString()[0];
            double result = 0;

            switch (op)
            {
                case '+':
                    result = num1 + num2;
                    break;
                case '-':
                    result = num1 - num2;
                    break;
                case '*':
                    result = num1 * num2;
                    break;
                case '/':
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    else
                    {
                        lblResult.Text = "错误：除数不能为零！";
                        return;
                    }
                    break;
                default:
                    lblResult.Text = "错误：无效的运算符！";
                    return;
            }

            lblResult.Text = $"计算结果: {result}";
        }
    }
}
