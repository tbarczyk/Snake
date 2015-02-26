using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake
{
    class SnakePart
    {
        public int X{ get; set; }
        public int Y{ get; set; }

        public SnakePart()
        {
            X = 0;
            Y = 0;
        }

        public SnakePart(int a, int b)
        {
            X = a;
            Y = b;
        }
    }
}
