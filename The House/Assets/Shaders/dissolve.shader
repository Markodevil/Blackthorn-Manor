// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.06,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:33355,y:32710,varname:node_4013,prsc:2|diff-6991-OUT,alpha-2324-OUT,clip-5227-OUT;n:type:ShaderForge.SFN_Tex2d,id:3730,x:32690,y:33092,ptovrint:False,ptlb:noise texture,ptin:_noisetexture,varname:node_3730,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Step,id:6282,x:32964,y:33330,varname:node_6282,prsc:2|A-3730-RGB,B-9797-OUT;n:type:ShaderForge.SFN_Slider,id:9797,x:32089,y:33380,ptovrint:False,ptlb:dissolve slider,ptin:_dissolveslider,varname:node_9797,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_ComponentMask,id:5227,x:33173,y:33330,varname:node_5227,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-6282-OUT;n:type:ShaderForge.SFN_Tex2d,id:1467,x:31526,y:32439,ptovrint:True,ptlb:Base Color_copy,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:9a7f9c930a3fd644682ea6cd771d8020,ntxv:1,isnm:False;n:type:ShaderForge.SFN_Multiply,id:6991,x:33016,y:32690,varname:node_6991,prsc:2|A-3405-OUT,B-2324-OUT;n:type:ShaderForge.SFN_Add,id:3405,x:32779,y:32442,varname:node_3405,prsc:2|A-5982-OUT,B-7159-OUT;n:type:ShaderForge.SFN_Add,id:2324,x:32788,y:32754,varname:node_2324,prsc:2|A-7159-OUT,B-3732-OUT;n:type:ShaderForge.SFN_Vector1,id:659,x:32474,y:32365,varname:node_659,prsc:2,v1:1.5;n:type:ShaderForge.SFN_Multiply,id:4877,x:32233,y:32536,varname:node_4877,prsc:2|A-2210-OUT,B-3399-OUT;n:type:ShaderForge.SFN_Multiply,id:2210,x:31985,y:32536,varname:node_2210,prsc:2|A-656-RGB,B-7487-OUT;n:type:ShaderForge.SFN_Color,id:656,x:31985,y:32353,ptovrint:False,ptlb:node_8037_copy,ptin:_node_8037_copy,varname:_node_8037_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.3,c2:0.8,c3:0.9,c4:1;n:type:ShaderForge.SFN_Desaturate,id:7487,x:31758,y:32552,varname:node_7487,prsc:2|COL-1467-RGB,DES-6570-OUT;n:type:ShaderForge.SFN_Slider,id:6570,x:31423,y:32669,ptovrint:False,ptlb:amount_copy,ptin:_amount_copy,varname:_amount_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Lerp,id:3399,x:31985,y:32781,varname:node_3399,prsc:2|A-4643-OUT,B-6498-OUT,T-6003-OUT;n:type:ShaderForge.SFN_Vector1,id:4643,x:31966,y:32701,varname:node_4643,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:6498,x:31705,y:32808,varname:node_6498,prsc:2|A-2346-OUT,B-6003-OUT;n:type:ShaderForge.SFN_Vector1,id:6003,x:31705,y:32973,varname:node_6003,prsc:2,v1:2;n:type:ShaderForge.SFN_RemapRange,id:2346,x:31471,y:32808,varname:node_2346,prsc:2,frmn:0,frmx:1,tomn:0.5,tomx:1|IN-5457-OUT;n:type:ShaderForge.SFN_Noise,id:1547,x:31088,y:32815,varname:node_1547,prsc:2|XY-9350-OUT;n:type:ShaderForge.SFN_Multiply,id:9350,x:30928,y:32815,varname:node_9350,prsc:2|A-4420-UVOUT,B-1996-TSL;n:type:ShaderForge.SFN_Slider,id:3732,x:32710,y:32947,ptovrint:False,ptlb:Opacity_copy,ptin:_Opacity_copy,varname:_Opacity_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.4320287,max:1;n:type:ShaderForge.SFN_Multiply,id:7159,x:32566,y:32700,varname:node_7159,prsc:2|A-2417-OUT,B-3447-OUT;n:type:ShaderForge.SFN_Multiply,id:5982,x:32515,y:32440,varname:node_5982,prsc:2|A-659-OUT,B-4877-OUT;n:type:ShaderForge.SFN_Slider,id:3447,x:32350,y:32904,ptovrint:False,ptlb:scanline opacity_copy,ptin:_scanlineopacity_copy,varname:_scanlineopacity_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.7079754,max:1;n:type:ShaderForge.SFN_Power,id:2417,x:32350,y:32700,varname:node_2417,prsc:2|VAL-8343-OUT,EXP-397-OUT;n:type:ShaderForge.SFN_Frac,id:8343,x:31999,y:32938,varname:node_8343,prsc:2|IN-5031-OUT;n:type:ShaderForge.SFN_Vector1,id:397,x:32157,y:32972,varname:node_397,prsc:2,v1:10;n:type:ShaderForge.SFN_OneMinus,id:9824,x:31772,y:33210,varname:node_9824,prsc:2|IN-7716-OUT;n:type:ShaderForge.SFN_Add,id:7716,x:31595,y:33210,varname:node_7716,prsc:2|A-3026-OUT,B-6758-OUT;n:type:ShaderForge.SFN_Multiply,id:3026,x:31397,y:33210,varname:node_3026,prsc:2|A-3339-OUT,B-6738-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:543,x:30980,y:33088,varname:node_543,prsc:2;n:type:ShaderForge.SFN_Append,id:6738,x:31180,y:33324,varname:node_6738,prsc:2|A-7218-OUT,B-9272-OUT;n:type:ShaderForge.SFN_Vector1,id:7218,x:30993,y:33277,varname:node_7218,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:9272,x:30993,y:33384,cmnt:canLine,varname:node_9272,prsc:2,v1:1;n:type:ShaderForge.SFN_Append,id:2989,x:31180,y:33546,varname:node_2989,prsc:2|A-7015-OUT,B-2113-OUT;n:type:ShaderForge.SFN_Vector1,id:7015,x:31013,y:33546,varname:node_7015,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:2113,x:31013,y:33623,varname:node_2113,prsc:2,v1:3;n:type:ShaderForge.SFN_Append,id:3339,x:31180,y:33107,varname:node_3339,prsc:2|A-543-X,B-543-Y;n:type:ShaderForge.SFN_ComponentMask,id:5031,x:31965,y:33210,varname:node_5031,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-9824-OUT;n:type:ShaderForge.SFN_ScreenPos,id:4420,x:30743,y:32715,varname:node_4420,prsc:2,sctp:0;n:type:ShaderForge.SFN_Time,id:1996,x:30743,y:32876,varname:node_1996,prsc:2;n:type:ShaderForge.SFN_Time,id:518,x:31168,y:33699,varname:node_518,prsc:2;n:type:ShaderForge.SFN_Multiply,id:6758,x:31394,y:33604,varname:node_6758,prsc:2|A-2989-OUT,B-518-TSL;n:type:ShaderForge.SFN_Multiply,id:5457,x:31294,y:32815,varname:node_5457,prsc:2|A-1547-OUT,B-1648-OUT;n:type:ShaderForge.SFN_Vector1,id:1648,x:31258,y:32963,varname:node_1648,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Noise,id:4474,x:32656,y:33492,varname:node_4474,prsc:2;proporder:3730-9797-1467-656-6570-3732-3447;pass:END;sub:END;*/

Shader "Shader Forge/dissolve" {
    Properties {
        _noisetexture ("noise texture", 2D) = "white" {}
        _dissolveslider ("dissolve slider", Range(0, 1)) = 1
        _MainTex ("Base Color_copy", 2D) = "gray" {}
        _node_8037_copy ("node_8037_copy", Color) = (0.3,0.8,0.9,1)
        _amount_copy ("amount_copy", Range(0, 1)) = 1
        _Opacity_copy ("Opacity_copy", Range(0, 1)) = 0.4320287
        _scanlineopacity_copy ("scanline opacity_copy", Range(0, 1)) = 0.7079754
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _noisetexture; uniform float4 _noisetexture_ST;
            uniform float _dissolveslider;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _node_8037_copy;
            uniform float _amount_copy;
            uniform float _Opacity_copy;
            uniform float _scanlineopacity_copy;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 projPos : TEXCOORD3;
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float4 _noisetexture_var = tex2D(_noisetexture,TRANSFORM_TEX(i.uv0, _noisetexture));
                clip(step(_noisetexture_var.rgb,_dissolveslider).r - 0.5);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 node_1996 = _Time;
                float2 node_9350 = ((sceneUVs * 2 - 1).rg*node_1996.r);
                float2 node_1547_skew = node_9350 + 0.2127+node_9350.x*0.3713*node_9350.y;
                float2 node_1547_rnd = 4.789*sin(489.123*(node_1547_skew));
                float node_1547 = frac(node_1547_rnd.x*node_1547_rnd.y*(1+node_1547_skew.x));
                float node_6003 = 2.0;
                float4 node_518 = _Time;
                float node_7159 = (pow(frac((1.0 - ((float2(i.posWorld.r,i.posWorld.g)*float2(1.0,1.0))+(float2(0.0,3.0)*node_518.r))).g),10.0)*_scanlineopacity_copy);
                float node_2324 = (node_7159+_Opacity_copy);
                float3 diffuseColor = (((1.5*((_node_8037_copy.rgb*lerp(_MainTex_var.rgb,dot(_MainTex_var.rgb,float3(0.3,0.59,0.11)),_amount_copy))*lerp(1.0,(((node_1547*0.1)*0.5+0.5)*node_6003),node_6003)))+node_7159)*node_2324);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor,node_2324);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _noisetexture; uniform float4 _noisetexture_ST;
            uniform float _dissolveslider;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _node_8037_copy;
            uniform float _amount_copy;
            uniform float _Opacity_copy;
            uniform float _scanlineopacity_copy;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 projPos : TEXCOORD3;
                LIGHTING_COORDS(4,5)
                UNITY_FOG_COORDS(6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float4 _noisetexture_var = tex2D(_noisetexture,TRANSFORM_TEX(i.uv0, _noisetexture));
                clip(step(_noisetexture_var.rgb,_dissolveslider).r - 0.5);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 node_1996 = _Time;
                float2 node_9350 = ((sceneUVs * 2 - 1).rg*node_1996.r);
                float2 node_1547_skew = node_9350 + 0.2127+node_9350.x*0.3713*node_9350.y;
                float2 node_1547_rnd = 4.789*sin(489.123*(node_1547_skew));
                float node_1547 = frac(node_1547_rnd.x*node_1547_rnd.y*(1+node_1547_skew.x));
                float node_6003 = 2.0;
                float4 node_518 = _Time;
                float node_7159 = (pow(frac((1.0 - ((float2(i.posWorld.r,i.posWorld.g)*float2(1.0,1.0))+(float2(0.0,3.0)*node_518.r))).g),10.0)*_scanlineopacity_copy);
                float node_2324 = (node_7159+_Opacity_copy);
                float3 diffuseColor = (((1.5*((_node_8037_copy.rgb*lerp(_MainTex_var.rgb,dot(_MainTex_var.rgb,float3(0.3,0.59,0.11)),_amount_copy))*lerp(1.0,(((node_1547*0.1)*0.5+0.5)*node_6003),node_6003)))+node_7159)*node_2324);
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * node_2324,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _noisetexture; uniform float4 _noisetexture_ST;
            uniform float _dissolveslider;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 _noisetexture_var = tex2D(_noisetexture,TRANSFORM_TEX(i.uv0, _noisetexture));
                clip(step(_noisetexture_var.rgb,_dissolveslider).r - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
