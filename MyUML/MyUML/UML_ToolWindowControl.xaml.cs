//------------------------------------------------------------------------------
// <copyright file="UML_ToolWindowControl.xaml.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using MyUML.ClassObjects;
using MyUML.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MyUML
{
    /// <summary>
    /// Interaction logic for UML_ToolWindowControl.
    /// </summary>
    public partial class UML_ToolWindowControl : UserControl// ContentControl  needed to clear Canvas???
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
            this.ucvm.GetCanvasImage = this.GetCanvasImage;
        }

        private void generateUI()
        {
            this.dragCanvas.Children.Clear();
            dragCanvas.UpdateLayout();

            double Left = 5; double Top = 5; double BiggestHeight = 0;
            DataContext = ucvm;
            
            foreach (UMLViewModel u in ucvm  )
            {  
                Border b = new Border();
                    b.Background = new SolidColorBrush(Colors.White);
                    b.BorderThickness = new Thickness(1);
                    b.Margin = new Thickness(5);
                    b.BorderBrush = new SolidColorBrush(Colors.Black);
                    b.Width = Double.NaN; b.Height = Double.NaN;
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
                b.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                b.Arrange(new Rect(0, 0, b.DesiredSize.Width, b.DesiredSize.Height));
                System.Diagnostics.Debug.Write( "  " +b.ActualWidth + " " + b.ActualHeight);
                //Arrange
                Canvas.SetLeft(b, Left);
                Canvas.SetTop(b, Top);
                Left += (b.ActualWidth + 10.0);
                if (b.ActualHeight > BiggestHeight)
                { BiggestHeight = (b.ActualHeight + 10.0); }
                if (Left > dragCanvas.ActualWidth)
                { Top += BiggestHeight; Left = 0; }
            }
        }

        private BitmapEncoder GetCanvasImage()
        {
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)dragCanvas.RenderSize.Width, (int)dragCanvas.RenderSize.Height, 96d, 96d, System.Windows.Media.PixelFormats.Default);
            rtb.Render(dragCanvas);
            BitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(rtb));
            return pngEncoder;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            dragCanvas.Children.Clear();
            dragCanvas.UpdateLayout();
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