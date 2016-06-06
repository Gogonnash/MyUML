using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUML.ClassObjects
{
    class MyClass
    {
        private String name;
        private List<MyMethod> mMethods;
        private List<MyAttribute> mAttributes;

        public MyClass()
        {
            mMethods = new List<MyMethod>();
            mAttributes = new List<MyAttribute>();
        }
        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public List<MyMethod> MMethods
        {
            get { return mMethods; }
            set { mMethods = value; }
        }

        public List<MyAttribute> MAttributes
        {
            get { return mAttributes; }
            set { mAttributes = value; }
        }

        public void addAttribute(String type, String name)
        {
            MyAttribute a = new MyAttribute();
            a.Name = name;
            a.Type = type;
            mAttributes.Add(a);
        }
        public void addMethod(String type, String name, List<String[]> param)
        {
            MyMethod m = new MyMethod();
            m.Name = name;
            m.ReturnType = type;
            m.Parameter = param;
            mMethods.Add(m);
        }
    }
}
