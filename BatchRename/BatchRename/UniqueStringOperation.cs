using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class UniqueStringOperation : StringOperation, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public override string Operate(string origin)
        {
            origin = Guid.NewGuid().ToString();
            return origin;
        }

        public override StringOperation Clone()
        {
            //tạo mới 1 uniqueStringOperation
            var newUniqueStringOperation = new UniqueStringOperation()
            {
                Args = new StringArgs()
                {
                }
            };

            return newUniqueStringOperation;
        }

        public override void Config()
        {
        }

        public override string Name => "Unique Name";
        public override string Description => "Create a unique name for file(s)";
    }


}


