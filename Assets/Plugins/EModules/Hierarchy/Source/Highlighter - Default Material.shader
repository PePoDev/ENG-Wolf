Shader "Hidden/EM-X/Highlighter - Neon Background"
{
	Properties{
		_MainTex("Texture", Any) = "white" {}
	_Mask("Mask", Color) = (1,1,1,1)
	}
		

		CGINCLUDE
#pragma vertex vert
#pragma fragment frag
#pragma target 2.0
	
#include "UnityCG.cginc"

		struct appdata_t {
		fixed4 vertex : POSITION;
		fixed4 color : COLOR;
		fixed2 texcoord : TEXCOORD0;
		UNITY_VERTEX_INPUT_INSTANCE_ID
	};

	struct v2f {
		fixed4 vertex : SV_POSITION;
		fixed4 color : COLOR;
		fixed2 texcoord : TEXCOORD0;
		fixed2 clipUV : TEXCOORD1;
		UNITY_VERTEX_OUTPUT_STEREO
	};

	uniform fixed4 _Mask;
	uniform fixed4 _MainTex_ST;
	uniform fixed4x4 unity_GUIClipTextureMatrix;

	v2f vert(appdata_t v)
	{
		v2f o;
		UNITY_SETUP_INSTANCE_ID(v);
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
		o.vertex = UnityObjectToClipPos(v.vertex);
		fixed3 eyePos = UnityObjectToViewPos(v.vertex);
		o.clipUV = mul(unity_GUIClipTextureMatrix, fixed4(eyePos.xy, 0, 1.0));
		o.color = v.color;// v.color;
		o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
		return o;
	}

	uniform bool _ManualTex2SRGB;
	sampler2D _MainTex;
	sampler2D _GUIClipTexture;

	fixed4 frag(v2f i) : SV_Target
	{
	
		fixed4 colTex = tex2D(_MainTex, i.texcoord);
	if (_ManualTex2SRGB)
		colTex.rgb = LinearToGammaSpace(colTex.rgb);

	colTex.rgb = colTex.rgb * i.color.rgb;
	
	return colTex;
	//return fixed4(col.rgb, tex2D(_GUIClipTexture, i.texcoord).a * alpha);
	}
		ENDCG

		/*SubShader {
		Tags{ "ForceSupported" = "True" }

			Lighting Off
			//Blend SrcAlpha One, One One
				Blend SrcAlpha OneMinusSrcAlpha, One One

			Cull Off
			ZWrite Off
			ZTest Always

			Pass{
			CGPROGRAM
			ENDCG
		}
	}*/

	SubShader {
		Tags{ "ForceSupported" = "True" }

			Lighting Off
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Off
			ZWrite Off
			ZTest Always

			Pass{
			CGPROGRAM
			ENDCG
		}
	}
}
