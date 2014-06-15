// Upgrade NOTE: replaced 'glstate.matrix.mvp' with 'UNITY_MATRIX_MVP'

Shader "Custom/ProgressBar2" {

Properties {
	_Color ("Color", Color) = (1,1,1,1)
	_InsideTex ("Inside (RGBA)", 2D) = "white" {}
	_OutsideTex ("Outside (RGBA)", 2D) = "black" {}	
	_Progress ("Progress", Range(0.0,1.0)) = 0.0
}
 
SubShader {
	Tags { "Queue"="Transparent" "LightMode" = "Always" }
	LOD 200
	Blend SrcAlpha OneMinusSrcAlpha

CGPROGRAM
#pragma surface surf Lambert
 
uniform sampler2D _InsideTex;
uniform sampler2D _OutsideTex;
uniform float4 _Color;
uniform float _Progress;
 
struct Input {
	float2 uv_InsideTex;
	float2 uv_OutsideTex;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c = tex2D(_InsideTex, IN.uv_InsideTex);
	fixed4 outside = tex2D(_OutsideTex, IN.uv_OutsideTex);
	c *= _Color;
	c.a *= IN.uv_InsideTex.x < _Progress;	
	c = (1.0 - outside.a)*c + outside*outside.a;

	o.Albedo = c.rgb;
	o.Alpha = c.a;
}

ENDCG
}
 
}