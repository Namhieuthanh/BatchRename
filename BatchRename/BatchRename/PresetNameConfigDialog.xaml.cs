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
    /// Interaction logic for PresetNameConfigDialog.xaml
    /// </summary>
    public partial class PresetNameConfigDialog : Window
    {
        string presetName;
        public PresetNameConfigDialog(string name)
        {
            InitializeComponent();
            presetName = name;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (presetNameTextBox.Text == "")
            {
                MessageBox.Show("Please input a name for preset!");
            }
            else
            {
                presetName = presetNameTextBox.Text;
                DialogResult = true;
                Close();
            }
        }
    }
}
