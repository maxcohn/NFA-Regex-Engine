using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFA_Regex_Engine
{
    class Notation
    {
        public const char CONCAT_OP = '.';

        private static Dictionary<char, int> precedenceMap;

        static Notation()
        {
            precedenceMap = new Dictionary<char, int>()
            {
                {'(', 1},
                {'|', 2},
                {CONCAT_OP, 3},
                {'?', 4},
                {'*', 4},
                {'+', 4},
                {'^', 5}

            };
        }

        private static int getPrecedence(char c)
        {
            return precedenceMap.ContainsKey(c) ? precedenceMap[c] : -1;
        }

        /// <summary>
        /// Returns true if the char is a valid operand in a regular expression
        /// </summary>
        /// <param name="c">Character in question of being an operand</param>
        /// <returns>True if the character is an operand, false otherwise</returns>
        private static bool isOperand(char c)
        {
            //TODO update to add more operands
            return (c >= 65 && c <= 90 || c >= 97 && c <= 122);
        }

        /// <summary>
        /// Returns true if the char is a valid unary operator in a regular expression
        /// </summary>
        /// <param name="c">Character in question of being a unary operator</param>
        /// <returns>True if the character is a unary operator, false otherwise</returns>
        private static bool isUnaryOperator(char c)
        {
            return (c == '+' || c == '?' || c == '*');
        }

        /// <summary>
        /// Returns true if the char is a valid binary operator in a regular expression
        /// </summary>
        /// <param name="c">Character in question of being a binary operator</param>
        /// <returns>True if the character is a binary operator, false otherwise</returns>
        private static bool isBinaryOperator(char c)
        {
            return (c == '|' || c == CONCAT_OP);
        }

        /// <summary>
        /// Returns true if the char is a valid operator in a regular expression
        /// </summary>
        /// <param name="c">Character in question of being an operator</param>
        /// <returns>True if the character is an operator, false otherwise</returns>
        private static bool isOperator(char c)
        {
            return (isUnaryOperator(c) || isBinaryOperator(c) || c == '(' || c == ')');
        }

        /// <summary>
        /// Adds concatentation operator where neccessary in an infix regular expression
        /// </summary>
        /// <param name="infix">Regular expression in infix notation</param>
        /// <returns>Infix regular expression with concatenation operators where neccessary</returns>
        private static string addConcatOperator(string infix)
        {
            List<char> newRegex = new List<char>();

            char[] infixArr = infix.ToCharArray();

            for(int i = 0; i < infixArr.Length; i++)
            {
                char curChar = infixArr[i];

                newRegex.Add(curChar);

                if (i + 1 != infixArr.Length)
                {
                    char nextChar = infixArr[i + 1];

                    

                    if (isOperand(curChar) && isOperand(nextChar))
                    {
                        // if the current char is an operand, and the next char is an operand, add concat
                        newRegex.Add(CONCAT_OP);
                    }
                    else if (isUnaryOperator(curChar) && isOperand(nextChar))
                    {
                        // if the current char is a unary operator, and the next char is an operand, add concat
                        newRegex.Add(CONCAT_OP);

                    }
                    else if (isOperand(curChar) && nextChar == '(')
                    {
                        // if the current char is an operand, and the next char is '(', add concat
                        newRegex.Add(CONCAT_OP);
                    }
                    else if (curChar == ')' && isOperand(nextChar))
                    {
                        // if the current char is ')', and the next char is an operand, add concat
                        newRegex.Add(CONCAT_OP);
                    }
                }
            }

            string formatRegex = "";
            foreach(char c in newRegex)
            {
                formatRegex += c;
            }

            return formatRegex;

        }

        public static string infixToPostfix(string infix)
        {
            string formRegex = addConcatOperator(infix); // formatted regex with concat operator in infix notation

            // output list and operator stack needed to convert infix to postfix (Shunting yard algorithm)
            List<char> output = new List<char>();
            Stack<char> opStack = new Stack<char>();

            // loop through all characters in the formatted infix regular expression
            foreach (char curChar in formRegex.ToCharArray()) {

                // check if the current char is an operator
                if (!isOperator(curChar))
                {
                    // if the char isn't an operator, just append it to the output list
                    output.Add(curChar);
                    continue;
                }

                // check if the opStack is empty
                if (opStack.Count == 0)
                {
                    // if it is, push the current operator onto it
                    opStack.Push(curChar);
                } else if (curChar == '(')
                {
                    opStack.Push(curChar);

                } else if (curChar == ')')
                {

                    // if the current char is a closing parnthesis, pop until we reach the opening one
                    while (opStack.Peek() != '(')
                    {
                        output.Add(opStack.Pop());
                    }
                    // get rid of '(' left in stack
                    opStack.Pop();

                }
                else {
                    // while the stack has at least one element, loop
                    while (opStack.Count > 0)
                    {
                        // if the precedence of the current
                        if (getPrecedence(opStack.Peek()) >= getPrecedence(curChar))
                        {
                            output.Add(opStack.Pop());
                        }
                        else
                        {
                            break;
                        }
                        
                    }
                    opStack.Push(curChar);
                }

            }

            // pop the rest of the stack and add it to the output list
            while(opStack.Count > 0)
            {
                output.Add(opStack.Pop());
            }

            string postfix = "";
            foreach(char c in output)
            {
                postfix += c;
            }

            return postfix;

            
        }
    }
}
