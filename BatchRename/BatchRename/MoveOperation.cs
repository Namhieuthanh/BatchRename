using System.ComponentModel;
using PropertyChanged;
namespace BatchRename
{
    public class MoveArgs : StringArgs
    {
        public int Size { get; set; }
        public int Type { get; set; } //0: chuyển từ đầu sang cuối; 1: chuyển từ cuối lên đầu
    }


    public class MoveOperation : StringOperation, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public override string Operate(string origin)
        {
            var res = "";

            var args = Args as MoveArgs;
            var size = args.Size;
            var type = args.Type;

            if (size > origin.Length)
            {
                return "Error";
            }

            if (type == 0)
            {
                res = origin.Substring(size) + origin.Substring(0, size);
            }
            else if (type == 1)
            {
                res = origin.Substring(origin.Length - size) + origin.Substring(0, origin.Length - size);
            }
            return res;
        }

        public override StringOperation Clone()
        {
            //tạo mới 1 moveOperation trống
            var newMoveOperation = new MoveOperation()
            {
                Args = new MoveArgs()
                {
                    Size = 0,
                    Type = 0
                }
            };
            //hiện dialog để người dùng customize
            var screen = new MoveConfigDialog(newMoveOperation.Args);
            if (screen.ShowDialog() == true)
            {

            }
            //lấy args sau khi người dùng config, nếu size vẫn là 0  thì không return newMoveOperation
            //đó là trường hợp người dùng cancel
            var ArgsAfterConfiguration = newMoveOperation.Args as MoveArgs;
            if (ArgsAfterConfiguration.Size == 0)
            {
                return null;
            }
            else
            {
                //trả về moveOperation mà người dùng đã custom
                return newMoveOperation;
            }
        }

        public override StringOperation Clone(string[] args)
        {
            if (args.Length != 2)
            {
                return null;
            }
            //tạo mới 1 moveOperation, tham số là các giá trị string truyền vào
            var newMoveOperation = new MoveOperation();

            newMoveOperation.Args = new MoveArgs()
            {
                Size = int.Parse(args[0]),
                Type = int.Parse(args[1])
            };


            //trả về replaceOperation mà người dùng đã custom
            return newMoveOperation;
        }

        public override void Config()
        {
            var screen = new MoveConfigDialog(Args);
            if (screen.ShowDialog() == true)
            {

            }
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Description"));
            }
        }

        public override string Name => "Move";
        public override string Description
        {
            get
            {
                var args = Args as MoveArgs;
                var res = "";
                if (args.Type == 0)
                {
                    res = $"Move {args.Size} character(s) from begin to end";
                }
                if (args.Type == 1)
                {
                    res = $"Move {args.Size} character(s) from end to begin";
                }
                return res;
            }
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Description));

        }

        public override string PresetSaver()
        {
            string result = Name;
            var args = Args as MoveArgs;

            result = result + "/" + args.Size.ToString() + "/" + args.Type.ToString();
            return result;
        }
    }
}
