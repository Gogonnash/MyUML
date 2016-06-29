using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUML.ClassObjects
{
    [Serializable]
    class MyMethod
    {
        private  String name;
        private String returnType;
        private List<String[]> parameter;
        private char modifier;

        public MyMethod() {
            parameter = new List<String[]>();
        }

        
        public String Name
        {
            get { return name; }
            set { name = value; }
        }
        public String ReturnType
        {
            get { return returnType; }
            set { returnType = value; }
        }

        public List<String[]> Parameter
        {
            get { return parameter; }
            set { parameter = value; }
        }

        public char Modifier
        {
            get { return modifier;  }
            set { modifier = value; }
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
