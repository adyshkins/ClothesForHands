using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesForHands.EF
{
    public class AppData
    {
        // Создание объекта Context для обращения к модели
        public static Entities Context { get; } = new Entities();
    }
}
