Shader "Unlit/PerspectiveTransform" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Point1 ("Point 1", Vector) = (0,0,0,0)
        _Point2 ("Point 2", Vector) = (0,0,0,0)
        _Point3 ("Point 3", Vector) = (0,0,0,0)
        _Point4 ("Point 4", Vector) = (0,0,0,0)

        // _StencilComp ("Stencil Comparison", Float) = 8
        // _Stencil ("Stencil ID", Float) = 0
        // _StencilOp ("Stencil Operation", Float) = 0
        // _StencilWriteMask ("Stencil Write Mask", Float) = 255
        // _StencilReadMask ("Stencil Read Mask", Float) = 255
    }

    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        Cull Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float2 uv : TEXCOORD0;
                float4 vertex : POSITION;
                fixed4 color : COLOR;
            };
            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Point1;
            float4 _Point2;
            float4 _Point3;
            float4 _Point4;

            // float2 _Points[4] = {
                //     float2(0.0, 0.0),
                //     float2(0.5, 0.0),
                //     float2(0.5, 0.5),
                //     float2(0.0, 0.5),
            // };


            // From: Iq
            // https://www.shadertoy.com/view/lsBSDm
            // given a point p and a quad defined by four points {a,b,c,d}, return the bilinear
            // coordinates of p in the quad. Returns (-1,-1) if the point is outside of the quad.
            float xross(float2 a, float2 b) { return a.x*b.y - a.y*b.x; }
            float2 invBilinear(float2 p, float2 a, float2 b, float2 c, float2 d) {
                float2 e = b-a;
                float2 f = d-a;
                float2 g = a-b+c-d;
                float2 h = p-a;
                
                float k2 = xross(g, f);
                float k1 = xross(e, f) + xross(h, g);
                float k0 = xross(h, e);
                
                float w = k1*k1 - 4.0*k0*k2;
                
                if(w < 0.0) return float2(-1.0, -1.0);

                w = sqrt(w);
                float v1 = (-k1 - w)/(2.0*k2);
                float v2 = (-k1 + w)/(2.0*k2);
                float u1 = (h.x - f.x*v1)/(e.x + g.x*v1);
                float u2 = (h.x - f.x*v2)/(e.x + g.x*v2);
                bool  b1 = v1>0.0 && v1<1.0 && u1>0.0 && u1<1.0;
                bool  b2 = v2>0.0 && v2<1.0 && u2>0.0 && u2<1.0;

                float2 res = float2(-1.0, -1.0);
                if(b1 && !b2) res = float2(u1, v1);
                if(!b1 && b2) res = float2(u2, v2);
                
                return res;
            }

            v2f vert(appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target {
                fixed4 color = 0;
                // float2 texUv = invBilinear(uv, _Points[0], _Points[1], _Points[2], _Points[3]);
                float2 texUv = invBilinear(i.uv, _Point1, _Point2, _Point3, _Point4);

                if (texUv.x > -0.5) {
                    color = min(tex2D(_MainTex, texUv), i.color);
                }

                return color;
            }

            ENDCG
        }
    }
}
