#include <stdlib.h>
#include <raylib.h>
#include <rlgl.h>
#include "../noob.h"

Vector2 res = {2300, 1000};

int main(int argc, char** argv) {
	if (argc == 0) exit(1);
	_noob_set_wdir(argv[0]);

	SetTargetFPS(60);
	InitWindow(res.x, res.y, "Raymarching");
	SetWindowState(FLAG_WINDOW_RESIZABLE);
	
	Shader shader = LoadShader(NULL, "raymarch.fs");
  int res_loc = GetShaderLocation(shader, "res");

	Rectangle rect = {0};
	
	while (!WindowShouldClose()) {
		res.x = GetScreenWidth();
		res.y = GetScreenHeight();
		rect.width = res.x;
		rect.height = res.y;

		SetShaderValue(shader, res_loc, &res, SHADER_UNIFORM_VEC2);

		BeginDrawing();
		BeginShaderMode(shader);
		DrawRectangleRec(rect, WHITE);
		EndShaderMode();
		EndDrawing();
	}
}
