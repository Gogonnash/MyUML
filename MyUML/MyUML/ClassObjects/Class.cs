using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUML.ClassObjects
{
    [Serializable]
    class Class
    {
        private IEnumerable<MethodDeclarationSyntax> method;
        private IEnumerable<FieldDeclarationSyntax> attribute;
        private String className;


        
        public String ClassName
        {
            set
            {
                if (value != null)
                {
                    this.className = value;
                }
            }
            get
            {
                return this.className;
            }
        }

        public IEnumerable<MethodDeclarationSyntax> Method
        {
            set
            {
                if (value != null)
                {
                    this.method = value; 
                }
            }
            get
            {
                return this.method;
            }
        }

        public IEnumerable<FieldDeclarationSyntax> Attribute
        {
            set
            {
                if (value != null)
                {
                    this.attribute = value;
                }
            }
            get
            {
                return this.attribute;
            }
        }
    }
}
