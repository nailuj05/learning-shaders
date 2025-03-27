#version 330

out vec4 FragColor;
uniform float time;
uniform vec2 res;

void main() {
		 vec2 c = vec2(-0.5251993, -0.5251993) * sin(time * 0.5); 
		 vec2 uv = (gl_FragCoord.xy / res);
		 float aspect = res.x / res.y;
		 uv.x *= aspect;
		 vec2 z = (uv - vec2(0.5 * aspect, 0.5)) * (2 / sin(0.25 * time));

		 int maxIter = 600;
		 int i = 0;
		 for (i = 0; i < maxIter; i++) {
		 		 float x = z.x * z.x - z.y * z.y + c.x;
				 float y = 2.0 * z.x * z.y + c.y;
				 z = vec2(x,y);

				 if(dot(z,z) > 2.0) break;
		 }

		 float t = float(i) / float(maxIter);
		 vec3 color = mix(vec3(0.0, 0.0, 0.0), vec3(uv, 0.5), t);

		 FragColor = vec4(color, 1.0);
}
