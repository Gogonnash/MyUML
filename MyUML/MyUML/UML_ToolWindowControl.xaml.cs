//------------------------------------------------------------------------------
// <copyright file="UML_ToolWindowControl.xaml.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyUML
{
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


       /* #region PreviewMouseRightButtonDown

        void PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            // If the user right-clicks while dragging an element, assume that they want 
            // to manipulate the z-index of the element being dragged (even if it is  
            // behind another element at the time).
            if (this.dragCanvas.ElementBeingDragged != null)
                this.elementForContextMenu = this.dragCanvas.ElementBeingDragged;
            else
                this.elementForContextMenu =
                    this.dragCanvas.FindCanvasChild(e.Source as DependencyObject);
        }

        #endregion // Window1_PreviewMouseRightButtonDown
        */
    }
}