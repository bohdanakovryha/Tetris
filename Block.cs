using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public abstract class Block
    {
        protected abstract Position[][] Tiles { get; }
        protected abstract Position StartOffSet { get; }
        public abstract int Id { get; }
        private int rotationState;
        private Position offset;

        public Block()
        {
            offset = new Position(StartOffSet.Row, StartOffSet.Column);

        }

        // Повертає колекцію позицій плиток з урахуванням зміщення та поточного стану обертання
        public IEnumerable<Position> TilePositions()
        {
            foreach (Position p in Tiles[rotationState])
            {
                yield return new Position(p.Row + offset.Row, p.Column + offset.Column);
            }
        }

        //RotateCW() обертає блок за годинниковою стрілкою, змінюючи стан обертання
        public void RotateCW()
        {
            rotationState = (rotationState + 1) % Tiles.Length;

        }

        //RotateCCW() обертає блок проти годинникової стрілки, змінюючи стан обертання
        public void RotateCCW()
        {
            if (rotationState == 0)
            {
                rotationState = Tiles.Length - 1;
            }
            else
            {
                rotationState--;
            }
        }

        //Move() переміщує блок на вказану кількість рядків і стовпців
        public void Move(int rows, int columns)
        {
            offset.Row += rows;
            offset.Column += columns;
        }

        //Reset() скидає стан блоку до початкового положення та початкового стану обертання
        public void Reset()
        {
            rotationState = 0;
            offset.Row = StartOffSet.Row;
            offset.Column = StartOffSet.Column;
        }
    }
}
