# version 330

uniform vec2 res;
uniform float time;

out vec4 finalColor;

vec3 palette( float t ) {
    vec3 a = vec3(0.5, 0.5, 0.5);
    vec3 b = vec3(0.5, 0.5, 0.5);
    vec3 c = vec3(1.0, 0.7, 0.4);
    vec3 d = vec3(0.0,0.15,0.25);

    return a + b*cos( 6.28318*(c*t+d) );
}

void main() {
	vec2 uv = gl_FragCoord.xy;
	float cx = (2 * uv.x - res.x) / res.y;
	float cy = (2 * uv.y - res.y) / res.y;
	float d = sqrt(cx * cx + cy * cy);
	d -= 0.5;
	d += 0.01 * res.y / (2 * (uv.x - uv.y) + res.y - res.x);
  d = 0.1/abs(d);
	finalColor = vec4(palette(clamp(0.0,1.0,d)), 1.0);
}
