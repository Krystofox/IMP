using System.Numerics;
using Game.Graphics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Game.Graphics.Shaders;
using static Game.GameResources;

namespace Game.Graphics;

public class Light : ILazyLoad
{
    public bool Enabled;
    public Vector3 Position;
    public Vector3 Target;
    public Color Color;

    public int EnabledLoc;
    public int PosLoc;
    public int TargetLoc;
    public int ColorLoc;
    public int LightId;

    public Light(int lightId, Vector3 position, Vector3 target, Color color)
    {
        Enabled = true;
        Position = position;
        Target = target;
        Color = color;
        LightId = lightId;
        GetGResources().lazyObjects.Add(this);
    }
    public void Initialize()
    {
        if (EnabledLoc == 0)
        {
            Shaders shaders = GetShaders();
            string enabledName = "lights[" + LightId + "].enabled";
            string posName = "lights[" + LightId + "].position";
            string targetName = "lights[" + LightId + "].target";
            string colorName = "lights[" + LightId + "].color";

            EnabledLoc = GetShaderLocation(shaders.lighting, enabledName);
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
        Shaders s = GetShaders();
        for (int i = 0; i < s.shaders.Length; i++)
        {
            Shader shader = s.shaders[i];

            SetShaderValue(
            shader,
            EnabledLoc,
            Enabled ? 1 : 0,
            ShaderUniformDataType.Int
        );
            SetShaderValue(shader, PosLoc, Position, ShaderUniformDataType.Vec3);
            SetShaderValue(shader, TargetLoc, Target, ShaderUniformDataType.Vec3);
            float[] color =
            [
                Color.R / (float)255,
                Color.G / (float)255,
                Color.B / (float)255,
                Color.A / (float)255
            ];
            SetShaderValue(shader, ColorLoc, color, ShaderUniformDataType.Vec4);
        }
    }

    public void Dispose()
    {
        
    }
}