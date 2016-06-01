using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUML.ClassObjects
{
    [Serializable]
    class ClassCollection: Collection<Class>
    {
        public ClassCollection()
        {

        }
    }
}
