using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace SnakesNLadders
{
    public class Snake
    {
        public int Head { get; }
        public int Tail { get; }

        public Snake(int head, int tail)
        {
            Head = head;
            Tail = tail;
        }
    }
}
