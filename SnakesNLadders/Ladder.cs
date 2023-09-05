using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SnakesNLadders
{
    public class Ladder
    {
        public int Top { get; }
        public int Bottom { get; }

        public Ladder(int top, int bottom)
        {
            Top = top;
            Bottom = bottom;
        }
    }
}
