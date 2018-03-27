// Developed by SIJO STUDIOS Pty Ltd
// Developer: Simon Anderson
// Version: 1.2
Shader "SIJO/doubleSided/Unlit_AlphaCutOff" 
{
Properties 
{
_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
_ColorTint ("Tint", Color) = (1.0, 1.0, 1.0, 1.0)
_Cutoff ("Alpha cutoff", Range (0,1)) = 0.5
}
SubShader 
{
Tags { "RenderType" = "Transparent" }

//used for backface culling
Cull Off

// Surface shaders are placed between CGPROGRAM and ENDCG
// - They use #pragma to let unity know its a surface shader
// - Must be in a SubShader block
CGPROGRAM
#pragma surface surf Unlit alphatest:_Cutoff
struct Input 
{
float2 uv_MainTex;
};
sampler2D _MainTex;	

// applies a color tint to the shader	
fixed4 _ColorTint;

half4 LightingUnlit(SurfaceOutput s, half3 lightDir, half atten)
{
return half4(s.Albedo, s.Alpha);
}

// applies the texture to the UV's
void surf (Input IN, inout SurfaceOutput o) 
{
fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _ColorTint;
o.Albedo = c.rgb;
o.Alpha = c.a;
}
ENDCG
} 
}