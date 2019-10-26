using System.ComponentModel;
using PropertyChanged;
namespace BatchRename
{
    public class MoveArgs : StringArgs, INotifyPropertyChanged
    {
        public string From { get; set; }
        public string To { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    


}
