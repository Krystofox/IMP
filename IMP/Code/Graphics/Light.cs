using System.Numerics;
using Game.Graphics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Game.Graphics.Shaders;
using static Game.GameResources;

namespace Game.Graphics;

public enum LightType
{
    Directorional,
    Point
}

public class Light : ILazyLoad
{
    public bool Enabled;
    public LightType Type;
    public Vector3 Position;
    public Vector3 Target;
    public Color Color;

    public int EnabledLoc;
    public int TypeLoc;
    public int PosLoc;
    public int TargetLoc;
    public int ColorLoc;
    public int LightId;

    public Light(int lightId, LightType type, Vector3 position, Vector3 target, Color color)
    {
        Enabled = true;
        Type = type;
        Position = position;
        Target = target;
        Color = color;
        LightId = lightId;
        GetGResources().lazyObjects.Add(this);
        //GOOD FOR NOW ( Implement light managment and dynamic unloading )
    }
    public void Initialize()
    {
        if (EnabledLoc == 0)
        {
            Shaders shaders = GetShaders();
            string enabledName = "lights[" + LightId + "].enabled";
            string typeName = "lights[" + LightId + "].type";
            string posName = "lights[" + LightId + "].position";
            string targetName = "lights[" + LightId + "].target";
            string colorName = "lights[" + LightId + "].color";

            EnabledLoc = GetShaderLocation(shaders.lighting, enabledName);
            TypeLoc = GetShaderLocation(shaders.lighting, typeName);
            PosLoc = GetShaderLocation(shaders.lighting, posName);
            TargetLoc = GetShaderLocation(shaders.lighting, targetName);
            ColorLoc = GetShaderLocation(shaders.lighting, colorName);
        }
        UpdateLight();
    }

    public void Update()
    {
        GetGResources().lazyObjects.Add(this);
    }

    public void UpdateLight()
    {
        Shaders shaders = GetShaders();
        SetShaderValue(
            shaders.lighting,
            EnabledLoc,
            Enabled ? 1 : 0,
            ShaderUniformDataType.SHADER_UNIFORM_INT
        );
        SetShaderValue(shaders.lighting, TypeLoc, (int)Type, ShaderUniformDataType.SHADER_UNIFORM_INT);
        SetShaderValue(shaders.lighting, PosLoc, Position, ShaderUniformDataType.SHADER_UNIFORM_VEC3);
        SetShaderValue(shaders.lighting, TargetLoc, Target, ShaderUniformDataType.SHADER_UNIFORM_VEC3);
        float[] color = new[]
        {
                (float)Color.R / (float)255,
                (float)Color.G / (float)255,
                (float)Color.B / (float)255,
                (float)Color.A / (float)255
            };
        SetShaderValue(shaders.lighting, ColorLoc, color, ShaderUniformDataType.SHADER_UNIFORM_VEC4);
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}