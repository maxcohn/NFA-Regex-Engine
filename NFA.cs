using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFA_Regex_Engine
{
    class NFA
    {
        private State start;

        /// <summary>
        /// Creates a new NFA with given start state
        /// </summary>
        /// <param name="start">Start state in the NFA</param>
        public NFA(State start)
        {
            this.start = start;
        }

        #region "Operator Functions"
        /// <summary>
        /// Concatenates the given NFA to the end of this NFA
        /// </summary>
        /// <param name="appendingNFA">NFA being appended to the end of this NFA</param>
        public void Concatenate(NFA appendingNFA)
        {
            // when concatenating two NFAs, accepting states of the first NFA
            // are given epsilon transitions to the start state of the seconds NFA

            // keep a list of visited states so we don't add states over and over
            List<State> visited = new List<State>();

            // reference to the current state we're working with
            State curState;

            // queue of states to be checked
            Queue<State> stateQueue = new Queue<State>();

            stateQueue.Enqueue(this.GetStart());

            // while states exist in the queue, continue to loop
            while (stateQueue.Count > 0)
            {
                curState = stateQueue.Dequeue();

                if (visited.Contains(curState))
                    continue;

                // add this state to the list of visited states
                visited.Add(curState);

                // queue up all states that can be transitioned to
                foreach(Transition t in curState.GetTransitions()){
                    stateQueue.Enqueue(t.GetState());
                }
                
                // if this state is accepting, add an epsilon transition to the start of
                // the appendingNFA
                if (curState.IsAccepting())
                {
                    curState.AddTransition(Transition.Epsilon, appendingNFA.GetStart());

                    // this state is no longer accepting
                    curState.SetAccepting(false);
                }
            }
        }
           
        /// <summary>
        /// Creates an alternation between this NFA and the given NFA
        /// </summary>
        /// <param name="secondNFA">NFA being alternated with this NFA</param>
        public void Alternation(NFA secondNFA)
        {
            // alternating NFAs requires we make a new start state that has epsilon transitions to
            // both of the given NFAs

            // create new start state (not accepting)
            State newStart = new State(false);

            // add epsilon transitions from the new start to the start of the two original NFAs
            newStart.AddTransition(Transition.Epsilon, this.GetStart());
            newStart.AddTransition(Transition.Epsilon, secondNFA.GetStart());

            // make the start of this NFA the new start state
            this.SetStart(newStart);
        }

        /// <summary>
        /// Wraps the entire NFA in a kleene star condition, meaning there can be any number
        /// of occurances of the NFA
        /// </summary>
        public void KleeneStar()
        {
            // adding a kleene star condition to our NFA requires: 
            // adding a new start state that is accepting and has an epsilon transition to the original start state
            // adding an epsilon transition from the original accepting states to the original start state

            // the new start state for the NFA (is accepting)
            State newStart = new State(true);

            // add a transition from the new start to the old start
            newStart.AddTransition(Transition.Epsilon, this.GetStart());


            // search all states for ones that are accepting, and then add an epsilon transition to the original start state
            
            // keep a list of visited states so we don't add states over and over
            List<State> visited = new List<State>();

            // reference to the current state we're working with
            State curState;

            // queue of states to be checked
            Queue<State> stateQueue = new Queue<State>();

            stateQueue.Enqueue(this.GetStart());

            // while states exist in the queue, continue to loop
            while (stateQueue.Count > 0)
            {
                curState = stateQueue.Dequeue();

                if (visited.Contains(curState))
                    continue;

                // add this state to the list of visited states
                visited.Add(curState);

                // queue up all states that can be transitioned to
                foreach (Transition t in curState.GetTransitions())
                {
                    stateQueue.Enqueue(t.GetState());
                }

                // if this state is accepting, add an epsilon transition to the start of
                // the appendingNFA
                if (curState.IsAccepting())
                {
                    curState.AddTransition(Transition.Epsilon, newStart);
                }
            }

            // make original start no longer accepting
            this.GetStart().SetAccepting(false);

            // set newStart to be the start of this NFA
            this.SetStart(newStart);            

        }

        public void QMark()
        {

        }

        public void KleenePlus()
        {

        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Start state of the NFA</returns>
        public State GetStart()
        {
            return this.start;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newStart">State that will become the new start state</param>
        public void SetStart(State newStart)
        {
            this.start = newStart;
        }
        
    }
}
