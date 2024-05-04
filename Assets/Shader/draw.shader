Shader "Unlit/draw"
{
    Properties
    {
        [MainTexture] _MainTex ("Texture", 2D) = "black" {}
        _color("color",Color)=(1,1,1,1)
        _BrushPos("BrushPos",Vector)=(0,0,0,0)
        _BrushSize("BrushSize",Vector)=(0,0,0,0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque"  "RenderPipeline" = "UniversalPipeline"}
        LOD 100
        Blend One One
        Pass
        {
            Name "draw"
            HLSLPROGRAM
            

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            // The Blit.hlsl file provides the vertex shader (Vert),
            // input structure (Attributes) and output strucutre (Varyings)
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
            #pragma vertex Vert
            #pragma fragment frag
            

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            TEXTURE2D(_MaskTex);
            SAMPLER(sampler_MaskTex);
            
            float4 _BrushPos;
            float4 _BrushSize;
            float4 _color;

            half4 frag (Varyings i) : SV_Target
            {
                half2 pos=_BrushPos;
                half2 posUV=(i.texcoord -1+pos)*_BrushSize.xy+0.5;
                #if UNITY_UV_STARTS_AT_TOP
                posUV.y=1.0-posUV.y;
                #endif
                
                half4 col = SAMPLE_TEXTURE2D(_BlitTexture, sampler_LinearClamp, posUV);
                // apply fog
                return col;
            }
            ENDHLSL
        }
        
    }
}
