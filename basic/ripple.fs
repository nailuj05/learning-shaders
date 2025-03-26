#version 330

out vec4 FragColor;
uniform float time;
uniform vec2 res;

void main() {
		vec2 uv = gl_FragCoord.xy / res;
		// vec2 center = vec2(0.5, 0.5);
		// float dist = length(uv - center) * 10;
		// float ripple = sin(distance(uv, center) - time) * 0.05;
		FragColor = vec4(uv, 0.25, 1);
}