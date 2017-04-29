// Compiled shader for PC, Mac & Linux Standalone

//////////////////////////////////////////////////////////////////////////
// 
// NOTE: This is *not* a valid shader file, the contents are provided just
// for information and for debugging purposes only.
// 
//////////////////////////////////////////////////////////////////////////
// Skipping shader variants that would not be included into build of current scene.

Shader "Skybox/Procedural" {
Properties {
[KeywordEnum(None, Simple, High Quality)]  _SunDisk ("Sun", Float) = 2.000000
 _SunSize ("Sun Size", Range(0.000000,1.000000)) = 0.040000
 _AtmosphereThickness ("Atmoshpere Thickness", Range(0.000000,5.000000)) = 1.000000
 _SkyTint ("Sky Tint", Color) = (0.500000,0.500000,0.500000,1.000000)
 _GroundColor ("Ground", Color) = (0.369000,0.349000,0.341000,1.000000)
 _Exposure ("Exposure", Range(0.000000,8.000000)) = 1.300000
}
SubShader { 
 Tags { "QUEUE"="Background" "RenderType"="Background" "PreviewType"="Skybox" }
 Pass {
  Tags { "QUEUE"="Background" "RenderType"="Background" "PreviewType"="Skybox" }
  ZWrite Off
  Cull Off
  //////////////////////////////////
  //                              //
  //      Compiled programs       //
  //                              //
  //////////////////////////////////
//////////////////////////////////////////////////////
Keywords set in this variant: _SUNDISK_NONE 
-- Vertex shader for "metal":
Uses vertex data channel "Vertex"

Constant Buffer "Globals" (170 bytes) on slot 0 {
  Matrix4x4 glstate_matrix_mvp at 16
  Matrix4x4 unity_ObjectToWorld at 80
  Vector4 _WorldSpaceLightPos0 at 0
  ScalarHalf _Exposure at 144
  VectorHalf3 _GroundColor at 152
  VectorHalf3 _SkyTint at 160
  ScalarHalf _AtmosphereThickness at 168
}

Shader Disassembly:
#include <metal_stdlib>
#include <metal_texture>
using namespace metal;
struct Globals_Type
{
    float4 _WorldSpaceLightPos0;
    float4 hlslcc_mtx4x4glstate_matrix_mvp[4];
    float4 hlslcc_mtx4x4unity_ObjectToWorld[4];
    half _Exposure;
    half3 _GroundColor;
    half3 _SkyTint;
    half _AtmosphereThickness;
};

struct Mtl_VertexIn
{
    float4 POSITION0 [[ attribute(0) ]] ;
};

struct Mtl_VertexOut
{
    float4 mtl_Position [[ position ]];
    half TEXCOORD0 [[ user(TEXCOORD0) ]];
    half3 TEXCOORD1 [[ user(TEXCOORD1) ]];
    half3 TEXCOORD2 [[ user(TEXCOORD2) ]];
};

vertex Mtl_VertexOut xlatMtlMain(
    constant Globals_Type& Globals [[ buffer(0) ]],
    Mtl_VertexIn input [[ stage_in ]])
{
    Mtl_VertexOut output;
    float4 u_xlat0;
    half3 u_xlat16_1;
    float4 u_xlat2;
    float4 u_xlat3;
    half3 u_xlat16_3;
    bool u_xlatb3;
    float3 u_xlat4;
    half3 u_xlat16_4;
    float4 u_xlat5;
    float3 u_xlat6;
    float u_xlat9;
    float u_xlat11;
    float3 u_xlat12;
    float u_xlat17;
    float u_xlat18;
    float u_xlat19;
    float u_xlat21;
    float u_xlat24;
    float u_xlat25;
    u_xlat0 = input.POSITION0.yyyy * Globals.hlslcc_mtx4x4glstate_matrix_mvp[1];
    u_xlat0 = Globals.hlslcc_mtx4x4glstate_matrix_mvp[0] * input.POSITION0.xxxx + u_xlat0;
    u_xlat0 = Globals.hlslcc_mtx4x4glstate_matrix_mvp[2] * input.POSITION0.zzzz + u_xlat0;
    output.mtl_Position = u_xlat0 + Globals.hlslcc_mtx4x4glstate_matrix_mvp[3];
    u_xlat0.xyz = (-float3(Globals._SkyTint.xxyz.yzw)) + float3(1.0, 1.0, 1.0);
    u_xlat0.xyz = u_xlat0.xyz * float3(0.300000012, 0.300000042, 0.300000012) + float3(0.5, 0.419999987, 0.324999988);
    u_xlat0.xyz = u_xlat0.xyz * u_xlat0.xyz;
    u_xlat0.xyz = u_xlat0.xyz * u_xlat0.xyz;
    u_xlat0.xyz = float3(1.0, 1.0, 1.0) / u_xlat0.xyz;
    u_xlat16_1.x = log2(Globals._AtmosphereThickness);
    u_xlat16_1.x = half(float(u_xlat16_1.x) * 2.5);
    u_xlat16_1.x = exp2(u_xlat16_1.x);
    u_xlat16_1.xy = half2(float2(u_xlat16_1.xx) * float2(0.049999997, 0.0314159282));
    u_xlat2.xyz = input.POSITION0.yyy * Globals.hlslcc_mtx4x4unity_ObjectToWorld[1].xyz;
    u_xlat2.xyz = Globals.hlslcc_mtx4x4unity_ObjectToWorld[0].xyz * input.POSITION0.xxx + u_xlat2.xyz;
    u_xlat2.xyz = Globals.hlslcc_mtx4x4unity_ObjectToWorld[2].xyz * input.POSITION0.zzz + u_xlat2.xyz;
    u_xlat21 = dot(u_xlat2.xyz, u_xlat2.xyz);
    u_xlat21 = rsqrt(u_xlat21);
    u_xlat2.xzw = float3(u_xlat21) * u_xlat2.xyz;
    u_xlatb3 = u_xlat2.z>=0.0;
    if(u_xlatb3){
        u_xlat3.x = u_xlat2.z * u_xlat2.z + 0.0506249666;
        u_xlat3.x = sqrt(u_xlat3.x);
        u_xlat3.x = (-u_xlat2.y) * u_xlat21 + u_xlat3.x;
        u_xlat21 = (-u_xlat2.y) * u_xlat21 + 1.0;
        u_xlat9 = u_xlat21 * 5.25 + -6.80000019;
        u_xlat9 = u_xlat21 * u_xlat9 + 3.82999992;
        u_xlat9 = u_xlat21 * u_xlat9 + 0.458999991;
        u_xlat21 = u_xlat21 * u_xlat9 + -0.00286999997;
        u_xlat21 = u_xlat21 * 1.44269502;
        u_xlat21 = exp2(u_xlat21);
        u_xlat21 = u_xlat21 * 0.246031836;
        u_xlat3.xy = u_xlat3.xx * float2(0.5, 20.0);
        u_xlat4.xyz = u_xlat2.xzw * u_xlat3.xxx;
        u_xlat4.xyz = u_xlat4.xyz * float3(0.5, 0.5, 0.5) + float3(0.0, 1.00010002, 0.0);
        u_xlat9 = dot(u_xlat4.xyz, u_xlat4.xyz);
        u_xlat9 = sqrt(u_xlat9);
        u_xlat17 = (-u_xlat9) + 1.0;
        u_xlat17 = u_xlat17 * 230.831207;
        u_xlat17 = exp2(u_xlat17);
        u_xlat24 = dot(Globals._WorldSpaceLightPos0.xyz, u_xlat4.xyz);
        u_xlat24 = u_xlat24 / u_xlat9;
        u_xlat25 = dot(u_xlat2.xzw, u_xlat4.xyz);
        u_xlat9 = u_xlat25 / u_xlat9;
        u_xlat24 = (-u_xlat24) + 1.0;
        u_xlat25 = u_xlat24 * 5.25 + -6.80000019;
        u_xlat25 = u_xlat24 * u_xlat25 + 3.82999992;
        u_xlat25 = u_xlat24 * u_xlat25 + 0.458999991;
        u_xlat24 = u_xlat24 * u_xlat25 + -0.00286999997;
        u_xlat24 = u_xlat24 * 1.44269502;
        u_xlat24 = exp2(u_xlat24);
        u_xlat9 = (-u_xlat9) + 1.0;
        u_xlat25 = u_xlat9 * 5.25 + -6.80000019;
        u_xlat25 = u_xlat9 * u_xlat25 + 3.82999992;
        u_xlat25 = u_xlat9 * u_xlat25 + 0.458999991;
        u_xlat9 = u_xlat9 * u_xlat25 + -0.00286999997;
        u_xlat9 = u_xlat9 * 1.44269502;
        u_xlat9 = exp2(u_xlat9);
        u_xlat9 = u_xlat9 * 0.25;
        u_xlat9 = u_xlat24 * 0.25 + (-u_xlat9);
        u_xlat9 = u_xlat17 * u_xlat9 + u_xlat21;
        u_xlat9 = max(u_xlat9, 0.0);
        u_xlat9 = min(u_xlat9, 50.0);
        u_xlat5.xyz = u_xlat0.xyz * float3(u_xlat16_1.yyy) + float3(0.0125663709, 0.0125663709, 0.0125663709);
        u_xlat6.xyz = (-float3(u_xlat9)) * u_xlat5.xyz;
        u_xlat6.xyz = u_xlat6.xyz * float3(1.44269502, 1.44269502, 1.44269502);
        u_xlat6.xyz = exp2(u_xlat6.xyz);
        u_xlat9 = u_xlat3.y * u_xlat17;
        u_xlat3.xzw = u_xlat2.xzw * u_xlat3.xxx + u_xlat4.xyz;
        u_xlat4.x = dot(u_xlat3.xzw, u_xlat3.xzw);
        u_xlat4.x = sqrt(u_xlat4.x);
        u_xlat11 = (-u_xlat4.x) + 1.0;
        u_xlat11 = u_xlat11 * 230.831207;
        u_xlat11 = exp2(u_xlat11);
        u_xlat18 = dot(Globals._WorldSpaceLightPos0.xyz, u_xlat3.xzw);
        u_xlat18 = u_xlat18 / u_xlat4.x;
        u_xlat3.x = dot(u_xlat2.xzw, u_xlat3.xzw);
        u_xlat3.x = u_xlat3.x / u_xlat4.x;
        u_xlat17 = (-u_xlat18) + 1.0;
        u_xlat24 = u_xlat17 * 5.25 + -6.80000019;
        u_xlat24 = u_xlat17 * u_xlat24 + 3.82999992;
        u_xlat24 = u_xlat17 * u_xlat24 + 0.458999991;
        u_xlat17 = u_xlat17 * u_xlat24 + -0.00286999997;
        u_xlat17 = u_xlat17 * 1.44269502;
        u_xlat17 = exp2(u_xlat17);
        u_xlat3.x = (-u_xlat3.x) + 1.0;
        u_xlat24 = u_xlat3.x * 5.25 + -6.80000019;
        u_xlat24 = u_xlat3.x * u_xlat24 + 3.82999992;
        u_xlat24 = u_xlat3.x * u_xlat24 + 0.458999991;
        u_xlat3.x = u_xlat3.x * u_xlat24 + -0.00286999997;
        u_xlat3.x = u_xlat3.x * 1.44269502;
        u_xlat3.x = exp2(u_xlat3.x);
        u_xlat3.x = u_xlat3.x * 0.25;
        u_xlat3.x = u_xlat17 * 0.25 + (-u_xlat3.x);
        u_xlat21 = u_xlat11 * u_xlat3.x + u_xlat21;
        u_xlat21 = max(u_xlat21, 0.0);
        u_xlat21 = min(u_xlat21, 50.0);
        u_xlat3.xzw = u_xlat5.xyz * (-float3(u_xlat21));
        u_xlat3.xzw = u_xlat3.xzw * float3(1.44269502, 1.44269502, 1.44269502);
        u_xlat3.xzw = exp2(u_xlat3.xzw);
        u_xlat21 = u_xlat3.y * u_xlat11;
        u_xlat3.xyz = float3(u_xlat21) * u_xlat3.xzw;
        u_xlat3.xyz = u_xlat6.xyz * float3(u_xlat9) + u_xlat3.xyz;
        u_xlat4.xyz = u_xlat0.xyz * float3(u_xlat16_1.xxx);
        u_xlat4.xyz = u_xlat3.xyz * u_xlat4.xyz;
        u_xlat3.xyz = u_xlat3.xyz * float3(0.0199999996, 0.0199999996, 0.0199999996);
        u_xlat16_4.xyz = half3(u_xlat4.xyz);
        u_xlat16_3.xyz = half3(u_xlat3.xyz);
    } else {
        u_xlat21 = min(u_xlat2.z, -0.00100000005);
        u_xlat21 = -9.99999975e-05 / u_xlat21;
        u_xlat5.xyz = float3(u_xlat21) * u_xlat2.xzw + float3(0.0, 1.00010002, 0.0);
        u_xlat5.w = dot((-u_xlat2.xzw), u_xlat5.xyz);
        u_xlat5.x = dot(Globals._WorldSpaceLightPos0.xyz, u_xlat5.xyz);
        u_xlat5.xy = (-u_xlat5.xw) + float2(1.0, 1.0);
        u_xlat19 = u_xlat5.y * 5.25 + -6.80000019;
        u_xlat19 = u_xlat5.y * u_xlat19 + 3.82999992;
        u_xlat19 = u_xlat5.y * u_xlat19 + 0.458999991;
        u_xlat12.x = u_xlat5.y * u_xlat19 + -0.00286999997;
        u_xlat12.x = u_xlat12.x * 1.44269502;
        u_xlat5.y = exp2(u_xlat12.x);
        u_xlat19 = u_xlat5.x * 5.25 + -6.80000019;
        u_xlat19 = u_xlat5.x * u_xlat19 + 3.82999992;
        u_xlat19 = u_xlat5.x * u_xlat19 + 0.458999991;
        u_xlat5.x = u_xlat5.x * u_xlat19 + -0.00286999997;
        u_xlat5.xyz = u_xlat5.xyy * float3(1.44269502, 0.25, 0.249900013);
        u_xlat5.x = exp2(u_xlat5.x);
        u_xlat5.x = u_xlat5.x * 0.25 + u_xlat5.y;
        u_xlat12.xz = float2(u_xlat21) * float2(0.5, 20.0);
        u_xlat6.xyz = u_xlat2.xzw * u_xlat12.xxx;
        u_xlat6.xyz = u_xlat6.xyz * float3(0.5, 0.5, 0.5) + float3(0.0, 1.00010002, 0.0);
        u_xlat21 = dot(u_xlat6.xyz, u_xlat6.xyz);
        u_xlat21 = sqrt(u_xlat21);
        u_xlat21 = (-u_xlat21) + 1.0;
        u_xlat21 = u_xlat21 * 230.831207;
        u_xlat21 = exp2(u_xlat21);
        u_xlat5.x = u_xlat21 * u_xlat5.x + (-u_xlat5.z);
        u_xlat5.x = max(u_xlat5.x, 0.0);
        u_xlat5.x = min(u_xlat5.x, 50.0);
        u_xlat6.xyz = u_xlat0.xyz * float3(u_xlat16_1.yyy) + float3(0.0125663709, 0.0125663709, 0.0125663709);
        u_xlat5.xyz = (-u_xlat5.xxx) * u_xlat6.xyz;
        u_xlat5.xyz = u_xlat5.xyz * float3(1.44269502, 1.44269502, 1.44269502);
        u_xlat3.xyz = exp2(u_xlat5.xyz);
        u_xlat21 = u_xlat12.z * u_xlat21;
        u_xlat5.xyz = float3(u_xlat21) * u_xlat3.xyz;
        u_xlat0.xyz = u_xlat0.xyz * float3(u_xlat16_1.xxx) + float3(0.0199999996, 0.0199999996, 0.0199999996);
        u_xlat4.xyz = u_xlat0.xyz * u_xlat5.xyz;
        u_xlat16_4.xyz = half3(u_xlat4.xyz);
        u_xlat16_3.xyz = half3(u_xlat3.xyz);
    }
    u_xlat0.x = u_xlat2.z * -50.0;
    u_xlat16_1.xyz = half3(Globals._GroundColor.xxyz.yzw * Globals._GroundColor.xxyz.yzw);
    u_xlat16_1.xyz = half3(u_xlat16_1.xyz * u_xlat16_3.xyz + u_xlat16_4.xyz);
    output.TEXCOORD1.xyz = half3(u_xlat16_1.xyz * half3(Globals._Exposure));
    u_xlat16_1.x = dot(Globals._WorldSpaceLightPos0.xyz, (-u_xlat2.xzw));
    u_xlat16_1.x = half(u_xlat16_1.x * u_xlat16_1.x);
    u_xlat16_1.x = half(float(u_xlat16_1.x) * 0.75 + 0.75);
    u_xlat16_1.xyz = half3(u_xlat16_1.xxx * u_xlat16_4.xyz);
    output.TEXCOORD2.xyz = half3(u_xlat16_1.xyz * half3(Globals._Exposure));
    output.TEXCOORD0 = half(u_xlat0.x);
    return output;
}


-- Fragment shader for "metal":
Shader Disassembly:
#include <metal_stdlib>
#include <metal_texture>
using namespace metal;
struct Mtl_FragmentIn
{
    half TEXCOORD0 [[ user(TEXCOORD0) ]] ;
    half3 TEXCOORD1 [[ user(TEXCOORD1) ]] ;
    half3 TEXCOORD2 [[ user(TEXCOORD2) ]] ;
};

struct Mtl_FragmentOut
{
    half4 SV_Target0 [[ color(0) ]];
};

fragment Mtl_FragmentOut xlatMtlMain(
    Mtl_FragmentIn input [[ stage_in ]])
{
    Mtl_FragmentOut output;
    half3 u_xlat16_0;
    half3 u_xlat16_1;
    u_xlat16_0.x = input.TEXCOORD0;
    u_xlat16_0.x = clamp(u_xlat16_0.x, 0.0h, 1.0h);
    u_xlat16_1.xyz = half3(input.TEXCOORD1.xyz + (-input.TEXCOORD2.xyz));
    u_xlat16_0.xyz = half3(u_xlat16_0.xxx * u_xlat16_1.xyz + input.TEXCOORD2.xyz);
    output.SV_Target0.xyz = sqrt(u_xlat16_0.xyz);
    output.SV_Target0.w = 1.0;
    return output;
}


-- Vertex shader for "glcore":
Shader Disassembly:
#ifdef VERTEX
#version 150
#extension GL_ARB_explicit_attrib_location : require
#extension GL_ARB_shader_bit_encoding : enable

uniform 	vec4 _WorldSpaceLightPos0;
uniform 	vec4 hlslcc_mtx4x4glstate_matrix_mvp[4];
uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
uniform 	float _Exposure;
uniform 	vec3 _GroundColor;
uniform 	vec3 _SkyTint;
uniform 	float _AtmosphereThickness;
in  vec4 in_POSITION0;
out float vs_TEXCOORD0;
out vec3 vs_TEXCOORD1;
out vec3 vs_TEXCOORD2;
vec4 u_xlat0;
vec2 u_xlat1;
vec4 u_xlat2;
vec4 u_xlat3;
vec3 u_xlat4;
vec4 u_xlat5;
vec3 u_xlat6;
vec3 u_xlat8;
float u_xlat9;
vec2 u_xlat15;
bool u_xlatb15;
float u_xlat17;
float u_xlat21;
float u_xlat22;
void main()
{
    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4glstate_matrix_mvp[1];
    u_xlat0 = hlslcc_mtx4x4glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
    u_xlat0 = hlslcc_mtx4x4glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
    gl_Position = u_xlat0 + hlslcc_mtx4x4glstate_matrix_mvp[3];
    u_xlat0.xyz = (-vec3(_SkyTint.x, _SkyTint.y, _SkyTint.z)) + vec3(1.0, 1.0, 1.0);
    u_xlat0.xyz = u_xlat0.xyz * vec3(0.300000012, 0.300000042, 0.300000012) + vec3(0.5, 0.419999987, 0.324999988);
    u_xlat0.xyz = u_xlat0.xyz * u_xlat0.xyz;
    u_xlat0.xyz = u_xlat0.xyz * u_xlat0.xyz;
    u_xlat0.xyz = vec3(1.0, 1.0, 1.0) / u_xlat0.xyz;
    u_xlat21 = log2(_AtmosphereThickness);
    u_xlat21 = u_xlat21 * 2.5;
    u_xlat21 = exp2(u_xlat21);
    u_xlat1.xy = vec2(u_xlat21) * vec2(0.049999997, 0.0314159282);
    u_xlat2.xyz = in_POSITION0.yyy * hlslcc_mtx4x4unity_ObjectToWorld[1].xyz;
    u_xlat2.xyz = hlslcc_mtx4x4unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
    u_xlat2.xyz = hlslcc_mtx4x4unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
    u_xlat21 = dot(u_xlat2.xyz, u_xlat2.xyz);
    u_xlat21 = inversesqrt(u_xlat21);
    u_xlat2.xzw = vec3(u_xlat21) * u_xlat2.xyz;
    u_xlatb15 = u_xlat2.z>=0.0;
    if(u_xlatb15){
        u_xlat15.x = u_xlat2.z * u_xlat2.z + 0.0506249666;
        u_xlat15.x = sqrt(u_xlat15.x);
        u_xlat21 = (-u_xlat2.y) * u_xlat21 + u_xlat15.x;
        u_xlat15.x = (-u_xlat2.z) * 1.0 + 1.0;
        u_xlat22 = u_xlat15.x * 5.25 + -6.80000019;
        u_xlat22 = u_xlat15.x * u_xlat22 + 3.82999992;
        u_xlat22 = u_xlat15.x * u_xlat22 + 0.458999991;
        u_xlat15.x = u_xlat15.x * u_xlat22 + -0.00286999997;
        u_xlat15.x = u_xlat15.x * 1.44269502;
        u_xlat15.x = exp2(u_xlat15.x);
        u_xlat3.xy = vec2(u_xlat21) * vec2(0.5, 20.0);
        u_xlat4.xyz = u_xlat2.xzw * u_xlat3.xxx;
        u_xlat4.xyz = u_xlat4.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.0, 1.00010002, 0.0);
        u_xlat21 = dot(u_xlat4.xyz, u_xlat4.xyz);
        u_xlat21 = sqrt(u_xlat21);
        u_xlat15.y = (-u_xlat21) + 1.0;
        u_xlat15.xy = u_xlat15.xy * vec2(0.246031836, 230.831207);
        u_xlat22 = exp2(u_xlat15.y);
        u_xlat9 = dot(_WorldSpaceLightPos0.xyz, u_xlat4.xyz);
        u_xlat9 = u_xlat9 / u_xlat21;
        u_xlat17 = dot(u_xlat2.xzw, u_xlat4.xyz);
        u_xlat21 = u_xlat17 / u_xlat21;
        u_xlat9 = (-u_xlat9) + 1.0;
        u_xlat17 = u_xlat9 * 5.25 + -6.80000019;
        u_xlat17 = u_xlat9 * u_xlat17 + 3.82999992;
        u_xlat17 = u_xlat9 * u_xlat17 + 0.458999991;
        u_xlat9 = u_xlat9 * u_xlat17 + -0.00286999997;
        u_xlat9 = u_xlat9 * 1.44269502;
        u_xlat9 = exp2(u_xlat9);
        u_xlat21 = (-u_xlat21) + 1.0;
        u_xlat17 = u_xlat21 * 5.25 + -6.80000019;
        u_xlat17 = u_xlat21 * u_xlat17 + 3.82999992;
        u_xlat17 = u_xlat21 * u_xlat17 + 0.458999991;
        u_xlat21 = u_xlat21 * u_xlat17 + -0.00286999997;
        u_xlat21 = u_xlat21 * 1.44269502;
        u_xlat21 = exp2(u_xlat21);
        u_xlat21 = u_xlat21 * 0.25;
        u_xlat21 = u_xlat9 * 0.25 + (-u_xlat21);
        u_xlat21 = u_xlat22 * u_xlat21 + u_xlat15.x;
        u_xlat21 = max(u_xlat21, 0.0);
        u_xlat21 = min(u_xlat21, 50.0);
        u_xlat5.xyz = u_xlat0.xyz * u_xlat1.yyy + vec3(0.0125663709, 0.0125663709, 0.0125663709);
        u_xlat6.xyz = (-vec3(u_xlat21)) * u_xlat5.xyz;
        u_xlat6.xyz = u_xlat6.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat6.xyz = exp2(u_xlat6.xyz);
        u_xlat21 = u_xlat3.y * u_xlat22;
        u_xlat3.xzw = u_xlat2.xzw * u_xlat3.xxx + u_xlat4.xyz;
        u_xlat22 = dot(u_xlat3.xzw, u_xlat3.xzw);
        u_xlat22 = sqrt(u_xlat22);
        u_xlat9 = (-u_xlat22) + 1.0;
        u_xlat9 = u_xlat9 * 230.831207;
        u_xlat9 = exp2(u_xlat9);
        u_xlat4.x = dot(_WorldSpaceLightPos0.xyz, u_xlat3.xzw);
        u_xlat4.x = u_xlat4.x / u_xlat22;
        u_xlat3.x = dot(u_xlat2.xzw, u_xlat3.xzw);
        u_xlat22 = u_xlat3.x / u_xlat22;
        u_xlat3.x = (-u_xlat4.x) + 1.0;
        u_xlat17 = u_xlat3.x * 5.25 + -6.80000019;
        u_xlat17 = u_xlat3.x * u_xlat17 + 3.82999992;
        u_xlat17 = u_xlat3.x * u_xlat17 + 0.458999991;
        u_xlat3.x = u_xlat3.x * u_xlat17 + -0.00286999997;
        u_xlat3.x = u_xlat3.x * 1.44269502;
        u_xlat3.x = exp2(u_xlat3.x);
        u_xlat22 = (-u_xlat22) + 1.0;
        u_xlat17 = u_xlat22 * 5.25 + -6.80000019;
        u_xlat17 = u_xlat22 * u_xlat17 + 3.82999992;
        u_xlat17 = u_xlat22 * u_xlat17 + 0.458999991;
        u_xlat22 = u_xlat22 * u_xlat17 + -0.00286999997;
        u_xlat22 = u_xlat22 * 1.44269502;
        u_xlat22 = exp2(u_xlat22);
        u_xlat22 = u_xlat22 * 0.25;
        u_xlat22 = u_xlat3.x * 0.25 + (-u_xlat22);
        u_xlat15.x = u_xlat9 * u_xlat22 + u_xlat15.x;
        u_xlat15.x = max(u_xlat15.x, 0.0);
        u_xlat15.x = min(u_xlat15.x, 50.0);
        u_xlat3.xzw = u_xlat5.xyz * (-u_xlat15.xxx);
        u_xlat3.xzw = u_xlat3.xzw * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat3.xzw = exp2(u_xlat3.xzw);
        u_xlat15.x = u_xlat3.y * u_xlat9;
        u_xlat3.xyz = u_xlat15.xxx * u_xlat3.xzw;
        u_xlat3.xyz = u_xlat6.xyz * vec3(u_xlat21) + u_xlat3.xyz;
        u_xlat4.xyz = u_xlat0.xyz * u_xlat1.xxx;
        u_xlat4.xyz = u_xlat3.xyz * u_xlat4.xyz;
        u_xlat3.xyz = u_xlat3.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996);
    } else {
        u_xlat21 = min(u_xlat2.z, -0.00100000005);
        u_xlat21 = -9.99999975e-05 / u_xlat21;
        u_xlat5.xyz = vec3(u_xlat21) * u_xlat2.xzw + vec3(0.0, 1.00010002, 0.0);
        u_xlat15.x = dot((-u_xlat2.xzw), u_xlat5.xyz);
        u_xlat15.y = dot(_WorldSpaceLightPos0.xyz, u_xlat5.xyz);
        u_xlat15.xy = (-u_xlat15.xy) + vec2(1.0, 1.0);
        u_xlat9 = u_xlat15.x * 5.25 + -6.80000019;
        u_xlat9 = u_xlat15.x * u_xlat9 + 3.82999992;
        u_xlat9 = u_xlat15.x * u_xlat9 + 0.458999991;
        u_xlat15.x = u_xlat15.x * u_xlat9 + -0.00286999997;
        u_xlat15.x = u_xlat15.x * 1.44269502;
        u_xlat15.x = exp2(u_xlat15.x);
        u_xlat9 = u_xlat15.y * 5.25 + -6.80000019;
        u_xlat9 = u_xlat15.y * u_xlat9 + 3.82999992;
        u_xlat9 = u_xlat15.y * u_xlat9 + 0.458999991;
        u_xlat22 = u_xlat15.y * u_xlat9 + -0.00286999997;
        u_xlat22 = u_xlat22 * 1.44269502;
        u_xlat22 = exp2(u_xlat22);
        u_xlat5.xy = u_xlat15.xx * vec2(0.25, 0.249900013);
        u_xlat15.x = u_xlat22 * 0.25 + u_xlat5.x;
        u_xlat5.xz = vec2(u_xlat21) * vec2(0.5, 20.0);
        u_xlat6.xyz = u_xlat2.xzw * u_xlat5.xxx;
        u_xlat6.xyz = u_xlat6.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.0, 1.00010002, 0.0);
        u_xlat21 = dot(u_xlat6.xyz, u_xlat6.xyz);
        u_xlat21 = sqrt(u_xlat21);
        u_xlat21 = (-u_xlat21) + 1.0;
        u_xlat21 = u_xlat21 * 230.831207;
        u_xlat21 = exp2(u_xlat21);
        u_xlat15.x = u_xlat21 * u_xlat15.x + (-u_xlat5.y);
        u_xlat15.x = max(u_xlat15.x, 0.0);
        u_xlat15.x = min(u_xlat15.x, 50.0);
        u_xlat5.xyw = u_xlat0.xyz * u_xlat1.yyy + vec3(0.0125663709, 0.0125663709, 0.0125663709);
        u_xlat8.xyz = (-u_xlat15.xxx) * u_xlat5.xyw;
        u_xlat8.xyz = u_xlat8.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat3.xyz = exp2(u_xlat8.xyz);
        u_xlat21 = u_xlat5.z * u_xlat21;
        u_xlat8.xyz = vec3(u_xlat21) * u_xlat3.xyz;
        u_xlat0.xyz = u_xlat0.xyz * u_xlat1.xxx + vec3(0.0199999996, 0.0199999996, 0.0199999996);
        u_xlat4.xyz = u_xlat0.xyz * u_xlat8.xyz;
    //ENDIF
    }
    vs_TEXCOORD0 = u_xlat2.z * -50.0;
    u_xlat0.xyz = vec3(_GroundColor.x, _GroundColor.y, _GroundColor.z) * vec3(_GroundColor.x, _GroundColor.y, _GroundColor.z);
    u_xlat0.xyz = u_xlat0.xyz * u_xlat3.xyz + u_xlat4.xyz;
    vs_TEXCOORD1.xyz = u_xlat0.xyz * vec3(_Exposure);
    u_xlat0.x = dot(_WorldSpaceLightPos0.xyz, (-u_xlat2.xzw));
    u_xlat0.x = u_xlat0.x * u_xlat0.x;
    u_xlat0.x = u_xlat0.x * 0.75 + 0.75;
    u_xlat0.xyz = u_xlat0.xxx * u_xlat4.xyz;
    vs_TEXCOORD2.xyz = u_xlat0.xyz * vec3(_Exposure);
    return;
}

#endif
#ifdef FRAGMENT
#version 150
#extension GL_ARB_explicit_attrib_location : require
#extension GL_ARB_shader_bit_encoding : enable

in  float vs_TEXCOORD0;
in  vec3 vs_TEXCOORD1;
in  vec3 vs_TEXCOORD2;
layout(location = 0) out vec4 SV_Target0;
vec3 u_xlat0;
vec3 u_xlat1;
void main()
{
    u_xlat0.x = vs_TEXCOORD0;
    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
    u_xlat1.xyz = vs_TEXCOORD1.xyz + (-vs_TEXCOORD2.xyz);
    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + vs_TEXCOORD2.xyz;
    SV_Target0.xyz = sqrt(u_xlat0.xyz);
    SV_Target0.w = 1.0;
    return;
}

#endif


-- Fragment shader for "glcore":
Shader Disassembly:
// All GLSL source is contained within the vertex program

-- Vertex shader for "metal":
Uses vertex data channel "Vertex"

Constant Buffer "Globals" (178 bytes) on slot 0 {
  Matrix4x4 glstate_matrix_mvp at 16
  Matrix4x4 unity_ObjectToWorld at 80
  Vector4 _WorldSpaceLightPos0 at 0
  VectorHalf4 _LightColor0 at 144
  ScalarHalf _Exposure at 152
  VectorHalf3 _GroundColor at 160
  VectorHalf3 _SkyTint at 168
  ScalarHalf _AtmosphereThickness at 176
}

Shader Disassembly:
#include <metal_stdlib>
#include <metal_texture>
using namespace metal;
struct Globals_Type
{
    float4 _WorldSpaceLightPos0;
    float4 hlslcc_mtx4x4glstate_matrix_mvp[4];
    float4 hlslcc_mtx4x4unity_ObjectToWorld[4];
    half4 _LightColor0;
    half _Exposure;
    half3 _GroundColor;
    half3 _SkyTint;
    half _AtmosphereThickness;
};

struct Mtl_VertexIn
{
    float4 POSITION0 [[ attribute(0) ]] ;
};

struct Mtl_VertexOut
{
    float4 mtl_Position [[ position ]];
    half3 TEXCOORD0 [[ user(TEXCOORD0) ]];
    half3 TEXCOORD1 [[ user(TEXCOORD1) ]];
    half3 TEXCOORD2 [[ user(TEXCOORD2) ]];
    half3 TEXCOORD3 [[ user(TEXCOORD3) ]];
};

vertex Mtl_VertexOut xlatMtlMain(
    constant Globals_Type& Globals [[ buffer(0) ]],
    Mtl_VertexIn input [[ stage_in ]])
{
    Mtl_VertexOut output;
    float4 u_xlat0;
    half3 u_xlat16_1;
    float4 u_xlat2;
    float4 u_xlat3;
    half3 u_xlat16_3;
    bool u_xlatb3;
    float3 u_xlat4;
    half3 u_xlat16_4;
    float4 u_xlat5;
    float3 u_xlat6;
    float u_xlat9;
    float u_xlat11;
    float3 u_xlat12;
    float u_xlat17;
    float u_xlat18;
    float u_xlat19;
    float u_xlat21;
    float u_xlat24;
    float u_xlat25;
    u_xlat0 = input.POSITION0.yyyy * Globals.hlslcc_mtx4x4glstate_matrix_mvp[1];
    u_xlat0 = Globals.hlslcc_mtx4x4glstate_matrix_mvp[0] * input.POSITION0.xxxx + u_xlat0;
    u_xlat0 = Globals.hlslcc_mtx4x4glstate_matrix_mvp[2] * input.POSITION0.zzzz + u_xlat0;
    output.mtl_Position = u_xlat0 + Globals.hlslcc_mtx4x4glstate_matrix_mvp[3];
    u_xlat0.xyz = (-float3(Globals._SkyTint.xxyz.yzw)) + float3(1.0, 1.0, 1.0);
    u_xlat0.xyz = u_xlat0.xyz * float3(0.300000012, 0.300000042, 0.300000012) + float3(0.5, 0.419999987, 0.324999988);
    u_xlat0.xyz = u_xlat0.xyz * u_xlat0.xyz;
    u_xlat0.xyz = u_xlat0.xyz * u_xlat0.xyz;
    u_xlat0.xyz = float3(1.0, 1.0, 1.0) / u_xlat0.xyz;
    u_xlat16_1.x = log2(Globals._AtmosphereThickness);
    u_xlat16_1.x = half(float(u_xlat16_1.x) * 2.5);
    u_xlat16_1.x = exp2(u_xlat16_1.x);
    u_xlat16_1.xy = half2(float2(u_xlat16_1.xx) * float2(0.049999997, 0.0314159282));
    u_xlat2.xyz = input.POSITION0.yyy * Globals.hlslcc_mtx4x4unity_ObjectToWorld[1].xyz;
    u_xlat2.xyz = Globals.hlslcc_mtx4x4unity_ObjectToWorld[0].xyz * input.POSITION0.xxx + u_xlat2.xyz;
    u_xlat2.xyz = Globals.hlslcc_mtx4x4unity_ObjectToWorld[2].xyz * input.POSITION0.zzz + u_xlat2.xyz;
    u_xlat21 = dot(u_xlat2.xyz, u_xlat2.xyz);
    u_xlat21 = rsqrt(u_xlat21);
    u_xlat2.xzw = float3(u_xlat21) * u_xlat2.xyz;
    u_xlatb3 = u_xlat2.z>=0.0;
    if(u_xlatb3){
        u_xlat3.x = u_xlat2.z * u_xlat2.z + 0.0506249666;
        u_xlat3.x = sqrt(u_xlat3.x);
        u_xlat3.x = (-u_xlat2.y) * u_xlat21 + u_xlat3.x;
        u_xlat21 = (-u_xlat2.y) * u_xlat21 + 1.0;
        u_xlat9 = u_xlat21 * 5.25 + -6.80000019;
        u_xlat9 = u_xlat21 * u_xlat9 + 3.82999992;
        u_xlat9 = u_xlat21 * u_xlat9 + 0.458999991;
        u_xlat21 = u_xlat21 * u_xlat9 + -0.00286999997;
        u_xlat21 = u_xlat21 * 1.44269502;
        u_xlat21 = exp2(u_xlat21);
        u_xlat21 = u_xlat21 * 0.246031836;
        u_xlat3.xy = u_xlat3.xx * float2(0.5, 20.0);
        u_xlat4.xyz = u_xlat2.xzw * u_xlat3.xxx;
        u_xlat4.xyz = u_xlat4.xyz * float3(0.5, 0.5, 0.5) + float3(0.0, 1.00010002, 0.0);
        u_xlat9 = dot(u_xlat4.xyz, u_xlat4.xyz);
        u_xlat9 = sqrt(u_xlat9);
        u_xlat17 = (-u_xlat9) + 1.0;
        u_xlat17 = u_xlat17 * 230.831207;
        u_xlat17 = exp2(u_xlat17);
        u_xlat24 = dot(Globals._WorldSpaceLightPos0.xyz, u_xlat4.xyz);
        u_xlat24 = u_xlat24 / u_xlat9;
        u_xlat25 = dot(u_xlat2.xzw, u_xlat4.xyz);
        u_xlat9 = u_xlat25 / u_xlat9;
        u_xlat24 = (-u_xlat24) + 1.0;
        u_xlat25 = u_xlat24 * 5.25 + -6.80000019;
        u_xlat25 = u_xlat24 * u_xlat25 + 3.82999992;
        u_xlat25 = u_xlat24 * u_xlat25 + 0.458999991;
        u_xlat24 = u_xlat24 * u_xlat25 + -0.00286999997;
        u_xlat24 = u_xlat24 * 1.44269502;
        u_xlat24 = exp2(u_xlat24);
        u_xlat9 = (-u_xlat9) + 1.0;
        u_xlat25 = u_xlat9 * 5.25 + -6.80000019;
        u_xlat25 = u_xlat9 * u_xlat25 + 3.82999992;
        u_xlat25 = u_xlat9 * u_xlat25 + 0.458999991;
        u_xlat9 = u_xlat9 * u_xlat25 + -0.00286999997;
        u_xlat9 = u_xlat9 * 1.44269502;
        u_xlat9 = exp2(u_xlat9);
        u_xlat9 = u_xlat9 * 0.25;
        u_xlat9 = u_xlat24 * 0.25 + (-u_xlat9);
        u_xlat9 = u_xlat17 * u_xlat9 + u_xlat21;
        u_xlat9 = max(u_xlat9, 0.0);
        u_xlat9 = min(u_xlat9, 50.0);
        u_xlat5.xyz = u_xlat0.xyz * float3(u_xlat16_1.yyy) + float3(0.0125663709, 0.0125663709, 0.0125663709);
        u_xlat6.xyz = (-float3(u_xlat9)) * u_xlat5.xyz;
        u_xlat6.xyz = u_xlat6.xyz * float3(1.44269502, 1.44269502, 1.44269502);
        u_xlat6.xyz = exp2(u_xlat6.xyz);
        u_xlat9 = u_xlat3.y * u_xlat17;
        u_xlat3.xzw = u_xlat2.xzw * u_xlat3.xxx + u_xlat4.xyz;
        u_xlat4.x = dot(u_xlat3.xzw, u_xlat3.xzw);
        u_xlat4.x = sqrt(u_xlat4.x);
        u_xlat11 = (-u_xlat4.x) + 1.0;
        u_xlat11 = u_xlat11 * 230.831207;
        u_xlat11 = exp2(u_xlat11);
        u_xlat18 = dot(Globals._WorldSpaceLightPos0.xyz, u_xlat3.xzw);
        u_xlat18 = u_xlat18 / u_xlat4.x;
        u_xlat3.x = dot(u_xlat2.xzw, u_xlat3.xzw);
        u_xlat3.x = u_xlat3.x / u_xlat4.x;
        u_xlat17 = (-u_xlat18) + 1.0;
        u_xlat24 = u_xlat17 * 5.25 + -6.80000019;
        u_xlat24 = u_xlat17 * u_xlat24 + 3.82999992;
        u_xlat24 = u_xlat17 * u_xlat24 + 0.458999991;
        u_xlat17 = u_xlat17 * u_xlat24 + -0.00286999997;
        u_xlat17 = u_xlat17 * 1.44269502;
        u_xlat17 = exp2(u_xlat17);
        u_xlat3.x = (-u_xlat3.x) + 1.0;
        u_xlat24 = u_xlat3.x * 5.25 + -6.80000019;
        u_xlat24 = u_xlat3.x * u_xlat24 + 3.82999992;
        u_xlat24 = u_xlat3.x * u_xlat24 + 0.458999991;
        u_xlat3.x = u_xlat3.x * u_xlat24 + -0.00286999997;
        u_xlat3.x = u_xlat3.x * 1.44269502;
        u_xlat3.x = exp2(u_xlat3.x);
        u_xlat3.x = u_xlat3.x * 0.25;
        u_xlat3.x = u_xlat17 * 0.25 + (-u_xlat3.x);
        u_xlat21 = u_xlat11 * u_xlat3.x + u_xlat21;
        u_xlat21 = max(u_xlat21, 0.0);
        u_xlat21 = min(u_xlat21, 50.0);
        u_xlat3.xzw = u_xlat5.xyz * (-float3(u_xlat21));
        u_xlat3.xzw = u_xlat3.xzw * float3(1.44269502, 1.44269502, 1.44269502);
        u_xlat3.xzw = exp2(u_xlat3.xzw);
        u_xlat21 = u_xlat3.y * u_xlat11;
        u_xlat3.xyz = float3(u_xlat21) * u_xlat3.xzw;
        u_xlat3.xyz = u_xlat6.xyz * float3(u_xlat9) + u_xlat3.xyz;
        u_xlat4.xyz = u_xlat0.xyz * float3(u_xlat16_1.xxx);
        u_xlat4.xyz = u_xlat3.xyz * u_xlat4.xyz;
        u_xlat3.xyz = u_xlat3.xyz * float3(0.0199999996, 0.0199999996, 0.0199999996);
        u_xlat16_4.xyz = half3(u_xlat4.xyz);
        u_xlat16_3.xyz = half3(u_xlat3.xyz);
    } else {
        u_xlat21 = min(u_xlat2.z, -0.00100000005);
        u_xlat21 = -9.99999975e-05 / u_xlat21;
        u_xlat5.xyz = float3(u_xlat21) * u_xlat2.xzw + float3(0.0, 1.00010002, 0.0);
        u_xlat5.w = dot((-u_xlat2.xzw), u_xlat5.xyz);
        u_xlat5.x = dot(Globals._WorldSpaceLightPos0.xyz, u_xlat5.xyz);
        u_xlat5.xy = (-u_xlat5.xw) + float2(1.0, 1.0);
        u_xlat19 = u_xlat5.y * 5.25 + -6.80000019;
        u_xlat19 = u_xlat5.y * u_xlat19 + 3.82999992;
        u_xlat19 = u_xlat5.y * u_xlat19 + 0.458999991;
        u_xlat12.x = u_xlat5.y * u_xlat19 + -0.00286999997;
        u_xlat12.x = u_xlat12.x * 1.44269502;
        u_xlat5.y = exp2(u_xlat12.x);
        u_xlat19 = u_xlat5.x * 5.25 + -6.80000019;
        u_xlat19 = u_xlat5.x * u_xlat19 + 3.82999992;
        u_xlat19 = u_xlat5.x * u_xlat19 + 0.458999991;
        u_xlat5.x = u_xlat5.x * u_xlat19 + -0.00286999997;
        u_xlat5.xyz = u_xlat5.xyy * float3(1.44269502, 0.25, 0.249900013);
        u_xlat5.x = exp2(u_xlat5.x);
        u_xlat5.x = u_xlat5.x * 0.25 + u_xlat5.y;
        u_xlat12.xz = float2(u_xlat21) * float2(0.5, 20.0);
        u_xlat6.xyz = u_xlat2.xzw * u_xlat12.xxx;
        u_xlat6.xyz = u_xlat6.xyz * float3(0.5, 0.5, 0.5) + float3(0.0, 1.00010002, 0.0);
        u_xlat21 = dot(u_xlat6.xyz, u_xlat6.xyz);
        u_xlat21 = sqrt(u_xlat21);
        u_xlat21 = (-u_xlat21) + 1.0;
        u_xlat21 = u_xlat21 * 230.831207;
        u_xlat21 = exp2(u_xlat21);
        u_xlat5.x = u_xlat21 * u_xlat5.x + (-u_xlat5.z);
        u_xlat5.x = max(u_xlat5.x, 0.0);
        u_xlat5.x = min(u_xlat5.x, 50.0);
        u_xlat6.xyz = u_xlat0.xyz * float3(u_xlat16_1.yyy) + float3(0.0125663709, 0.0125663709, 0.0125663709);
        u_xlat5.xyz = (-u_xlat5.xxx) * u_xlat6.xyz;
        u_xlat5.xyz = u_xlat5.xyz * float3(1.44269502, 1.44269502, 1.44269502);
        u_xlat3.xyz = exp2(u_xlat5.xyz);
        u_xlat21 = u_xlat12.z * u_xlat21;
        u_xlat5.xyz = float3(u_xlat21) * u_xlat3.xyz;
        u_xlat0.xyz = u_xlat0.xyz * float3(u_xlat16_1.xxx) + float3(0.0199999996, 0.0199999996, 0.0199999996);
        u_xlat4.xyz = u_xlat0.xyz * u_xlat5.xyz;
        u_xlat16_4.xyz = half3(u_xlat4.xyz);
        u_xlat16_3.xyz = half3(u_xlat3.xyz);
    }
    u_xlat16_1.xyz = half3(Globals._GroundColor.xxyz.yzw * Globals._GroundColor.xxyz.yzw);
    u_xlat16_1.xyz = half3(u_xlat16_1.xyz * u_xlat16_3.xyz + u_xlat16_4.xyz);
    output.TEXCOORD1.xyz = half3(u_xlat16_1.xyz * half3(Globals._Exposure));
    u_xlat16_1.x = dot(Globals._WorldSpaceLightPos0.xyz, (-u_xlat2.xzw));
    u_xlat16_1.x = half(u_xlat16_1.x * u_xlat16_1.x);
    u_xlat16_1.x = half(float(u_xlat16_1.x) * 0.75 + 0.75);
    u_xlat16_1.xyz = half3(u_xlat16_1.xxx * u_xlat16_4.xyz);
    output.TEXCOORD2.xyz = half3(u_xlat16_1.xyz * half3(Globals._Exposure));
    u_xlat16_1.xyz = half3(u_xlat16_3.xyz * Globals._LightColor0.xyz);
    output.TEXCOORD3.xyz = half3(u_xlat16_1.xyz * half3(Globals._Exposure));
    output.TEXCOORD0.xyz = half3((-u_xlat2.xzw));
    return output;
}


-- Fragment shader for "metal":
Constant Buffer "Globals" (18 bytes) on slot 0 {
  Vector4 _WorldSpaceLightPos0 at 0
  ScalarHalf _SunSize at 16
}

Shader Disassembly:
#include <metal_stdlib>
#include <metal_texture>
using namespace metal;
struct Globals_Type
{
    float4 _WorldSpaceLightPos0;
    half _SunSize;
};

struct Mtl_FragmentIn
{
    half3 TEXCOORD0 [[ user(TEXCOORD0) ]] ;
    half3 TEXCOORD1 [[ user(TEXCOORD1) ]] ;
    half3 TEXCOORD2 [[ user(TEXCOORD2) ]] ;
    half3 TEXCOORD3 [[ user(TEXCOORD3) ]] ;
};

struct Mtl_FragmentOut
{
    half4 SV_Target0 [[ color(0) ]];
};

fragment Mtl_FragmentOut xlatMtlMain(
    constant Globals_Type& Globals [[ buffer(0) ]],
    Mtl_FragmentIn input [[ stage_in ]])
{
    Mtl_FragmentOut output;
    half3 u_xlat16_0;
    half3 u_xlat16_1;
    bool u_xlatb2;
    half3 u_xlat16_3;
    u_xlat16_0.xyz = half3(float3(input.TEXCOORD0.xyz) + Globals._WorldSpaceLightPos0.xyz);
    u_xlat16_0.x = dot(u_xlat16_0.xyz, u_xlat16_0.xyz);
    u_xlat16_0.x = sqrt(u_xlat16_0.x);
    u_xlat16_3.x = half(float(1.0) / float(Globals._SunSize));
    u_xlat16_0.x = half(u_xlat16_3.x * u_xlat16_0.x);
    u_xlat16_0.x = clamp(u_xlat16_0.x, 0.0h, 1.0h);
    u_xlat16_3.x = half(float(u_xlat16_0.x) * -2.0 + 3.0);
    u_xlat16_0.x = half(u_xlat16_0.x * u_xlat16_0.x);
    u_xlat16_0.x = half((-float(u_xlat16_3.x)) * float(u_xlat16_0.x) + 1.0);
    u_xlat16_0.x = half(u_xlat16_0.x * u_xlat16_0.x);
    u_xlat16_0.x = half(float(u_xlat16_0.x) * 8000.0);
    u_xlat16_3.x = half(float(input.TEXCOORD0.y) * 50.0);
    u_xlat16_3.x = clamp(u_xlat16_3.x, 0.0h, 1.0h);
    u_xlat16_1.xyz = half3(input.TEXCOORD1.xyz + (-input.TEXCOORD2.xyz));
    u_xlat16_3.xyz = half3(u_xlat16_3.xxx * u_xlat16_1.xyz + input.TEXCOORD2.xyz);
    u_xlat16_1.xyz = half3(u_xlat16_0.xxx * input.TEXCOORD3.xyz + u_xlat16_3.xyz);
    u_xlatb2 = input.TEXCOORD0.y<0.0;
    u_xlat16_0.xyz = (bool(u_xlatb2)) ? u_xlat16_1.xyz : u_xlat16_3.xyz;
    output.SV_Target0.xyz = sqrt(u_xlat16_0.xyz);
    output.SV_Target0.w = 1.0;
    return output;
}


-- Vertex shader for "glcore":
Shader Disassembly:
#ifdef VERTEX
#version 150
#extension GL_ARB_explicit_attrib_location : require
#extension GL_ARB_shader_bit_encoding : enable

uniform 	vec4 _WorldSpaceLightPos0;
uniform 	vec4 hlslcc_mtx4x4glstate_matrix_mvp[4];
uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
uniform 	vec4 _LightColor0;
uniform 	float _Exposure;
uniform 	vec3 _GroundColor;
uniform 	vec3 _SkyTint;
uniform 	float _AtmosphereThickness;
in  vec4 in_POSITION0;
out vec3 vs_TEXCOORD0;
out vec3 vs_TEXCOORD1;
out vec3 vs_TEXCOORD2;
out vec3 vs_TEXCOORD3;
vec4 u_xlat0;
vec2 u_xlat1;
vec4 u_xlat2;
vec4 u_xlat3;
vec3 u_xlat4;
vec4 u_xlat5;
vec3 u_xlat6;
vec3 u_xlat8;
float u_xlat9;
vec2 u_xlat15;
bool u_xlatb15;
float u_xlat17;
float u_xlat21;
float u_xlat22;
void main()
{
    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4glstate_matrix_mvp[1];
    u_xlat0 = hlslcc_mtx4x4glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
    u_xlat0 = hlslcc_mtx4x4glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
    gl_Position = u_xlat0 + hlslcc_mtx4x4glstate_matrix_mvp[3];
    u_xlat0.xyz = (-vec3(_SkyTint.x, _SkyTint.y, _SkyTint.z)) + vec3(1.0, 1.0, 1.0);
    u_xlat0.xyz = u_xlat0.xyz * vec3(0.300000012, 0.300000042, 0.300000012) + vec3(0.5, 0.419999987, 0.324999988);
    u_xlat0.xyz = u_xlat0.xyz * u_xlat0.xyz;
    u_xlat0.xyz = u_xlat0.xyz * u_xlat0.xyz;
    u_xlat0.xyz = vec3(1.0, 1.0, 1.0) / u_xlat0.xyz;
    u_xlat21 = log2(_AtmosphereThickness);
    u_xlat21 = u_xlat21 * 2.5;
    u_xlat21 = exp2(u_xlat21);
    u_xlat1.xy = vec2(u_xlat21) * vec2(0.049999997, 0.0314159282);
    u_xlat2.xyz = in_POSITION0.yyy * hlslcc_mtx4x4unity_ObjectToWorld[1].xyz;
    u_xlat2.xyz = hlslcc_mtx4x4unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
    u_xlat2.xyz = hlslcc_mtx4x4unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
    u_xlat21 = dot(u_xlat2.xyz, u_xlat2.xyz);
    u_xlat21 = inversesqrt(u_xlat21);
    u_xlat2.xzw = vec3(u_xlat21) * u_xlat2.xyz;
    u_xlatb15 = u_xlat2.z>=0.0;
    if(u_xlatb15){
        u_xlat15.x = u_xlat2.z * u_xlat2.z + 0.0506249666;
        u_xlat15.x = sqrt(u_xlat15.x);
        u_xlat21 = (-u_xlat2.y) * u_xlat21 + u_xlat15.x;
        u_xlat15.x = (-u_xlat2.z) * 1.0 + 1.0;
        u_xlat22 = u_xlat15.x * 5.25 + -6.80000019;
        u_xlat22 = u_xlat15.x * u_xlat22 + 3.82999992;
        u_xlat22 = u_xlat15.x * u_xlat22 + 0.458999991;
        u_xlat15.x = u_xlat15.x * u_xlat22 + -0.00286999997;
        u_xlat15.x = u_xlat15.x * 1.44269502;
        u_xlat15.x = exp2(u_xlat15.x);
        u_xlat3.xy = vec2(u_xlat21) * vec2(0.5, 20.0);
        u_xlat4.xyz = u_xlat2.xzw * u_xlat3.xxx;
        u_xlat4.xyz = u_xlat4.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.0, 1.00010002, 0.0);
        u_xlat21 = dot(u_xlat4.xyz, u_xlat4.xyz);
        u_xlat21 = sqrt(u_xlat21);
        u_xlat15.y = (-u_xlat21) + 1.0;
        u_xlat15.xy = u_xlat15.xy * vec2(0.246031836, 230.831207);
        u_xlat22 = exp2(u_xlat15.y);
        u_xlat9 = dot(_WorldSpaceLightPos0.xyz, u_xlat4.xyz);
        u_xlat9 = u_xlat9 / u_xlat21;
        u_xlat17 = dot(u_xlat2.xzw, u_xlat4.xyz);
        u_xlat21 = u_xlat17 / u_xlat21;
        u_xlat9 = (-u_xlat9) + 1.0;
        u_xlat17 = u_xlat9 * 5.25 + -6.80000019;
        u_xlat17 = u_xlat9 * u_xlat17 + 3.82999992;
        u_xlat17 = u_xlat9 * u_xlat17 + 0.458999991;
        u_xlat9 = u_xlat9 * u_xlat17 + -0.00286999997;
        u_xlat9 = u_xlat9 * 1.44269502;
        u_xlat9 = exp2(u_xlat9);
        u_xlat21 = (-u_xlat21) + 1.0;
        u_xlat17 = u_xlat21 * 5.25 + -6.80000019;
        u_xlat17 = u_xlat21 * u_xlat17 + 3.82999992;
        u_xlat17 = u_xlat21 * u_xlat17 + 0.458999991;
        u_xlat21 = u_xlat21 * u_xlat17 + -0.00286999997;
        u_xlat21 = u_xlat21 * 1.44269502;
        u_xlat21 = exp2(u_xlat21);
        u_xlat21 = u_xlat21 * 0.25;
        u_xlat21 = u_xlat9 * 0.25 + (-u_xlat21);
        u_xlat21 = u_xlat22 * u_xlat21 + u_xlat15.x;
        u_xlat21 = max(u_xlat21, 0.0);
        u_xlat21 = min(u_xlat21, 50.0);
        u_xlat5.xyz = u_xlat0.xyz * u_xlat1.yyy + vec3(0.0125663709, 0.0125663709, 0.0125663709);
        u_xlat6.xyz = (-vec3(u_xlat21)) * u_xlat5.xyz;
        u_xlat6.xyz = u_xlat6.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat6.xyz = exp2(u_xlat6.xyz);
        u_xlat21 = u_xlat3.y * u_xlat22;
        u_xlat3.xzw = u_xlat2.xzw * u_xlat3.xxx + u_xlat4.xyz;
        u_xlat22 = dot(u_xlat3.xzw, u_xlat3.xzw);
        u_xlat22 = sqrt(u_xlat22);
        u_xlat9 = (-u_xlat22) + 1.0;
        u_xlat9 = u_xlat9 * 230.831207;
        u_xlat9 = exp2(u_xlat9);
        u_xlat4.x = dot(_WorldSpaceLightPos0.xyz, u_xlat3.xzw);
        u_xlat4.x = u_xlat4.x / u_xlat22;
        u_xlat3.x = dot(u_xlat2.xzw, u_xlat3.xzw);
        u_xlat22 = u_xlat3.x / u_xlat22;
        u_xlat3.x = (-u_xlat4.x) + 1.0;
        u_xlat17 = u_xlat3.x * 5.25 + -6.80000019;
        u_xlat17 = u_xlat3.x * u_xlat17 + 3.82999992;
        u_xlat17 = u_xlat3.x * u_xlat17 + 0.458999991;
        u_xlat3.x = u_xlat3.x * u_xlat17 + -0.00286999997;
        u_xlat3.x = u_xlat3.x * 1.44269502;
        u_xlat3.x = exp2(u_xlat3.x);
        u_xlat22 = (-u_xlat22) + 1.0;
        u_xlat17 = u_xlat22 * 5.25 + -6.80000019;
        u_xlat17 = u_xlat22 * u_xlat17 + 3.82999992;
        u_xlat17 = u_xlat22 * u_xlat17 + 0.458999991;
        u_xlat22 = u_xlat22 * u_xlat17 + -0.00286999997;
        u_xlat22 = u_xlat22 * 1.44269502;
        u_xlat22 = exp2(u_xlat22);
        u_xlat22 = u_xlat22 * 0.25;
        u_xlat22 = u_xlat3.x * 0.25 + (-u_xlat22);
        u_xlat15.x = u_xlat9 * u_xlat22 + u_xlat15.x;
        u_xlat15.x = max(u_xlat15.x, 0.0);
        u_xlat15.x = min(u_xlat15.x, 50.0);
        u_xlat3.xzw = u_xlat5.xyz * (-u_xlat15.xxx);
        u_xlat3.xzw = u_xlat3.xzw * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat3.xzw = exp2(u_xlat3.xzw);
        u_xlat15.x = u_xlat3.y * u_xlat9;
        u_xlat3.xyz = u_xlat15.xxx * u_xlat3.xzw;
        u_xlat3.xyz = u_xlat6.xyz * vec3(u_xlat21) + u_xlat3.xyz;
        u_xlat4.xyz = u_xlat0.xyz * u_xlat1.xxx;
        u_xlat4.xyz = u_xlat3.xyz * u_xlat4.xyz;
        u_xlat3.xyz = u_xlat3.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996);
    } else {
        u_xlat21 = min(u_xlat2.z, -0.00100000005);
        u_xlat21 = -9.99999975e-05 / u_xlat21;
        u_xlat5.xyz = vec3(u_xlat21) * u_xlat2.xzw + vec3(0.0, 1.00010002, 0.0);
        u_xlat15.x = dot((-u_xlat2.xzw), u_xlat5.xyz);
        u_xlat15.y = dot(_WorldSpaceLightPos0.xyz, u_xlat5.xyz);
        u_xlat15.xy = (-u_xlat15.xy) + vec2(1.0, 1.0);
        u_xlat9 = u_xlat15.x * 5.25 + -6.80000019;
        u_xlat9 = u_xlat15.x * u_xlat9 + 3.82999992;
        u_xlat9 = u_xlat15.x * u_xlat9 + 0.458999991;
        u_xlat15.x = u_xlat15.x * u_xlat9 + -0.00286999997;
        u_xlat15.x = u_xlat15.x * 1.44269502;
        u_xlat15.x = exp2(u_xlat15.x);
        u_xlat9 = u_xlat15.y * 5.25 + -6.80000019;
        u_xlat9 = u_xlat15.y * u_xlat9 + 3.82999992;
        u_xlat9 = u_xlat15.y * u_xlat9 + 0.458999991;
        u_xlat22 = u_xlat15.y * u_xlat9 + -0.00286999997;
        u_xlat22 = u_xlat22 * 1.44269502;
        u_xlat22 = exp2(u_xlat22);
        u_xlat5.xy = u_xlat15.xx * vec2(0.25, 0.249900013);
        u_xlat15.x = u_xlat22 * 0.25 + u_xlat5.x;
        u_xlat5.xz = vec2(u_xlat21) * vec2(0.5, 20.0);
        u_xlat6.xyz = u_xlat2.xzw * u_xlat5.xxx;
        u_xlat6.xyz = u_xlat6.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.0, 1.00010002, 0.0);
        u_xlat21 = dot(u_xlat6.xyz, u_xlat6.xyz);
        u_xlat21 = sqrt(u_xlat21);
        u_xlat21 = (-u_xlat21) + 1.0;
        u_xlat21 = u_xlat21 * 230.831207;
        u_xlat21 = exp2(u_xlat21);
        u_xlat15.x = u_xlat21 * u_xlat15.x + (-u_xlat5.y);
        u_xlat15.x = max(u_xlat15.x, 0.0);
        u_xlat15.x = min(u_xlat15.x, 50.0);
        u_xlat5.xyw = u_xlat0.xyz * u_xlat1.yyy + vec3(0.0125663709, 0.0125663709, 0.0125663709);
        u_xlat8.xyz = (-u_xlat15.xxx) * u_xlat5.xyw;
        u_xlat8.xyz = u_xlat8.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat3.xyz = exp2(u_xlat8.xyz);
        u_xlat21 = u_xlat5.z * u_xlat21;
        u_xlat8.xyz = vec3(u_xlat21) * u_xlat3.xyz;
        u_xlat0.xyz = u_xlat0.xyz * u_xlat1.xxx + vec3(0.0199999996, 0.0199999996, 0.0199999996);
        u_xlat4.xyz = u_xlat0.xyz * u_xlat8.xyz;
    //ENDIF
    }
    u_xlat0.xyz = vec3(_GroundColor.x, _GroundColor.y, _GroundColor.z) * vec3(_GroundColor.x, _GroundColor.y, _GroundColor.z);
    u_xlat0.xyz = u_xlat0.xyz * u_xlat3.xyz + u_xlat4.xyz;
    vs_TEXCOORD1.xyz = u_xlat0.xyz * vec3(_Exposure);
    u_xlat0.x = dot(_WorldSpaceLightPos0.xyz, (-u_xlat2.xzw));
    u_xlat0.x = u_xlat0.x * u_xlat0.x;
    u_xlat0.x = u_xlat0.x * 0.75 + 0.75;
    u_xlat0.xyz = u_xlat0.xxx * u_xlat4.xyz;
    vs_TEXCOORD2.xyz = u_xlat0.xyz * vec3(_Exposure);
    u_xlat0.xyz = u_xlat3.xyz * _LightColor0.xyz;
    vs_TEXCOORD3.xyz = u_xlat0.xyz * vec3(_Exposure);
    vs_TEXCOORD0.xyz = (-u_xlat2.xzw);
    return;
}

#endif
#ifdef FRAGMENT
#version 150
#extension GL_ARB_explicit_attrib_location : require
#extension GL_ARB_shader_bit_encoding : enable

uniform 	vec4 _WorldSpaceLightPos0;
uniform 	float _SunSize;
in  vec3 vs_TEXCOORD0;
in  vec3 vs_TEXCOORD1;
in  vec3 vs_TEXCOORD2;
in  vec3 vs_TEXCOORD3;
layout(location = 0) out vec4 SV_Target0;
vec3 u_xlat0;
bool u_xlatb0;
vec3 u_xlat1;
vec3 u_xlat2;
void main()
{
    u_xlat0.xyz = vs_TEXCOORD0.xyz + _WorldSpaceLightPos0.xyz;
    u_xlat0.x = dot(u_xlat0.xyz, u_xlat0.xyz);
    u_xlat0.x = sqrt(u_xlat0.x);
    u_xlat2.x = float(1.0) / _SunSize;
    u_xlat0.x = u_xlat2.x * u_xlat0.x;
    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
    u_xlat2.x = u_xlat0.x * -2.0 + 3.0;
    u_xlat0.x = u_xlat0.x * u_xlat0.x;
    u_xlat0.x = (-u_xlat2.x) * u_xlat0.x + 1.0;
    u_xlat0.x = u_xlat0.x * u_xlat0.x;
    u_xlat0.x = u_xlat0.x * 8000.0;
    u_xlat2.x = vs_TEXCOORD0.y * 50.0;
    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
    u_xlat1.xyz = vs_TEXCOORD1.xyz + (-vs_TEXCOORD2.xyz);
    u_xlat2.xyz = u_xlat2.xxx * u_xlat1.xyz + vs_TEXCOORD2.xyz;
    u_xlat1.xyz = u_xlat0.xxx * vs_TEXCOORD3.xyz + u_xlat2.xyz;
    u_xlatb0 = vs_TEXCOORD0.y<0.0;
    u_xlat0.xyz = (bool(u_xlatb0)) ? u_xlat1.xyz : u_xlat2.xyz;
    SV_Target0.xyz = sqrt(u_xlat0.xyz);
    SV_Target0.w = 1.0;
    return;
}

#endif


-- Fragment shader for "glcore":
Shader Disassembly:
// All GLSL source is contained within the vertex program

//////////////////////////////////////////////////////
Keywords set in this variant: _SUNDISK_HIGH_QUALITY 
-- Vertex shader for "metal":
Uses vertex data channel "Vertex"

Constant Buffer "Globals" (178 bytes) on slot 0 {
  Matrix4x4 glstate_matrix_mvp at 16
  Matrix4x4 unity_ObjectToWorld at 80
  Vector4 _WorldSpaceLightPos0 at 0
  VectorHalf4 _LightColor0 at 144
  ScalarHalf _Exposure at 152
  VectorHalf3 _GroundColor at 160
  VectorHalf3 _SkyTint at 168
  ScalarHalf _AtmosphereThickness at 176
}

Shader Disassembly:
#include <metal_stdlib>
#include <metal_texture>
using namespace metal;
struct Globals_Type
{
    float4 _WorldSpaceLightPos0;
    float4 hlslcc_mtx4x4glstate_matrix_mvp[4];
    float4 hlslcc_mtx4x4unity_ObjectToWorld[4];
    half4 _LightColor0;
    half _Exposure;
    half3 _GroundColor;
    half3 _SkyTint;
    half _AtmosphereThickness;
};

struct Mtl_VertexIn
{
    float4 POSITION0 [[ attribute(0) ]] ;
};

struct Mtl_VertexOut
{
    float4 mtl_Position [[ position ]];
    half3 TEXCOORD0 [[ user(TEXCOORD0) ]];
    half3 TEXCOORD1 [[ user(TEXCOORD1) ]];
    half3 TEXCOORD2 [[ user(TEXCOORD2) ]];
    half3 TEXCOORD3 [[ user(TEXCOORD3) ]];
};

vertex Mtl_VertexOut xlatMtlMain(
    constant Globals_Type& Globals [[ buffer(0) ]],
    Mtl_VertexIn input [[ stage_in ]])
{
    Mtl_VertexOut output;
    float4 u_xlat0;
    half3 u_xlat16_1;
    float4 u_xlat2;
    float4 u_xlat3;
    half3 u_xlat16_3;
    bool u_xlatb3;
    float3 u_xlat4;
    half3 u_xlat16_4;
    float4 u_xlat5;
    float3 u_xlat6;
    float u_xlat9;
    float u_xlat11;
    float3 u_xlat12;
    float u_xlat17;
    float u_xlat18;
    float u_xlat19;
    float u_xlat21;
    float u_xlat24;
    float u_xlat25;
    u_xlat0 = input.POSITION0.yyyy * Globals.hlslcc_mtx4x4glstate_matrix_mvp[1];
    u_xlat0 = Globals.hlslcc_mtx4x4glstate_matrix_mvp[0] * input.POSITION0.xxxx + u_xlat0;
    u_xlat0 = Globals.hlslcc_mtx4x4glstate_matrix_mvp[2] * input.POSITION0.zzzz + u_xlat0;
    output.mtl_Position = u_xlat0 + Globals.hlslcc_mtx4x4glstate_matrix_mvp[3];
    u_xlat0.xyz = (-float3(Globals._SkyTint.xxyz.yzw)) + float3(1.0, 1.0, 1.0);
    u_xlat0.xyz = u_xlat0.xyz * float3(0.300000012, 0.300000042, 0.300000012) + float3(0.5, 0.419999987, 0.324999988);
    u_xlat0.xyz = u_xlat0.xyz * u_xlat0.xyz;
    u_xlat0.xyz = u_xlat0.xyz * u_xlat0.xyz;
    u_xlat0.xyz = float3(1.0, 1.0, 1.0) / u_xlat0.xyz;
    u_xlat16_1.x = log2(Globals._AtmosphereThickness);
    u_xlat16_1.x = half(float(u_xlat16_1.x) * 2.5);
    u_xlat16_1.x = exp2(u_xlat16_1.x);
    u_xlat16_1.xy = half2(float2(u_xlat16_1.xx) * float2(0.049999997, 0.0314159282));
    u_xlat2.xyz = input.POSITION0.yyy * Globals.hlslcc_mtx4x4unity_ObjectToWorld[1].xyz;
    u_xlat2.xyz = Globals.hlslcc_mtx4x4unity_ObjectToWorld[0].xyz * input.POSITION0.xxx + u_xlat2.xyz;
    u_xlat2.xyz = Globals.hlslcc_mtx4x4unity_ObjectToWorld[2].xyz * input.POSITION0.zzz + u_xlat2.xyz;
    u_xlat21 = dot(u_xlat2.xyz, u_xlat2.xyz);
    u_xlat21 = rsqrt(u_xlat21);
    u_xlat2.xzw = float3(u_xlat21) * u_xlat2.xyz;
    u_xlatb3 = u_xlat2.z>=0.0;
    if(u_xlatb3){
        u_xlat3.x = u_xlat2.z * u_xlat2.z + 0.0506249666;
        u_xlat3.x = sqrt(u_xlat3.x);
        u_xlat3.x = (-u_xlat2.y) * u_xlat21 + u_xlat3.x;
        u_xlat21 = (-u_xlat2.y) * u_xlat21 + 1.0;
        u_xlat9 = u_xlat21 * 5.25 + -6.80000019;
        u_xlat9 = u_xlat21 * u_xlat9 + 3.82999992;
        u_xlat9 = u_xlat21 * u_xlat9 + 0.458999991;
        u_xlat21 = u_xlat21 * u_xlat9 + -0.00286999997;
        u_xlat21 = u_xlat21 * 1.44269502;
        u_xlat21 = exp2(u_xlat21);
        u_xlat21 = u_xlat21 * 0.246031836;
        u_xlat3.xy = u_xlat3.xx * float2(0.5, 20.0);
        u_xlat4.xyz = u_xlat2.xzw * u_xlat3.xxx;
        u_xlat4.xyz = u_xlat4.xyz * float3(0.5, 0.5, 0.5) + float3(0.0, 1.00010002, 0.0);
        u_xlat9 = dot(u_xlat4.xyz, u_xlat4.xyz);
        u_xlat9 = sqrt(u_xlat9);
        u_xlat17 = (-u_xlat9) + 1.0;
        u_xlat17 = u_xlat17 * 230.831207;
        u_xlat17 = exp2(u_xlat17);
        u_xlat24 = dot(Globals._WorldSpaceLightPos0.xyz, u_xlat4.xyz);
        u_xlat24 = u_xlat24 / u_xlat9;
        u_xlat25 = dot(u_xlat2.xzw, u_xlat4.xyz);
        u_xlat9 = u_xlat25 / u_xlat9;
        u_xlat24 = (-u_xlat24) + 1.0;
        u_xlat25 = u_xlat24 * 5.25 + -6.80000019;
        u_xlat25 = u_xlat24 * u_xlat25 + 3.82999992;
        u_xlat25 = u_xlat24 * u_xlat25 + 0.458999991;
        u_xlat24 = u_xlat24 * u_xlat25 + -0.00286999997;
        u_xlat24 = u_xlat24 * 1.44269502;
        u_xlat24 = exp2(u_xlat24);
        u_xlat9 = (-u_xlat9) + 1.0;
        u_xlat25 = u_xlat9 * 5.25 + -6.80000019;
        u_xlat25 = u_xlat9 * u_xlat25 + 3.82999992;
        u_xlat25 = u_xlat9 * u_xlat25 + 0.458999991;
        u_xlat9 = u_xlat9 * u_xlat25 + -0.00286999997;
        u_xlat9 = u_xlat9 * 1.44269502;
        u_xlat9 = exp2(u_xlat9);
        u_xlat9 = u_xlat9 * 0.25;
        u_xlat9 = u_xlat24 * 0.25 + (-u_xlat9);
        u_xlat9 = u_xlat17 * u_xlat9 + u_xlat21;
        u_xlat9 = max(u_xlat9, 0.0);
        u_xlat9 = min(u_xlat9, 50.0);
        u_xlat5.xyz = u_xlat0.xyz * float3(u_xlat16_1.yyy) + float3(0.0125663709, 0.0125663709, 0.0125663709);
        u_xlat6.xyz = (-float3(u_xlat9)) * u_xlat5.xyz;
        u_xlat6.xyz = u_xlat6.xyz * float3(1.44269502, 1.44269502, 1.44269502);
        u_xlat6.xyz = exp2(u_xlat6.xyz);
        u_xlat9 = u_xlat3.y * u_xlat17;
        u_xlat3.xzw = u_xlat2.xzw * u_xlat3.xxx + u_xlat4.xyz;
        u_xlat4.x = dot(u_xlat3.xzw, u_xlat3.xzw);
        u_xlat4.x = sqrt(u_xlat4.x);
        u_xlat11 = (-u_xlat4.x) + 1.0;
        u_xlat11 = u_xlat11 * 230.831207;
        u_xlat11 = exp2(u_xlat11);
        u_xlat18 = dot(Globals._WorldSpaceLightPos0.xyz, u_xlat3.xzw);
        u_xlat18 = u_xlat18 / u_xlat4.x;
        u_xlat3.x = dot(u_xlat2.xzw, u_xlat3.xzw);
        u_xlat3.x = u_xlat3.x / u_xlat4.x;
        u_xlat17 = (-u_xlat18) + 1.0;
        u_xlat24 = u_xlat17 * 5.25 + -6.80000019;
        u_xlat24 = u_xlat17 * u_xlat24 + 3.82999992;
        u_xlat24 = u_xlat17 * u_xlat24 + 0.458999991;
        u_xlat17 = u_xlat17 * u_xlat24 + -0.00286999997;
        u_xlat17 = u_xlat17 * 1.44269502;
        u_xlat17 = exp2(u_xlat17);
        u_xlat3.x = (-u_xlat3.x) + 1.0;
        u_xlat24 = u_xlat3.x * 5.25 + -6.80000019;
        u_xlat24 = u_xlat3.x * u_xlat24 + 3.82999992;
        u_xlat24 = u_xlat3.x * u_xlat24 + 0.458999991;
        u_xlat3.x = u_xlat3.x * u_xlat24 + -0.00286999997;
        u_xlat3.x = u_xlat3.x * 1.44269502;
        u_xlat3.x = exp2(u_xlat3.x);
        u_xlat3.x = u_xlat3.x * 0.25;
        u_xlat3.x = u_xlat17 * 0.25 + (-u_xlat3.x);
        u_xlat21 = u_xlat11 * u_xlat3.x + u_xlat21;
        u_xlat21 = max(u_xlat21, 0.0);
        u_xlat21 = min(u_xlat21, 50.0);
        u_xlat3.xzw = u_xlat5.xyz * (-float3(u_xlat21));
        u_xlat3.xzw = u_xlat3.xzw * float3(1.44269502, 1.44269502, 1.44269502);
        u_xlat3.xzw = exp2(u_xlat3.xzw);
        u_xlat21 = u_xlat3.y * u_xlat11;
        u_xlat3.xyz = float3(u_xlat21) * u_xlat3.xzw;
        u_xlat3.xyz = u_xlat6.xyz * float3(u_xlat9) + u_xlat3.xyz;
        u_xlat4.xyz = u_xlat0.xyz * float3(u_xlat16_1.xxx);
        u_xlat4.xyz = u_xlat3.xyz * u_xlat4.xyz;
        u_xlat3.xyz = u_xlat3.xyz * float3(0.0199999996, 0.0199999996, 0.0199999996);
        u_xlat16_4.xyz = half3(u_xlat4.xyz);
        u_xlat16_3.xyz = half3(u_xlat3.xyz);
    } else {
        u_xlat21 = min(u_xlat2.z, -0.00100000005);
        u_xlat21 = -9.99999975e-05 / u_xlat21;
        u_xlat5.xyz = float3(u_xlat21) * u_xlat2.xzw + float3(0.0, 1.00010002, 0.0);
        u_xlat5.w = dot((-u_xlat2.xzw), u_xlat5.xyz);
        u_xlat5.x = dot(Globals._WorldSpaceLightPos0.xyz, u_xlat5.xyz);
        u_xlat5.xy = (-u_xlat5.xw) + float2(1.0, 1.0);
        u_xlat19 = u_xlat5.y * 5.25 + -6.80000019;
        u_xlat19 = u_xlat5.y * u_xlat19 + 3.82999992;
        u_xlat19 = u_xlat5.y * u_xlat19 + 0.458999991;
        u_xlat12.x = u_xlat5.y * u_xlat19 + -0.00286999997;
        u_xlat12.x = u_xlat12.x * 1.44269502;
        u_xlat5.y = exp2(u_xlat12.x);
        u_xlat19 = u_xlat5.x * 5.25 + -6.80000019;
        u_xlat19 = u_xlat5.x * u_xlat19 + 3.82999992;
        u_xlat19 = u_xlat5.x * u_xlat19 + 0.458999991;
        u_xlat5.x = u_xlat5.x * u_xlat19 + -0.00286999997;
        u_xlat5.xyz = u_xlat5.xyy * float3(1.44269502, 0.25, 0.249900013);
        u_xlat5.x = exp2(u_xlat5.x);
        u_xlat5.x = u_xlat5.x * 0.25 + u_xlat5.y;
        u_xlat12.xz = float2(u_xlat21) * float2(0.5, 20.0);
        u_xlat6.xyz = u_xlat2.xzw * u_xlat12.xxx;
        u_xlat6.xyz = u_xlat6.xyz * float3(0.5, 0.5, 0.5) + float3(0.0, 1.00010002, 0.0);
        u_xlat21 = dot(u_xlat6.xyz, u_xlat6.xyz);
        u_xlat21 = sqrt(u_xlat21);
        u_xlat21 = (-u_xlat21) + 1.0;
        u_xlat21 = u_xlat21 * 230.831207;
        u_xlat21 = exp2(u_xlat21);
        u_xlat5.x = u_xlat21 * u_xlat5.x + (-u_xlat5.z);
        u_xlat5.x = max(u_xlat5.x, 0.0);
        u_xlat5.x = min(u_xlat5.x, 50.0);
        u_xlat6.xyz = u_xlat0.xyz * float3(u_xlat16_1.yyy) + float3(0.0125663709, 0.0125663709, 0.0125663709);
        u_xlat5.xyz = (-u_xlat5.xxx) * u_xlat6.xyz;
        u_xlat5.xyz = u_xlat5.xyz * float3(1.44269502, 1.44269502, 1.44269502);
        u_xlat3.xyz = exp2(u_xlat5.xyz);
        u_xlat21 = u_xlat12.z * u_xlat21;
        u_xlat5.xyz = float3(u_xlat21) * u_xlat3.xyz;
        u_xlat0.xyz = u_xlat0.xyz * float3(u_xlat16_1.xxx) + float3(0.0199999996, 0.0199999996, 0.0199999996);
        u_xlat4.xyz = u_xlat0.xyz * u_xlat5.xyz;
        u_xlat16_4.xyz = half3(u_xlat4.xyz);
        u_xlat16_3.xyz = half3(u_xlat3.xyz);
    }
    u_xlat16_1.xyz = half3(Globals._GroundColor.xxyz.yzw * Globals._GroundColor.xxyz.yzw);
    u_xlat16_1.xyz = half3(u_xlat16_1.xyz * u_xlat16_3.xyz + u_xlat16_4.xyz);
    output.TEXCOORD1.xyz = half3(u_xlat16_1.xyz * half3(Globals._Exposure));
    u_xlat16_1.x = dot(Globals._WorldSpaceLightPos0.xyz, (-u_xlat2.xzw));
    u_xlat16_1.x = half(u_xlat16_1.x * u_xlat16_1.x);
    u_xlat16_1.x = half(float(u_xlat16_1.x) * 0.75 + 0.75);
    u_xlat16_1.xyz = half3(u_xlat16_1.xxx * u_xlat16_4.xyz);
    output.TEXCOORD2.xyz = half3(u_xlat16_1.xyz * half3(Globals._Exposure));
    u_xlat16_1.xyz = half3(u_xlat16_3.xyz * Globals._LightColor0.xyz);
    output.TEXCOORD3.xyz = half3(u_xlat16_1.xyz * half3(Globals._Exposure));
    output.TEXCOORD0.xyz = half3((-input.POSITION0.xyz));
    return output;
}


-- Fragment shader for "metal":
Constant Buffer "Globals" (82 bytes) on slot 0 {
  Matrix4x4 unity_ObjectToWorld at 16
  Vector4 _WorldSpaceLightPos0 at 0
  ScalarHalf _SunSize at 80
}

Shader Disassembly:
#include <metal_stdlib>
#include <metal_texture>
using namespace metal;
struct Globals_Type
{
    float4 _WorldSpaceLightPos0;
    float4 hlslcc_mtx4x4unity_ObjectToWorld[4];
    half _SunSize;
};

struct Mtl_FragmentIn
{
    half3 TEXCOORD0 [[ user(TEXCOORD0) ]] ;
    half3 TEXCOORD1 [[ user(TEXCOORD1) ]] ;
    half3 TEXCOORD2 [[ user(TEXCOORD2) ]] ;
    half3 TEXCOORD3 [[ user(TEXCOORD3) ]] ;
};

struct Mtl_FragmentOut
{
    half4 SV_Target0 [[ color(0) ]];
};

fragment Mtl_FragmentOut xlatMtlMain(
    constant Globals_Type& Globals [[ buffer(0) ]],
    Mtl_FragmentIn input [[ stage_in ]])
{
    Mtl_FragmentOut output;
    half3 u_xlat16_0;
    float3 u_xlat1;
    bool u_xlatb1;
    half3 u_xlat16_2;
    half3 u_xlat16_3;
    float u_xlat10;
    u_xlat16_0.x = log2(Globals._SunSize);
    u_xlat16_0.x = half(float(u_xlat16_0.x) * 0.649999976);
    u_xlat16_0.x = exp2(u_xlat16_0.x);
    u_xlat1.xyz = float3(input.TEXCOORD0.yyy) * Globals.hlslcc_mtx4x4unity_ObjectToWorld[1].xyz;
    u_xlat1.xyz = Globals.hlslcc_mtx4x4unity_ObjectToWorld[0].xyz * float3(input.TEXCOORD0.xxx) + u_xlat1.xyz;
    u_xlat1.xyz = Globals.hlslcc_mtx4x4unity_ObjectToWorld[2].xyz * float3(input.TEXCOORD0.zzz) + u_xlat1.xyz;
    u_xlat10 = dot(u_xlat1.xyz, u_xlat1.xyz);
    u_xlat10 = rsqrt(u_xlat10);
    u_xlat1.xyz = float3(u_xlat10) * u_xlat1.xyz;
    u_xlat1.x = dot(Globals._WorldSpaceLightPos0.xyz, u_xlat1.xyz);
    u_xlat16_3.x = half((-u_xlat1.x) * -1.98000002 + 1.98010004);
    u_xlat16_0.z = half(u_xlat1.x * u_xlat1.x + 1.0);
    u_xlat16_0.xz = half2(float2(u_xlat16_0.xz) * float2(10.0, 0.0100164423));
    u_xlat16_3.x = log2(u_xlat16_3.x);
    u_xlat16_0.x = half(u_xlat16_3.x * u_xlat16_0.x);
    u_xlat16_0.x = exp2(u_xlat16_0.x);
    u_xlat16_0.x = half(max(float(u_xlat16_0.x), 9.99999975e-05));
    u_xlat16_0.x = half(u_xlat16_0.z / u_xlat16_0.x);
    u_xlat16_3.x = half(u_xlat1.y * 50.0);
    u_xlat16_3.x = clamp(u_xlat16_3.x, 0.0h, 1.0h);
    u_xlatb1 = u_xlat1.y<0.0;
    u_xlat16_2.xyz = half3(input.TEXCOORD1.xyz + (-input.TEXCOORD2.xyz));
    u_xlat16_3.xyz = half3(u_xlat16_3.xxx * u_xlat16_2.xyz + input.TEXCOORD2.xyz);
    u_xlat16_2.xyz = half3(u_xlat16_0.xxx * input.TEXCOORD3.xyz + u_xlat16_3.xyz);
    u_xlat16_0.xyz = (bool(u_xlatb1)) ? u_xlat16_2.xyz : u_xlat16_3.xyz;
    output.SV_Target0.xyz = sqrt(u_xlat16_0.xyz);
    output.SV_Target0.w = 1.0;
    return output;
}


-- Vertex shader for "glcore":
Shader Disassembly:
#ifdef VERTEX
#version 150
#extension GL_ARB_explicit_attrib_location : require
#extension GL_ARB_shader_bit_encoding : enable

uniform 	vec4 _WorldSpaceLightPos0;
uniform 	vec4 hlslcc_mtx4x4glstate_matrix_mvp[4];
uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
uniform 	vec4 _LightColor0;
uniform 	float _Exposure;
uniform 	vec3 _GroundColor;
uniform 	vec3 _SkyTint;
uniform 	float _AtmosphereThickness;
in  vec4 in_POSITION0;
out vec3 vs_TEXCOORD0;
out vec3 vs_TEXCOORD1;
out vec3 vs_TEXCOORD2;
out vec3 vs_TEXCOORD3;
vec4 u_xlat0;
vec2 u_xlat1;
vec4 u_xlat2;
vec4 u_xlat3;
vec3 u_xlat4;
vec4 u_xlat5;
vec3 u_xlat6;
vec3 u_xlat8;
float u_xlat9;
vec2 u_xlat15;
bool u_xlatb15;
float u_xlat17;
float u_xlat21;
float u_xlat22;
void main()
{
    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4glstate_matrix_mvp[1];
    u_xlat0 = hlslcc_mtx4x4glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
    u_xlat0 = hlslcc_mtx4x4glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
    gl_Position = u_xlat0 + hlslcc_mtx4x4glstate_matrix_mvp[3];
    u_xlat0.xyz = (-vec3(_SkyTint.x, _SkyTint.y, _SkyTint.z)) + vec3(1.0, 1.0, 1.0);
    u_xlat0.xyz = u_xlat0.xyz * vec3(0.300000012, 0.300000042, 0.300000012) + vec3(0.5, 0.419999987, 0.324999988);
    u_xlat0.xyz = u_xlat0.xyz * u_xlat0.xyz;
    u_xlat0.xyz = u_xlat0.xyz * u_xlat0.xyz;
    u_xlat0.xyz = vec3(1.0, 1.0, 1.0) / u_xlat0.xyz;
    u_xlat21 = log2(_AtmosphereThickness);
    u_xlat21 = u_xlat21 * 2.5;
    u_xlat21 = exp2(u_xlat21);
    u_xlat1.xy = vec2(u_xlat21) * vec2(0.049999997, 0.0314159282);
    u_xlat2.xyz = in_POSITION0.yyy * hlslcc_mtx4x4unity_ObjectToWorld[1].xyz;
    u_xlat2.xyz = hlslcc_mtx4x4unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
    u_xlat2.xyz = hlslcc_mtx4x4unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
    u_xlat21 = dot(u_xlat2.xyz, u_xlat2.xyz);
    u_xlat21 = inversesqrt(u_xlat21);
    u_xlat2.xzw = vec3(u_xlat21) * u_xlat2.xyz;
    u_xlatb15 = u_xlat2.z>=0.0;
    if(u_xlatb15){
        u_xlat15.x = u_xlat2.z * u_xlat2.z + 0.0506249666;
        u_xlat15.x = sqrt(u_xlat15.x);
        u_xlat21 = (-u_xlat2.y) * u_xlat21 + u_xlat15.x;
        u_xlat15.x = (-u_xlat2.z) * 1.0 + 1.0;
        u_xlat22 = u_xlat15.x * 5.25 + -6.80000019;
        u_xlat22 = u_xlat15.x * u_xlat22 + 3.82999992;
        u_xlat22 = u_xlat15.x * u_xlat22 + 0.458999991;
        u_xlat15.x = u_xlat15.x * u_xlat22 + -0.00286999997;
        u_xlat15.x = u_xlat15.x * 1.44269502;
        u_xlat15.x = exp2(u_xlat15.x);
        u_xlat3.xy = vec2(u_xlat21) * vec2(0.5, 20.0);
        u_xlat4.xyz = u_xlat2.xzw * u_xlat3.xxx;
        u_xlat4.xyz = u_xlat4.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.0, 1.00010002, 0.0);
        u_xlat21 = dot(u_xlat4.xyz, u_xlat4.xyz);
        u_xlat21 = sqrt(u_xlat21);
        u_xlat15.y = (-u_xlat21) + 1.0;
        u_xlat15.xy = u_xlat15.xy * vec2(0.246031836, 230.831207);
        u_xlat22 = exp2(u_xlat15.y);
        u_xlat9 = dot(_WorldSpaceLightPos0.xyz, u_xlat4.xyz);
        u_xlat9 = u_xlat9 / u_xlat21;
        u_xlat17 = dot(u_xlat2.xzw, u_xlat4.xyz);
        u_xlat21 = u_xlat17 / u_xlat21;
        u_xlat9 = (-u_xlat9) + 1.0;
        u_xlat17 = u_xlat9 * 5.25 + -6.80000019;
        u_xlat17 = u_xlat9 * u_xlat17 + 3.82999992;
        u_xlat17 = u_xlat9 * u_xlat17 + 0.458999991;
        u_xlat9 = u_xlat9 * u_xlat17 + -0.00286999997;
        u_xlat9 = u_xlat9 * 1.44269502;
        u_xlat9 = exp2(u_xlat9);
        u_xlat21 = (-u_xlat21) + 1.0;
        u_xlat17 = u_xlat21 * 5.25 + -6.80000019;
        u_xlat17 = u_xlat21 * u_xlat17 + 3.82999992;
        u_xlat17 = u_xlat21 * u_xlat17 + 0.458999991;
        u_xlat21 = u_xlat21 * u_xlat17 + -0.00286999997;
        u_xlat21 = u_xlat21 * 1.44269502;
        u_xlat21 = exp2(u_xlat21);
        u_xlat21 = u_xlat21 * 0.25;
        u_xlat21 = u_xlat9 * 0.25 + (-u_xlat21);
        u_xlat21 = u_xlat22 * u_xlat21 + u_xlat15.x;
        u_xlat21 = max(u_xlat21, 0.0);
        u_xlat21 = min(u_xlat21, 50.0);
        u_xlat5.xyz = u_xlat0.xyz * u_xlat1.yyy + vec3(0.0125663709, 0.0125663709, 0.0125663709);
        u_xlat6.xyz = (-vec3(u_xlat21)) * u_xlat5.xyz;
        u_xlat6.xyz = u_xlat6.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat6.xyz = exp2(u_xlat6.xyz);
        u_xlat21 = u_xlat3.y * u_xlat22;
        u_xlat3.xzw = u_xlat2.xzw * u_xlat3.xxx + u_xlat4.xyz;
        u_xlat22 = dot(u_xlat3.xzw, u_xlat3.xzw);
        u_xlat22 = sqrt(u_xlat22);
        u_xlat9 = (-u_xlat22) + 1.0;
        u_xlat9 = u_xlat9 * 230.831207;
        u_xlat9 = exp2(u_xlat9);
        u_xlat4.x = dot(_WorldSpaceLightPos0.xyz, u_xlat3.xzw);
        u_xlat4.x = u_xlat4.x / u_xlat22;
        u_xlat3.x = dot(u_xlat2.xzw, u_xlat3.xzw);
        u_xlat22 = u_xlat3.x / u_xlat22;
        u_xlat3.x = (-u_xlat4.x) + 1.0;
        u_xlat17 = u_xlat3.x * 5.25 + -6.80000019;
        u_xlat17 = u_xlat3.x * u_xlat17 + 3.82999992;
        u_xlat17 = u_xlat3.x * u_xlat17 + 0.458999991;
        u_xlat3.x = u_xlat3.x * u_xlat17 + -0.00286999997;
        u_xlat3.x = u_xlat3.x * 1.44269502;
        u_xlat3.x = exp2(u_xlat3.x);
        u_xlat22 = (-u_xlat22) + 1.0;
        u_xlat17 = u_xlat22 * 5.25 + -6.80000019;
        u_xlat17 = u_xlat22 * u_xlat17 + 3.82999992;
        u_xlat17 = u_xlat22 * u_xlat17 + 0.458999991;
        u_xlat22 = u_xlat22 * u_xlat17 + -0.00286999997;
        u_xlat22 = u_xlat22 * 1.44269502;
        u_xlat22 = exp2(u_xlat22);
        u_xlat22 = u_xlat22 * 0.25;
        u_xlat22 = u_xlat3.x * 0.25 + (-u_xlat22);
        u_xlat15.x = u_xlat9 * u_xlat22 + u_xlat15.x;
        u_xlat15.x = max(u_xlat15.x, 0.0);
        u_xlat15.x = min(u_xlat15.x, 50.0);
        u_xlat3.xzw = u_xlat5.xyz * (-u_xlat15.xxx);
        u_xlat3.xzw = u_xlat3.xzw * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat3.xzw = exp2(u_xlat3.xzw);
        u_xlat15.x = u_xlat3.y * u_xlat9;
        u_xlat3.xyz = u_xlat15.xxx * u_xlat3.xzw;
        u_xlat3.xyz = u_xlat6.xyz * vec3(u_xlat21) + u_xlat3.xyz;
        u_xlat4.xyz = u_xlat0.xyz * u_xlat1.xxx;
        u_xlat4.xyz = u_xlat3.xyz * u_xlat4.xyz;
        u_xlat3.xyz = u_xlat3.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996);
    } else {
        u_xlat21 = min(u_xlat2.z, -0.00100000005);
        u_xlat21 = -9.99999975e-05 / u_xlat21;
        u_xlat5.xyz = vec3(u_xlat21) * u_xlat2.xzw + vec3(0.0, 1.00010002, 0.0);
        u_xlat15.x = dot((-u_xlat2.xzw), u_xlat5.xyz);
        u_xlat15.y = dot(_WorldSpaceLightPos0.xyz, u_xlat5.xyz);
        u_xlat15.xy = (-u_xlat15.xy) + vec2(1.0, 1.0);
        u_xlat9 = u_xlat15.x * 5.25 + -6.80000019;
        u_xlat9 = u_xlat15.x * u_xlat9 + 3.82999992;
        u_xlat9 = u_xlat15.x * u_xlat9 + 0.458999991;
        u_xlat15.x = u_xlat15.x * u_xlat9 + -0.00286999997;
        u_xlat15.x = u_xlat15.x * 1.44269502;
        u_xlat15.x = exp2(u_xlat15.x);
        u_xlat9 = u_xlat15.y * 5.25 + -6.80000019;
        u_xlat9 = u_xlat15.y * u_xlat9 + 3.82999992;
        u_xlat9 = u_xlat15.y * u_xlat9 + 0.458999991;
        u_xlat22 = u_xlat15.y * u_xlat9 + -0.00286999997;
        u_xlat22 = u_xlat22 * 1.44269502;
        u_xlat22 = exp2(u_xlat22);
        u_xlat5.xy = u_xlat15.xx * vec2(0.25, 0.249900013);
        u_xlat15.x = u_xlat22 * 0.25 + u_xlat5.x;
        u_xlat5.xz = vec2(u_xlat21) * vec2(0.5, 20.0);
        u_xlat6.xyz = u_xlat2.xzw * u_xlat5.xxx;
        u_xlat6.xyz = u_xlat6.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.0, 1.00010002, 0.0);
        u_xlat21 = dot(u_xlat6.xyz, u_xlat6.xyz);
        u_xlat21 = sqrt(u_xlat21);
        u_xlat21 = (-u_xlat21) + 1.0;
        u_xlat21 = u_xlat21 * 230.831207;
        u_xlat21 = exp2(u_xlat21);
        u_xlat15.x = u_xlat21 * u_xlat15.x + (-u_xlat5.y);
        u_xlat15.x = max(u_xlat15.x, 0.0);
        u_xlat15.x = min(u_xlat15.x, 50.0);
        u_xlat5.xyw = u_xlat0.xyz * u_xlat1.yyy + vec3(0.0125663709, 0.0125663709, 0.0125663709);
        u_xlat8.xyz = (-u_xlat15.xxx) * u_xlat5.xyw;
        u_xlat8.xyz = u_xlat8.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat3.xyz = exp2(u_xlat8.xyz);
        u_xlat21 = u_xlat5.z * u_xlat21;
        u_xlat8.xyz = vec3(u_xlat21) * u_xlat3.xyz;
        u_xlat0.xyz = u_xlat0.xyz * u_xlat1.xxx + vec3(0.0199999996, 0.0199999996, 0.0199999996);
        u_xlat4.xyz = u_xlat0.xyz * u_xlat8.xyz;
    //ENDIF
    }
    vs_TEXCOORD0.xyz = (-in_POSITION0.xyz);
    u_xlat0.xyz = vec3(_GroundColor.x, _GroundColor.y, _GroundColor.z) * vec3(_GroundColor.x, _GroundColor.y, _GroundColor.z);
    u_xlat0.xyz = u_xlat0.xyz * u_xlat3.xyz + u_xlat4.xyz;
    vs_TEXCOORD1.xyz = u_xlat0.xyz * vec3(_Exposure);
    u_xlat0.x = dot(_WorldSpaceLightPos0.xyz, (-u_xlat2.xzw));
    u_xlat0.x = u_xlat0.x * u_xlat0.x;
    u_xlat0.x = u_xlat0.x * 0.75 + 0.75;
    u_xlat0.xyz = u_xlat0.xxx * u_xlat4.xyz;
    vs_TEXCOORD2.xyz = u_xlat0.xyz * vec3(_Exposure);
    u_xlat0.xyz = u_xlat3.xyz * _LightColor0.xyz;
    vs_TEXCOORD3.xyz = u_xlat0.xyz * vec3(_Exposure);
    return;
}

#endif
#ifdef FRAGMENT
#version 150
#extension GL_ARB_explicit_attrib_location : require
#extension GL_ARB_shader_bit_encoding : enable

uniform 	vec4 _WorldSpaceLightPos0;
uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
uniform 	float _SunSize;
in  vec3 vs_TEXCOORD0;
in  vec3 vs_TEXCOORD1;
in  vec3 vs_TEXCOORD2;
in  vec3 vs_TEXCOORD3;
layout(location = 0) out vec4 SV_Target0;
vec4 u_xlat0;
vec3 u_xlat1;
vec3 u_xlat2;
bool u_xlatb4;
float u_xlat6;
void main()
{
    u_xlat0.x = log2(_SunSize);
    u_xlat0.x = u_xlat0.x * 0.649999976;
    u_xlat0.x = exp2(u_xlat0.x);
    u_xlat2.xyz = vs_TEXCOORD0.yyy * hlslcc_mtx4x4unity_ObjectToWorld[1].xyz;
    u_xlat2.xyz = hlslcc_mtx4x4unity_ObjectToWorld[0].xyz * vs_TEXCOORD0.xxx + u_xlat2.xyz;
    u_xlat2.xyz = hlslcc_mtx4x4unity_ObjectToWorld[2].xyz * vs_TEXCOORD0.zzz + u_xlat2.xyz;
    u_xlat1.x = dot(u_xlat2.xyz, u_xlat2.xyz);
    u_xlat1.x = inversesqrt(u_xlat1.x);
    u_xlat2.xyz = u_xlat2.xyz * u_xlat1.xxx;
    u_xlat2.x = dot(_WorldSpaceLightPos0.xyz, u_xlat2.xyz);
    u_xlat6 = (-u_xlat2.x) * -1.98000002 + 1.98010004;
    u_xlat0.y = u_xlat2.x * u_xlat2.x + 1.0;
    u_xlat0.xy = u_xlat0.xy * vec2(10.0, 0.0100164423);
    u_xlat6 = log2(u_xlat6);
    u_xlat0.x = u_xlat6 * u_xlat0.x;
    u_xlat0.x = exp2(u_xlat0.x);
    u_xlat0.x = max(u_xlat0.x, 9.99999975e-05);
    u_xlat0.x = u_xlat0.y / u_xlat0.x;
    u_xlat2.x = u_xlat2.y * 50.0;
    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
    u_xlatb4 = u_xlat2.y<0.0;
    u_xlat1.xyz = vs_TEXCOORD1.xyz + (-vs_TEXCOORD2.xyz);
    u_xlat1.xyz = u_xlat2.xxx * u_xlat1.xyz + vs_TEXCOORD2.xyz;
    u_xlat0.xyw = u_xlat0.xxx * vs_TEXCOORD3.xyz + u_xlat1.xyz;
    u_xlat0.xyz = (bool(u_xlatb4)) ? u_xlat0.xyw : u_xlat1.xyz;
    SV_Target0.xyz = sqrt(u_xlat0.xyz);
    SV_Target0.w = 1.0;
    return;
}

#endif


-- Fragment shader for "glcore":
Shader Disassembly:
// All GLSL source is contained within the vertex program

 }
}
Fallback Off
}