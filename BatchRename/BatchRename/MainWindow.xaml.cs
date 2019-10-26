using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //hộp thoại chọn file add vào listFIles
        Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
        //hộp thoại chọn folder
        FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
        //list chứa các files cần đổi tên
        BindingList<File> listFiles = new BindingList<File>();
        //list chứa các folders cần đổi tên
        BindingList<Folder> listFolders = new BindingList<Folder>();
        List<StringOperation> _prototypes = new List<StringOperation>();

        BindingList<StringOperation> _actions = new BindingList<StringOperation>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //add các loại prototype mà mình có
            var prototype1 = new ReplaceOperation()
            {
                Args = new ReplaceArgs()
                {
                    From = "From",
                    To = "To"
                }
            };
            var prototype2 = new MoveOperation()
            {
                Args = new MoveArgs()
                {
                    Size = 0,
                    Type = 0
                }
            };
            //add vào list prototypes
            _prototypes.Add(prototype1);
            _prototypes.Add(prototype2);
            //set source cho prototypesComboBox, filesListView, folderListView và operationListBox
            prototypesComboBox.ItemsSource = _prototypes;
            operationsListBox.ItemsSource = _actions;
            filesListView.ItemsSource = listFiles;
            foldersListView.ItemsSource = listFolders;

        }

        /// <summary>
        /// Hàm add file vào list files, thực hiện khi bấm nút add file(s)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addFilesButton_Click(object sender, RoutedEventArgs e)
        {
            //set tính năng chọn nhiều files
            openFileDialog.Multiselect = true;
            //đường dẫn đầu tiên mà hộp thoại hiện lên
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
       
            if (openFileDialog.ShowDialog() == true)
            {
                //add từng tên file vào trong listFiles
                foreach (string fullFileName in openFileDialog.FileNames)
                {
                    //tạo 1 file mới
                    File file = new File();
                    //tách fullFileName thành name và path
                    file.Name = System.IO.Path.GetFileName(fullFileName);
                    file.Path = System.IO.Path.GetDirectoryName(fullFileName);
                    //add file mới tạo vào list
                    listFiles.Add(file);
                }
                    
            }
        }

        /// <summary>
        /// Hàm add folder vào list folders, thực hiện khi bấm nút add folder(s)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addFoldersButton_Click(object sender, RoutedEventArgs e)
        {
            //đường dẫn đầu tiên mà hộp thoại hiện lên
            folderBrowserDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //tạo folder mới
                Folder folder = new Folder();
        
                var fullName = folderBrowserDialog.SelectedPath;
                //tách fullname thành path và name
                folder.Path = System.IO.Path.GetDirectoryName(fullName);
                folder.Name = System.IO.Path.GetFileName(fullName);
                //add vào list folder
                listFolders.Add(folder);
            }
           
        }

        /// <summary>
        /// Hàm thực hiện tất cả các action trong list actions vào tất cả các files và folder trong listfiles và listfolders, thực hiện khi bấm nút start
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startButton_Click(object sender, RoutedEventArgs e)
        {
         
        }


        /// <summary>
        /// Hàm thực hiện việc lưu preset, thực hiện khi bấm nút save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void savePresetButton_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Hàm xóa preset được chọn từ presetComboBox, thực hiện khi bấm nút Del
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deletePresetButton_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Hàm sử dụng bộ preset, đưa các method trong preset vào list method, thực hiện khi bấm nút use
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void usePresetButton_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Hàm thực hiện việc config lại method, hiện dialog để config, thực hiện khi phải chuột vào 1 method trong listmethod và chọn edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var item = operationsListBox.SelectedItem as StringOperation;

            item.Config();
        }

        /// <summary>
        /// Hàm thực hiện các method vào các file, folder để hiện ra new name cho người dùng xem trước, thực hiện khi bấm nút review
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void reviewButton_Click(object sender, RoutedEventArgs e)
        {

        }

   

        /// <summary>
        /// Hàm xóa 1 action khỏi list actions, thực hiện khi người dùng bấm chuột phải vào 1 item trong list actions và chọn delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuDelItem_Click_1(object sender, RoutedEventArgs e)
        {
            var action = operationsListBox.SelectedItem as StringOperation;
            _actions.Remove(action);
        }

        /// <summary>
        /// Hàm thực hiện việc enable nút add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void prototypesComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (prototypesComboBox.SelectedItem != null)
            {
                addMethodButton.IsEnabled = true;
            }
            
        }
       
        /// <summary>
        /// Hàm thực hiện việc add method vừa được chọn từ combobox, hiện dialog để config
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addMethodButton_Click(object sender, RoutedEventArgs e)
        {
            var action = prototypesComboBox.SelectedItem as StringOperation;
            _actions.Add(action.Clone());
        }
    }

}
