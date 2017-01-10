Shader "Lescieux/SurfaceShader" {
	Properties {
		_MainTex ("First Texture", 2D) = "texture1" {}
		_SecondTex ("Other Texture", 2D) = "texture2" {}
		_SliderValue ("SliderValue", Range (0, 1) ) = 0.0 
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		//Pour rendu des shaders, utilisation de surf
		//CS SHADERG SHEET
		CGPROGRAM
		#pragma surface surf Lambert

		//Mettre meme nom que dans properties (Sampler2D = texture)
		sampler2D _MainTex;
		sampler2D _SecondTex;
		float _SliderValue;

		struct Input {
			float2 uv_MainTex;
			float2 uv_SecondTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
		//Base
			//_MainTex à coord uv Maintex (retourne une couleur)
			half4 firstTex = tex2D (_MainTex, IN.uv_MainTex);
			half4 secondTex = tex2D (_SecondTex, IN.uv_SecondTex);
			
			//Récupérer float de Slider
			//Combinaison linéaire entre les deux textures
			//Ajouter à albedo
			o.Albedo = firstTex.rgb * (1.0 - _SliderValue) + secondTex.rgb * _SliderValue;
			o.Alpha = firstTex.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
