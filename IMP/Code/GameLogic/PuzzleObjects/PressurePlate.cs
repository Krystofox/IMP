using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;
using BepuUtilities;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Game.GameLogicThread;
using Game.PhysicsMain;
using static Game.PhysicsMain.Physics;
using Game.Graphics;
using static Game.GameResources;
using System.Reflection.Metadata;

namespace Game.GameLogic;

class PressurePlateObject : IUpdatableObject
{
    public uint Id { get; private set; }
    public string Name => "Pillar";
    ModelPressurePlate modelPressurePlate = new ModelPressurePlate();
    Vector3 Position;
    Vector3 Rotation;
    BodyHandle colisionMesh;
    DetectionContact moveBlock;
    public PressurePlateObject(Vector3 position, Vector3 rotation)
    {
        Id = GetNewID();
        Position = position;
        Rotation = rotation;
        GetGResources().lazyObjects.Add(modelPressurePlate);
        moveBlock = new DetectionContact(position, rotation, new Vector3(2, 2, 3f), false);
    }
    bool waitForUnpress = false;
    public void Update()
    {
        //gateModel.Position = Position + new Vector3(0,0,4.5f);
        IsPushed = moveBlock.ContactDetected();
        Animate();
        modelPressurePlate.Position = PosAnim;
        modelPressurePlate.Rotation = Rotation;
        modelPressurePlate.Draw();
        Physics phys = GetPhysics();
        new ColisionMeshD(Position, new Vector3(2, 2, 3), Quaternion.Identity, Color.Blue).Draw();
        
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
        GetGameLogicThread().updatables.Remove(this);
    }
}
