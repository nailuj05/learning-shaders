#version 330

in vec2 fragTexCoord;
out vec4 finalColor;

uniform sampler2D texture0;

bool getAlive(vec2 coord) {
	return texture(texture0, vec2(coord.x, 1.0 - coord.y)).r > 0.01;
}

void main()
{
	vec2 texelSize = 1.0 / vec2(textureSize(texture0, 0));

	vec2 offsets[8] =
		vec2[8](
						vec2(-texelSize.x, -texelSize.y), vec2(0.0, -texelSize.y), vec2(texelSize.x, -texelSize.y),
						vec2(-texelSize.x, 0.0),          /* ------self------ */   vec2(texelSize.x, 0),
						vec2(-texelSize.x, texelSize.y),  vec2(0.0, texelSize.y),  vec2(texelSize.x, texelSize.y)
						);


	bool alive = getAlive(fragTexCoord);
	int neighbors = 0;
	for(int i = 0; i < 8; ++i) {
		neighbors += getAlive(mod(fragTexCoord + offsets[i], vec2(1.0))) ? 1 : 0;
	}

	float newState = float((neighbors == 3) || (alive && neighbors == 2));
	finalColor = vec4(newState);
}
