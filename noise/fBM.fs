#version 330

uniform float time;
uniform vec2 res;

out vec4 finalColor;

float random(vec2 st) {
	return fract(sin(dot(st.xy + 242.32, vec2(12.9898, 78.233))) * 43758.5453123);
}

// Based on Morgan McGuire @morgan3d
// https://www.shadertoy.com/view/4dS3Wd
float noise (in vec2 _st) {
    vec2 i = floor(_st);
    vec2 f = fract(_st);

    // Four corners in 2D of a tile
    float a = random(i);
    float b = random(i + vec2(1.0, 0.0));
    float c = random(i + vec2(0.0, 1.0));
    float d = random(i + vec2(1.0, 1.0));

    vec2 u = f * f * (3.0 - 2.0 * f);

    return mix(a, b, u.x) +
            (c - a)* u.y * (1.0 - u.x) +
            (d - b) * u.x * u.y;
}

#define OCTAVES 8

float fBM(in vec2 st) {    
	float value = 0.0;
	float amplitude = .5;
	
	for (int i = 0; i < OCTAVES; i++) {
		value += amplitude * noise(st);
		st *= 2.;
		amplitude *= .5;
	}
	return value;
}

void main() {
	vec2 uv = gl_FragCoord.xy / res;
	uv.x *= res.x/res.y;
	uv += vec2(0.5);
	
	vec2 p = uv * 4.0;
	finalColor = vec4(vec3(fBM(p)), 1.0);
}
