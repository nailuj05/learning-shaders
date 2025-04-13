#version 330

in vec3 fragNormal;

vec3 light = vec3(-1,1,-1);

out vec4 fragColor;

void main() {
	float diff = max(dot(fragNormal, normalize(light)), 0.0);
	fragColor = vec4(vec3(diff + 0.2), 1.0);
}
