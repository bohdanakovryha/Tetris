namespace Tetris
{
    internal class TBlock : Block
    {
        // Масив позицій плиток блоку для кожного можливого обертання
        private readonly Position[][] tiles = new Position[][]
       {

            new Position[] { new(0,1), new(1,0), new(1,1), new(1,2) },
            new Position[] { new(0,1), new(1,1), new(1,2), new(2,1) },
            new Position[] { new(1,0), new(1,1), new(1,2), new(2,1) },
            new Position[] { new(0,1), new(1,0), new(1,1), new(2,1) }
       };

        public override int Id => 6;

        // Перевизначена властивість, що визначає початкове зміщення блоку
        protected override Position StartOffSet => new Position(0, 3);

        // Перевизначена властивість, що повертає масив позицій плиток блоку
        protected override Position[][] Tiles => tiles;
    }
}