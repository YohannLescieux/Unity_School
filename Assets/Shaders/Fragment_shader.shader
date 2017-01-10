Shader "Lescieux/FragmentShader" {
	Properties {
		_GrayScaleMap ("GrayScaleMap", 2D) = "gray" {}
		_SliderValue ("SliderValue", Range (0.05, 5.0) ) = 0.05
	}

	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Lambert vertex:disp

		sampler2D _GrayScaleMap;
		float _SliderValue;

		struct Input {
			float2 uv_GrayScaleMap;
			float3 worldPos;
		};
		
		void disp (inout appdata_full v) {
			v.vertex.xyz += v.normal * (tex2Dlod(_GrayScaleMap, float4(v.texcoord.xy, 0.0, 0.0)).r * _SliderValue);
		}

		void surf (Input IN, inout SurfaceOutput o) {
			half4 colo;
			colo.r = 0.0;
			colo.g = 0.0;
			colo.b = 0.0;
			if(IN.worldPos.y < 1) {
				colo.b = 0.5 - IN.worldPos.y;
			}
			if(IN.worldPos.y > 0.1) {
				colo.g = IN.worldPos.y; 
			}
			if(IN.worldPos.y > 1) {
				colo.r = IN.worldPos.y - 1;
				colo.b = IN.worldPos.y - 1;
			}
			colo.a = 1;
			o.Alpha = colo.a;
			o.Albedo = colo.rgb;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}