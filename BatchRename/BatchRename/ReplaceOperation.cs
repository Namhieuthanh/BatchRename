using System.ComponentModel;
using PropertyChanged;
namespace BatchRename
{
    public class ReplaceArgs : StringArgs
    {
        public string From { get; set; }
        public string To { get; set; }
    }

    public class ReplaceOperation : StringOperation, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public override string Operate(string origin)
        {
            var args = Args as ReplaceArgs;
            var from = args.From;
            var to = args.To;

            return origin.Replace(from, to);
        }

        public override StringOperation Clone()
        {
            //tạo mới 1 replaceOperation, set from và to là ""
            var newReplaceOperation = new ReplaceOperation()
            {
                Args = new ReplaceArgs()
                {
                    From = "",
                    To = ""
                }
            };
            //hiện dialog để người dùng customize
            var screen = new ReplaceConfigDialog(newReplaceOperation.Args);
            if (screen.ShowDialog() == true)
            {

            }
            //lấy args sau khi người dùng config, nếu nó vẫn là from"" to "" thì không return newReplaceOperation
            //đó là trường hợp người dùng cancel
            var ArgsAfterConfiguration = newReplaceOperation.Args as ReplaceArgs;
            if (ArgsAfterConfiguration.From == "" && ArgsAfterConfiguration.To == "")
            {
                return null;
            }
            else
            {
                //trả về replaceOperation mà người dùng đã custom
                return newReplaceOperation;
            }
        }

        public override StringOperation Clone(string[] args)
        {
            if ((args.Length < 1) && (args.Length > 2))
            {
                return null;
            }
            //tạo mới 1 replaceOperation, tham số là các giá trị string truyền vào
            var newReplaceOperation = new ReplaceOperation();

            if (args.Length == 1)
            {
                newReplaceOperation.Args = new ReplaceArgs()
                {
                    From = args[0],
                    To = ""
                };
            }
            else
            {
                newReplaceOperation.Args = new ReplaceArgs()
                {
                    From = args[0],
                    To = args[1]
                };
            }

            //trả về replaceOperation mà người dùng đã custom
            return newReplaceOperation;
        }

        public override void Config()
        {
            var screen = new ReplaceConfigDialog(Args);
            if (screen.ShowDialog() == true)
            {

            }
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Description"));
            }
        }

        public override string Name => "Replace";
        public override string Description
        {
            get
            {
                var args = Args as ReplaceArgs;

                return $"Replace from {args.From} to {args.To}";
            }
        }
    }


}
