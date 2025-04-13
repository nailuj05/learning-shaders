#version 330

uniform float time;
uniform vec2 res;

out vec4 finalColor;

vec2 random_vec2(vec2 st, float seed) {
	st += vec2(seed);
	return fract(sin(vec2(dot(st, vec2(12.9898, 78.233)),
									      dot(st, vec2(127.1, 311.7)))) * 43758.5453123);
}

float voronoi(vec2 uv, float seed) {
    vec2 i_uv = floor(uv);
    vec2 f_uv = fract(uv);

    float minDist = 1.0;
    for(int y = -1; y <= 1; y++) {
        for(int x = -1; x <= 1; x++) {
            vec2 neighbor = vec2(float(x), float(y));
            vec2 point = random_vec2(i_uv + neighbor, seed);
            vec2 diff = neighbor + point - f_uv;
            float dist = length(diff);
            minDist = min(minDist, dist);
        }
    }
    return minDist;
}

void main() {
	vec2 uv = gl_FragCoord.xy / res * 50;

	float seed = 64.234;
	float v = voronoi(uv, seed);
	
	finalColor = vec4(vec3(v), 1.0);
}
