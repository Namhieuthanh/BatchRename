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
    /// Interaction logic for MoveConfigDialog.xaml
    /// </summary>
    public partial class MoveConfigDialog : Window
    {
        MoveArgs myArgs;
    
        public MoveConfigDialog(StringArgs args)
        {
            InitializeComponent();
            //Lấy thông tin cũ hiện lên dialog
            myArgs = args as MoveArgs;
            sizeTextBox.Text = myArgs.Size.ToString();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (sizeTextBox.Text == "0")
            {
                MessageBox.Show("Size cannot be 0!");
            }
            else
            {
                //Lấy thông tin mới từ dialog gán vào args
                myArgs.Size = int.Parse(sizeTextBox.Text);
                myArgs.Type = typeComboBox.SelectedIndex;
                DialogResult = true;
                Close();
            }
        }
    }
}
