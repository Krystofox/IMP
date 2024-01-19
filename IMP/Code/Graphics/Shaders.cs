using System.Numerics;
using Game.Graphics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Game.Graphics.GraphicsState;

namespace Game.Graphics;
class Shaders
{
    public Shader lighting;
    static Shaders instance;
    unsafe public Shaders()
    {
        instance = this;
        lighting = LoadShader("assets/Shaders/lighting.vs", "assets/Shaders/lighting.fs");
        lighting.Locs[(int)ShaderLocationIndex.SHADER_LOC_VECTOR_VIEW] = GetShaderLocation(lighting, "viewPos");
        int ambientLoc = GetShaderLocation(lighting, "ambient");
        float[] ambient = new[] { 0.1f, 0.1f, 0.1f, 1.0f };
        SetShaderValue(lighting, ambientLoc, ambient, ShaderUniformDataType.SHADER_UNIFORM_VEC4);
    }

    public static Shaders GetShaders()
    {
        return instance;
    }

}

