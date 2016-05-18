﻿//------------------------------------------------------------------------------
// <copyright file="UML_ToolWindow.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace MyUML
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.Shell;

    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    /// </summary>
    /// <remarks>
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane,
    /// usually implemented by the package implementer.
    /// <para>
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its
    /// implementation of the IVsUIElementPane interface.
    /// </para>
    /// </remarks>
    [Guid("4d632711-0c15-434a-b580-faf644a27a5f")]
    public class UML_ToolWindow : ToolWindowPane
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UML_ToolWindow"/> class.
        /// </summary>
        public UML_ToolWindow() : base(null)
        {
            this.Caption = "UML_ToolWindow";

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
            // the object returned by the Content property.
            this.Content = new UML_ToolWindowControl();
        }
    }
}
