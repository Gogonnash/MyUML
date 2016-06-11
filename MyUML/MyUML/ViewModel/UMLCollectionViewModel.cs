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
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace MyUML.ViewModel
{
    class UMLCollectionViewModel : System.Collections.ObjectModel.Collection<UMLViewModel>
    {
        public ClassCollection classCol;

        public RelayCommand saveUMLCommand;
        public RelayCommand loadUMLCommand;
        public RelayCommand saveCanvasAsImage;

        public RelayCommand generateUMLCommand;
        public RelayCommand generateCodeCommand;

        private Action redrawDel;
        private Func<BitmapEncoder> getCanvasImage;

        public ICommand SaveUMLCommand { get { return saveUMLCommand; } }
        public ICommand LoadUMLCommand { get { return loadUMLCommand; } }
        public ICommand SaveCanvasAsImage { get { return saveCanvasAsImage; } }

        public ICommand GenerateUMLCommand { get { return generateUMLCommand; } }
        public ICommand GenerateCodeCommand { get { return generateCodeCommand; } }
        
        public UMLCollectionViewModel()
        {
            saveUMLCommand = new RelayCommand(SaveUML, ReturnTrue);
            loadUMLCommand = new RelayCommand(LoadUML, ReturnTrue);
            saveCanvasAsImage = new RelayCommand(SaveCanvasImage, ReturnTrue);

            generateUMLCommand = new RelayCommand(GenerateUML, ReturnTrue);
            generateCodeCommand = new RelayCommand(GenerateCode, ReturnTrue);
        }
        public Action RedrawDel
        {
            set {
                if(value!= null) this.redrawDel = value; }
            get { return this.redrawDel; }
        }
        public Func<BitmapEncoder> GetCanvasImage
        {
            set
            {
                if (value != null) this.getCanvasImage = value;
            }
            get { return this.getCanvasImage; }
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

        private void LoadUML()
        {
            // Displays an OpenFileDialog so the user can select a Cursor.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "All files (*.*)|*.*";
            openFileDialog1.Title = "Select a UML File";

            // Show the Dialog.
            // If the user clicked OK in the dialog and
            // a .CUR file was selected, open it.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            { 
                FileStream fs = (FileStream)openFileDialog1.OpenFile();
                BinaryFormatter bf = new BinaryFormatter();
                bf.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
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

        private void SaveUML()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.Title = "Save your UML";
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
                BinaryFormatter bf = new BinaryFormatter();
                bf.AssemblyFormat =   System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
                bf.Serialize(fs, classCol);
                fs.Close();
            }
        }

       private void SaveCanvasImage()
        {
            BitmapEncoder pngEncoder = this.GetCanvasImage();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "PNG Image|*.png";
            saveFileDialog1.Title = "Save as Image File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs =  (System.IO.FileStream)saveFileDialog1.OpenFile();
                pngEncoder.Save(fs);
                fs.Close();
            }
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
