Shader "Custom/Displacement/Vertex Round Displacement Shader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
		_Amplitude ("Amplitude", Vector)=(1,1,1,1)
		_Frequency ("Frequency", Vector)=(1,1,1,1)
		_Speed ("Speed", Vector)=(1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "IgnoreProjector"="true" }
        LOD 200

		Blend SrcAlpha OneMinusSrcAlpha
		Pass {

		Stencil {
				Ref 0
				Comp Equal
				Pass IncrSat
				Fail IncrSat
		}

		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct input {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};
			struct v2f {
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};
			float4 _Color;
			float4 _Amplitude;
			float4 _Frequency;
			float4 _Speed;
			v2f vert(input v) {
				v2f o;
				v.vertex.zx += _Amplitude.x * float2(cos(_Frequency.x*3.14159265359*(v.uv.x-0.5)+_Time.y*_Speed.x),sin(_Frequency.x*3.14159265359*(v.uv.y-0.5)+_Time.y*_Speed.x))
					+ _Amplitude.y * float2(sin(_Frequency.y*3.14159265359*(v.uv.x - 0.5) + _Time.y*_Speed.y), cos(_Frequency.y*3.14159265359*(v.uv.y - 0.5) + _Time.y*_Speed.y))
					+ _Amplitude.z * float2(cos(_Frequency.z*3.14159265359*(v.uv.x - 0.5) + _Time.y*_Speed.z), sin(_Frequency.z*3.14159265359*(v.uv.y - 0.5) + _Time.y*_Speed.z))
					+ _Amplitude.w * float2(sin(_Frequency.w*3.14159265359*(v.uv.x - 0.5) + _Time.y*_Speed.w), cos(_Frequency.w*3.14159265359*(v.uv.y - 0.5) + _Time.y*_Speed.w))
					;
				o.uv = v.uv;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			fixed4 frag() : COLOR
			{
				return _Color;
			}
		ENDCG
		}
    }
    FallBack "Diffuse"
}
