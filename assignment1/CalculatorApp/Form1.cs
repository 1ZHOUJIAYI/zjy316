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
                lblResult.Text = "��������Ч�����֣�";
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
                        lblResult.Text = "���󣺳�������Ϊ�㣡";
                        return;
                    }
                    break;
                default:
                    lblResult.Text = "������Ч���������";
                    return;
            }

            lblResult.Text = $"������: {result}";
        }
    }
}
