# BluePrism.WordLadder

## This is a console program written in C# to solve a Word Ladder - part of Blue Prism developers tests. 

### This is using a Breadth First Search algorithm, since the word ladder is a unweighted bidirectional graph.
### The program also pre-processes the word dictionary .txt file to generate two lists: 
####	- One containing unique tuples of all the valid words and a boolean to save their visited states. 
####	- And a dictionary containing wildcard words as keys and the words they can transform into. 

## Nuget Packages used in this build:

### - CommandLine		https://github.com/commandlineparser

### For unit testing purposes:
### - xUnit				https://github.com/xunit/xunit
### - NSubstitute		https://github.com/nsubstitute/NSubstitute
### - FluentAssertions	https://fluentassertions.com/
