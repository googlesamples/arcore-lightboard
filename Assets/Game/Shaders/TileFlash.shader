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

Shader "Unlit/Tile Flash" {

	Properties {
    	_Color ("Main Color", Color) = (1,1,1,0.5)
		_MainTex ("Texture", 2D) = "white" { }
		_Frame("Frame Number", Int) = 0
	}

		SubShader{
			Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
			LOD 100

			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
			cull Off

			Pass {
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				fixed _Frame;
				fixed4 _Color;
				sampler2D _MainTex;
			
				struct vertex2fragment {
   			 		float4 pos : SV_POSITION;
    				float2 uv : TEXCOORD0;
    		 		float4 color : COLOR;
				};
			
				float4 _MainTex_ST;
			

				vertex2fragment vert (appdata_full vertData) {
					vertex2fragment output;
    				output.pos = UnityObjectToClipPos (vertData.vertex);
    				output.uv = TRANSFORM_TEX (vertData.texcoord, _MainTex);
    				output.color = vertData.color * _Color;
   					return output;
				}
			
				half4 frag (vertex2fragment input) : COLOR {

					uint x = _Frame % 4;
					uint y = _Frame / 4;

					fixed2 hitUVs = input.uv * 0.25;
					fixed2 offset = fixed2(x * 0.25, (3 - y) * 0.25);

					fixed4 texColor = tex2D(_MainTex, hitUVs + offset);
					return input.color * texColor;

				}
			ENDCG
    	}
	}
} 
