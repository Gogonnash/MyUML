using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUML.ClassObjects
{
    [Serializable]
    //hier Enums?
    class MyAttribute
    {
        private String name;
        private String type;
        private char modifier;

       // MyAttribute() { }

       /* MyAttribute(String name, String type)
        {
            this.name = name;
            this.type = type;
        }*/

        public String Name
        {
            get { return name; }
            set { name = value; }
        }
        public String Type
        {
            get { return type; }
            set { type = value; }
        }

        public char Modifier
        {
            get { return modifier; }
            set { modifier = value; }
        }

    }
}
