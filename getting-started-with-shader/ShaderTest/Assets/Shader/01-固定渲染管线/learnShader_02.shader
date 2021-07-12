Shader "Custom/learnShader_02"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color("Color",Color) = (1,1,1,1)
    }
    SubShader
    {
        Lighting On
        Material
        {
            Diffuse[_Color]//设置自己反射光颜色
            Ambient[_Color]//设置环境光颜色
        }

        Pass
        {
            SetTexture[_MainTex]
            {
                combine Texture * Previous  //合并纹理
            }
        }
    }
}
