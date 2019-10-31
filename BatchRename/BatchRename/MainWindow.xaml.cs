using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
        //list chứa các prototypes mà mình có
        List<StringOperation> _prototypes = new List<StringOperation>();
        //list chứa các actions được add 
        BindingList<StringOperation> _actions = new BindingList<StringOperation>();
        //list chứa các presets mà mình có
        BindingList<String> _presets = new BindingList<string>();
        //đường dẫn đến thư mục chứa presets
        string path = AppDomain.CurrentDomain.BaseDirectory+"Presets";
        //chuỗi hằng: ký tự phân tách
        const string Seperator = "/";

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
            var prototype3 = new UniqueStringOperation();
            //add vào list prototypes
            _prototypes.Add(prototype1);
            _prototypes.Add(prototype2);
            _prototypes.Add(prototype3);
            //load các presets mà mình có
            Directory.CreateDirectory(path);
            var PresetsLocation = new DirectoryInfo(path);
            FileInfo[] files = PresetsLocation.GetFiles("*.txt");
            foreach(var file in files)
            {
                _presets.Add(System.IO.Path.GetFileNameWithoutExtension(file.Name));
            }
            //set source cho prototypesComboBox, filesListView, folderListView, presetsCombobox và operationListBox
            prototypesComboBox.ItemsSource = _prototypes;
            operationsListBox.ItemsSource = _actions;
            filesListView.ItemsSource = listFiles;
            foldersListView.ItemsSource = listFolders;
            presetsComboBox.ItemsSource = _presets;
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
                    //tách fullFileName thành name, extension và path
                    file.Name = System.IO.Path.GetFileNameWithoutExtension(fullFileName);
                    file.Extension = System.IO.Path.GetExtension(fullFileName);
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
            if (listFiles.Count == 0 && listFolders.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("Please add file(s)/folder(s)!");
            }
            else
            {
                DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Are you sure?",
               "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                {
                    for (int iFile = 0; iFile < listFiles.Count; iFile++)
                    {
                        for (int iAction = 0; iAction < _actions.Count; iAction++)
                        {
                            listFiles[iFile].NewName = _actions[iAction].Operate(listFiles[iFile].Name);
                        }
                    }
                    for (int iFolder = 0; iFolder < listFolders.Count; iFolder++)
                    {
                        for (int iAction = 0; iAction < _actions.Count; iAction++)
                        {
                            listFolders[iFolder].NewName = _actions[iAction].Operate(listFolders[iFolder].Name);
                        }
                    }
                    for (int iFile = 0; iFile < listFiles.Count; iFile++)
                    {
                        string source = listFiles[iFile].Path + "\\" + listFiles[iFile].Name
                            + listFiles[iFile].Extension;
                        string dest = listFiles[iFile].Path + "\\" + listFiles[iFile].NewName
                            + listFiles[iFile].Extension;
                        if (System.IO.File.Exists(dest))
                        {
                            string newName;
                            int count = 0;
                            do
                            {
                                count++;
                                newName = string.Format("{0}({1}){2}",
                                listFiles[iFile].Path + "\\" + listFiles[iFile].NewName, count, listFiles[iFile].Extension);
                            }
                            while (System.IO.File.Exists(newName));
                            System.IO.File.Move(source, newName);
                        }
                        else
                        {
                            System.IO.File.Move(source, dest);
                        }

                    }
                    for (int iFolder = 0; iFolder < listFolders.Count; iFolder++)
                    {
                        string source = listFolders[iFolder].Path + "\\" + listFolders[iFolder].Name;
                        string dest = listFolders[iFolder].Path + "\\" + listFolders[iFolder].NewName;
                        if (System.IO.Directory.Exists(dest))
                        {
                            string newName;
                            int count = 0;
                            do
                            {
                                count++;
                                newName = string.Format("{0}({1})",
                                    listFolders[iFolder].Path + "\\" + listFolders[iFolder].NewName, count);
                            }
                            while (System.IO.Directory.Exists(newName));
                            System.IO.Directory.Move(source, newName);
                        }
                        else
                        {
                            System.IO.Directory.Move(source, dest);
                        }
                    }
                    listFolders.Clear();
                    listFiles.Clear();
                    System.Windows.Forms.MessageBox.Show("Done");
                }
            } 
        }

        /// <summary>
        /// Hàm thực hiện việc lưu preset, thực hiện khi bấm nút save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void savePresetButton_Click(object sender, RoutedEventArgs e)
        {
            string presetName = "";
            var screen = new PresetNameConfigDialog(presetName);
            screen.ShowDialog();
            if (presetName != "")
            {
                string filePath = path + "\\" + presetName + ".txt";
                string[] lines = new string[_actions.Count];

                for (int i = 0; i < _actions.Count; i++)
                {
                    lines[i] = _actions[i].PresetSaver();
                }

                System.IO.File.WriteAllLines(filePath, lines);
            }
        }

        /// <summary>
        /// Hàm xóa preset được chọn từ presetComboBox, thực hiện khi bấm nút Del
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deletePresetButton_Click(object sender, RoutedEventArgs e)
        {
            var delPreset = presetsComboBox.SelectedItem as String;
            if (delPreset != null)
            {
                DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Are you sure?",
               "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                {
                    _presets.Remove(delPreset);
                    System.IO.File.Delete(path + "\\" + delPreset + ".txt");
                }
            }
           
        }

        /// <summary>
        /// Hàm sử dụng bộ preset, đưa các method trong preset vào list method, thực hiện khi bấm nút use
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void usePresetButton_Click(object sender, RoutedEventArgs e)
        {                      
            var usedPreset = presetsComboBox.SelectedItem as String;
            if (usedPreset != null)
            {
                _actions.Clear();

                var lines = System.IO.File.ReadAllLines(path + "\\" + usedPreset + ".txt");
                foreach (var line in lines)
                {
                    String[] tokens = line.Split(new string[] { Seperator }, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < _prototypes.Count; i++)
                    {
                        if (_prototypes[i].Name == tokens[0])
                        {

                            string[] args = new string[tokens.Length - 1];

                            Array.Copy(tokens, 1, args, 0, tokens.Length - 1);
                            _actions.Add(_prototypes[i].Clone(args));
                        }
                    }
                }
            }
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
        private void previewButton_Click(object sender, RoutedEventArgs e)
        {
            if(listFiles.Count==0 && listFolders.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("Please add file(s)/folder(s)!");
            }
            else
            {
                for (int iFile = 0; iFile < listFiles.Count; iFile++)
                {
                    for (int iAction = 0; iAction < _actions.Count; iAction++)
                    {
                        listFiles[iFile].NewName = _actions[iAction].Operate(listFiles[iFile].Name);
                    }
                }
                for (int iFolder = 0; iFolder < listFolders.Count; iFolder++)
                {
                    for (int iAction = 0; iAction < _actions.Count; iAction++)
                    {
                        listFolders[iFolder].NewName = _actions[iAction].Operate(listFolders[iFolder].Name);
                    }
                }
            }
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
