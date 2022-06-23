Shader "Brave/HueShift" {
	Properties {
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
		_Perpendicular ("Is Perpendicular Tilt", Float) = 1
		_HueShift ("HueShift", Float) = 0
		_TimeHueShiftFactor ("Time Factor", Float) = 0
		_SaturationShift ("SaturationShift", Float) = 1
		_ValueShift ("ValueShift", Float) = 1
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
	Fallback "VertexLit"
}