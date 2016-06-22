using EnvDTE;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.LanguageServices;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUML.ClassObjects
{
    [Serializable]
    class ClassCollection: Collection<MyClass>
    {
        public ClassCollection()
        {
            
            //Microsoft.CodeAnalysis.Document doc = GetActiveDocument();
            List<Microsoft.CodeAnalysis.Document> docs = GetDocuments();
            foreach (Microsoft.CodeAnalysis.Document doc in docs)
            {
                if (doc != null)
                {
                    Microsoft.CodeAnalysis.SyntaxTree syntaxTree;
                    doc.TryGetSyntaxTree(out syntaxTree);
                    SyntaxNode root = syntaxTree.GetRoot();
                    var children = root.ChildNodes();
                    var nameSpace = children.OfType<NamespaceDeclarationSyntax>().SingleOrDefault();
                    var classes = nameSpace.ChildNodes().OfType<ClassDeclarationSyntax>();

                    foreach (var c in classes)
                    {
                        MyClass myclass = new MyClass(c);
                        this.Add(myclass);
                    }
                }
            }
                       
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

        private List<Microsoft.CodeAnalysis.Document> GetDocuments()
        {
            List<Microsoft.CodeAnalysis.Document> documentList = new List<Microsoft.CodeAnalysis.Document>();

            var dte = Package.GetGlobalService(typeof(DTE)) as DTE;
            var documents = dte?.Documents;
            if (documents == null) return null;

            var componentModel = (IComponentModel)Package.GetGlobalService(typeof(SComponentModel));
            var workspace = (Workspace)componentModel.GetService<VisualStudioWorkspace>();

            foreach (EnvDTE.Document document in documents)
            {
                var documentid = workspace.CurrentSolution.GetDocumentIdsWithFilePath(document.FullName).FirstOrDefault();
                if (documentid != null)
                    documentList.Add(workspace.CurrentSolution.GetDocument(documentid));
            }
            return documentList;
        }
    }

}
