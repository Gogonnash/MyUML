using MyUML.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using EnvDTE;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.LanguageServices;
using Microsoft.VisualStudio.Shell;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using MyUML.ClassObjects;

namespace MyUML.ViewModel
{
    class UMLCollectionViewModel : System.Collections.ObjectModel.Collection<UMLViewModel>
    {
        public ClassCollection classCol;

        public RelayCommand saveCommand;
        public RelayCommand loadCommand;

        public RelayCommand generateUMLCommand;
        public RelayCommand generateCodeCommand;



        public ICommand SaveCommand { get { return saveCommand; } }
        public ICommand LoadCommand { get { return loadCommand; } }
        public ICommand GenerateUMLCommand { get { return generateUMLCommand; } }
        public ICommand GenerateCodeCommand { get { return generateCodeCommand; } }
        
        public UMLCollectionViewModel()
        {
            saveCommand = new RelayCommand(Save, ReturnTrue);
            loadCommand = new RelayCommand(Load, ReturnTrue);
            generateUMLCommand = new RelayCommand(GenerateUML, ReturnTrue);
            generateCodeCommand = new RelayCommand(GenerateCode, ReturnTrue);

            /*
            try
            {
                //fake UML Load
                MyClass c = new MyClass();
                c.Name = "meineKlasse";
                List<String[]> l = new List<String[]>();
                String[] param = new String[2];
                param[0] = "type1";
                param[1] = "name1";
                l.Add(param);
                c.addMethod("void", "MyMethod", l);
                c.addMethod("double", "AnotherMethod", l);
                c.addAttribute("string", "hallo");
                classCol = new ClassCollection();
                classCol.Add(c);

                MyClass c2 = new MyClass();
                c2.Name = "meineKlasse2";
                List<String[]> l2 = new List<String[]>();
                String[] param2 = new String[2];
                param2[0] = "type2";
                param2[1] = "name2";
                l2.Add(param2);
                c2.addMethod("void", "MyMethod2", l);
                c2.addMethod("double", "AnotherMethod2", l);
                c2.addAttribute("string", "welt");
                classCol.Add(c2);


                MyClass c3 = new MyClass();
                c3.Name = "meineKlasse3";
                List<String[]> l3 = new List<String[]>();
                String[] param3 = new String[2];
                param3[0] = "type3";
                param3[1] = "name3";
                l3.Add(param3);
                c3.addMethod("void", "MyMethod3", l);
                classCol.Add(c3);

                FetchFromModels();
            }
            catch (Exception c)
            {
                System.Diagnostics.Debug.Write(c.Message);
            }
            */
        }

        public void FetchFromModels()
        { //A Bookviewmodel for each book is added

            foreach (MyClass c in classCol)
            {
                this.Add(new UMLViewModel(c));
            }
        
        }

        private void GenerateCode()
        {
            throw new NotImplementedException();
        }


        private void GenerateUML()
        {
            classCol = new ClassCollection();
            FetchFromModels();
            /*
            Microsoft.CodeAnalysis.Document doc = GetActiveDocument();
            Microsoft.CodeAnalysis.SyntaxTree syntaxTree;
            doc.TryGetSyntaxTree(out syntaxTree);
            SyntaxNode root = syntaxTree.GetRoot();
            var children = root.ChildNodes();
            var nameSpace = children.OfType<NamespaceDeclarationSyntax>().SingleOrDefault();
            var classes = nameSpace.ChildNodes().OfType<ClassDeclarationSyntax>();

            String output = "Klassen:\n\n";

            foreach (var c in classes)
            {
                // Klassennamen auflisten
                output = output + c.Identifier + "\n";

                // Atribute auflisten
                output = output + "Attribute:\n";
                var attributes = c.ChildNodes().OfType<FieldDeclarationSyntax>();
                if (attributes.Count<FieldDeclarationSyntax>() == 0)
                    output = output + " -- keine Attribute --\n";
                else
                    foreach (var a in attributes)
                    {
                        String varName = a.ChildNodes().OfType<VariableDeclarationSyntax>().First().ChildNodes().ElementAt(1).ToString();
                        String varType = a.ChildNodes().OfType<VariableDeclarationSyntax>().First().ChildNodes().ElementAt(0).ToString();
                        output = output + varName + " : " + varType +  "\n";
                    }
                output = output + "\n";

                // Methoden auflisten
                output = output + "Methoden:\n";
                var methods = c.ChildNodes().OfType<MethodDeclarationSyntax>();
                if (methods.Count<MethodDeclarationSyntax>() == 0)
                    output = output + " -- keine Methoden --\n";
                else
                    foreach (var m in methods)
                    {
                        String methodName = m.Identifier.ToString();                        
                        var methodParams = m.ParameterList.ChildNodes().OfType<ParameterSyntax>();
                        output = output + methodName;
                        if (methodParams.Count() != 0)
                        {
                            output = output + " (";
                            var lastParam = methodParams.Last();
                            foreach (var p in methodParams)
                            {
                                String paramName = p.Identifier.ToString();
                                String paramType = p.ChildNodes().First().ToString();
                                if (p != lastParam)
                                    output = output + paramName + ":" + paramType + ",";
                                else
                                    output = output + paramName + ":" + paramType;                                                  

                            }
                            output = output + ")\n";
                        }
                        else
                        {
                            output = output + "()\n";
                        }
                        
                    }
                output = output + " ____________\n\n";

            }
            
            MessageBox.Show(
                output,
                "Projektname: " + doc.Project.Name);
            */
        }

        private void Load()
        {
            throw new NotImplementedException();

         }

        private void Save()
        {
            throw new NotImplementedException();
        }

        public Boolean ReturnTrue()
        { //Commands can always be done
            return true;
        }


        private Microsoft.CodeAnalysis.Document GetActiveDocument()
        {
            var dte = Package.GetGlobalService(typeof(DTE)) as DTE;
            var activeDocument = dte?.ActiveDocument;
            if (activeDocument == null) return null;

            var componentModel = (IComponentModel)Package.GetGlobalService(typeof(SComponentModel));
            var workspace = (Workspace)componentModel.GetService<VisualStudioWorkspace>();

            var documentid = workspace.CurrentSolution.GetDocumentIdsWithFilePath(activeDocument.FullName).FirstOrDefault();
            if (documentid == null) return null;

            return workspace.CurrentSolution.GetDocument(documentid);
        }
    }



}
