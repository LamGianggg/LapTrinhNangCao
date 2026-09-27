using System;
using System.Linq;
using System.Windows.Forms;

namespace SimpleCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void txtB_TextChanged(object sender, EventArgs e)
        {
        }

        private void Label4_Click(object sender, EventArgs e)
        {
        }

        private void CalculateWithLinq(Func<double, double, double> operation, bool isDivide = false)
        {
            var rawInputs = new[] { txtA.Text, txtB.Text };

            if (!rawInputs.All(s => double.TryParse(s, out _)))
            {
                MessageBox.Show("Vui lòng nhập số hợp lệ vào cả 2 ô!");
                return;
            }

            var numbers = rawInputs.Select(double.Parse).ToArray();

            if (isDivide && numbers.Last() == 0)
            {
                MessageBox.Show("Không thể chia cho 0!");
                return;
            }

            double result = numbers.Aggregate(operation);

            txtResult.Text = result.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CalculateWithLinq((a, b) => a + b);
        }

        private void btnSubtract_Click(object sender, EventArgs e)
        {
            CalculateWithLinq((a, b) => a - b);
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            CalculateWithLinq((a, b) => a * b);
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            CalculateWithLinq((a, b) => a / b, isDivide: true);
        }
    }
}
