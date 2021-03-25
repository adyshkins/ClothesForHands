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
using System.Windows.Shapes;

namespace ClothesForHands.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditMinCountWindow.xaml
    /// </summary>
    public partial class EditMinCountWindow : Window
    {
        public EditMinCountWindow()
        {
            InitializeComponent();
        }

        public EditMinCountWindow(int minCount)
        {
            InitializeComponent();
            txtMinCount.Text = minCount.ToString();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
