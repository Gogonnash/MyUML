using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUML.ClassObjects
{
    [Serializable]
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

        public MyClass(ClassDeclarationSyntax myclass)
        {
            // Listen initialisieren
            mMethods = new List<MyMethod>();
            mAttributes = new List<MyAttribute>();

            // Klassen-Name
            this.name = myclass.Identifier.ToString();

            // Attribute
            var attributes = myclass.ChildNodes().OfType<FieldDeclarationSyntax>();
            if (attributes.Count<FieldDeclarationSyntax>() != 0)
            {
                foreach (var a in attributes)
                {
                    // Standard: protected
                    char modifier = '#';
                    var modifierList = a.Modifiers;
                    foreach (var mod in modifierList)
                    {
                        if (mod.ToString().ToLower().Equals("private"))
                        {
                            modifier = '-';
                        }
                        if (mod.ToString().ToLower().Equals("public"))
                        {
                            modifier = '+';
                        }
                    }

                    String varType = a.ChildNodes().OfType<VariableDeclarationSyntax>().First().ChildNodes().ElementAt(0).ToString();
                    String varName = a.ChildNodes().OfType<VariableDeclarationSyntax>().First().ChildNodes().ElementAt(1).ToString();
                    // Falls Variable initalisiert wurde, alles ab dem "="-Zeichen entfernen
                    if (varName.IndexOf("=")!= -1)
                    {
                        varName = varName.Remove(varName.IndexOf("="));
                    }
                    this.addAttribute(modifier, varType, varName);
                }                
            }

            //Methoden
            var methods = myclass.ChildNodes().OfType<MethodDeclarationSyntax>();
            if (methods.Count<MethodDeclarationSyntax>() != 0)
            {
                foreach (var m in methods)
                {
                    // Standard: protected
                    char modifier = '#';
                    var modifierList = m.Modifiers;
                    foreach (var mod in modifierList)
                    {
                        if (mod.ToString().ToLower().Equals("private"))
                        {
                            modifier = '-';
                        }
                        if (mod.ToString().ToLower().Equals("public"))
                        {
                            modifier = '+';
                        }
                    }

                    // Methoden-Name
                    String methodName = m.Identifier.ToString();

                    // Methoden Rückgabetyp
                    String returnType = m.ReturnType.ToString();

                    // Methoden-Parameter
                    var methodParams = m.ParameterList.ChildNodes().OfType<ParameterSyntax>();
                    List<String[]> paramList = new List<String[]>();
                    if (methodParams.Count() != 0)
                    {
                        foreach(var p in methodParams)
                        {
                            String paramName = p.Identifier.ToString();
                            String paramType = p.ChildNodes().First().ToString();
                            paramList.Add(new String[] { paramName , paramType });
                        }
                    }
                    this.addMethod(modifier, returnType, methodName, paramList);

                }
            }
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

        public void addAttribute(char modifier, String type, String name)
        {
            MyAttribute a = new MyAttribute();
            a.Name = name;
            a.Type = type;
            a.Modifier = modifier;
            mAttributes.Add(a);
        }
        public void addMethod(char modifier, String type, String name, List<String[]> param)
        {
            MyMethod m = new MyMethod();
            m.Name = name;
            m.ReturnType = type;
            m.Parameter = param;
            m.Modifier = modifier;
            mMethods.Add(m);
        }
    }
}
