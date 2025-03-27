#ifndef RELOAD_H
#define RELOAD_H

#include <raylib.h>
#include "../noob.h"

size_t si = 0;

Shader shader;
int last_modified = 0;
int time_loc, res_loc;

void ReloadShader(char **shaders,int force) {
	int modified = noob_get_last_modified(shaders[si]);
	if(modified > last_modified || force) {
		last_modified = modified;
		Shader new_shader = LoadShader(0, shaders[si]);
		if (new_shader.id != 0) {
			UnloadShader(shader);
			shader = new_shader;
			time_loc = GetShaderLocation(shader, "time");
			res_loc = GetShaderLocation(shader, "res");
			printf("shader reloaded\n");
		} else {	
			printf("shader reloading failed\n");
		}
	}
}

#endif
