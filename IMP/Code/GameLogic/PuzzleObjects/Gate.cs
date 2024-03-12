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

class GateObject : IUpdatableObject
{
    public uint Id { get; private set; }
    public string Name => "Gate";
    ModelGate gateModel = new ModelGate();
    Vector3 Position;
    Vector3 Rotation;
    BodyHandle colisionMesh;
    DetectionContact moveBlock;
    public GateObject(Vector3 position, Vector3 rotation)
    {
        Id = GetNewID();
        Position = position;
        Rotation = rotation;
        GetGResources().lazyObjects.Add(gateModel);

        moveBlock = new DetectionContact(position, rotation, new Vector3(6, 1, 4.5f), false);



        Box box = new Box(6, 1, 4.5f);
        Physics phys = GetPhysics();
        RigidPose pose = new RigidPose
        {
            Orientation = Quaternion.CreateFromYawPitchRoll(-Rotation.Y, -Rotation.X, Rotation.Z),
            Position = position
        };

        colisionMesh = phys.simulation.Bodies.Add(BodyDescription.CreateKinematic(pose, phys.simulation.Shapes.Add(box), -1));
    }
    bool waitForUnpress = false;
    public void Update()
    {
        //gateModel.Position = Position + new Vector3(0,0,4.5f);
        Animate();
        gateModel.Position = PosAnim;
        gateModel.Rotation = Rotation;
        gateModel.Draw();
        Physics phys = GetPhysics();
        phys.simulation.Bodies[colisionMesh].Pose.Position = PosAnim + new Vector3(0, 0, -2);
        new ColisionMeshD(gateModel.Position + new Vector3(0, 0, -2), new Vector3(6, 1, 4.5f), Quaternion.Identity, Color.Blue).Draw();
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
        GetGameLogicThread().updatables.Remove(this);
    }
}
