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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MyUML.ViewModel
{
    class UMLCollectionViewModel : System.Collections.ObjectModel.Collection<UMLViewModel>
    {
        public ClassCollection classCol;

        public RelayCommand saveCommand;
        public RelayCommand loadCommand;

        public RelayCommand generateUMLCommand;
        public RelayCommand generateCodeCommand;

        private Action redrawDel;
        private string NameToSave = "MyUML";

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
        }
        public Action RedrawDel
        {
            set {
                if(value!= null) this.redrawDel = value; }
            get { return this.redrawDel; }
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
            this.RedrawDel();
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
            //in this Case: 
            //C:\Users\...\Documents\Visual Studio 2012\Projects\Book-Manager-Projekt\Book-Manager\bin\Debug
            string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string path = dir + @"\" + NameToSave;
            if (File.Exists(path))
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                BinaryFormatter bf = new BinaryFormatter();
                ClassCollection cc = (ClassCollection)bf.Deserialize(fs);
                fs.Close();
                classCol.Clear();
                foreach (MyClass c in cc)
                {
                    classCol.Add(c);
                }
                FetchFromModels();
                this.RedrawDel();
            }

        }

        private void Save()
        {
            string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string path = dir + @"\"+ NameToSave;
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, classCol);
            fs.Close();
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
