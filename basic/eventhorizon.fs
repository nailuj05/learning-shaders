# version 330

uniform vec2 res;
uniform float time;

out vec4 finalColor;

void main() {
	vec2 uv = gl_FragCoord.xy;
	float cx = (2 * uv.x - res.x) / res.y;
	float cy = (2 * uv.y - res.y) / res.y;
	float d = sqrt(cx * cx + cy * cy);
	d -= 0.5;
	d += 0.01 * res.y / (2 * (uv.x - uv.y) + res.y - res.x);
  d = 0.1/abs(d);
	finalColor = vec4(vec3(d), 1.0);
}
