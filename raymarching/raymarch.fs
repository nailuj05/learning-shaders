# version 330

uniform vec2 res;
out vec4 finalColor;

struct Primitive {
	int type;
	vec3 position;
	vec3 color;
	float radius;
	vec3 size;
};

Primitive primitives[4];

// proudly stolen from: https://iquilezles.org/articles/distfunctions/
float sd_floor(vec3 p) {
	return p.y;
}

float sd_sphere(vec3 p, float r) {
	return length(p) - r;
}

float sd_box(vec3 p, vec3 b) {
	vec3 q = abs(p) - b;
  return length(max(q,0.0)) + min(max(q.x,max(q.y,q.z)),0.0);
}

float sd_primitive(vec3 p, int i) {
		if (primitives[i].type == 0) {
			return sd_floor(p - primitives[i].position);
		}
		else if (primitives[i].type == 1) {
			return sd_sphere(p - primitives[i].position, primitives[i].radius);
		}
		else if (primitives[i].type == 2) {
			return sd_box(p - primitives[i].position, primitives[i].size);
		}
		return 3.402823466e+38;
}

float smin(float a, float b, float k)
{
    k *= 1.0;
    float r = exp2(-a/k) + exp2(-b/k);
    return -k*log2(r);
}

vec3 smin_col(vec3 a, vec3 b, float k)
{
    k *= 1.0;
    vec3 r = exp2(-a/k) + exp2(-b/k);
    return -k*log2(r);
}

vec4 map(vec3 p) {
	float m = 3.402823466e+38;
	vec3 col = vec3(1.0);
	for(int i = 0; i < 4; ++i) {
		float d = sd_primitive(p, i);
		if (d < m) {
			m = min(m, d);
			col = primitives[i].color;
		}
	}
	return vec4(col, m);
}

void init_prim() {
	primitives[0] = Primitive(0, vec3(0, -2, 0), vec3(0.5,0.5,0.5), 0., vec3(0));
	primitives[1] = Primitive(1, vec3(1,  0, 3), vec3(1,0,0), 1., vec3(0));
	primitives[2] = Primitive(1, vec3(-1, 0, 3), vec3(0,1,0), 1., vec3(0));
	primitives[3] = Primitive(2, vec3(0,  0, 3), vec3(0,0,1), 0., vec3(1,1,1));
}

float softshadow (vec3 ro, vec3 rd, float mint, float maxt, float k ) {
	float res = 1.0;
	float t = mint;
	for( int i=0; i<256 && t<maxt; i++ )
    {
			float h = map(ro + rd*t).w;
			if( h<0.001 )
				return 0.0;
			res = min(res, k*h/t);
			t += h;
    }
	return res;
}

void main() {
	vec3 sun = normalize(vec3(1, 4, -1));
	init_prim();
	
	// initialize clip space 
	vec2 uv = gl_FragCoord.xy / res;
	float aspect = res.x / res.y;
	uv.x *= aspect;
	uv -= vec2(0.5 * aspect, 0.5);

	// initialize raymarching
	vec3 ro = vec3(0, 0, -3);
	vec3 rd = normalize(vec3(uv, 1));
	vec3 col = vec3(1);
	float d = 0.0;
	vec3 p;
	
	// march the ray :)
	for (int i = 0; i < 100; ++i) { 
		p = ro + rd * d;
	  vec4 x = map(p);
		d += x.w;
		
		col = x.rgb;

		if (x.w < 0.001) break;
		if (length(p) > 50) {
			col = vec3(mix(vec3(0.373,0.624,0.839), vec3(0.878,0.937,0.965), uv.y));
			break;
		}
	}

	float shade = softshadow(p, sun, 1, 5, 4);
	
	finalColor = vec4(col * clamp(shade, 0.2, 1), 1.0);
}
