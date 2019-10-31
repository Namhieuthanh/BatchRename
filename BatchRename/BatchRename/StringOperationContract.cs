using System.ComponentModel;
using PropertyChanged;
namespace BatchRename
{
    public class StringArgs
    {

    }

    public abstract class StringOperation : INotifyPropertyChanged
    {
        public StringArgs Args { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// thực hiện nghiệp vụ đổi tên ở đây
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        public abstract string Operate(string origin);

        public abstract string Name { get; }
        public abstract string Description { get; }
        /// <summary>
        /// Clone ra 1 operation từ mẫu prototype, hiện dialog để người dùng customize
        /// </summary>
        /// <returns></returns>
        public abstract StringOperation Clone();
        /// <summary>
        /// Clone ra 1 operation từ mẫu prototype, các tham số được truyền vào từ mảng string
        /// </summary>
        /// <returns></returns>
        public abstract StringOperation Clone(string[] args);
        /// <summary>
        /// Người dùng chọn edit, hiện dialog để người dùng config lại operation
        /// </summary>
        public abstract void Config();
    }
}
