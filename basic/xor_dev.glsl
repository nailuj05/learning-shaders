#version 330

out vec4 o;
uniform float time;
uniform vec2 res;

// Cool ass shader taken from cool ass dev: https://www.shadertoy.com/view/WfS3Dd

void main() {
  vec2 r = res;
  vec2 p=(gl_FragCoord.xy*2.-r)/r.y,l,v=p*(1.-(l+=abs(.7-dot(p,p))))/.2;

  for(float i;i++<8.;o+=(sin(v.xyyx)+1.)*abs(v.x-v.y)*.2) {
    v+=cos(v.yx*i+vec2(0,i)+time)/i+.7;
  }

  o=tanh(exp(p.y*vec4(1,-1,-2,0))*exp(-4.*l.x)/o);
}
