#include "noob.h"

#define RLBUILD "-lraylib -lGL -lm -lpthread -ldl -lrt -lX11"
#define CFLAGS "-Wpedantic -Wextra -Werror -Ishared -O2"

#define BASIC "basic/basic"
#define MASKS "masks/masks"
#define POLY "poly/poly"
#define GOL "game-of-life/gol"
#define RAYM "raymarching/raymarch"
#define NOISE "noise/noise"

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
	else if (noob_has_flag(argc, argv, "poly")) {
		noob_run("cc "CFLAGS" "POLY".c -o "POLY" "RLBUILD);
		if (run) noob_run("./"POLY);
	}
	else if (noob_has_flag(argc, argv, "gol")) {
		noob_run("cc "CFLAGS" "GOL".c -o "GOL" "RLBUILD);
		if (run) noob_run("./"GOL);
	}
	else if (noob_has_flag(argc, argv, "raymarch")) {
		noob_run("cc "CFLAGS" "RAYM".c -o "RAYM" "RLBUILD);
		if (run) noob_run("./"RAYM);
	}
	else if (noob_has_flag(argc, argv, "noise")) {
		noob_run("cc "CFLAGS" "NOISE".c -o "NOISE" "RLBUILD);
		if (run) noob_run("./"NOISE);
	}

  return 0;
}
