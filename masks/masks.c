// ---------- Masks ----------- //

#include <stdlib.h>
#include <raylib.h>
#include "../noob.h"
#include "../shared/reload.h"
#define RAYGUI_IMPLEMENTATION
#include "../shared/raygui.h"

#define FONTSIZE 30

char *shaders[] = {"circle.fs", "rect.fs", NULL };
Vector2 res = {1000, 1000};

int GetNextShader();
void LoadDroppedTex();
void DoUI(float*, float*);

int main(int argc, char **argv) {
	if (argc == 0) exit(1);
	_noob_set_wdir(argv[0]);
	
	SetTargetFPS(60);
	InitWindow(res.x, res.y, "Masks");

	GuiLoadStyle("cyber.rgs");
	GuiSetStyle(DEFAULT, TEXT_SIZE, FONTSIZE);

	Texture2D texture = LoadTexture("tex.png");
	Rectangle rect = {0, 0, 800, 600};
	float size = 0.25f, sharp = 2.0f;
	int res_loc, sharp_loc, size_loc;

	res.x = texture.width;
	res.y = texture.height;
	rect.width = res.x;
	rect.height = res.y;
	SetWindowSize(res.x, res.y);

	while (!WindowShouldClose()) {
		// Switch and/or reload shader (when modified)
		int f = GetNextShader();
		if(ReloadShader(shaders, f)) {
			res_loc = GetShaderLocation(shader, "res");
			size_loc = GetShaderLocation(shader, "size");
			sharp_loc = GetShaderLocation(shader, "sharp");
		}

		SetShaderValue(shader, res_loc, &res, SHADER_UNIFORM_VEC2);
		SetShaderValue(shader, size_loc, &size, SHADER_UNIFORM_FLOAT);
		SetShaderValue(shader, sharp_loc, &sharp, SHADER_UNIFORM_FLOAT);
		
		// Draw Shader
		BeginDrawing();
		ClearBackground(BLACK);

		BeginShaderMode(shader);
		DrawTexture(texture, 0, 0, WHITE);
		EndShaderMode();

		DoUI(&size, &sharp);
		
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

void DoUI(float *size, float *sharp) {
	GuiDrawRectangle((Rectangle){10, 10, 360, 91}, 1, BLACK, BLACK);
	
	Rectangle r1 = {160, 20, 200, 30};
	Rectangle r2 = {160, 60, 200, 30};
	GuiSlider(r1, "Size: ", "", size, 0.0f, 0.75f);
	GuiSlider(r2, "Shapness: ", "", sharp, 0.1f, 10.0f);
}
