#version 330

out vec4 FragColor;
uniform float time;
uniform vec2 res;

vec2[] points = vec2[4](vec2(0.0, 0.0),vec2(0.0, 1.0),vec2(1.0, 0.0),vec2(1.0, 1.0));

void main() {
	vec2 uv = gl_FragCoord.xy / res;
	float aspect = res.x / res.y;
	uv.x *= aspect;

	for(int i = 0; i < 4; i++) {
		vec2 center = vec2(points[i].x * aspect, points[i].y);
		float dist = length(uv - center) * 8;
		float ripple = sin(dist * 25 - (time + i) * 3);
		float decay = exp(-dist * 0.8);
		uv += ripple * normalize(uv - center) * decay;
	}
	FragColor = vec4(uv, 0.5, 1);
}
