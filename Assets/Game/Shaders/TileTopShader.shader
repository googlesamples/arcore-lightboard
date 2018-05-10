// Copyright 2018 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

Shader "Custom/TileTopShader" {
	Properties {
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_HitTex("HitTexture (RGB)", 2D) = "white" {}
		_Smoothness("Smoothness (RGBA)", 2D) = "white" {}
		_SmoothScale("Smoothness Scale", Range(0,1)) = 0.0
		_Metallic("Metallic", Range(0,1)) = 0.0
		_HitFrame("Frame Number", Int) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows

		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _HitTex;
		sampler2D _Smoothness;

		struct Input {
			float2 uv_MainTex;
		};

		half _SmoothScale;
		half _Metallic;
		int _HitFrame;
		fixed4 _Color;

		#pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {

			fixed4 texColor = tex2D(_MainTex, IN.uv_MainTex) * _Color;

			int x = _HitFrame % 4;
			int y = _HitFrame / 4;

			fixed2 hitUVs = IN.uv_MainTex * 0.25;
			fixed2 offset = fixed2(x * 0.25, (3 - y) * 0.25);

			fixed4 hitColor = tex2D(_HitTex, hitUVs + offset);
			o.Albedo = texColor.rgb + hitColor.rgb;
			o.Emission = hitColor.rgb;
			fixed4 smoothness = tex2D(_Smoothness, IN.uv_MainTex);
			o.Smoothness = smoothness.rgb * _SmoothScale;
			o.Metallic = smoothness.a * _Metallic;
			o.Alpha = hitColor.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
