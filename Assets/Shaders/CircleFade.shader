// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "ArcShield"
{
	Properties 
	{
		_Color("Main Color", Color) = (1, 1, 1, 1)
		_MainTex("Main Texture", 2D) = "white" {}
		_Center("Center", Float) = 0.5
		_Radius("Radius", Float) = 0.5
		_Feather("Feather", Range(0.001, 0.05)) = 0.02
		_StartRadian("Start Radian", Float) = 0
		_StopRadian("Stop Radian", float) = 3.14
	}

	Subshader 
	{
		Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
		Pass 
		{
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			uniform half4 _Color;
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform float _Center;
			uniform float _Radius;
			uniform float _Feather;
			uniform float _StartRadian;
			uniform float _StopRadian;

			struct vertexInput 
			{
				float4 vertex: POSITION;
				float2 texcoord: TEXCOORD0;
			};

			struct vertexOutput
			{
				float4 pos: SV_POSITION;
				float2 texcoord: TEXCOORD0;
			};

			vertexOutput vert(vertexInput v) 
			{
				vertexOutput o;
				UNITY_INITIALIZE_OUTPUT(vertexOutput, o); // d3d11 requires intialization
				o.pos = UnityObjectToClipPos(v.vertex);
				o.texcoord = v.texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
				return o;			
			}

			float drawCircleFade(float2 uv, float2 center, float radius, float feather,
				float startRadian, float stopRadian)
			{
				float dx = uv.x - center.x;
				float dy = uv.y - center.y;
				float distanceSq = dx * dx + dy * dy;
				float radiusSq = radius * radius;
				float rad = atan2(dy, dx);

				if (rad < startRadian)
					rad += 2 * 3.1416;

				if (distanceSq < radiusSq && rad > startRadian && rad < stopRadian)
				{
					return smoothstep(radiusSq, radiusSq - feather, distanceSq);
				}

				return 0;
			}

			half4 frag(vertexOutput i): COLOR
			{
				float4 col = tex2D(_MainTex, i.texcoord) * _Color;
				col.a *= drawCircleFade(i.texcoord, _Center, _Radius, _Feather,
					_StartRadian, _StopRadian);
				return col;
			}

			ENDCG
		}
	}
}