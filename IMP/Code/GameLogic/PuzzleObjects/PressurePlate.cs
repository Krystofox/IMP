using System.Numerics;
using Raylib_cs;
using Game.Graphics;
using static Game.GameResources;

namespace Game.GameLogic;

class PressurePlateObject : IUpdatableObject
{
    public string Name => "Pillar";
    ModelPressurePlate modelPressurePlate = new ModelPressurePlate();
    Vector3 Position;
    DetectionContact moveBlock;
    public PressurePlateObject(Vector3 position)
    {
        Position = position;
        GetGResources().lazyObjects.Add(modelPressurePlate);
        moveBlock = new DetectionContact(position, new Vector3(0, 0, 0), new Vector3(2, 2, 3f), false);
    }
    public void Update()
    {
        IsPushed = moveBlock.ContactDetected();
        Animate();
        modelPressurePlate.Position = PosAnim;
        modelPressurePlate.Draw();
        #if HITBOX
            new ColisionMeshD(Position, new Vector3(2, 2, 3), Quaternion.Identity, Color.Blue).Draw();
        #endif
    }

    Vector3 PosAnim;
    float t = 1f;
    public bool IsPushed = false;
    public void Animate()
    {
        PosAnim = Vector3.Lerp(Position, Position + new Vector3(0, 0, 0.25f), t);
        float dir = IsPushed ? -1 : 1;
        t += dir * 0.01f;
        t = float.Clamp(t, 0, 1);
    }

    public void Dispose()
    {
        modelPressurePlate.Dispose();
    }
}
