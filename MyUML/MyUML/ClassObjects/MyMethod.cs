using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUML.ClassObjects
{
    class MyMethod
    {
        String name;
        String returnType;
        List<String[]> parameter;

        MyMethod() { }

        MyMethod(String name)
        {
            this.name = name;
        }

        MyMethod(String name, String returnType)
        {
            this.name = name;
            this.returnType = returnType;
        }

        public void addParameter(String type, String name)
        {
            String[] param = new String[2];
            param[0] = type;
            param[1] = name;

            parameter.Add(param);
        }
    }
}
