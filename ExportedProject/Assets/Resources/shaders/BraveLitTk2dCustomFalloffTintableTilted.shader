Shader "Brave/LitTk2dCustomFalloffTintableTilted" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_OverrideColor ("Override Color", Vector) = (1,1,1,0)
		_Perpendicular ("Is Perpendicular Tilt", Float) = 1
		_MaxValue ("Max Value", Float) = 1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "tk2d/BlendVertexColor"
}