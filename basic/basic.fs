#version 330

out vec4 FragColor;
uniform float time;
uniform vec2 res;

void main() {
		vec2 uv = gl_FragCoord.xy / res;
		// FragColor = vec4(uv + 0.5 * sin(time + uv.x * 10), 0.5, 1);
		// FragColor = vec4(sin(time) * uv.x, uv.y, 0.5, 1);
		float vignette = 1.0 - distance(uv, vec2(0.5));
		float checker = mod(floor(uv.x * 8) + floor(uv.y * 6), 2) + sin(time - uv.x);
		FragColor = vec4(uv, 0.5, 1) * checker * vignette;
}