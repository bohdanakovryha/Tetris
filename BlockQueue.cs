using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Tetris
{
    //class BlockQueue представляє чергу блоків для гри Тетріс
    public class BlockQueue
    {
        //Масив блоків, які можуть бути додані в чергу
        private readonly Block[] blocks = new Block[]
        {
            new IBlock(),
            new JBlock(),
            new LBlock(),
            new OBlock(),
            new SBlock(),
            new TBlock(),
        };

        // Генератор випадкових чисел
        private readonly Random random = new Random();

        // Послідовний блок, який буде вийматися з черги
        public Block NextBlock { get; private set; }

        //Конструктор класу BlockQueue, який встановлює початковий блок черги
        public BlockQueue()
        {
            NextBlock = RandomBlock();
        }

        //Метод, який повертає випадковий блок з масиву blocks
        private Block RandomBlock()
        {
            return blocks[random.Next(blocks.Length)];
        }

        //Метод, який повертає наступний блок у черзі і оновлює чергу новим випадковим блоком
        public Block GetAndUpdate()
        {
            Block block = NextBlock;

            // Генерація нового блоку, щоб оновити чергу
            do
            {
                NextBlock = RandomBlock();
            }
            while(block.Id == NextBlock.Id); // Перевірка, чи новий блок не є таким самим як попередній

            return block;
        }
    }
}
