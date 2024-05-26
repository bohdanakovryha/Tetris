using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    //Клас, що представляє ігрове поле Tetris
    public class GameGrid
    {
        // Масив, що представляє сітку гри 
        private readonly int[,] grid;

        // Кількість рядків та стовпців у сітці гри
        public int Rows { get; }
        public int Columns { get; }

        // Індексатор для доступу до елементів сітки гри
        public int this[int r, int c]
        {
            get => grid[r, c];
            set => grid[r, c] = value;
        }

        //Конструктор класу GameGrid
        public GameGrid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            grid = new int[rows, columns];
        }

        //IsInside(int r, int c) перевіряє, чи вказана позиція (r, c) знаходиться в межах сітки гри
        public bool IsInside(int r, int c)
        {
            return r >= 0 && r < Rows && c >= 0 && c < Columns; //Повертає true, якщо позиція знаходиться всередині сітки гри, в іншому випадку - false
        }

        //IsEmpty(int r, int c) перевіряє, чи вказана позиція (r, c) є вільною в сітці гри.
        public bool IsEmpty(int r, int c)
        {
            return IsInside(r, c) && grid[r,c] == 0; //Повертає true, якщо позиція (r, c) є вільною, в іншому випадку повертає false.
        }

        //IsRowFull(int r) перевіряє, чи вказаний рядок повністю заповнений блоками 
        public bool IsRowFull(int r)
        {
            for( int c = 0; c < Columns; c++)
            {
                if (grid[r,c] == 0)
                {
                    return false;
                }
            }
            return true;
        }

        //IsRowEmpty(int r) перевіряє, чи вказаний рядок повністю пустий
        public bool IsRowEmpty(int r)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r,c] != 0)
                {
                    return false;
                }
            }
            return true;
        }

        //ClearRow(int r) очищує вказаний рядок, заповнюючи його нулями
        private void ClearRow(int r)
        {
            for( int c = 0; c < Columns; c++)
            {
                grid[r,c] = 0;
            }
        }

        // MoveRowDown(int r, int numRows) переміщує вказаний рядок вниз на певну кількість рядків
        private void MoveRowDown(int r, int numRows) 
        { 
            for( int c = 0; c < Columns; c++)
            {
                grid[r + numRows, c] = grid[r, c];
                grid[r, c] = 0;

            }
        }

        //  ClearFullRows() очищає всі рядки, які повністю заповнені, і переміщає решту рядків вниз
        public int ClearFullRows()
        {
            int cleared = 0;

            //Цей цикл перебирає рядки знизу вгору
            for ( int r = Rows-1; r >= 0; r--)
            {
                if (IsRowFull(r))
                {
                    ClearRow(r);
                    cleared++;
                }
                else if (cleared > 0)
                {
                    MoveRowDown(r, cleared);
                }
            }
            return cleared;
        }

    }
}
