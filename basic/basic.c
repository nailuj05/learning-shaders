// ---------- Basics -----------
// Here I explore some simple shaders to get started.
// This C program is just a wrapper using raylib to
// get a window in which I can draw with a shader.  Shaders themselves
// are the .fs (fragement shader) files in this folder.  Cycle through
// shaders using SPACE, hide UI with H

#include <stdlib.h>
#include <raylib.h>
#include "../noob.h"
#include "../shared/reload.h"

#define FONTSIZE 30

char *shaders[] = {"basic.fs", "ripple.fs", "ripples.fs", "fract.fs", "frac.fs", "eventhorizon.fs", "xor_dev.glsl", NULL};
int time_loc, res_loc;

Vector2 res = {1000, 1000};

int GetNextShader();

int main(int argc, char **argv) {
	if (argc == 0) exit(1);
	_noob_set_wdir(argv[0]);

	SetTargetFPS(60);
	InitWindow(res.x, res.y, "Basic");
	SetWindowState(FLAG_WINDOW_RESIZABLE);
	
	Rectangle rect = {0, 0, 800, 600};
	int drawText = 1;

	float s_time = 0.0f;

	while (!WindowShouldClose()) {
		// Switch and/or reload shader (when modified)
		int f = GetNextShader();
		if(ReloadShader(shaders, f)) {
			time_loc = GetShaderLocation(shader, "time");
			res_loc = GetShaderLocation(shader, "res");
		}

		res.x = GetScreenWidth();
		res.y = GetScreenHeight();
		rect.width = res.x;
		rect.height = res.y;
		s_time += GetFrameTime();
		
		// update time and resolution in shader (pass from cpu)
		SetShaderValue(shader, time_loc, &s_time, SHADER_UNIFORM_FLOAT);
		SetShaderValue(shader, res_loc, &res, SHADER_UNIFORM_VEC2);

		// Draw Shader
		BeginDrawing();
		ClearBackground(BLACK);

		BeginShaderMode(shader);
		DrawRectangleRec(rect, WHITE);
		EndShaderMode();

		// Draw FPS and Shader name
		drawText = IsKeyPressed(KEY_H) ? !drawText : drawText;
		if(drawText) {
			int text_width = MeasureText(shaders[si], FONTSIZE) / 2;
			DrawText(shaders[si], res.x / 2 - text_width, 5, FONTSIZE, RED);
			DrawFPS(5, 5);
		}

		EndDrawing();
	}

	UnloadShader(shader);
	CloseWindow();
	return 0;
}

int GetNextShader() {
	if(IsKeyPressed(KEY_SPACE)) {
		si = shaders[si + 1] == NULL ? 0 : si + 1;
		return 1;
	}
	return 0;
}

