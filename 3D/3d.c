#include <stdlib.h>
#include <raylib.h>
#include <raymath.h>
#include "../noob.h"
#include "../shared/reload.h"

Vector2 res = {1000, 1000};

int main(int argc, char **argv) {
	if (argc == 0) exit(1);
	_noob_set_wdir(argv[0]);

	SetTargetFPS(60);
	InitWindow(res.x, res.y, "3D");
	SetWindowState(FLAG_WINDOW_RESIZABLE);

	Shader shader = LoadShader("simple.vs", "simple.fs");
	int modelLoc = GetShaderLocationAttrib(shader, "matModel");

	Matrix modelMatrix = MatrixIdentity();
	SetShaderValueMatrix(shader, modelLoc, modelMatrix);
	
	Mesh sphere = GenMeshSphere(1.0f, 32, 32);
	Model model = LoadModelFromMesh(sphere);
	model.materials[0].shader = shader;
	
	Camera3D cam = {0};
	cam.position = (Vector3){0.0f, 0.0f, -5.0f}; 
	cam.target =   (Vector3){0.0f, 0.0f, 0.0f};      
	cam.up =       (Vector3){0.0f, 1.0f, 0.0f};          
	cam.fovy = 45.0f;                                
	cam.projection = CAMERA_PERSPECTIVE;	
		
	while (!WindowShouldClose()) {
		BeginDrawing();
		ClearBackground(BLACK);
		BeginMode3D(cam);
		
		DrawModel(model, (Vector3){0,0,0}, 1.0f, WHITE);

		EndMode3D();
		EndDrawing();
	}

	UnloadModel(model);
	UnloadShader(shader);
	CloseWindow();

	return 0;
}
