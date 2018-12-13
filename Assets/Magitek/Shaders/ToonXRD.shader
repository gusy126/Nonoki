// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ToonXRD"
{
	Properties
	{
		_SSSL("SSSL", 2D) = "white" {}
		_Lit("Lit", 2D) = "white" {}
		_Dark("Dark", 2D) = "white" {}
		_InnerLineColor("Inner Line Color", Color) = (0,0,0,1)
		_SpecularIntensity("Specular Intensity", Range( 0 , 20)) = 1
		_SpecPower("Spec Power", Range( 0 , 100)) = 60
		_EmissionColor("Emission Color", Color) = (0,0,0,0)
		_EmissionPower("Emission Power", Range( 0 , 100)) = 0
		_OutlineWidth("Outline Width", Range( 0.001 , 0.01)) = 0.001
		_OutlineColor("Outline Color", Color) = (0,0,0,0)
		_ToonRamp("Toon Ramp", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ }
		Cull Front
		CGPROGRAM
		#pragma target 3.0
		#pragma surface outlineSurf Outline nofog  keepalpha noshadow noambient novertexlights nolightmap nodynlightmap nodirlightmap nometa noforwardadd vertex:outlineVertexDataFunc 
		void outlineVertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float outlineVar = _OutlineWidth;
			v.vertex.xyz += ( v.normal * outlineVar );
		}
		inline half4 LightingOutline( SurfaceOutput s, half3 lightDir, half atten ) { return half4 ( 0,0,0, s.Alpha); }
		void outlineSurf( Input i, inout SurfaceOutput o )
		{
			o.Emission = _OutlineColor.rgb;
		}
		ENDCG
		

		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		Blend SrcAlpha OneMinusSrcAlpha
		CGINCLUDE
		#include "UnityPBSLighting.cginc"
		#include "UnityCG.cginc"
		#include "UnityShaderVariables.cginc"
		#include "Lighting.cginc"
		#pragma target 5.0
		struct Input
		{
			float2 uv_texcoord;
			float3 viewDir;
			float3 worldPos;
			float3 worldNormal;
		};

		struct SurfaceOutputCustomLightingCustom
		{
			half3 Albedo;
			half3 Normal;
			half3 Emission;
			half Metallic;
			half Smoothness;
			half Occlusion;
			half Alpha;
			Input SurfInput;
			UnityGIInput GIData;
		};

		uniform float4 _EmissionColor;
		uniform float _EmissionPower;
		uniform sampler2D _Lit;
		uniform float4 _Lit_ST;
		uniform float _SpecPower;
		uniform float _SpecularIntensity;
		uniform sampler2D _SSSL;
		uniform float4 _SSSL_ST;
		uniform float4 _InnerLineColor;
		uniform sampler2D _Dark;
		uniform float4 _Dark_ST;
		uniform sampler2D _ToonRamp;
		uniform float _OutlineWidth;
		uniform float4 _OutlineColor;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			v.vertex.xyz += 0;
		}

		inline half4 LightingStandardCustomLighting( inout SurfaceOutputCustomLightingCustom s, half3 viewDir, UnityGI gi )
		{
			UnityGIInput data = s.GIData;
			Input i = s.SurfInput;
			half4 c = 0;
			float3 ase_worldPos = i.worldPos;
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aseld
			float3 ase_worldlightDir = 0;
			#else //aseld
			float3 ase_worldlightDir = normalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			#endif //aseld
			float3 ase_worldNormal = i.worldNormal;
			float dotResult32 = dot( ( i.viewDir * float3( -1,-1,-1 ) ) , reflect( ase_worldlightDir , ase_worldNormal ) );
			float2 uv_SSSL = i.uv_texcoord * _SSSL_ST.xy + _SSSL_ST.zw;
			float4 tex2DNode3 = tex2D( _SSSL, uv_SSSL );
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aselc
			float4 ase_lightColor = 0;
			#else //aselc
			float4 ase_lightColor = _LightColor0;
			#endif //aselc
			float2 uv_Dark = i.uv_texcoord * _Dark_ST.xy + _Dark_ST.zw;
			float2 uv_Lit = i.uv_texcoord * _Lit_ST.xy + _Lit_ST.zw;
			float4 tex2DNode13 = tex2D( _Lit, uv_Lit );
			float dotResult7 = dot( ase_worldlightDir , ase_worldNormal );
			float2 appendResult172 = (float2((0.01 + (dotResult7 - -1.0) * (0.99 - 0.01) / (1.0 - -1.0)) , 0.0));
			float4 lerpResult47 = lerp( tex2D( _Dark, uv_Dark ) , tex2DNode13 , ( tex2DNode3.g + tex2D( _ToonRamp, appendResult172 ).r ));
			float4 lerpResult49 = lerp( _InnerLineColor , lerpResult47 , tex2DNode3.a);
			c.rgb = ( ( ( ( ( pow( (0.0 + (dotResult32 - -1.0) * (1.0 - 0.0) / (1.0 - -1.0)) , _SpecPower ) * _SpecularIntensity ) * tex2DNode3.r ) * ase_lightColor ) + lerpResult49 ) + ( ase_lightColor * tex2DNode3.b * float4(0,0,0,0) ) ).rgb;
			c.a = 1;
			return c;
		}

		inline void LightingStandardCustomLighting_GI( inout SurfaceOutputCustomLightingCustom s, UnityGIInput data, inout UnityGI gi )
		{
			s.GIData = data;
		}

		void surf( Input i , inout SurfaceOutputCustomLightingCustom o )
		{
			o.SurfInput = i;
			float2 uv_Lit = i.uv_texcoord * _Lit_ST.xy + _Lit_ST.zw;
			float4 tex2DNode13 = tex2D( _Lit, uv_Lit );
			o.Emission = ( _EmissionColor * _EmissionPower * ( 1.0 - tex2DNode13.a ) ).rgb;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf StandardCustomLighting keepalpha fullforwardshadows exclude_path:deferred vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 5.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float3 worldNormal : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldNormal = worldNormal;
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.viewDir = worldViewDir;
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = IN.worldNormal;
				SurfaceOutputCustomLightingCustom o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputCustomLightingCustom, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15401
-1273.333;12;1266;639.6667;354.644;778.3226;2.547457;True;True
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;41;-338.7717,-325.017;Float;True;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldNormalVector;54;-284.1668,-134.1249;Float;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldNormalVector;103;-591.1364,421.2473;Float;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;44;-635.751,260.0536;Float;False;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;33;-317.9301,-519.7692;Float;True;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DotProductOpNode;7;-373.612,321.4593;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ReflectOpNode;154;-54.70654,-192.2873;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;178;-36.92273,-289.7316;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;-1,-1,-1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DotProductOpNode;32;148.6008,-280.9536;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;127;-236.8791,323.3293;Float;False;5;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;3;FLOAT;0.01;False;4;FLOAT;0.99;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;172;-46.71246,323.4085;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;161;-130.9794,118.8852;Float;True;Property;_ToonRamp;Toon Ramp;10;0;Create;True;0;0;False;0;e63aa4b278eea7345a5d171adb23229b;b7e1eaa3d94e2734e92f89a1dc6592aa;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.RangedFloatNode;31;347.8816,-273.9313;Float;False;Property;_SpecPower;Spec Power;5;0;Create;True;0;0;False;0;60;15.9;0;100;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;157;350.2432,-462.3275;Float;False;5;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;29;606.8809,-187.9314;Float;False;Property;_SpecularIntensity;Specular Intensity;4;0;Create;True;0;0;False;0;1;10;0;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;180;115.9743,221.3483;Float;True;Property;_TextureSample0;Texture Sample 0;12;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;30;729.8803,-286.9313;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;3;110.7503,-86.19073;Float;True;Property;_SSSL;SSSL;0;0;Create;True;0;0;False;0;None;15fea3afe2d53b24389f9ea3d8fb16aa;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;133;578.8006,227.3428;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;13;418.6812,545.8564;Float;True;Property;_Lit;Lit;1;0;Create;True;0;0;False;0;None;521265a8b627d4646942e37c025fcd21;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;929.8806,-267.9315;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;14;421.6806,340.8424;Float;True;Property;_Dark;Dark;2;0;Create;True;0;0;False;0;None;c1d334d75a99be2479d403023077a2c8;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;1123.88,-141.9313;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;-1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LightColorNode;138;1116.969,-12.65523;Float;False;0;3;COLOR;0;FLOAT3;1;FLOAT;2
Node;AmplifyShaderEditor.LerpOp;47;788.9573,462.9996;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;16;755.7665,242.8867;Float;False;Property;_InnerLineColor;Inner Line Color;3;0;Create;True;0;0;False;0;0,0,0,1;0.8,0.8,0.8,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;147;1219.579,211.488;Float;False;Constant;_Color0;Color 0;10;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;1298.376,-131.348;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;49;1007.404,157.0123;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;25;1450.177,-19.54803;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;115;1628.473,-281.2395;Float;False;Property;_EmissionColor;Emission Color;6;0;Create;True;0;0;False;0;0,0,0,0;1,0,0,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;182;1306.298,541.2601;Float;False;2;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;131;1609.077,222.8965;Float;False;Property;_OutlineColor;Outline Color;9;0;Create;True;0;0;False;0;0,0,0,0;0,0.1258549,1,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;1433.376,93.25201;Float;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;129;1620.797,-108.6417;Float;False;Property;_EmissionPower;Emission Power;7;0;Create;True;0;0;False;0;0;0.8;0;100;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;132;1607.458,422.9717;Float;False;Property;_OutlineWidth;Outline Width;8;0;Create;True;0;0;False;0;0.001;0.00175;0.001;0.01;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;26;1617.977,29.05198;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OutlineNode;130;1889.792,114.911;Float;False;0;True;None;0;0;Front;3;0;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;116;1963.616,-161.7195;Float;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;2112.721,-205.4858;Float;False;True;7;Float;ASEMaterialInspector;0;0;CustomLighting;ToonXRD;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;True;0.004;1,0.01667563,0,0;VertexScale;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;7;0;44;0
WireConnection;7;1;103;0
WireConnection;154;0;41;0
WireConnection;154;1;54;0
WireConnection;178;0;33;0
WireConnection;32;0;178;0
WireConnection;32;1;154;0
WireConnection;127;0;7;0
WireConnection;172;0;127;0
WireConnection;157;0;32;0
WireConnection;180;0;161;0
WireConnection;180;1;172;0
WireConnection;30;0;157;0
WireConnection;30;1;31;0
WireConnection;133;0;3;2
WireConnection;133;1;180;1
WireConnection;28;0;30;0
WireConnection;28;1;29;0
WireConnection;27;0;28;0
WireConnection;27;1;3;1
WireConnection;47;0;14;0
WireConnection;47;1;13;0
WireConnection;47;2;133;0
WireConnection;23;0;27;0
WireConnection;23;1;138;0
WireConnection;49;0;16;0
WireConnection;49;1;47;0
WireConnection;49;2;3;4
WireConnection;25;0;23;0
WireConnection;25;1;49;0
WireConnection;182;1;13;4
WireConnection;24;0;138;0
WireConnection;24;1;3;3
WireConnection;24;2;147;0
WireConnection;26;0;25;0
WireConnection;26;1;24;0
WireConnection;130;0;131;0
WireConnection;130;1;132;0
WireConnection;116;0;115;0
WireConnection;116;1;129;0
WireConnection;116;2;182;0
WireConnection;0;2;116;0
WireConnection;0;13;26;0
WireConnection;0;11;130;0
ASEEND*/
//CHKSM=BFBE2686770AEA9F8BF8124E28B5C7724950934F