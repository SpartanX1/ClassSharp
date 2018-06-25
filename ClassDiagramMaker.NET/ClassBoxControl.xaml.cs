using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClassDiagramMaker.NET
{
    /// <summary>
    /// Interaction logic for ClassBoxControl.xaml
    /// </summary>
    public partial class ClassBoxControl : UserControl
    {

        public ClassBoxControl(int height = 100, int width = 100)
        {
            MinHeight = height;
            MinWidth = width;
            InitializeComponent();
        }

    
        private void ButtonAddVariable_Click(object sender, RoutedEventArgs e)
        {
            var newClassVar = new TextBox
            {
                Text = "var name : type"
            };

       
            VariablePanel.Children.Remove(AddBtn);
            VariablePanel.Children.Add(newClassVar);
            VariablePanel.Children.Add(AddBtn);
        }
    }
}
