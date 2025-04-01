# version 330

out vec4 FragColor;
uniform float time;
uniform vec2 res;

//https://iquilezles.org/articles/palettes/
vec3 palette( float t ) {
    vec3 a = vec3(0.5, 0.5, 0.5);
    vec3 b = vec3(0.5, 0.5, 0.5);
    vec3 c = vec3(1.0, 1.0, 1.0);
    vec3 d = vec3(0.263,0.416,0.557);

    return a + b*cos( 6.28318*(c*t+d) );
}

void main() {
	vec2 uv = gl_FragCoord.xy / res;
	vec2 uv0 = uv - 0.5;
		
	float aspect = res.x / res.y;
	uv.x *= aspect;
	uv = fract(uv * 4) - 0.5;

	// distance based color with a little distortion
	float dist = length(uv) + length(uv0) * 0.5;
	vec3 col = palette(length(uv0) + time);
		
	dist = sin(dist * 7. + time) / 7.;
	dist = 0.02 / abs(dist);
	
	FragColor = vec4(col * dist, 1);
}
