# BluePrism.WordLadder

## This is a console program written in C# to solve a Word Ladder - part of Blue Prism developers tests. 

### It uses a Breadth First Search algorithm (https://en.wikipedia.org/wiki/Breadth-first_search), since the word ladder is an unweighted bidirectional graph.

### The program takes four arguments:
#### - The first word of the word ladder
#### - The target word of the word ladder
#### - The file name for the word dictionary to be used as steps.
#### - The file name to be created as output for the word dictionary to be used as steps.
```
C:\...> BluePrism.WordLadder HIRE SORT c:\words-english.txt c:\word-ladder.txt
```

## How it works:
### The program pre-processes the word dictionary .txt file to generate two dictionaries: 
####	- One containing unique items of all the valid words (vertexes) and a boolean to save their visited states. 
####	- And another containing wildcard words as keys and the words they can transform into as values (adjacent edges). 
####		i.e.: { "*IRE", ["HIRE", "SIRE", "DIRE"] } 

## Nuget Packages used in this build:

### - CommandLine		https://github.com/commandlineparser

### For unit testing purposes:
### - xUnit				https://github.com/xunit/xunit
### - NSubstitute		https://github.com/nsubstitute/NSubstitute
### - FluentAssertions	https://fluentassertions.com/
