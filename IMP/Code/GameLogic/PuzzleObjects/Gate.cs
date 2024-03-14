using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;
using Raylib_cs;
using Game.PhysicsMain;
using static Game.PhysicsMain.Physics;
using Game.Graphics;
using static Game.GameResources;

namespace Game.GameLogic;

class GateObject : IUpdatableObject
{
    public string Name => "Gate";
    ModelGate gateModel = new ModelGate();
    Vector3 Position;
    BodyHandle colisionMesh;
    DetectionContact moveBlock;
    public GateObject(Vector3 position)
    {
        Position = position;
        GetGResources().lazyObjects.Add(gateModel);

        moveBlock = new DetectionContact(position, new Vector3(0,0,0), new Vector3(6, 1, 4.5f), false);

        Box box = new Box(6, 1, 4.5f);
        Physics phys = GetPhysics();
        colisionMesh = phys.simulation.Bodies.Add(BodyDescription.CreateKinematic(position, phys.simulation.Shapes.Add(box), -1));
    }
    public void Update()
    {
        Animate();
        gateModel.Position = PosAnim;
        gateModel.Draw();
        Physics phys = GetPhysics();
        phys.simulation.Bodies[colisionMesh].Pose.Position = PosAnim + new Vector3(0, 0, -2);
        #if HITBOX
            new ColisionMeshD(gateModel.Position + new Vector3(0, 0, -2), new Vector3(6, 1, 4.5f), Quaternion.Identity, Color.Blue).Draw();
        #endif
    }
    Vector3 PosAnim;
    float t = 1f;
    public bool IsOpen = false;
    public void Animate()
    {
        PosAnim = Vector3.Lerp(Position, Position + new Vector3(0, 0, 4.5f), t);
        float dir = IsOpen ? -1 : 1;
        if (!moveBlock.ContactDetected())
        {
            t += dir * 0.01f;
            t = float.Clamp(t, 0, 1);
        }
    }

    public void Dispose()
    {
        gateModel.Dispose();
    }
}
