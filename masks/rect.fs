# version 330

uniform float size;
uniform float sharp;
uniform vec2 res;

in vec2 fragTexCoord;
in vec4 fragColor;
out vec4 finalColor;
uniform sampler2D texture0;


void main() {
	vec4 texColor = texture(texture0, fragTexCoord);
	vec2 uv = gl_FragCoord.xy / res;
	float aspect = res.x / res.y;
	uv.x *= aspect;

	vec2 center = vec2(0.5 * aspect, 0.5);
	float dist = max(0.0, max(0.5 - uv.x, max(uv.x - 0.5, max(0.5 - uv.y, uv.y - 0.5))));
	
	finalColor = vec4(texColor.xyz, 1 - sharp/size * (dist - size));
}
