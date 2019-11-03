using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{

    public class FullNameOperation : StringOperation
    {
        public override string Name => "FullName Style";

        public override string Description
        {
            get
            {
                var args = Args as StringArgs;
                return $"Normalize Args To FullName Style";
            }
        }

        public override StringOperation Clone()
        {
            var newFullNameOperation = new FullNameOperation()
            {
                Args = new StringArgs()
                {
                }
            };

            return newFullNameOperation;
        }

        public override StringOperation Clone(string[] args)
        {
            return Clone();
        }

        public override void Config()
        {
        }

        //Xac dinh theo phia truoc cua no la ki tu gi de dinh dang ban than no
        //Su dung ham Trim de loai bo " " o dau va cuoi
        //Tac dung: Loai bo dau " " thua, viet hoa chu dau moi tu, viet thuong cac chu con lai
        public override string Operate(string origin)
        {
            string output = "";
            origin = origin.Trim();

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
                    if (origin[i - 1] == ' ')
                    {
                    }
                    else
                    {
                        output += origin[i];
                    }
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

        public override string PresetSaver()
        {
            return "";
        }
    }
}
