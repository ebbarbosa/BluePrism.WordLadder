# BluePrism.WordLadder

## This project has a web api and a console program written in C# to solve a Word Ladder - part of Blue Prism developers tests. 

### It uses a Breadth First Search algorithm (https://en.wikipedia.org/wiki/Breadth-first_search), to find the shortest-path since the word ladder is an unweighted bidirectional graph.

### The console program takes four arguments:
	- The first word of the word ladder
	- The target word of the word ladder
	- The file name for the dictionary of words to be used as steps.
	- The file name to be created as output for the word ladder result.

### The web api has a get method with two arguments and it uses a standard dictionary provided by BluePrism:
	- The first word of the word ladder
	- The target word of the word ladder

#### Usage:

#### WebApi:
#####	After cloning this repository, run the webApi and try the GET method available in the swagger interface or via curl.
```
curl -X GET "https://localhost:5001/WordLadder?startWord=pint&endWord=axis" -H  "accept: text/plain"
```

#### Console:
#####	After cloning this repository, go to the src folder in your local machine and run dotnet publish:

```
C:\...\BluePrism.WordLadder> cd src
C:\...\BluePrism.WordLadder\src> dotnet publish -p:PublishProfile=FolderProfile
```

##### Once published, go to the publish folder and run the program passing the desired arguments:

```
C:\...\BluePrism.WordLadder\src> cd publish 
C:\...\BluePrism.WordLadder\src\publish> BluePrism.WordLadder <start word 4 letters> <end word four letters> <path to .\words-english.txt> <path to .\word-ladder.txt>
```

##### There is a copy of the provided words-english.txt file inside the contents folder, so you may run it like so for a smoke test: 

```
C:\...\BluePrism.WordLadder\src\publish> BluePrism.WordLadder MUST HIRE ..\..\content\words-english.txt .\answer.txt
```
```
Time taken with graphs = 2 ms

Answer file created in file:///C:\...\BluePrism.WordLadder\src\publish\answer.txt

Press any <key> to exit...
C:\...\BluePrism.WordLadder\src\publish>
```

##### Once successfully run it will open the generated .txt file containing the word ladder automatically.

##

### How does it work:

#### The program pre-processes the word dictionary .txt file to generate two dictionaries: 
####	- One containing unique items of all the valid words (vertexes) as keys and a boolean value to save their visited states. 
####	- And another containing wildcard words as keys and the words they can transform into as values (adjacent edges). i.e.: this is an entry --- { Key = "*IRE", Value = ["HIRE", "SIRE", "DIRE"] } - this is where the algorithm spends most of its efforts in creating this dictionary of edges.
####	The BFS algorithm has a time complexity of O(V + E) where V are the vertices of the tree and E the edges. In our word ladder case V would be the words in the dictionary provided and E would be the possbile transformations for the word. 
####	The space complexity for BFS is O(V) but since we preprocessed the vertices and the edges we augmented it to O(2*V) for the creation of word visited dictionary and O(4*V) = E for the preprocessed wildcards dictionary. They do not multiply because we created them in the same loop so O(2*V + 4*V) = O(6V) = O(V).
####	Since V and E are pre processed, our time performance enhances to a minor complexity of O(V + log(E)) because we now have the adjacent edges in memory, the trade off is of course space which is more used but stays within the O(V) order.
	

## Nuget Packages used in this build:

### - <a href="https://github.com/commandlineparser">Command Line Parser</a>
### - <a href="https://fluentvalidation.net/">Fluent Validations</a>
### - <a href="http://www.ninject.org/">Ninject</a>

### For unit testing purposes:
### - <a href="https://github.com/xunit/xunit">xUnit</a>
### - <a href="https://github.com/nsubstitute/NSubstitute">NSubstitute</a>
### - <a href="https://fluentassertions.com/">Fluent Assertions</a>

### Debugging Information: 

#### To debug the program please check the ~\src\BluePrism.WordLadder\Properties\launchSettings.json file and change the commandLineArgs setting according to your specifications.
