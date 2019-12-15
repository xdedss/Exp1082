Shader "Unlit/SlitShader"
{
    Properties
    {
		_Blur("Blur", float) = 0
		_Intensity("Intensity", float) = 0
        _MainTex0 ("Blur0", 2D) = "black" {}
	_MainTex1("Blur1", 2D) = "black" {}
	_MainTex2("Blur2", 2D) = "black" {}
	_MainTex3("Blur3", 2D) = "black" {}
	_MainTex4("Blur4", 2D) = "black" {}
	_MainTex5("Blur5", 2D) = "black" {}
	_MainTex7_5("Blur7_5", 2D) = "black" {}
	_MainTex10("Blur10", 2D) = "black" {}
	_MainTex25("Blur25", 2D) = "black" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
		Blend One One
		ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

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

            sampler2D _MainTex0;
            float4 _MainTex0_ST;
			sampler2D _MainTex1;
			sampler2D _MainTex2;
			sampler2D _MainTex3;
			sampler2D _MainTex4;
			sampler2D _MainTex5;
			sampler2D _MainTex7_5;
			sampler2D _MainTex10;
			sampler2D _MainTex25;
			float _Blur;
			float _Intensity;

			float4 lerp(float4 a, float4 b, float t) {
				return (1 - t) * a + t * b;
			}

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex0);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				fixed4 col;
				if (_Blur < 1) {
					col = lerp(tex2D(_MainTex0, i.uv), tex2D(_MainTex1, i.uv), _Blur);
				}
				else if (_Blur < 2) {
					col = lerp(tex2D(_MainTex1, i.uv), tex2D(_MainTex2, i.uv), _Blur - 1);
				}
				else if (_Blur < 3) {
					col = lerp(tex2D(_MainTex2, i.uv), tex2D(_MainTex3, i.uv), _Blur - 2);
				}
				else if (_Blur < 4) {
					col = lerp(tex2D(_MainTex3, i.uv), tex2D(_MainTex4, i.uv), _Blur - 3);
				}
				else if (_Blur < 5) {
					col = lerp(tex2D(_MainTex4, i.uv), tex2D(_MainTex5, i.uv), _Blur - 4);
				}
				else if (_Blur < 7.5) {
					col = lerp(tex2D(_MainTex5, i.uv), tex2D(_MainTex7_5, i.uv), (_Blur - 5)/2.5);
				}
				else if (_Blur < 10) {
					col = lerp(tex2D(_MainTex7_5, i.uv), tex2D(_MainTex10, i.uv), (_Blur - 7.5)/2.5);
				}
				else if (_Blur < 25) {
					col = lerp(tex2D(_MainTex10, i.uv), tex2D(_MainTex25, i.uv), (_Blur - 10)/15);
				}
				else {
					col = tex2D(_MainTex25, i.uv);
				}
                return col * _Intensity;
            }
            ENDCG
        }
    }
}
