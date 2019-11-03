using System.ComponentModel;
using PropertyChanged;
namespace BatchRename
{
    public class NewCaseArgs : StringArgs
    {
        public string Style { get; set; }
    }

    public class NewCaseOperation : StringOperation, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public override string Name => "NewCase";
        // Sau khi get Style ta duoc cac chuoi "0";"1";"2" theo vi tri cua cac Style trong method NewCase
        public override string Description
        {
            get
            {
                var args = Args as NewCaseArgs;
                var res = "";
                if (args.Style == "0")
                {
                    res = $"Change Style of the name to: Upper";
                }
                if (args.Style == "1")
                {
                    res = $"Change Style of the name to: Lower";
                }
                if (args.Style == "2")
                {
                    res = $"Change Style of the name to: FirstUpper";
                }
                return res;
            }
        }

        public override StringOperation Clone()
        {
            var newNewCaseOperation = new NewCaseOperation()
            {
                Args = new NewCaseArgs()
                {
                    Style = ""
                }
            };
            var screen = new NewCaseConfigDialog(newNewCaseOperation.Args);
            if (screen.ShowDialog() == true)
            {

            }
            var ArgsAfterConfiguration = newNewCaseOperation.Args as NewCaseArgs;
            if (ArgsAfterConfiguration.Style == "")
            {
                return null;
            }
            else
            {
                return newNewCaseOperation;
            }
        }

        public override StringOperation Clone(string[] args)
        {
            var newNewCaseOperation = new NewCaseOperation();

            newNewCaseOperation.Args = new NewCaseArgs()
            {
                Style = args[0].ToString()
            };
            return newNewCaseOperation;
        }

        public override void Config()
        {
            var screen = new NewCaseConfigDialog(Args);
            if (screen.ShowDialog() == true)
            {

            }
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Description"));
            }
        }
        //Ham co tac dung dieu chinh font theo lua chon
        //FirstUpper: Hoat dong giong FullName nhung bo di chuc nang loai bo ki tu " "
        public override string Operate(string origin)
        {
            var args = Args as NewCaseArgs;
            var style = args.Style;

            if (style == "0")
            {
                return origin.ToUpper();
            }
            if (style == "1")
            {
                return origin.ToLower();
            }
            if (style == "2")
            {
                string output = "";
                if (origin[0] >= 'a' && origin[0] <= 'z')
                {
                    output += (char)(origin[0] - 'a' + 'A');
                }
                else
                {
                    output += origin[0];
                }

                for (int i = 1; i < origin.Length; i++)
                {
                    if (origin[i] == ' ')
                    {
                        output += origin[i];
                    }

                    if (origin[i] >= 'a' && origin[i] <= 'z')
                    {
                        if (origin[i - 1] == ' ')
                        {
                            output += (char)(origin[i] - 'a' + 'A');
                        }
                        else
                        {
                            output += origin[i];
                        }
                    }

                    if (origin[i] >= 'A' && origin[i] <= 'Z')
                    {
                        if (origin[i - 1] == ' ')
                        {
                            output += origin[i];
                        }
                        else
                        {
                            output += (char)(origin[i] + 'a' - 'A');
                        }
                    }
                }
                return output;
            }
            return origin;
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
