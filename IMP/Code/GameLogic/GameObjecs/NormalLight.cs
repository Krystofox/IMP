using System.Numerics;
using Game.Graphics;
using Raylib_cs;
using static Game.GameLogicThread;

namespace Game.GameLogic;

class NormalLight : IUpdatableObject
{
    public string Name => "NormalLight";
    Light light;
    public NormalLight(Vector3 position)
    {
        light = new Light(
            1,
            position,
            Vector3.Zero,
            new Color(255,255,0,255)
        );
        light.Update();
    }

    public void Update()
    {

    }

    public void Dispose()
    {
        light.Dispose();
    }
}
