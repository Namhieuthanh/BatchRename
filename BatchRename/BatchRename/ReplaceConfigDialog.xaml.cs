using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for ReplaceConfigDialog.xaml
    /// </summary>
    public partial class ReplaceConfigDialog : Window
    {
        ReplaceArgs myArgs;

        public ReplaceConfigDialog(StringArgs args)
        {
            InitializeComponent();
            //Lấy from và to(cũ) từ ReplaceArgs hiện lên dialog
            myArgs = args as ReplaceArgs;
            fromTextBox.Text = myArgs.From;
            toTextBox.Text = myArgs.To;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (fromTextBox.Text == "")
            {
                MessageBox.Show("String From cannot be empty");
            }
            else
            {
                //Lấy from và to(mới) từ dialog gán vào args
                myArgs.From = fromTextBox.Text;
                myArgs.To = toTextBox.Text;
                DialogResult = true;
                Close();
            }
        }
    }
}

