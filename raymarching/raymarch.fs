# version 330

uniform vec2 res;
out vec4 finalColor;

float sd_sphere(vec3 p, float r) {
	return length(p) - r;
}

void main() {
	vec2 uv = gl_FragCoord.xy / res;
	float aspect = res.x / res.y;
	uv.x *= aspect;
	uv -= vec2(0.5 * aspect, 0.5);
	
	vec3 ro = vec3(0, 0, -3);
	vec3 rd = normalize(vec3(uv, 1));
	
	vec3 col = vec3(0);
	
	float d = 0.0;
	
	for (int i = 0; i < 50; ++i) { 
		vec3 p = ro + rd * d;
		float c = sd_sphere(p - vec3(0,0,5), 2);
		d += c;

		if (c < 0.05)
			break;
	}

	col = vec3(d * 0.1);
	
	finalColor = vec4(col, 1.0);
}
