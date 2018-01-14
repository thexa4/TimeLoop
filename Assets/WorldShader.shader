// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Draw/WorldShader"
{
	Properties
	{

	}
	SubShader
	{
		// No culling or depth
		//Cull On ZWrite On ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float4 normal : NORMAL;
				float4 rayEnd : TEXCOORD1;
				float3 rayDirection : TEXCOORD2;
				float4 objectPos : TEXCOORD3;
				float3 worldNormal : TEXCOORD4;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;

				o.rayEnd = mul(v.vertex, unity_ObjectToWorld);
				o.rayDirection = o.rayEnd - _WorldSpaceCameraPos;
				o.normal = v.normal;
				o.worldNormal = UnityObjectToWorldNormal(v.normal);

				o.objectPos = v.vertex;

				return o;
			}
			
			sampler2D _MainTex;

			float3 distance_cylinder(float3 position, float radius)
			{
				return length(position.xy) - radius;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				float rayStart = _WorldSpaceCameraPos;

				float2 offsets = (i.uv - 0.5);

				float4 viewVertex = mul(i.vertex, unity_CameraInvProjection);
				viewVertex.z = 0;


				float raylength = 10;
				


				//fixed4 col = tex2D(_MainTex, i.uv);
				// just invert the colors
				//col = 1 - col;
				//return col;
				//return float4(i.vertex.x / 1024, i.vertex.y, 0, 1);
				//return float4(offsets.x, offsets.y, 0, 1);
				//return vertex;

				float distance = distance_cylinder(rayStart, 2) / 1000;
				//return float4(distance, distance, distance, 0);

				float3 near = unity_CameraWorldClipPlanes[4].xyz;
				float3 far = unity_CameraWorldClipPlanes[5].xyz;

				float3 direction = far - near;

				//  return float4(distance_cylinder(i.rayEnd - i.rayDirection, 0), 1);

				float sunRadius = 200;
				float time = i.objectPos.y * 10 + _Time[0];

				float4 lightpos = float4(sin(time), 0, cos(time), 1);

				float4 samplePos = i.objectPos;
				samplePos.y *= 10;

				float4 relativeLightDirection = normalize(samplePos - lightpos);
				
				//return lightpos;
				float sunIntensity = dot(relativeLightDirection, i.normal);

				float surfaceAngle = dot(i.worldNormal, -normalize(i.rayDirection));
				//return surfaceAngle;

				float4 color = sunIntensity * float4(1, 1, 1, 1) + (1 - sunIntensity) * float4(0.55, 0.55, 0.8, 1);
				return surfaceAngle * color;

				//return float4(i.rayDirection, 1);
			}
			ENDCG
		}
	}
}
