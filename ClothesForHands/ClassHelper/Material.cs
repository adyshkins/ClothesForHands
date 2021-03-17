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
       
    }
}
