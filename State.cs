using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFA_Regex_Engine
{
    class State
    {
        private Boolean accepting = false;
        private List<Transition> transitions;


        /// <summary>
        /// Creates a new State with no transitions
        /// </summary>
        /// <param name="accepting"></param>
        public State(bool accepting)
        {
            this.accepting = accepting;

            this.transitions = new List<Transition>();
        }

        /// <summary>
        /// Creates a new State
        /// </summary>
        /// <param name="accepting">Boolean identifing whether the state is accepting or not</param>
        /// <param name="transitions">List of Transitions associated with the state</param>
        public State(bool accepting, List<Transition> transitions) : this(accepting)
        {
            // add all given transitions to our new list
            foreach(Transition t in transitions)
            {
                this.transitions.Add(t);
            }
            
        }

        /// <summary>
        /// Sets whether the state is accepting or not
        /// </summary>
        /// <param name="accepting">New accepting value</param>
        public void SetAccepting(bool accepting)
        {
            this.accepting = accepting;
        }

        /// <summary>
        /// Returns whether the state is accepting or not
        /// </summary>
        /// <returns>Boolean representing whether the state is accepting or not</returns>
        public bool IsAccepting()
        {
            return accepting;
        }

        /// <summary>
        /// Returns whether the state is an end state or not
        /// </summary>
        /// <returns>Boolean representing whether the state is an end state or not</returns>
        public bool IsEndState()
        {
            // if this state has no transitions, it is an end state
            return transitions.Count == 0;
        }

        /// <summary>
        /// Adds a new Transition to the state
        /// </summary>
        /// <param name="symbol">Symbol required to activate the transition</param>
        /// <param name="newState">State that will be transitioned to</param>
        public void AddTransition(char symbol, State newState)
        {
            transitions.Add(new Transition(symbol, newState));
        }

        /// <summary>
        /// Adds a new Transition to the state
        /// </summary>
        /// <param name="t">Transition being added to the list of transitions</param>
        public void AddTransition(Transition t)
        {
            transitions.Add(t);
        }

        /// <summary>
        /// Returns list of all transitions from this state
        /// </summary>
        /// <returns>List of transitions from this state</returns>
        public List<Transition> GetTransitions()
        {
            return transitions;
        }
    }
}
