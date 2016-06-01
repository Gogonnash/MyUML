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
    class UMLViewModel
    {
        //public ClassCollection classCol;

        public RelayCommand saveCommand;
        public RelayCommand loadCommand;

        public RelayCommand generateUMLCommand;
        public RelayCommand generateCodeCommand;



        public ICommand SaveCommand { get { return saveCommand; } }
        public ICommand LoadCommand { get { return loadCommand; } }
        public ICommand GenerateUMLCommand { get { return generateUMLCommand; } }
        public ICommand GenerateCodeCommand { get { return generateCodeCommand; } }


        public UMLViewModel()
        {
            saveCommand = new RelayCommand(Save, ReturnTrue);
            loadCommand = new RelayCommand(Load, ReturnTrue);
            generateUMLCommand = new RelayCommand(GenerateUML, ReturnTrue);
            generateCodeCommand = new RelayCommand(GenerateCode, ReturnTrue);
            //classCol = new ClassCollection();

            //Class c = new Class();
            //classCol.Add(c);
        }

        private void GenerateCode()
        {
            throw new NotImplementedException();
        }

        private void GenerateUML()
        {
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
                        output = output + a.ToString() + "\n";
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
                        output = output + m.Identifier + "\n";
                    }
                output = output + " ____________\n\n";

            }

            MessageBox.Show(
                output,
                "Projektname: " + doc.Project.Name);
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
