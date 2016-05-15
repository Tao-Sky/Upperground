Shader "ManageColors" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_bwBlendV ("Black & White V", Range (0, 1)) = 0
		_bwBlendR ("Black & White R", Range (0, 1)) = 0
		_bwBlendB ("Black & White B", Range (0, 1)) = 0
		_bwBlendG ("Black & White G", Range (0, 1)) = 0

	}
	SubShader {
		Pass {
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
 
			#include "UnityCG.cginc"
 
			uniform sampler2D _MainTex;
			uniform float _bwBlendV;
			uniform float _bwBlendG;
			uniform float _bwBlendR;
			uniform float _bwBlendB;
 
			float4 frag(v2f_img i) : COLOR {
				float4 c = tex2D(_MainTex, i.uv);
				float lum = c.r*.3 + c.g*.59 + c.b*.11;
				float3 bw = float3( lum, lum, lum ); 

				float4 result = c;
				if(c.g <0.4 && c.b < c.r + 0.4 && c.b > c.r -0.4)
				{
					result.rgb = lerp(c.rgb, bw, _bwBlendV);
				}
				else if(c.g >0.6)
				{
					result.rgb = lerp(c.rgb, bw, _bwBlendG);
				}
				else if(c.r >0.6)
				{
					result.rgb = lerp(c.rgb, bw, _bwBlendR);
				}
				else if(c.b >0.6)
				{
					result.rgb = lerp(c.rgb, bw, _bwBlendB);
				}
				return result;
			}
			ENDCG
		}
	}
}