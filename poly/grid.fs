# version 330

uniform vec2 res;
uniform float scale;

out vec4 finalColor;

void main() {
    vec2 uv = gl_FragCoord.xy / res;
		float aspect = res.x/res.y;
	
		vec2 center = vec2(0.5 * aspect, 0.5);
		uv -= center;
		uv *= scale;
		
    vec2 grid = fract(uv);
		float thick = 0.0015 * scale;
    float line = min(step(thick, grid.x), step(thick, grid.y));
    
    finalColor = vec4(vec3(max(0.7, line)), 1.0);
}
