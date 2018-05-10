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

Shader "Custom/InnerTileShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_SmoothMap ("Smoothness", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_EmissionColor ("Emission Color", Color) = (0,0,0,0)

		_RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
      	_RimPower ("Rim Power", Range(0.25,4.0)) = 1.5
	}
	SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		
		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows

		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _SmoothMap;

		struct Input {
			float2 uv_MainTex;
			float2 uv_SmoothMap;
			float3 viewDir;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		fixed4 _EmissionColor;

      	float4 _RimColor;
      	float _RimPower;


		void surf (Input IN, inout SurfaceOutputStandard o) {

          	half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
			half rimAmount = clamp(0,1,pow(rim, _RimPower) * 2);

			fixed4 texColor = tex2D(_MainTex, IN.uv_MainTex) + _Color;
			fixed4 smoothColor = tex2D(_SmoothMap, IN.uv_SmoothMap);


			o.Albedo = lerp(texColor.rgb, _RimColor.rgb, rimAmount);
			o.Metallic = smoothColor.a * _Metallic;
			o.Smoothness = smoothColor * _Glossiness;

			o.Emission = _EmissionColor;

			o.Alpha = texColor.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
