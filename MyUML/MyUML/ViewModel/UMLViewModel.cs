using MyUML.ClassObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUML.ViewModel
{
    class UMLViewModel
    {
        public MyClass MClass { get; set; }
        public UMLViewModel(MyClass c)
        {
            this.MClass = c;
        }

        //Class Attributes
        public String ClassName
        {
            get { return MClass.Name; }
            set
            {
                try
                {
                    MClass.Name = value;
                }
                catch (Exception ex) { System.Windows.MessageBox.Show(ex.Message); }
            }
        }
        public List<MyMethod> Methods
        {
            get { return MClass.MMethods; }
            set
            {
                try
                {
                    MClass.MMethods = value;
                }
                catch (Exception ex) { System.Windows.MessageBox.Show(ex.Message); }
            }
        }
        public List<MyAttribute> Attributes
        {
            get { return MClass.MAttributes; }
            set
            {
                try
                {
                    MClass.MAttributes = value;
                }
                catch (Exception ex) { System.Windows.MessageBox.Show(ex.Message); }
            }
        }
    }
}

