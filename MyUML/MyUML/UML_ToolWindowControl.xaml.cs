//------------------------------------------------------------------------------
// <copyright file="UML_ToolWindowControl.xaml.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using MyUML.ClassObjects;
using MyUML.ViewModel;
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

        UMLCollectionViewModel ucvm;

        /// <summary>
        /// Initializes a new instance of the <see cref="UML_ToolWindowControl"/> class.
        /// </summary>
        public UML_ToolWindowControl()
        {
            this.InitializeComponent();
            ucvm = this.FindResource("myView") as UMLCollectionViewModel;
            if(ucvm != null)
            this.ucvm.RedrawDel = this.generateUI;
        }

        public void generateUI()
        {
            double Left = 0; double Top = 0; double BiggestHeight = 0;
            DataContext = ucvm;
            foreach (UMLViewModel u in ucvm  )
            {             
                Border b = new Border(); b.Background = new SolidColorBrush(Colors.White); b.BorderThickness = new Thickness(1); b.Margin = new Thickness(5); b.BorderBrush = new SolidColorBrush(Colors.Black);
                StackPanel sp = new StackPanel();
                b.Child = sp;
                TextBlock ClassName = new TextBlock();
                Binding ClassNameBinding = new Binding("ClassName");
                ClassNameBinding.Source = u;
                ClassName.SetBinding(TextBlock.TextProperty, ClassNameBinding);
                sp.Children.Add(ClassName);
                ListBox AttributeListBox = new ListBox();
                AttributeListBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                foreach (MyAttribute ma in u.Attributes)
                {
                    TextBlock Attribute = new TextBlock();
                    Run AttributeName = new Run();                                   
                    Binding AttributeNameBinding = new Binding("Name");
                    AttributeNameBinding.Source = ma;
                    AttributeName.SetBinding(Run.TextProperty, AttributeNameBinding);
                    Attribute.Inlines.Add(AttributeName);
                    Run Noun = new Run(" : ");
                    Attribute.Inlines.Add(Noun);
                    Run AttributeType = new Run();
                    Binding AttributeTypeBinding = new Binding("Type");
                    AttributeTypeBinding.Source = ma;
                    AttributeType.SetBinding(Run.TextProperty, AttributeTypeBinding);
                    Attribute.Inlines.Add(AttributeType);
                    AttributeListBox.Items.Add(Attribute);
                }
                sp.Children.Add(AttributeListBox);

                ListBox MethodListBox = new ListBox();
                MethodListBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                foreach (MyMethod mm in u.Methods)
                {
                    TextBlock Method = new TextBlock();
                    Run MethodType = new Run();
                    Binding MethodTypeBinding = new Binding("ReturnType");
                    MethodTypeBinding.Source = mm;
                    MethodType.SetBinding(Run.TextProperty, MethodTypeBinding);
                    Method.Inlines.Add(MethodType);
                    Run Space = new Run(" ");
                    Method.Inlines.Add(Space);
                    Run MethodName = new Run();
                    Binding MethodeNameBinding = new Binding("Name");
                    MethodeNameBinding.Source = mm;
                    MethodName.SetBinding(Run.TextProperty, MethodeNameBinding);
                    Method.Inlines.Add(MethodName);
                    Run openBrace = new Run("(");
                    Method.Inlines.Add(openBrace);
                    foreach (String[] s in mm.Parameter)
                    {
                        Run ParameterType = new Run();
                        Binding ParameterTypeBinding = new Binding("[0]");
                        ParameterTypeBinding.Source = s;
                        ParameterType.SetBinding(Run.TextProperty, ParameterTypeBinding);
                        Method.Inlines.Add(ParameterType);
                        Run Noun = new Run(" : ");
                        Method.Inlines.Add(Noun);
                        Run ParameterName = new Run();
                        Binding ParameterNameBinding = new Binding("[1]");
                        ParameterNameBinding.Source = s ;
                        ParameterName.SetBinding(Run.TextProperty, ParameterNameBinding);
                        Method.Inlines.Add(ParameterName);
                    }
                    Run closedBrace = new Run(")");
                    Method.Inlines.Add(closedBrace);
                    MethodListBox.Items.Add(Method);
                    
                }
                sp.Children.Add(MethodListBox);

                this.dragCanvas.Children.Add(b);

                //get actual size
                //System.Diagnostics.Debug.Write(b.ActualWidth + " " + sp.ActualWidth + " "+ ClassName.ActualWidth + " " +AttributeListBox.ActualWidth + "  " +dragCanvas.ActualWidth    );
                
                int generalSize = 100;
                Canvas.SetLeft(b as UIElement, Left);
                Canvas.SetTop(b as UIElement, Top);
                Left += generalSize;
                if (b.ActualHeight > BiggestHeight)
                    BiggestHeight = Height;
                if(Left>this.ActualWidth)
                {
                    Top += generalSize;
                    Left = 0;
                }
            }
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