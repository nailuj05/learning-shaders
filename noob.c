#include "noob.h"

#define RLBUILD "-lraylib -lGL -lm -lpthread -ldl -lrt -lX11"
#define CFLAGS "-Wpedantic -Wextra -Werror -Ishared"

#define BASIC "basic/basic"
#define MASKS "masks/masks"
#define GOL "game-of-life/gol"

int main(int argc, const char **argv) {
  noob_rebuild_yourself(argc, argv);

	if (noob_help(argc, argv, "usage: ./noob <shader-exercise>",  "-h : help"))
		exit(0);

	int run = noob_has_flag(argc, argv, "run");
	
  if (noob_has_flag(argc, argv, "basic")) {
		noob_run("cc "CFLAGS" "BASIC".c -o "BASIC" "RLBUILD);
		if (run) noob_run("./"BASIC);
  }
	else if (noob_has_flag(argc, argv, "masks")) {
		noob_run("cc "CFLAGS" "MASKS".c -o "MASKS" "RLBUILD);
		if (run) noob_run("./"MASKS);
	}
	else if (noob_has_flag(argc, argv, "gol")) {
		noob_run("cc "CFLAGS" "GOL".c -o "GOL" "RLBUILD);
		if (run) noob_run("./"GOL);
	}

  return 0;
}
