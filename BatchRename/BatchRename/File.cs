using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    class File: INotifyPropertyChanged
    {
        private string _newName;
        public event PropertyChangedEventHandler PropertyChanged;
        public string Name { get; set; }
        public string NewName {
            get { return _newName; }
            set {
                _newName = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("NewName"));
                }
            } 
        }
        public string Path { get; set; }
        public string Error { get; set; }
    }
     
  
}
