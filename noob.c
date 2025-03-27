#include "noob.h"

#define RLBUILD "-lraylib -lGL -lm -lpthread -ldl -lrt -lX11"

#define BASIC "basic/basic"

int main(int argc, const char **argv) {
  noob_rebuild_yourself(argc, argv);

	if (noob_help(argc, argv, "usage: ./noob <shader-exercise>",  "-h : help"))
		exit(0);

	int run = noob_has_flag(argc, argv, "run");
	
  if (noob_has_flag(argc, argv, "basic")) {
		noob_run("cc -Wall -Wextra -Werror -Ishared "BASIC".c -o "BASIC" "RLBUILD);
		if (run) noob_run("./"BASIC);
  }

  return 0;
}
