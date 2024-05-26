using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    //успадкування класу Block із простору імен Tetris
    public class OBlock : Block
    {
        private readonly Position[][] tiles = new Position[][]
        {
        new Position[] { new (0,0), new (0,1), new (1,0), new (1,1) }
        };


        public override int Id => 4;

        // Перевизначена властивість, що визначає початкове зміщення блоку
        protected override Position StartOffSet => new Position(0,4);

        // Перевизначена властивість, що повертає масив позицій плиток блоку
        protected override Position[][] Tiles => tiles;

    }
}
