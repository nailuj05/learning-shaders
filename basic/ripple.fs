#version 330

out vec4 FragColor;
uniform float time;
uniform vec2 res;

void main() {
		vec2 uv = gl_FragCoord.xy / res;
		float aspect = res.x / res.y;
		uv.x *= aspect;

		vec2 center = vec2(0.5 * aspect, 0.5);
		float dist = length(uv - center) * 10;
		float ripple = sin(dist * 20 - time * 4);
		uv += ripple * normalize(uv - center);

		FragColor = vec4(uv, 0.5, 1);
}
