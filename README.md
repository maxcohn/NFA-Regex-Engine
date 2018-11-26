# NFA Based Regular Expression Engine

## By Maxwell Cohn

Inspired by Russ Cox's "Implementing Regular Expressions" series

## Goal

The goal of this project was to develop a better understanding of Regular Expressions, because they were always intimidating
when I was a younger student. Turns out they're pretty awesome! I began working on this project before taking a course on
CS Theory, so there was a big learning curve when it came to udnerstanding finite state automata at first, but everything
began to click eventually. I learned a lot (from this project and further curiosity brought about by this project) about formal
language theory, which is a subject I though I would never have an interest in. Overall, this was a great learning experience
for learning about automata, implementation of more abstract concepts, graphs, and regular expressions.

This isn't meant to be used as a practical regular expression engine, but instead a practice and demonstration of how one would
be implemented. This application lacks support for many useful features in regular expressions, and won't work with certain
characters (epsilon being the first to come to mind as explained below). Hopefully someone out there who wants to understand regular
expressions more deeply finds this interesting and informative, because I know that when I was trying to learn, the currently
published NFA based regex engines were lacking in explanation.

Development of this project is on and off, as I'm busy with school and other projects that take priority. I plan on working on
this project from time to time as a kind of break from the stress of other projects.

## Implementation

### NFA Object

The NFA object was what manages each NFA, both complete and in progress.

The NFA keeps track of the start state and has all methods to apply operations to the NFA. The currently implemented operations are:

- Concatenation
- Alternation
- Kleene Star (0 or more occurences)

The other possible operations to implement are:

- Question Mark (0 or 1 occurence)
- Kleene Plus (1 or more occurence)
- Character Classes

The question makr and kleene plus operations are simple additions, as they are just modifications of kleene star. Character classes
are more complex, but definitely on the table for future implementation.

### State Object

The State object stores whether it is an accepting state or not and a list of transitions. The list of transitions acts as the
edges of the graph that is the NFA.

### Transition Object

The Transition object stores a symbol required to activate the transition, and the state to transition to. There is also a constant
to represent an "epsilon" transtion (a transition that doesn't require input to be activated). The constant is an epsilon character
(949 in Unicode) because this is not meant to be a used regex engine, so we don't need to worry about the actual symbol required for
a transition being an epsilon.

### The Compiler

The compiler takes the given regular expression and converts it from an infix notation to a postfix notation because it is more
natural to build the NFA when working with a postfix expression. The compiler then loops through each character in the given
expression and begins to build our NFA.

We have a stack that contains all NFAs while compiling. When a character is read, a basic NFA that just accepts that character
is created and pushed onto the NFA stack. Whenever an operator is reached, whether it be unary or binary, one or two (respectively)
NFAs are popped off the stack and modified based on the operator. Once the operation is applied, we push the new NFA back onto
the stack. We repeat this process until the expression has been completely read and the final NFA is left on the stack. We then
pop the final NFA off the stack and return it.

This leaves our result being a single complete NFA based on the given regular expression.

### Main Form

The form serves as a way to test code. Think of it as a sandbox to test the engine in.