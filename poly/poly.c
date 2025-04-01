// ------------- Polynomial -------------- //
// I wanted to try out plotting a polynomial function in a shader.
// This approach will work with any function as long as there exists a derivative.
// A shader actually lends itself somewhat well to this, as it is easily done in parallel
// and you get zooming/variable resolutions of a function for "free"

#include <stdlib.h>
#include <raylib.h>
#include "../noob.h"
#include "../shared/reload.h"
#define RAYGUI_IMPLEMENTATION
#include "../shared/raygui.h"

#define FONTSIZE 30

char *shaders[] = {"poly.fs" };
Vector2 res = {1000, 1000};

int main(int argc, char** argv) {
	if (argc == 0) exit(1);
	_noob_set_wdir(argv[0]);
	
	SetTargetFPS(60);
	InitWindow(res.x, res.y, "Poly");
	SetWindowState(FLAG_WINDOW_RESIZABLE);

	ReloadShader(shaders, 0);
	int res_loc = GetShaderLocation(shader, "res");
	int scale_loc = GetShaderLocation(shader, "scale");
	int thick_loc = GetShaderLocation(shader, "thick");
	float scale = 4.0;
	float thick = 0.005;
	
	GuiSetStyle(DEFAULT, TEXT_SIZE, FONTSIZE);

	while (!WindowShouldClose()) {
		ReloadShader(shaders, 0);

		res.x = GetScreenWidth();
		res.y = GetScreenHeight();

		SetShaderValue(shader, res_loc, &res, SHADER_UNIFORM_VEC2);
		SetShaderValue(shader, scale_loc, &scale, SHADER_UNIFORM_FLOAT);
		SetShaderValue(shader, thick_loc, &thick, SHADER_UNIFORM_FLOAT);

		BeginDrawing();
		ClearBackground(BLACK);

		BeginShaderMode(shader);
		DrawRectangle(0, 0, res.x, res.y, WHITE);
		EndShaderMode();

		Rectangle r1 = {100, 10, 200, 30};
		GuiSlider(r1, "Scale: ", "", &scale, 0.1f, 10.0f);
		Rectangle r2 = {100, 50, 200, 30};
		GuiSlider(r2, "Line: ", "", &thick, 0.001f, 0.01f);
			
		EndDrawing();
	}
}
