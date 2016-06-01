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
    }
}