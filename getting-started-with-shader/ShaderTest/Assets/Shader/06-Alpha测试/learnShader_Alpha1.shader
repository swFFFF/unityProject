Shader "Hidden/learnShader_Alpha1"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Aplha("a",Range(0,1)) = 0
    }
    SubShader
    {
        Pass
        {
            AlphaTest Greater[_Aplha]
            SetTexture[_MainTex]
            {
                combine texture * previous
            }
        }
    }
}
