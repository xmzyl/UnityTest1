Shader "Custom/NewShader" {
	SubShader {
		Pass {
		CGPROGRAM

		#pragma vertex C3E2v_varying
		#pragma fragment C3E3f_texture
		struct C3E2v_Output {
			float4 position : POSITION;
			float4 color : COLOR;
			float2 texCoord : TEXCOORD0;
		};
		
		struct C3E3f_Output {
			float4 color: COLOR;
		};

		
		C3E2v_Output C3E2v_varying(float2 position : POSITION,
			float4 color : COLOR,
			float2 texCoord : TEXCOORD0)
		{
			C3E2v_Output OUT;
			OUT.position = float4(position, 0, 1);
			OUT.color = color;
			OUT.texCoord = texCoord;
			return OUT;
		}
		
		C3E3f_Output C3E3f_texture(float2 texCoord : TEXCOORD0,
			uniform sampler2D decal)
		{
			C3E3f_Output OUT;
			OUT.color = tex2D(decal, texCoord);
			return OUT;
		}
		
		ENDCG
	}
	}
}
