# version 330

uniform vec2 res;
uniform float scale;
uniform float thick;

out vec4 finalColor;

float f(float x) {
	return pow(x, 3);
}

float df(float x) {
	return 3 * pow(x, 2);
}

void main() {
	vec2 uv = gl_FragCoord.xy / res * scale;
	float aspect = res.x/res.y;
	uv.x *= aspect;
	float frag_y = gl_FragCoord.y; 
	
	vec2 center = vec2(0.5 * aspect, 0.5) * scale;

	// function, upper and lower bound
	float y = f(uv.x - center.x) + center.y;
	float lb = y - thick * (df(uv.x - center.x) + center.y);
	float ub = y + thick * (df(uv.x - center.x) + center.y);

	float t = smoothstep(y, lb, uv.y) + 1 - smoothstep(ub, y, uv.y);
	finalColor = vec4(t);
}
