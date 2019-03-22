/*
HELLO HUMANS! DeanEncoded here!

This "Hanna" project was started from an idea about making a CLI based game.
You would essentially be making choices in the game and it would have alternate paths according to how you decide.

So instead of doing the game stand-alone..... I decided to make an "engine"... which gave the idea the power of custom games.
By building the project as an engine anyone could make their own CLI choice based game (or better known as Choose Your Own Adventure game).

I gave thought to the idea and made this "engine" in c++.... I hadn't written anything in c++ ever. This was my first c++ project. I was learning about c++ while making this.
This engine would take in a JSON file that contained all the data for a single game.

At first I just thought if someone was to write a game they'd just take a "template" JSON file and edit it to make their game... but then that
would be kind of hard and time consuming.

So I decided to make a Studio for the engine as well. Where one can simply create a game for the engine with a GUI!

WELL... that's how Hanna came to be.....
bye...
*/
#pragma once
#include <iostream>
#include <string>
#include <sstream>
#include <vector>
#include <fstream>
#include <stdlib.h>
#include <Windows.h>
#include <nlohmann/json.hpp>
class HannaCLIEngine
{
private:
	// these is meta data for a game
	std::string gameTitle, gameAuthor, gameDesc, startSq;

	// debug mode?
	bool debug = false;
	// debug mode should display some information to the user that isn't displayed when a game is not in debug mode.

	// A map for mapping our sequence ids to our sequence indexes
	std::map<std::string, int> sequenceMapper;
	std::map<std::string, int> choiceMapper;
	nlohmann::json sequences;

	// for storing any gameContainers e.g Inventory
	// basically a map of vectors (map of arrays) value[indexes pointing to array value]
	std::map<std::string, std::vector<std::string>> gameContainers;

	// for storing valid choice letters for the user... they can only choose letters in this variable
	std::vector<std::string> validChoiceLetters;
	// for storing current sequence id for engine debug reference
	std::string currentSequenceId;

public:
	HannaCLIEngine()
	{
		
	}

	bool loadGameFile(std::string gameFile) {

		using namespace nlohmann;

		std::ifstream fileInput;
		fileInput.open(gameFile);

		if (!fileInput.is_open()) {
			std::cout << "Couldn't open that file. Check your file please." << std::endl;
			return false;
		}else {
			std::string inputStr((std::istreambuf_iterator<char>(fileInput)), std::istreambuf_iterator<char>());
			json data;
			data = json::parse(inputStr);

			// load game meta data
			gameTitle = data["gameTitle"].get<std::string>();
			gameAuthor = data["gameAuthor"].get<std::string>();
			gameDesc = data["gameDesc"].get<std::string>();
			startSq = data["startSq"].get<std::string>();

			// for storing our sequences/levels
			sequences = data["sequences"];

			// lets start mapping sequence id's to their indexes
			for (int i = 0; i < sequences.size(); i++) {
				json tempSq = sequences[i];
				sequenceMapper.insert(std::pair<std::string, int>(tempSq["sqId"].get<std::string>(), i));
			}

			// lets find our containers!
			json containers = data["gameContainers"];
			for (int i = 0; i < containers.size(); i++) {
				// containers don't have limits for now
				// I was planning on adding limits for them for use in gameplay.
				// Right now as I'm writing this 17/03/2019 2109hrs I'm not adding limits.
				// std::map<std::string, std::vector<std::string>>
				std::vector<std::string> tempElementsContainer;
				// lets push some random value to it!
				//tempElementsContainer.push_back("hey");
				//tempElementsContainer.push_back("hey");

				gameContainers.insert(std::pair<std::string, std::vector<std::string>>(containers[i].get<std::string>(), tempElementsContainer));

			}

			// lets map choiceMapper [ just using lowercase letters ]
			choiceMapper.insert(std::pair<std::string, int>("a", 0));
			choiceMapper.insert(std::pair<std::string, int>("b", 1));
			choiceMapper.insert(std::pair<std::string, int>("c", 2));
			choiceMapper.insert(std::pair<std::string, int>("d", 3));

			// test gameContainers
			//std::cout << gameContainers["inventory"][1] << std::endl;
			// THEY'RE WORKING ALRIGHT.


			//std::cout << sequences[0]["hey"] << std::endl
			return true;
		}
	}

	// our sequences already map their indexes to their unique ids.... so we may just need an id to launch a sequence.
	void runSequence(std::string sqId) {
		using namespace nlohmann;

		// clear the screen??? If not in debug mode
		if (!debug) system("CLS");

		int sqIndex = sequenceMapper[sqId];
		json sequence = sequences[sqIndex];

		showText("--------------------------------------------------------\n", 3, true);

		// set cu
		currentSequenceId = sequence["sqId"].get<std::string>();

		// if we're in debug mode... show the user the sequence id of the current sequence
		if(debug) showText("Current Sequence > " + currentSequenceId + "\n", 14, true);

		// show mainText
		showText(sequence["mainText"].get<std::string>() + "\n", 3, true);
		// subsequently show the secondary text
		showText(sequence["secondaryText"].get<std::string>(), 3, true);

		// lets find out the type of sequence we're currently using
		std::string sqType = sequence["sqType"].get<std::string>();

		// clear validChoiceLetters
		validChoiceLetters.clear();
		if (sqType == "ordinary") {
			// this is an ordinary sequence. Roll with it boy!

			// now onto choices.
			// there are different types of choices
			// loop through choices figuring out which ones to display and which ones not to display according to their type and all that
			json choices = sequence["choices"];
			for (int i = 0; i < choices.size(); i++) {
				// get the choice type
				json choice = choices[i];

				if (choice["choiceType"].get<std::string>() == "set") {
					// it's ok just display this choice no worries

					// add the choice letter to validChoiceLetters? I guess so
					validChoiceLetters.push_back(choice["choiceLetter"]);
					showText(choice["choiceLetter"].get<std::string>() + ". " + choice["choiceText"].get<std::string>(),5,true);
				}
				else if (choice["choiceType"].get<std::string>() == "conditional") {
					// only show the text if it's condition is met! no choices
					json condition = choice["choiceCondition"];
					std::string conditionContainer = condition["container"].get<std::string>();
					std::string conditionValue = condition["value"].get<std::string>();

					// now check the users gameContainers[container] for conditionValue

					//std::map<std::string, std::vector<std::string>> gameContainers;
					for (int o = 0; o < gameContainers[conditionContainer].size(); o++) {
						// looping through all the items in users container (according to the condition)
						if (gameContainers[conditionContainer][o] == conditionValue) {
							// condition has been met. Display the choice!
							showText(choice["choiceLetter"].get<std::string>() + ". " + choice["choiceText"].get<std::string>(), 5, true);
							// should probably use a function for displaying choices. YOLO!
							// add the choice letter to validChoiceLetters? I guess so
							validChoiceLetters.push_back(choice["choiceLetter"]);
						}
					}
				}
			}
			// after choices have been displayed. Allow the user to make a choice
			makeChoice(choices);

		}
		else if (sqType == "end") {
			// this type of sequence is the end of a game!
			// mainText and secondaryText have already been displayed.
			showText("THE END THANKS FOR PLAYING", 2, true);
		}	
	}

	void startNewGame() {
		// run game from startSq value;
		runSequence(startSq);
	}


	void displayMeta() {
		using namespace std;
		cout << "Game Title : " << gameTitle << endl;
		cout << "Written by : " << gameAuthor << endl;
		cout << "\n \n";
	}

	void setDebugMode(bool d) {
		debug = d;
	}

	private:
		void makeChoice(nlohmann::json choices) {

			// what if validChoiceLetters has nothing????
			// e.g a user didn't put choices on a sequence... and that sequence is not an end sequence?
			// well tell the user the sequence doesn't have any choices and end the game

			if (validChoiceLetters.size() < 1) {
				// no valid choices in a normal sequence
				showText("\nThere are no valid choices in this sequence(" + currentSequenceId + "). Please add some choices or mark as an end sequence", 3, true);
				// the user may also have a problem where they only have conditional choices that the user may not meet at all in their game
				return; // exit and that's that for that!
			}

			showInputText("Your choice : "); //secondary text does something similar. But I'mma leave this here for now
			std::string userInput = getUserInput();
			// convert this userInput to lowercase
			std::for_each(userInput.begin(), userInput.end(), [](char& c) {
				c = ::tolower(c);
			});

			// IMPORTANT!! THERE SHOULD BE A BOOLEAN TO DETERMINE WHETHER CHOICE IS A SET OR CONDITIONAL. SO THAT WE DON'T DO SOME THINGS
			// or well... Thats not important anymore.... I didn't use a boolean.. the choice type ended up getting that I guess

			// we have letters mapped to choice indexes already


			// IMPORTANT!! what if the player chooses a letter that didn't appear for them????
			// maybe we should have an array that stores letters we can pick.... if the users letter doesn't match any of those
			// then tell them that they can't make that choice.... then restart makeChoices with the same choices as parameters
			
			// use this bool to determine when we find a valid choice letter
			bool foundValid = false;

	
			for (int i = 0; i < validChoiceLetters.size(); i++) {
				// check if the choice letter inputted by the user is valid as all
				// convert the choiceLetter here to lowercase as well eh???
				std::string validChoiceLetter = validChoiceLetters[i];
				std::for_each(validChoiceLetter.begin(), validChoiceLetter.end(), [](char& c) {
					c = ::tolower(c);
					});
				if (userInput == validChoiceLetter) {
					// the user did select a valid choice letter
					foundValid = true;
					break; // exit the for loop
				}
			}

			if (foundValid) {

				//Show choice outcome Text
				int choiceIndex = choiceMapper[userInput];
				showText(choices[choiceIndex]["outcomeText"], 3, true);

				// do we have a containerAdd for this choice????
				nlohmann::json containerAdd = choices[choiceIndex]["containerAdd"];

				if (containerAdd["container"].get<std::string>() != "hnull") {
					// this has a container add!
					std::string containerToAdd = containerAdd["container"].get<std::string>();
					// working with: value and actionText
					std::vector<std::string> tempElementsContainer = gameContainers[containerToAdd];
					tempElementsContainer.push_back(containerAdd["value"].get<std::string>());
					gameContainers[containerToAdd] = tempElementsContainer;
					// value added

					//showText(containerAdd["value"].get<std::string>() + " " + containerAdd["actionText"].get<std::string>() + " " + containerToAdd, 5, true);
					// this "ActionText of our may be very well redundant..... since we have outcomeText to tell us what happened.
					// I commented out the displaying of actionText. And it must be removed from the studio
				};
				// continue to nextSq of the choice made

				// press enter to continue bit
				showText("\nEnter to continue...", 3, true);
				std::string petc = getUserInput();

				if (choices[choiceIndex]["nextSq"].get<std::string>() == "") {
					if (debug) showText("Hmmmmm. Choice letter " + choices[choiceIndex]["choiceLetter"].get<std::string>() + " (sequence: " + currentSequenceId + ") doesn't have a next sequence.", 3, true);
					else showText("Hmmm. There's no sequence to go to.......",3,true);
					return;
				}
				else {
					runSequence(choices[choiceIndex]["nextSq"].get<std::string>());
				}
			}
			else {
				// choice is not valid
				showText("\nThat's not a valid choice!", 3, true);
				makeChoice(choices);
				return; // then just end this
				
			}

		}


	// getting any user input
	std::string getUserInput() {
		std::string userInput;
		std::getline(std::cin, userInput);
		return userInput;
	}

	void showText(std::string text, int consoleTextColor, bool endl) {
		SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), consoleTextColor);
		std::cout << text;
		if (endl) { std::cout << "\n"; }
	}

	// Using this second one for different color input. Don't @ me 
	void showInputText(std::string text) {
		SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), 8);
		std::cout << text;
		SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), 4);
	}
};