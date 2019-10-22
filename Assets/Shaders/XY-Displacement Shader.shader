Shader "Custom/Displacement/XY-Displacement Unlit Shader" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_Mult("Multiplier", Float) = 1
		_MainTex("Texture (RGBA)", 2D) = "white"{}
		_Disp("Displacement (Default Image|X=R,Z=G)", 2D) = ""{}
		_Scale("Scale (to fix UV)",Vector) = (1,1,1,0)
	}
	SubShader {
		Tags {"Queue" = "Transparent" "RenderType" = "Transparent" "IgnoreProjector" = "True"}
		LOD 200
		CGINCLUDE
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
			sampler2D _Disp;
			float4 _Disp_ST;
			Vector _Scale;
			float _Mult;


			v2f vert(appdata v)
			{
				v2f o;
				float3 dcolor = tex2Dlod(_Disp, float4(v.uv * _Disp_ST.xy + _Disp_ST.zw, 0, 0));
				v.vertex.x += _Mult * (dcolor.x - 0.5);
				v.vertex.z += _Mult * (dcolor.y - 0.5);
				v.uv.x += -_Scale.x*_Mult * (dcolor.x - 0.5);
				v.uv.y += -_Scale.y*_Mult * (dcolor.y - 0.5);
				o.uv = v.uv;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
		ENDCG

		Pass //1st Pass for setting Zbuffer
		{
			ZWrite On
			ColorMask 0

			CGPROGRAM
				half4 frag(v2f i) : COLOR
				{
					return half4(0,0,0,0);
				}
			ENDCG
		}
		Pass //2nd Pass for applying color
		{
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
				float4 _Color;
				fixed4 frag(v2f i) : SV_Target
				{ 
					fixed4 col = _Color* tex2D(_MainTex, i.uv);
					return col;
				}
			ENDCG
		}
	}
}
