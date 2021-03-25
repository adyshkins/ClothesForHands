using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesForHands.EF
{
    public partial class Material
    {
        public string GetImage
        {
            get
            {
                if (Image != null)
                {
                    return $"/PhotoMaterials{Image}";
                }
                else
                {
                    return Image;
                }
            }
        }

        public string GetTypeAndName { get => $"Тип материала: {TypeMaterial.Name} | Наименование материала: {Name}"; }

        public string GetMinCount { get => $"Минимальное количество: {MinCount} шт."; }

        public string GetSupp { get => $"Поставщики: "; }

        public string GetColor
        {
            get 
            {
                if (MinCount > Count)
                {
                    return "#f19292";
                }
                else if (MinCount * 3 < Count)
                {
                    return "#ffba01";
                }
                else
                {
                    return "ffffff";
                }
            }
        }

    }
}
