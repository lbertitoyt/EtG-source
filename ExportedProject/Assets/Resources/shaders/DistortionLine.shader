Shader "Brave/Internal/DistortionLine" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_WavePoint1 ("Wave Point 1", Vector) = (0,0,0,0)
		_WavePoint2 ("Wave Point 2", Vector) = (1,1,0,0)
		_DistortProgress ("Distort Progress", Range(0, 1)) = 0
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
}