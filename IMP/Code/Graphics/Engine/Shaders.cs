using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Game.Graphics;
class Shaders
{
    public Shader lighting;
    public Shader foliage;
    static Shaders instance;

    public Shader[] shaders = {
            LoadShader("assets/Shaders/lighting.vs", "assets/Shaders/lighting.fs"),
            LoadShader("assets/Shaders/lighting.vs", "assets/Shaders/foliage.fs")
        };
    unsafe public Shaders()
    {
        instance = this;
        float[] ambient = [0.1f, 0.1f, 0.1f, 1.0f];
        
        for (int i = 0; i < shaders.Length; i++)
        {
            shaders[i].Locs[(int)ShaderLocationIndex.VectorView] = GetShaderLocation(shaders[i], "viewPos");
            int ambientLoc = GetShaderLocation(shaders[i], "ambient");
            SetShaderValue(shaders[i], ambientLoc, ambient, ShaderUniformDataType.Vec4);
        }
        lighting = shaders[0];
        foliage = shaders[1];
    }

    unsafe public void SetVectorView(Vector3 cameraPosition)
    {
        for (int i = 0; i < shaders.Length; i++)
        {
            SetShaderValue(
                shaders[i],
                shaders[i].Locs[(int)ShaderLocationIndex.VectorView],
                cameraPosition,
                ShaderUniformDataType.Vec3
            );
        }
    }

    public static Shaders GetShaders()
    {
        return instance;
    }

}

