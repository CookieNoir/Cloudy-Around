Shader "Custom/Cloud UI"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		_Level("Level",  Range(0, 1)) = 0.5
		_Modifier("Modifier", Range(0.05, 5)) = 1
		_Gradient("Gradient",  Range(0.005, 1)) = 0.05
		_Amplitude("Amplitude", Vector) = (0.5,0,0,0)
		_Frequency("Frequency", Vector) = (3.14,0,0,0)
		_Offset("Line Offset", Vector) = (0,0,0,0)
	}

		SubShader
		{
			Tags
			{
				"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane"
				"CanUseSpriteAtlas" = "True"
			}

			Cull Off
			Lighting Off
			ZWrite Off
			ZTest[unity_GUIZTestMode]
			Blend SrcAlpha OneMinusSrcAlpha

			Pass
			{
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 2.0

				#include "UnityCG.cginc"
				#include "UnityUI.cginc"

				struct appdata_t
				{
					float4 vertex   : POSITION;
					float4 color    : COLOR;
					float2 texcoord : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct v2f
				{
					float4 vertex   : SV_POSITION;
					fixed4 color : COLOR;
					float2 texcoord  : TEXCOORD0;
					float4 worldPosition : TEXCOORD1;
					UNITY_VERTEX_OUTPUT_STEREO
				};

				sampler2D _MainTex;
				fixed4 _Color;
				float4 _MainTex_ST;
				float _Level;
				float _Gradient;
				float _Modifier;
				float4 _Amplitude;
				float4 _Frequency;
				float4 _Offset;

				v2f vert(appdata_t v)
				{
					v2f OUT;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
					OUT.worldPosition = v.vertex;
					OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

					OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

					OUT.color = v.color * _Color;
					return OUT;
				}

				fixed4 frag(v2f IN) : SV_Target
				{
					float val = _Level + _Modifier*(
						_Amplitude.x*sin(_Frequency.x*IN.texcoord.x + _Offset.x) +
						_Amplitude.y*cos(_Frequency.y*IN.texcoord.x + _Offset.y) +
						_Amplitude.z*cos(_Frequency.z*IN.texcoord.x + _Offset.z) +
						_Amplitude.w*cos(_Frequency.w*IN.texcoord.x + _Offset.w));
					half4 color;
					if (IN.texcoord.y<val)
						color = tex2D(_MainTex, IN.texcoord) * IN.color;
					else if (IN.texcoord.y < val+_Gradient)
						color = tex2D(_MainTex, IN.texcoord) * IN.color * (1/_Gradient*(val + _Gradient - IN.texcoord.y));
					else color = half4(0, 0, 0, 0);
					return color;
				}
			ENDCG
			}
		}
}