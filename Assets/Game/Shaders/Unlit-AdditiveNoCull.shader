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

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Additive NoCull" {

	Properties {
    	_Color ("Main Color", Color) = (1,1,1,0.5)
		_MainTex ("Texture", 2D) = "white" { }
	}

	SubShader {
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		LOD 100
	
		ZWrite Off
        Blend One One 
        Cull Off

	    Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			
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
    			half4 texColor = tex2D (_MainTex, input.uv);
    			return (input.color * texColor * _Color) * _Color.a;
			}
			ENDCG
    	}
	}
} 
