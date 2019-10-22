Shader "Custom/Lightning"
{
	Properties
	{
		_MainTex("Texture",2D) = "white"{}
		[HDR]_Color("Color", Color) = (1,1,1,1)
		_Amplitude("Amplitude", Vector) = (1,1,1,1)
		_Frequency("Frequency", Vector) = (1,1,1,1)
		_Offset("Line Offset", Vector) = (1,1,1,1)
	}
		SubShader
	{
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent" "IgnoreProjector" = "true" }
		Blend SrcAlpha OneMinusSrcAlpha
		ZTest Always
		Pass {
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct input {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};
			struct v2f {
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};
			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color;
			float4 _Amplitude;
			float4 _Frequency;
			float4 _Offset;
			v2f vert(input v) {
				v2f o;
				v.vertex.x += sqrt(v.uv.y)*(
					_Amplitude.x * (cos(_Frequency.x*v.uv.y + _Offset.x))
					+ _Amplitude.y * (sin(_Frequency.y*v.uv.y + _Offset.y))
					+ _Amplitude.z * (cos(_Frequency.z*v.uv.y + _Offset.z))
					+ _Amplitude.w * (sin(_Frequency.w*v.uv.y + _Offset.w))
					);
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = _Color * tex2D(_MainTex, i.uv)*(1-pow(abs(i.uv.y*2-1),3));
				return col;
			}
		ENDCG
		}
	}
}
