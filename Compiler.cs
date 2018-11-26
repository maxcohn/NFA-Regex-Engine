using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFA_Regex_Engine
{
    class Compiler
    {
        
        //TODO make this work fucker
        public static NFA Compile(string postfix)
        {
            // stack that stores NFA pieces
            Stack<NFA> nfaStack = new Stack<NFA>();

            // go through postfix notation
            foreach(char c in postfix.ToCharArray())
            {
                switch (c)
                {
        
                    // concatenate
                    case Notation.CONCAT_OP:
                        // pop previous two items on stack
                        NFA nCat1 = nfaStack.Pop();
                        NFA nCat2 = nfaStack.Pop();

                        // make them one item
                        nCat2.Concatenate(nCat1); //TODO make sure this is the right order

                        // push it onto the stack
                        nfaStack.Push(nCat2);

                        break;

                    // kleene star
                    case '*':
                        NFA nStar = nfaStack.Pop();
                        nStar.KleeneStar();

                        nfaStack.Push(nStar);

                        break;
                    
                    // alternation
                    case '|':

                        NFA nAlt1 = nfaStack.Pop();
                        NFA nAlt2 = nfaStack.Pop();

                        nAlt2.Alternation(nAlt1);

                        nfaStack.Push(nAlt2);

                        break;

                    // kleene plus
                    case '+':

                        //TODO future implementation
                        break;

                    // standard character
                    default:
                        // make new NFA piece containing 'c' and push it to the NFA stack

                        // we make an NFA in the format:
                        // -> O -a-> O
                        // where the second state is accepting

                        State start = new State(false);
                        State transState = new State(true);
                        start.AddTransition(c, transState);
                        
                        nfaStack.Push(new NFA(start));
                        break;
                
                }
                
            }

            return nfaStack.Pop();
        }
        
    
    }
}
