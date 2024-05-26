using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    //class Position представляє позицію в грі тетріс
    public class Position
    {
        // для зберігання координат розташування об'єкта
        public int Row { get; set; }
        public int Column { get; set; }

        // Position(int row, int column) - конструктор, який приймає значення рядка і стовпця для ініціалізації об'єкта Position
        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

    }
}
