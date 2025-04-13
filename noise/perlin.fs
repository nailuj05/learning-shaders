#version 330

uniform float time;
uniform vec2 res;

out vec4 finalColor;

vec2 fade(vec2 t) {
    return t * t * t * (t * (t * 6.0 - 15.0) + 10.0);
}

float random(vec2 st, float seed) {
	st += vec2(seed);
	return fract(sin(dot(st.xy, vec2(12.9898, 78.233))) * 43758.5453123);
}

vec2 random_gradient(vec2 p, float seed) {
	float rnd = random(p, seed) * 6.28318;
	return vec2(cos(rnd), sin(rnd));
}

float perlin(vec2 p, float seed) {
	vec2 i = floor(p);
	vec2 f = fract(p);

	vec2 g00 = random_gradient(i + vec2(0.0, 0.0), seed);
	vec2 g10 = random_gradient(i + vec2(1.0, 0.0), seed);
	vec2 g01 = random_gradient(i + vec2(0.0, 1.0), seed);
	vec2 g11 = random_gradient(i + vec2(1.0, 1.0), seed);
	
	float d00 = dot(g00, f - vec2(0.0, 0.0));
	float d10 = dot(g10, f - vec2(1.0, 0.0));
	float d01 = dot(g01, f - vec2(0.0, 1.0));
	float d11 = dot(g11, f - vec2(1.0, 1.0));

	vec2 u = fade(f);
	
	float mixX0 = mix(d00, d10, u.x);
	float mixX1 = mix(d01, d11, u.x);
	return mix(mixX0, mixX1, u.y);
}

void main() {
	vec2 uv = gl_FragCoord.xy / res * 50.0;
	uv.x *= res.x/res.y;
	
	float seed = 543.32;
	float noise = perlin(uv, 4.543);
	
	finalColor = vec4(vec3(noise), 1.0);
}
