#version 330

uniform float time;
uniform vec2 res;

out vec4 finalColor;

float random(vec2 st) {
    return fract(sin(dot(st.xy, vec2(12.9898, 78.233))) * 43758.5453123);
}

void main() {
	vec2 uv = gl_FragCoord.xy / res;
	finalColor = vec4(vec3(random(uv)), 1.0);
}
