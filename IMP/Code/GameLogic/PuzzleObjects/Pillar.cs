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

class PillarObject : IUpdatableObject
{
    public uint Id { get; private set; }
    public string Name => "Pillar";
    ModelButtonPart1 pillarModel = new ModelButtonPart1();
    Vector3 Position;
    Vector3 Rotation;
    BodyHandle colisionMesh;
    DetectionContact moveBlock;
    public PillarObject(Vector3 position, Vector3 rotation)
    {
        Id = GetNewID();
        Position = position;
        Rotation = rotation;
        GetGResources().lazyObjects.Add(pillarModel);

        moveBlock = new DetectionContact(position, rotation, new Vector3(2, 2, 3f), false);

        Box box = new Box(2, 2, 3);
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
        pillarModel.Position = PosAnim;
        pillarModel.Rotation = Rotation;
        pillarModel.Draw();
        Physics phys = GetPhysics();
        phys.simulation.Bodies[colisionMesh].Pose.Position = PosAnim + new Vector3(0, 0, -1.5f);
        new ColisionMeshD(pillarModel.Position + new Vector3(0, 0, -1.5f), new Vector3(2, 2, 3), Quaternion.Identity, Color.Blue).Draw();
    }

    Vector3 PosAnim;
    float t = 1f;
    public bool IsOpen = false;
    bool wasContact = false;
    int stage = 3;
    public void Animate()
    {
        PosAnim = Vector3.Lerp(Position, Position + new Vector3(0, 0, stage), t);
        bool contact = moveBlock.ContactDetected();
        
        if (contact)
        {
            if (IsOpen)
            {
                wasContact = true;
            }
        }
        else
        {
            if (wasContact)
            {
                stage--;
                if(stage < 0)
                    stage = 3;
                wasContact = false;
                
            }
            float dir = IsOpen ? -1 : 1;
            t += dir * 0.01f;
            t = float.Clamp(t, 0, 1);
        }
    }

    public void Dispose()
    {
        GetGameLogicThread().updatables.Remove(this);
    }
}
