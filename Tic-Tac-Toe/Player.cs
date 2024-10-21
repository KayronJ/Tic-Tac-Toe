using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe
{
    public class Player
    {
        public Player(string name, char symbol)
        {
            Name = name;
            Symbol = symbol;
        }
        public string Name { get; set; }
        public char Symbol { get; set; }
        public int Vitories { get; set; }
    }
}
