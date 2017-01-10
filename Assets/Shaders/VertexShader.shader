Shader "Lescieux/VertexShader" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_SliderValue ("SliderValue", Range (-0.05, 0.05) ) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert

		struct Input {
			float2 uv_MainTex;
		};

		sampler2D _MainTex;
		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		
		float _SliderValue;
		void vert (inout appdata_full v) {
			v.vertex.xyz += v.normal * _SliderValue;
		}
		
		ENDCG
	} 
	FallBack "Diffuse"
}
