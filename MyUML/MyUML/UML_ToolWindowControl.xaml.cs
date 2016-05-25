//------------------------------------------------------------------------------
// <copyright file="UML_ToolWindowControl.xaml.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace MyUML
{
    using EnvDTE;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.Diagnostics;
    using Microsoft.VisualStudio.ComponentModelHost;
    using Microsoft.VisualStudio.LanguageServices;
    using Microsoft.VisualStudio.Shell;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for UML_ToolWindowControl.
    /// </summary>
    public partial class UML_ToolWindowControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UML_ToolWindowControl"/> class.
        /// </summary>
        public UML_ToolWindowControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.CodeAnalysis.Document doc = GetActiveDocument();
            Microsoft.CodeAnalysis.SyntaxTree syntaxTree;
            doc.TryGetSyntaxTree(out syntaxTree);
            SyntaxNode root = syntaxTree.GetRoot();
            var children = root.ChildNodes();
            var nameSpace = children.OfType<NamespaceDeclarationSyntax>().SingleOrDefault();
            var classes = nameSpace.ChildNodes().OfType<ClassDeclarationSyntax>();

            String output = "";
            
            foreach (var child in classes)
            {
                output = output + child.ToString() + " Typ:" + child.GetType().ToString() +  "\n\n";
            }
            
            MessageBox.Show(
                output,
                "Projektname: " + doc.Project.Name);
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