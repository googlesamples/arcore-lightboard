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

Shader "Unlit/ProgressSpinner"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color1 ("Color 1", Color) = (1,1,1,1)
        _Color2 ("Color 2", Color) = (1,1,1,1)
        _Color3 ("Color 3", Color) = (1,1,1,1)
        _Fade ("Fade", Range(0,1) ) = 0
        _Rotation1 ("Rotation1", Float ) = 0
        _Rotation2 ("Rotation2", Float ) = 0
        _Rotation3 ("Rotation3", Float ) = 0
	}

	SubShader
	{
		Tags { "Queue" = "Transparent" }

		Pass
		{
            ZTest Off
            ZWrite Off
			Blend One One 
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
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color1;
            float4 _Color2;
            float4 _Color3;
            float _Fade;
            float _Rotation1;
            float _Rotation2;
            float _Rotation3;


            fixed2 RotateUVs(float2 uv, float angle) {
                uv -=0.5;
                float s = sin (angle);
                float c = cos (angle);

                float2x2 rotationMatrix = float2x2( c, -s, s, c);
                rotationMatrix *=0.5;
                rotationMatrix +=0.5;
                rotationMatrix = rotationMatrix * 2-1;
                float2 newUv = mul ( uv, rotationMatrix );
                return newUv += 0.5;
            }

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
                float2 baseUVs = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv = RotateUVs(baseUVs, _Rotation1);
                o.uv1 = RotateUVs(baseUVs, _Rotation2);
                o.uv2 = RotateUVs(baseUVs, _Rotation3);
				return o;
			}
                        /*

            v2f vert (appdata v) {
                float sinX = sin ( _RotationSpeed * _Time );
                float cosX = cos ( _RotationSpeed * _Time );
                float sinY = sin ( _RotationSpeed * _Time );
                float2x2 rotationMatrix = float2x2( cosX, -sinX, sinY, cosX);
                v.texcoord.xy = mul ( v.texcoord.xy, rotationMatrix );
            }

                        */

			
			fixed4 frag (v2f i) : SV_Target
			{
                fixed4 color1 = tex2D(_MainTex, i.uv).r * _Color1;
                fixed4 color2 = tex2D(_MainTex, i.uv1).g * _Color2;
                fixed4 color3 = tex2D(_MainTex, i.uv2).b * _Color3;
				return (color1 + color2 + color3) * _Fade;
			}
			ENDCG
		}
	}
}
