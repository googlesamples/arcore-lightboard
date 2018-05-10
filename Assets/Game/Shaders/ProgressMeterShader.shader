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

Shader "Unlit/ProgressMeter"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Color 1", Color) = (1,1,1,1)
		_Color2 ("Color 2", Color) = (1,1,1,1)
        _Progress ("Progress", Range (0, 1)) = 0
        _Fader ("Fader", Range (0, 1)) = 1
	}

	SubShader
	{
		Tags { "Queue" = "Transparent" }

		Pass
		{
		  	ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha 
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color;
			float4 _Color2;
            float _Progress;
            float _Fader;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 tex = tex2D(_MainTex, i.uv);
				float clipValue = 1 - (tex.a + (_Progress - 0.5));

				fixed4 color = fixed4(_Color.r,_Color.g,_Color.b,tex.r);
				if (clipValue > 0.5) color = _Color2;
				color.a *= tex.r * _Fader;
				return color;
			}
			ENDCG
		}
	}
}
