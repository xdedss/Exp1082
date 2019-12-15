Shader "Unlit/WaveShader"
{
    Properties
    {
		_T ("T", float) = 5
		_Offset ("Offset", float) = 0
		_Center("Center", float) = 0.5
		_Strength("Strength", float) = 0.5
        _Col ("Color", Color) = (1, 1, 0, 1)
		_Mask("Mask", 2D) = "white" {}
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

            float4 _Col;
			float _T;
			float _Offset;
			float _Center;
			float _Strength;
			sampler2D _Mask;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = (_Center + sin((i.uv.x + _Offset) / (_T) * 2 * 3.14159265) * _Strength) * _Col * tex2D(_Mask, i.uv).r;
                return col;
            }
            ENDCG
        }
    }
}
