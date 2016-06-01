using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUML.ClassObjects
{
    class MyAttribute
    {
        String name;
        String type;

        MyAttribute() { }

        MyAttribute(String name, String type)
        {
            this.name = name;
            this.type = type;
        }
    }
}
