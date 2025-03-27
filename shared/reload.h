#ifndef RELOAD_H
#define RELOAD_H

#include <raylib.h>
#include "../noob.h"

size_t si = 0;

Shader shader;
int last_modified = 0;

int ReloadShader(char **shaders,int force) {
	int modified = noob_get_last_modified(shaders[si]);
	if(modified > last_modified || force) {
		last_modified = modified;
		Shader new_shader = LoadShader(0, shaders[si]);
		if (new_shader.id != 0) {
			UnloadShader(shader);
			shader = new_shader;
			printf("shader reloaded\n");
			return 1;
		} else {	
			printf("shader reloading failed\n");
			return 0;
		}
	}
	return 0;
}

#endif
