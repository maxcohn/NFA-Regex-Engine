using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFA_Regex_Engine
{
    class Transition
    {
        public const char Epsilon = 'ε';


        private char symbol;
        private State nextState;

        public Transition(char symbol, State nextState)
        {
            this.symbol = symbol;
            this.nextState = nextState;
        }

        public State GetState()
        {
            return nextState;
        }
    }
}
