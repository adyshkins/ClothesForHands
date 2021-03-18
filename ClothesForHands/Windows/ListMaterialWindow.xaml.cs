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
using static ClothesForHands.EF.AppData;
using ClothesForHands.EF;

namespace ClothesForHands
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class ListMaterialWindow : Window
    {
        List<Material> materialsList = new List<Material>();
        int numberPege = 0;

        public ListMaterialWindow()
        {
            InitializeComponent();
            Filter();

        }

        private void Filter()
        {
            materialsList = Context.Material.ToList();

            materialsList = materialsList.OrderBy(i => i.ID).
                Skip(numberPege * 15).Take(15).ToList();

            lvMaterialList.ItemsSource = materialsList;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (numberPege > 0)
            {
                numberPege--;
            }
            btnGo1.Content = (numberPege + 1).ToString();
            btnGo2.Content = (numberPege + 2).ToString();
            btnGo3.Content = (numberPege + 3).ToString();
            Filter();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (materialsList.Count >= 15)
            {
                numberPege++;
                
                btnGo1.Content = (numberPege + 1).ToString();
                btnGo2.Content = (numberPege + 2).ToString();
                btnGo3.Content = (numberPege + 3).ToString();

                int countPage = (materialsList.Count / 15) + 1;

                if (Convert.ToInt32(btnGo2.Content) > countPage)
                {
                    btnGo2.Visibility = Visibility.Collapsed;
                }

                if (Convert.ToInt32(btnGo3.Content) > countPage)
                {
                    btnGo3.Visibility = Visibility.Collapsed;
                }

            }
            
            Filter();
            //MessageBox.Show(numberPege.ToString());
        }
    }
}
