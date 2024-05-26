using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    //class GameState представляє стан гри Тетріс
    public class GameState
    {
        private Block currentBlock; // Поточний блок на полі
        public Block CurrentBlock // Властивість для доступу до поточного блоку
        {
            get => currentBlock;
            private set
            {
                // Встановлення нового поточного блоку та його початкове розташування
                currentBlock = value;
                currentBlock.Reset();

                // Цей цикл перевіряє чи блок влізе на поле гри. Якщо ні, то блок зсувається вниз
                for ( int i = 0; i < 2; i++)
                {
                    currentBlock.Move(1, 0);

                    if(!BlockFits())
                    {
                        currentBlock.Move(-1, 0);
                    }
                }
            }
        }
        public GameGrid GameGrid { get; } // Сітка гри
        public BlockQueue BlockQueue { get; } // Черга блоків
        public bool GameOver { get; private set; } // Прапорець завершення гри
        public int Score { get; private set; } // Очки
        public Block HeldBlock { get; private set; } // Блок, що утримується гравцем
        public bool CanHold { get; private set; } // Можливість утримувати блок

        //GameState() - це конструктор класу GameState, ініціалізує об'єкти сітки гри та черги блоків
        public GameState()
        {
            GameGrid = new GameGrid(22, 10);
            BlockQueue = new BlockQueue();
            CurrentBlock = BlockQueue.GetAndUpdate();
            CanHold = true;
        }

        //BlockFits() перевіряє чи поточний блок можна розмістити на полі гри
        private bool BlockFits()
        {
            //// цей цикл проходиться по кожній позиції плиток поточного блоку
            foreach (Position p in CurrentBlock.TilePositions())
            {
                if (!GameGrid.IsEmpty(p.Row, p.Column))
                {
                    return false;
                }
            }
            return true;
        }

        //HoldBlock() використовується для утримання блоку гравцем
        public void HoldBlock()
        {
            if (!CanHold)
            {
                return;
            }
            // Якщо блок для утримання ще не встановлений
            if (HeldBlock == null)
            {
                HeldBlock = CurrentBlock;
                CurrentBlock = BlockQueue.GetAndUpdate();
            } 
            else
            {
                // обмін поточного блоку з блоком для утримання
                Block tmp = CurrentBlock;
                CurrentBlock = HeldBlock;
                HeldBlock = tmp;
            }

            CanHold = false;
        }

        //обертання поточного блоку за годинниковою стрілкою 
        public void RotateBlockCW()
        {
            CurrentBlock.RotateCW();

            if (!BlockFits())
            {
                CurrentBlock.RotateCCW();
            }
        }
        //обертання поточного блоку проти годинникової стрілки
        public void RotateBlockCCW()
        {
            CurrentBlock.RotateCCW();

            if (!BlockFits())
            {
                CurrentBlock.RotateCW();
            }
        }
        //MoveBlockLeft() переміщує поточний блок вліво на один стовпчик
        public void MoveBlockLeft()
        {
            CurrentBlock.Move(0, -1);

            if (!BlockFits())
            {
                CurrentBlock.Move(0, 1);
            }
        }
        //MoveBlockRight() переміщує поточний блок вправо на один стовпчик
        public void MoveBlockRight()
        {
            CurrentBlock.Move(0, 1);

            if (!BlockFits())
            {
                CurrentBlock.Move(0, -1);
            }
        }
        //IsGameOver() перевіряє чи гра завершилася
        private bool IsGameOver()
        {
            return !(GameGrid.IsRowEmpty(0) && GameGrid.IsRowEmpty(1));
        }

        // PlaceBlock() розміщує поточний блок на полі гри
        private void PlaceBlock()
        {
            // Цей цикл прохожиться по всіх позиціях плиток поточного блоку
            foreach (Position p in CurrentBlock.TilePositions())
            {
                // Встановлюємо значення плитки у сітці гри на ідентифікатор поточного блоку
                GameGrid[p.Row, p.Column] = CurrentBlock.Id;
            }

            Score += GameGrid.ClearFullRows();

            // Перевірка чи гра закінчилася
            if (IsGameOver())
            {
                GameOver = true;
            }
            else
            {
                CurrentBlock = BlockQueue.GetAndUpdate();
                CanHold = true;
            }
        }

        //MoveBlockDown() переміщує блок вниз на один рядок, якщо це можливо, інакше розміщує його на полі гри та обробляє події
        public void MoveBlockDown()
        {
            // Зсув поточного блоку вниз на один рядок
            CurrentBlock.Move(1,0);

            // Перевірка, чи блок влізе на поле гри
            if (!BlockFits())
            {
                CurrentBlock.Move(-1, 0); // Зсув блоку вгору
                PlaceBlock(); // Розміщення блоку на полі
            }
        }

        //TileDropDistance(Position p) обчислює відстань, на яку може опуститися плитка відносно заданої позиції p
        private int TileDropDistance(Position p)
        {
            int drop = 0;

            // Поки під клітинкою вільна клітинка, то відстань drop збільшується 
            while (GameGrid.IsEmpty(p.Row + drop + 1, p.Column))
            {
                drop++;
            }

            return drop;
        }
        //BlockDropDistance() розраховує відстань до падіння блока
        public int BlockDropDistance()
        {
            int drop = GameGrid.Rows;

            // Цей цикл перебирає всі позиції плиток блоку та знаходимо найменшу відстань до падіння
            foreach (Position p in CurrentBlock.TilePositions())
            {
                drop = System.Math.Min(drop, TileDropDistance(p));
            }

            return drop;
        }

        //DropBlock() опускає поточний блок до максимальної відстані й розміщує його на полі гри
        public void DropBlock()
        {
            CurrentBlock.Move(BlockDropDistance(), 0); // Зсув блоку на максимальну відстань
            PlaceBlock();
        }

    }
}
