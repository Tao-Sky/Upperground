Shader "Violet"
{
	Properties
	{
		_MyTexture ("My texture", 2D) = "white" {}
		_MyNormalMap ("My normal map", 2D) = "bump" {}	// Grey
		_MyColor ("My colour", Color) = (0, 0, 0, 1)	// (R, G, B, A)
	}
 
	SubShader
	{
		
		
		Tags
		{
			"Queue" = "Geometry"
			"RenderType" = "Opaque"
		}
		
	
		Pass {
			CGPROGRAM
			 
			sampler2D _MyTexture;
			sampler2D _MyNormalMap;
		 
			half4 _MyColor;
		
			#pragma vertex vert             
			#pragma fragment frag
			#include "UnityCG.cginc"
			 
			struct vertInput {
				float4 pos : POSITION;
				float2  uv : TEXCOORD0;
			};  
			 
			struct vertOutput {
				float4 pos : SV_POSITION;
				float2  uv : TEXCOORD0;
				fixed4 color : COLOR0;
			};
			 
			vertOutput vert(vertInput input) {
				vertOutput o;
				o.pos = mul(UNITY_MATRIX_MVP, input.pos);
				o.uv = input.uv;
				return o;
			}
			 
			float4 frag(vertOutput output) : COLOR {
				
				// remove red
				/*
				float4 result;
                result = tex2D(_MyTexture, output.uv);
				result.r = 0;
				return result;
				*/
				
				// no se
				/*
				float4 col;
                col = tex2D(_MyTexture, output.uv);
				
				fixed k = (col.r * 100) + (col.g * 10) + col.b; // pick pixel color
				
				output.color.a *= ceil(col.a) * ceil(abs(k - 110) / 110.0); // 0 alpha if yellow
				fixed b1 = ceil(col.a) * ceil(abs(k - 100) / 100.0); // 0 if red
				fixed b2 = ceil(col.a) * ceil(abs(k - 101) / 101.0); // 0 if pink

				fixed4 red =  fixed4(output.color.r * 0.65, output.color.g * 0.65, output.color.b * 0.65, output.color.a) * (1 - b1);
				fixed4 pink =output.color * (1 - b2);
				fixed4 tex = ((1,1,1,output.color.a)*col*b1*b2);

				return red + pink + tex;
				*/
				
				// test
				float4 col;
                col = tex2D(_MyTexture, output.uv);
				
				fixed4 sortie ; 
				
				
				
				if(col.r > _MyColor.r && col.g > _MyColor.g && col.b  >_MyColor.b){
					float grey = (((0.29*col.r)+(0.6*col.g))+(col.b*0.11));
					float3 finalCol = float3(grey,grey,grey);
					sortie = fixed4(finalCol, col.a);
					
				}
				else{
					sortie = col;
				}
				// 0.25*R + 0.6*G + 0.11*B 
				// (0.3 * R) + (0.59 * G) + (0.11 * B)
				
				return  sortie;
				
			}
			ENDCG
		}
	}	
}

/**

The geometry of your model is first passed through a function called vert which can alter its vertices. 
Then, individual triangles are passed through another function called frag which decides the final RGB colour for every pixel.

**/