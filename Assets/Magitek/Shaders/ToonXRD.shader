// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ToonXRD"
{
	Properties
	{
		_ILM("ILM", 2D) = "white" {}
		_Albedo("Albedo", 2D) = "white" {}
		_SSS("SSS", 2D) = "white" {}
		_InnerLineColor("Inner Line Color", Color) = (0,0,0,1)
		_SpecularColor("Specular Color", Color) = (1,1,1,1)
		_SpecularIntensity("Specular Intensity", Range( 0 , 20)) = 10
		_SpecPower("Spec Power", Range( 0 , 100)) = 60
		_EmissionColor("Emission Color", Color) = (0,0,0,0)
		_EmissionPower("Emission Power", Range( 0 , 20)) = 0
		_OutlineWidth("Outline Width", Range( 0.001 , 0.01)) = 0.001
		_OutlineColor("Outline Color", Color) = (0,0,0,0)
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
		

		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityPBSLighting.cginc"
		#include "UnityCG.cginc"
		#include "Lighting.cginc"
		#pragma target 3.5
		struct Input
		{
			float3 viewDir;
			float3 worldNormal;
			float3 worldPos;
			float2 uv_texcoord;
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

		uniform float _SpecPower;
		uniform float _SpecularIntensity;
		uniform sampler2D _ILM;
		uniform float4 _ILM_ST;
		uniform float4 _SpecularColor;
		uniform float4 _InnerLineColor;
		uniform sampler2D _SSS;
		uniform float4 _SSS_ST;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform float4 _EmissionColor;
		uniform float _EmissionPower;
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
			float3 ase_worldNormal = i.worldNormal;
			float3 ase_worldPos = i.worldPos;
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aseld
			float3 ase_worldlightDir = 0;
			#else //aseld
			float3 ase_worldlightDir = normalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			#endif //aseld
			float dotResult38 = dot( ase_worldNormal , ase_worldlightDir );
			float3 normalizeResult34 = normalize( ( ( dotResult38 * ( ase_worldNormal + ase_worldlightDir ) ) * float3( 2,2,2 ) ) );
			float dotResult32 = dot( i.viewDir , normalizeResult34 );
			float2 uv_ILM = i.uv_texcoord * _ILM_ST.xy + _ILM_ST.zw;
			float4 tex2DNode3 = tex2D( _ILM, uv_ILM );
			float2 uv_SSS = i.uv_texcoord * _SSS_ST.xy + _SSS_ST.zw;
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float dotResult7 = dot( ase_worldlightDir , ase_worldNormal );
			float temp_output_127_0 = (0.0 + (dotResult7 - -1.0) * (1.0 - 0.0) / (1.0 - -1.0));
			float temp_output_109_0 = (( temp_output_127_0 >= 0.05 && temp_output_127_0 <= 0.25 ) ? 0.05 :  temp_output_127_0 );
			float temp_output_110_0 = (( temp_output_109_0 >= 0.3 && temp_output_109_0 <= 0.45 ) ? 0.3 :  temp_output_109_0 );
			float temp_output_112_0 = (( temp_output_110_0 >= 0.55 && temp_output_110_0 <= 0.7 ) ? 0.7 :  temp_output_110_0 );
			float4 lerpResult47 = lerp( tex2D( _SSS, uv_SSS ) , tex2D( _Albedo, uv_Albedo ) , (( temp_output_112_0 >= 0.75 && temp_output_112_0 <= 0.95 ) ? 0.95 :  temp_output_112_0 ));
			float4 lerpResult49 = lerp( _InnerLineColor , lerpResult47 , tex2DNode3.a);
			float4 temp_output_26_0 = ( ( ( ( ( pow( dotResult32 , _SpecPower ) * _SpecularIntensity ) * tex2DNode3.r ) * _SpecularColor ) + lerpResult49 ) + ( _SpecularColor * tex2DNode3.b ) );
			c.rgb = temp_output_26_0.rgb;
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
			float3 ase_worldNormal = i.worldNormal;
			float3 ase_worldPos = i.worldPos;
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aseld
			float3 ase_worldlightDir = 0;
			#else //aseld
			float3 ase_worldlightDir = normalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			#endif //aseld
			float dotResult38 = dot( ase_worldNormal , ase_worldlightDir );
			float3 normalizeResult34 = normalize( ( ( dotResult38 * ( ase_worldNormal + ase_worldlightDir ) ) * float3( 2,2,2 ) ) );
			float dotResult32 = dot( i.viewDir , normalizeResult34 );
			float2 uv_ILM = i.uv_texcoord * _ILM_ST.xy + _ILM_ST.zw;
			float4 tex2DNode3 = tex2D( _ILM, uv_ILM );
			float2 uv_SSS = i.uv_texcoord * _SSS_ST.xy + _SSS_ST.zw;
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float dotResult7 = dot( ase_worldlightDir , ase_worldNormal );
			float temp_output_127_0 = (0.0 + (dotResult7 - -1.0) * (1.0 - 0.0) / (1.0 - -1.0));
			float temp_output_109_0 = (( temp_output_127_0 >= 0.05 && temp_output_127_0 <= 0.25 ) ? 0.05 :  temp_output_127_0 );
			float temp_output_110_0 = (( temp_output_109_0 >= 0.3 && temp_output_109_0 <= 0.45 ) ? 0.3 :  temp_output_109_0 );
			float temp_output_112_0 = (( temp_output_110_0 >= 0.55 && temp_output_110_0 <= 0.7 ) ? 0.7 :  temp_output_110_0 );
			float4 lerpResult47 = lerp( tex2D( _SSS, uv_SSS ) , tex2D( _Albedo, uv_Albedo ) , (( temp_output_112_0 >= 0.75 && temp_output_112_0 <= 0.95 ) ? 0.95 :  temp_output_112_0 ));
			float4 lerpResult49 = lerp( _InnerLineColor , lerpResult47 , tex2DNode3.a);
			float4 temp_output_26_0 = ( ( ( ( ( pow( dotResult32 , _SpecPower ) * _SpecularIntensity ) * tex2DNode3.r ) * _SpecularColor ) + lerpResult49 ) + ( _SpecularColor * tex2DNode3.b ) );
			o.Albedo = temp_output_26_0.rgb;
			o.Emission = ( ( 1.0 - tex2DNode3.a ) * _EmissionColor * _EmissionPower ).rgb;
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
			#pragma target 3.5
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
-1273.333;28.66667;1267;645;65.69629;556.9146;2.589473;True;True
Node;AmplifyShaderEditor.WorldNormalVector;54;-680.0576,-1225.626;Float;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;41;-734.6621,-1416.518;Float;True;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldNormalVector;103;-1509.573,675.6208;Float;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;44;-1554.187,514.4268;Float;False;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DotProductOpNode;38;-359.0139,-1370.295;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;39;-356.014,-1265.295;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DotProductOpNode;7;-1292.047,575.8326;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;-218.8127,-1331.878;Float;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;-25.40992,-1332.67;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;2,2,2;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TFHCRemapNode;127;-1155.314,577.7026;Float;False;5;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;33;90.43445,-765.8164;Float;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TFHCCompareWithRange;109;-923.9747,576.6216;Float;False;5;0;FLOAT;0;False;1;FLOAT;0.05;False;2;FLOAT;0.25;False;3;FLOAT;0.05;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalizeNode;34;125.8963,-1333.485;Float;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DotProductOpNode;32;400.4344,-752.8164;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;31;254.4344,-642.8164;Float;False;Property;_SpecPower;Spec Power;6;0;Create;True;0;0;False;0;60;20;0;100;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCCompareWithRange;110;-668.0746,572.7213;Float;False;5;0;FLOAT;0;False;1;FLOAT;0.3;False;2;FLOAT;0.45;False;3;FLOAT;0.3;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;29;513.4344,-556.8164;Float;False;Property;_SpecularIntensity;Specular Intensity;5;0;Create;True;0;0;False;0;10;3;0;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCCompareWithRange;112;-377.4745,573.0208;Float;False;5;0;FLOAT;0;False;1;FLOAT;0.55;False;2;FLOAT;0.7;False;3;FLOAT;0.7;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;30;636.4344,-655.8164;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;836.4344,-636.8164;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCCompareWithRange;111;-113.2032,576.8834;Float;False;5;0;FLOAT;0;False;1;FLOAT;0.75;False;2;FLOAT;0.95;False;3;FLOAT;0.95;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;3;-646.2245,-473.3905;Float;True;Property;_ILM;ILM;0;0;Create;True;0;0;False;0;None;fca5f7e26845a5343af4880d7c59a481;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;14;-335.7433,146.0872;Float;True;Property;_SSS;SSS;2;0;Create;True;0;0;False;0;None;b670d70c28f6f3245ac53bd5b9ac958f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;13;-338.7427,351.1019;Float;True;Property;_Albedo;Albedo;1;0;Create;True;0;0;False;0;None;32df05f7e38423949873a7b54679a4ad;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;22;863.1777,-185.548;Float;False;Property;_SpecularColor;Specular Color;4;0;Create;True;0;0;False;0;1,1,1,1;1,1,1,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;1030.434,-510.8164;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;16;459.2178,25.48673;Float;False;Property;_InnerLineColor;Inner Line Color;3;0;Create;True;0;0;False;0;0,0,0,1;0,0.4823529,1,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;47;150.0551,308.9724;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;49;754.4037,302.2123;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;1197.178,-89.54803;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;1299.178,106.452;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;115;1527.274,-303.2396;Float;False;Property;_EmissionColor;Emission Color;7;0;Create;True;0;0;False;0;0,0,0,0;0.8584906,0.2227216,0.2227216,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;129;1523.998,-124.0417;Float;False;Property;_EmissionPower;Emission Power;8;0;Create;True;0;0;False;0;0;0;0;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;117;1725.088,-396.2605;Float;False;2;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;25;1373.178,-19.54803;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;131;1543.078,236.0965;Float;False;Property;_OutlineColor;Outline Color;10;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;132;1482.06,431.7718;Float;False;Property;_OutlineWidth;Outline Width;9;0;Create;True;0;0;False;0;0.001;0.001;0.001;0.01;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;116;1939.417,-203.5194;Float;False;3;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;26;1554.178,22.45197;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OutlineNode;130;1814.994,332.7109;Float;False;0;True;None;0;0;Front;3;0;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;2112.721,-205.4858;Float;False;True;3;Float;ASEMaterialInspector;0;0;CustomLighting;ToonXRD;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;True;0.004;1,0.01667563,0,0;VertexScale;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;38;0;54;0
WireConnection;38;1;41;0
WireConnection;39;0;54;0
WireConnection;39;1;41;0
WireConnection;7;0;44;0
WireConnection;7;1;103;0
WireConnection;36;0;38;0
WireConnection;36;1;39;0
WireConnection;35;0;36;0
WireConnection;127;0;7;0
WireConnection;109;0;127;0
WireConnection;109;4;127;0
WireConnection;34;0;35;0
WireConnection;32;0;33;0
WireConnection;32;1;34;0
WireConnection;110;0;109;0
WireConnection;110;4;109;0
WireConnection;112;0;110;0
WireConnection;112;4;110;0
WireConnection;30;0;32;0
WireConnection;30;1;31;0
WireConnection;28;0;30;0
WireConnection;28;1;29;0
WireConnection;111;0;112;0
WireConnection;111;4;112;0
WireConnection;27;0;28;0
WireConnection;27;1;3;1
WireConnection;47;0;14;0
WireConnection;47;1;13;0
WireConnection;47;2;111;0
WireConnection;49;0;16;0
WireConnection;49;1;47;0
WireConnection;49;2;3;4
WireConnection;23;0;27;0
WireConnection;23;1;22;0
WireConnection;24;0;22;0
WireConnection;24;1;3;3
WireConnection;117;1;3;4
WireConnection;25;0;23;0
WireConnection;25;1;49;0
WireConnection;116;0;117;0
WireConnection;116;1;115;0
WireConnection;116;2;129;0
WireConnection;26;0;25;0
WireConnection;26;1;24;0
WireConnection;130;0;131;0
WireConnection;130;1;132;0
WireConnection;0;0;26;0
WireConnection;0;2;116;0
WireConnection;0;13;26;0
WireConnection;0;11;130;0
ASEEND*/
//CHKSM=DEFBF6360718A30C7743CBEB592822F1A8C8695A