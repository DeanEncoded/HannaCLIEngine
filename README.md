# HannaCLIEngine
Hanna is a game engine for creating Choose Your Own Adventure games that run in the command line. You can create complex paths for your games with the power of **sequences and conditionals**.
## How does Hanna work?
Hanna is made up of two components both written in seperate languages.
These two components are:
  - **The Engine** (***HannaCLIEngine***) [*Written in C++*]
  - **The Studio** (***Hanna-Studio***) [*Written in C#*]

The engine takes in a game file (a json file) that contains all the data for a game and then runs that as a game.
Then there's the Studio.
### Hanna Studio
![Hanna Studio Preview](https://raw.githubusercontent.com/deanencoded/hannacliengine/master/preview-images/studio_workspace_prev.jpg)
Hanna Studio is basically a "workspace" that allows you to make games for the engine. You can create game projects, work on them, run and test them all in the Studio.
#### How do I make a game?
Here is a rundown on how making a game for Hanna works.
Your game is generally made of **sequences**. These are kind of like levels/stages in your game. These **sequences** in your game have these properties:

 - **sqId** (*this is basically a sequence identifier. It's unique for all sequences in your game.*)
 - **sqType** (*the type of sequence to be run. It can either be an **ordinary** sequence [which has choices] or an **end** sequence [which displays its 'text' properties and ends the game]*)
 - **mainText** (*text that is primarily displayed when the the sequence runs*)
 - **secondaryText** (*text that is secondarily displayed when the the sequence runs*:: You can use this property to ask the player things like "What do you want to do?" or "Pick an item", things along those lines)
 - **mainText** (*text that is primarily displayed when the the sequence runs*)
 - **choices** (*choices are what they say they are. Your sequence can have multiple choices that the player can select from. Choices also have their own properties*)
	 - **choiceLetter** (*choices have letters assigned to them. The player uses letters to pick a choice from a list of choices*)
	 - **choiceType** (*choices have two types. They can either be **set** or **conditional**. A set choice always appears when a sequence is run and a conditional choice only appears if a certain condition is met*)
	 - **choiceCondition** (*this is where the condition of your conditional choice is stored. This property keeps sub-properties **container** and **value**... [if **value** is in a **container**, then the current choice will be displayed, otherwise it won't] Containers are explained below somewhere. There's also the idea of having multiple choiceConditions.......that's a feature to be added I guess*)
	 - **choiceText** (*this is the text that is displayed to the player as the choice*)
	 - **outcomeText** (*when the player makes a choice. The outcome text is displayed to the user*)
	 - **containerAdd** (*this property generally points to a **container** and a **value** [Just like **choiceCondition**]. The value specified in this property will be added to the container specified*)
	 - **containerDispose** (*this is a feature idea not currently implemented in the engine. This would allow a value to be disposed from a players container*)
	 - **nextSq** (*this property represents the sequence id of the sequence that runs after this choice is made*)


### Explaining Containers
Containers are well...... containers that contain things, in our case values!
You can add a container to your game and that container is assigned to the player when they run your game. Values end up in containers through choices.
Some choices are only displayed to the player if a certain value is in a certain container.

You can use Containers to store values of things such as *inventory*, *weapons*, *friends*, *actions* or whatever it is you want to store!

Think of it like this (A game has the container 'inventory'):
***The game asks you to pick an item. Choice A is a water bottle. And Choice B is a basket of bananas.***
If you pick choice A.... the value "water bottle" is added to the container "inventory".
If you pick choice B.... the value "basket of bananas" is added to the container "inventory".

Now what might you use this value for??? Maybe you have a sequence that only displays a choice if the player has 'water bottle' in their inventory. You can just make a conditional choice, make the conditional container 'inventory' and set the value to 'water bottle'. (That's it!)

Your games' containers can be added through this button in the studio.
![Containers Button Preview](https://raw.githubusercontent.com/deanencoded/hannacliengine/master/preview-images/studio_containers_button.jpg)

If you've understood how things work so far.. or have a rough idea, using the studio won't be such a big deal to you. Things may be complicated at first but it'll all make sense.

### Running my game
You can easily run your game inside the studio by simply clicking the Run Game button on to top-bar section of the studio.
![Studio Run Preview](https://raw.githubusercontent.com/deanencoded/hannacliengine/master/preview-images/studio_run_prev.jpg)You can also go into debug mode (which just shows you more information like which sequence is running..... Gonna need to add more information in there)

The engine is compiled into a separate application which is used by the studio to run your game. 
This separate file is named "hannacli.exe". It takes in 2 parameters:

 1. The json file to be run.
 2. Indicating if it's going to run in debug mode (optional)

You can run a game from it's JSON file straight from the command line:
```console
C:/Debug> hannacli mygame.json debug
// the game starts running
```
The Studio doesn't have exporting to desired file and location yet :( I was working on so many other things and I hadn't done that yet. But whenever you run your game in the studio, it is exported to the parent directory of the studio as "export.json".

Most of the UI comes clear as soon as you understand all of the above!

### Project Meta Data
Your game project will have meta data of course. When you create a new project you give your game a title and an author..... there will also be a description in there.
The main properties of your game are:
 - **gameTitle** 
 - **gameAuthor**
 - **gameDesc** (the description of your game)
 - **gameContainers** (containers used in your game)
 - **startSq** (the start sequence of your game)
 - **sequences** (the sequences in your game)



### Sample Projects
There will be some sample projects the sample-projects directory......
You can take a look at those and figure out the ropes!
Hanna Studio files are under the extension .hprj.

### Nuget Packages and References
Hanna components use some libraries. Nuget packages used are included in the source as well as references.

### TO-DO
*These are some of the to-dos for Hanna*
- [ ] Add a UML Diagram Storyboard into the studio.
- [ ] Add container value disposal.
- [ ] Add multiple conditionals to choices.
- [ ] Add proper game exporting.

***
*This entire project is not complete nor does it meet any professional standards in terms of code or anything. The studio may have lots of bugs as well as the engine itself.... There is always room for improvement :)*
***

### CONTRIBUTION
Go ahead and take a look at the source code! If you have any idea, fixes, whatever if may be...... feel free to contribute to this engine!
Looking forward to people creating some awesome games with it!
