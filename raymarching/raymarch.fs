# version 330

uniform float time;
uniform vec2 res;
out vec4 finalColor;

float sd_sphere(vec3 p, vec3 center, float radius) {
    return length(p - center) - radius;
}

vec4 smin(float a, vec3 ca, float b, vec3 cb, float k) {
    float h = clamp(0.5 + 0.5 * (b - a) / k, 0.0, 1.0);
		float d =  mix(b, a, h) - k * h * (1.0 - h);
		vec3 col = mix(ca, cb, h);
    return vec4(col, d);
}

vec4 map(vec3 p) {
	float d1 = sd_sphere(p, vec3(sin(time + 3.141) * 2, 0.0, 3.0), 1.0);
	float d2 = sd_sphere(p, vec3(sin(time) * 2, 0.0, 3.0), 1.0);
		vec4 rgbd = smin(d1, vec3(1.,0.,0.), d2, vec3(0.,1.,0.), 0.5);
    return rgbd;
}

vec3 normal(vec3 p) {
    float h = 0.001;
    vec2 k = vec2(1, -1);
    return normalize(k.xyy * map(p + k.xyy * h).w +
                     k.yyx * map(p + k.yyx * h).w +
                     k.yxy * map(p + k.yxy * h).w +
                     k.xxx * map(p + k.xxx * h).w);
}

vec4 raymarch(vec3 ro, vec3 rd, out vec3 p, float tmin = 0.001, float tmax = 50.0) {
	float t = 0.0;
	vec4 rgbd;
	
	for (int i = 0; i < 100; ++i) { 
		p = ro + rd * t;

		rgbd = map(p);
		float d = rgbd.w;
		
		t += d;
		
		if (d < tmin) break;
		if (t > tmax) break; 
	}
	rgbd.w = t;
	return rgbd;
}

void main() {
	vec3 sun = normalize(vec3(1, 4, -1));
	
	// initialize clip space 
	vec2 uv = gl_FragCoord.xy / res;
	float aspect = res.x / res.y;
	uv.x *= aspect;
	uv -= vec2(0.5 * aspect, 0.5);

	// raymarch init
	vec3 ro = vec3(0, 0, -3);
	vec3 rd = normalize(vec3(uv, 1));
	
	vec3 p;
	vec4 rgbd = raymarch(ro, rd, p);

	if (rgbd.w < 50.0) { // color
		vec3 n = normal(p);
		float diff = max(dot(n, normalize(sun)), 0.0);

		finalColor = vec4(vec3(rgbd.xyz * diff), 1.0);
		// finalColor = vec4(col * clamp(shade, 0.2, 1), 1.0);
	} else { // sky
		finalColor = vec4(vec3(mix(vec3(0.373,0.624,0.839), vec3(0.878,0.937,0.965), uv.y)), 1.0);
	}
}
