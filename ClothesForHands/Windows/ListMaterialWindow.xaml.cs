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
using ClothesForHands.Windows;

namespace ClothesForHands
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class ListMaterialWindow : Window
    {
        List<Material> materialsList = new List<Material>();

        List<Material> selectMaterials = new List<Material>();

        
        int numberPage = 0;
        int countMaterialOnPage;
        int allCount;

        int countPage = 0;

        List<string> listForSort = new List<string>()
        {
            "Наименование (по возрастанию)",
            "Наименование (по убыванию)",
            "Остаток на складе (по возрастанию)",
            "Остаток на складе (по убыванию)",
            "Стоимость (по возрастанию)",
            "Стоимость (по убыванию)"
        };

        List<TypeMaterial> listTypeMaterials = new List<TypeMaterial>();

        public ListMaterialWindow()
        {
            InitializeComponent();

            cmbSort.ItemsSource = listForSort;
            cmbSort.SelectedIndex = 0;

            listTypeMaterials = Context.TypeMaterial.ToList();
            listTypeMaterials.Insert(0, new TypeMaterial { Name = "Все типы" });
            cmbFiltr.ItemsSource = listTypeMaterials;
            cmbFiltr.DisplayMemberPath = "Name";
            cmbFiltr.SelectedIndex = 0;


            btnEditMinCount.Visibility = Visibility.Collapsed;

            Filter();

        }

        private void Filter()
        {
            materialsList = Context.Material.ToList(); // получаем полный список всех материалов

            // Поиск по наимнованию
            materialsList = materialsList.Where(i => i.Name.ToLower().Contains(txtSearch.Text.ToLower())).ToList();

            // сортировка

            switch (cmbSort.SelectedIndex)
            {
                case 0:
                    materialsList = materialsList.OrderBy(i => i.Name).ToList(); // сотрировка по возрастинию по имени
                    break;
                case 1:
                    materialsList = materialsList.OrderByDescending(i => i.Name).ToList(); // сортировка по имени по убыванию
                    break;

                case 2:
                    materialsList = materialsList.OrderBy(i => i.Count).ToList();
                    break;
                case 3:
                    materialsList = materialsList.OrderByDescending(i => i.Count).ToList();
                    break;

                case 4:
                    materialsList = materialsList.OrderBy(i => i.Price).ToList();
                    break;
                case 5:
                    materialsList = materialsList.OrderByDescending(i => i.Price).ToList();
                    break;

                default:
                    break;
            }


            // фильтрация

            if (cmbFiltr.SelectedIndex != 0) // если "все записи" то фильтрация не применяется
            {
                materialsList = materialsList.Where(i => i.TypeId == (cmbFiltr.SelectedIndex)).ToList();
            }

            allCount = materialsList.Count;

            countPage = (materialsList.Count / 15) + 1; // подсчет количества страниц если 15 записей на одной


            // постраничный вывод
            materialsList = materialsList.
                Skip(numberPage * 15).
                Take(15).
                ToList();

            countMaterialOnPage = materialsList.Count;

            txtCountOnPage.Text = countMaterialOnPage.ToString();
            txtAllCount.Text = allCount.ToString();

            lvMaterialList.ItemsSource = materialsList;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e) // нажатиекнопки назад
        {
            if (numberPage > 0)
            {
                numberPage--;
            }
            btnGo1.Visibility = Visibility.Visible;
            btnGo2.Visibility = Visibility.Visible;
            btnGo3.Visibility = Visibility.Visible;
            btnGo1.Content = (numberPage + 1).ToString();
            btnGo2.Content = (numberPage + 2).ToString();
            btnGo3.Content = (numberPage + 3).ToString();

            if (countPage < Convert.ToInt32(btnGo2.Content))
            {
                btnGo2.Visibility = Visibility.Collapsed;
            }
            if (countPage < Convert.ToInt32(btnGo3.Content))
            {
                btnGo3.Visibility = Visibility.Collapsed;
            }

            countPage = (materialsList.Count / 15) + 1;
            Filter();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e) // нажатие кнопки вперед
        {
            if (materialsList.Count >= 15)
            {
                numberPage++;
                btnGo1.Visibility = Visibility.Visible;
                btnGo2.Visibility = Visibility.Visible;
                btnGo3.Visibility = Visibility.Visible;
                btnGo1.Content = (numberPage + 1).ToString();
                btnGo2.Content = (numberPage + 2).ToString();
                btnGo3.Content = (numberPage + 3).ToString();

                if (countPage < Convert.ToInt32(btnGo2.Content))
                {
                    btnGo2.Visibility = Visibility.Collapsed;
                }
                if (countPage < Convert.ToInt32(btnGo3.Content))
                {
                    btnGo3.Visibility = Visibility.Collapsed;
                }

                countPage = (materialsList.Count / 15) + 1;

            }
            
            Filter();
          
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e) // ввод в строку поиска
        {
            Filter();
        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e) // выбор варианта сортировки
        {
            Filter();
        }

        private void cmbFiltr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void lvMaterialList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnEditMinCount.Visibility = Visibility.Visible; // делаем кнопку Изменить минимальное количество видимой
        }

        private void btnEditMinCount_Click(object sender, RoutedEventArgs e) 
        {
            
            foreach (var material in (lvMaterialList.SelectedItems)) // добавляем в список выбранные маериалы
            {
                selectMaterials.Add(material as Material); 
            }


            if (selectMaterials != null) // если список выбранных материалов не пустой 
            {
                ClassHelper.SelectMaterial.editMinCountMaterial = selectMaterials.Max(i => i.MinCount); // передаем максимальное значение свойства MinCount в статический класс 
                EditMinCountWindow editMinCountWindow = new EditMinCountWindow();
                editMinCountWindow.ShowDialog();
                foreach (var item in selectMaterials) // изменяем значение свойства MinCount всех выбранных материалов
                {
                    item.MinCount = ClassHelper.SelectMaterial.editMinCountMaterial;
                }
                Context.SaveChanges();
                lvMaterialList.SelectedIndex = -1; // очищаем выбор
                Filter();
                selectMaterials.Clear();
                
            }

        }
    }
}
