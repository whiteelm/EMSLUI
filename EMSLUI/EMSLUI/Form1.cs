using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EMSLUI
{
    public partial class Form1 : Form
    {

        [DllImport("EMSL.dll", CharSet = CharSet.Ansi, EntryPoint = "_emsln1@68",
            CallingConvention = CallingConvention.StdCall)]
        public static extern void BSC4dd1( double h1, double h2, double t, double e1, 
            double e2, ref double aw, ref double s, ref double l, ref double c,
            ref double um, ref double em, ref double z0);

        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                CLabel.Text = null;
                LLabel.Text = null;
                UmLabel.Text = null;
                EmLabel.Text = null;
                Z0Label.Text = null;
                var w = new double[2];
                var s = new double[1];
                w[0] = Convert.ToDouble(Aw1TextBox.Text);
                w[1] = Convert.ToDouble(Aw2TextBox.Text);
                s[0] = Convert.ToDouble(STextBox.Text);
                var h1 = Convert.ToDouble(H1TextBox.Text);
                var h2 = Convert.ToDouble(H2TextBox.Text);
                var t = Convert.ToDouble(TTextBox.Text);
                var e1 = Convert.ToDouble(E1TextBox.Text);
                var e2 = Convert.ToDouble(E2TextBox.Text);
                var l = new double[2, 2];
                var c = new double[2, 2];
                var um = new double[2, 2];
                var em = new double[4];
                var z0 = new double[2, 2];
                BSC4dd1(h1, h2, t, e1, e2, ref w[0], ref s[0],
                    ref l[0, 0], ref c[0, 0], ref um[0, 0], ref em[0], ref z0[0, 0]);
                for (var i = 0; i < 2; i++)
                {
                    for (var j = 0; j < 2; j++)
                    {
                        LLabel.Text += l[i, j].ToString("0.0000") + @"  ";
                        CLabel.Text += c[i, j].ToString("0.0000") + @"  ";
                        UmLabel.Text += um[i, j].ToString("0.0000") + @"  ";
                        Z0Label.Text += z0[i, j].ToString("0.0000") + @"  ";
                    }
                    LLabel.Text += Environment.NewLine;
                    CLabel.Text += Environment.NewLine;
                    UmLabel.Text += Environment.NewLine;
                    Z0Label.Text += Environment.NewLine;
                }
                EmLabel.Text += em[0].ToString("0.0000") + @"  ";
                EmLabel.Text += em[1].ToString("0.0000") + @"  ";

            }
            catch
            {
                MessageBox.Show("Проверьте входные данные",
                    "Ошибка", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Событие ввода с клавиатуры в текстбокс.
        /// </summary>
        private void ValidateDoubleTextBoxes_KeyPress(object sender,
            KeyPressEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.KeyChar.ToString(),
                @"[\d\b,]");
        }
    }
}
