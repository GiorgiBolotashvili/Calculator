using System.Drawing;
using System.Windows.Forms;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            MyForm form = new MyForm(80, 80, Color.LightBlue);
            form.Size = new Size(360, 660);
            form.BackColor = Color.RoyalBlue;
            form.Text = "Calculator";
            Application.Run(form);
        }
    }
}
