#include <stdlib.h>
#include <raylib.h>
#include <rlgl.h>
#include "../noob.h"

#define FONTSIZE 30

Vector2 res = {1000, 1000};
Vector2 renderRes = {250, 250};

Texture2D GenerateRandomStart();

int main(int argc, char **argv) {
	if (argc == 0) exit(1);
	_noob_set_wdir(argv[0]);

	SetTargetFPS(60);
	InitWindow(res.x, res.y, "Game of Life");
	
	Shader shader = LoadShader(NULL, "gol.glsl");
	
	// Create 2 rts, one for reading, one for writing
	RenderTexture2D bufs[2];
	bufs[0] = LoadRenderTexture(renderRes.x, renderRes.y);
	bufs[1] = LoadRenderTexture(renderRes.x, renderRes.y);
	
	BeginTextureMode(bufs[0]);
	ClearBackground(BLACK);
	DrawTexture(GenerateRandomStart(renderRes.x, renderRes.y), 0, 0, WHITE);
	EndTextureMode();
	
	int rid = 0, wid = 1;

	const float updateRate = 1.0f / 10.0f;
	float acc = 0.0f;
	
	while (!WindowShouldClose()) {
		float deltaTime = GetFrameTime();
    acc += deltaTime;
		
    if (acc >= updateRate) {
			while (acc >= updateRate) acc -= updateRate;
			rid = (rid + 1) % 2;
			wid = (wid + 1) % 2;
		}	

		// Draw Shader onto Render Texture
		BeginTextureMode(bufs[wid]);
  		ClearBackground(BLACK);
	  	BeginShaderMode(shader);
		    DrawTexture(bufs[rid].texture, 0, 0, WHITE);
   		EndShaderMode();
		EndTextureMode();

		// Draw Texture
		BeginDrawing();
		ClearBackground(BLACK);
	    DrawTextureEx(bufs[wid].texture, (Vector2){0,0}, 0, (res.x / renderRes.x), WHITE);
		EndDrawing();
	}

	UnloadShader(shader);
	CloseWindow();
	return 0;
}

Texture2D GenerateRandomStart() {
	return LoadTextureFromImage(GenImageWhiteNoise(renderRes.x, renderRes.y, 0.7f));
}
