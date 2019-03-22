#include "HannaCLIEngine.h"

int main(int argc, char* argv[])
{
	HannaCLIEngine engine;
	// loading game file from first argument

	if (argc <= 1) {
		std::cout << "No game file" << std::endl;
		return 0;
	}

	if (engine.loadGameFile(argv[1])) {
		// check if we're in debug mode
		bool debug = false;
		if (argc > 2) {
			std::string arg(argv[2]);
			if (arg == "debug") {
				debug = true;
				std::cout << "Debug mode active" << std::endl;
			}
		}
		// set the engine debug mode
		engine.setDebugMode(debug);

		// clear the console
		//system("CLS");
		// display game meta
		engine.displayMeta();

		// you could even run any sequence and it would pick up from there
		// which pegs the idea of save files.... we would need to save objects as well.
		// could probably use a menu too I think

		// trying to start a new game
		try {
			engine.startNewGame();
		}
		catch (...) {
			std::cout << "\nERROR SOMEWHERE: Something isn't right with your game. I don't know what it is but fix it!";
		}
	}
}