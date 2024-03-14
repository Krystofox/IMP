using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;
using Raylib_cs;
using Game.PhysicsMain;
using static Game.PhysicsMain.Physics;
using Game.Graphics;
using static Game.GameResources;

namespace Game.GameLogic;

class PillarObject : IUpdatableObject
{
    public string Name => "Pillar";
    ModelButtonPart1 pillarModel = new ModelButtonPart1();
    Vector3 Position;
    BodyHandle colisionMesh;
    DetectionContact moveBlock;
    public PillarObject(Vector3 position)
    {
        Position = position;
        GetGResources().lazyObjects.Add(pillarModel);

        moveBlock = new DetectionContact(position, new Vector3(0,0,0), new Vector3(2, 2, 3f), false);

        Box box = new Box(2, 2, 3);
        Physics phys = GetPhysics();
        colisionMesh = phys.simulation.Bodies.Add(BodyDescription.CreateKinematic(position, phys.simulation.Shapes.Add(box), -1));
    }
    public void Update()
    {
        Animate();
        pillarModel.Position = PosAnim;
        pillarModel.Draw();
        Physics phys = GetPhysics();
        phys.simulation.Bodies[colisionMesh].Pose.Position = PosAnim + new Vector3(0, 0, -1.5f);
        #if HITBOX
            new ColisionMeshD(pillarModel.Position + new Vector3(0, 0, -1.5f), new Vector3(2, 2, 3), Quaternion.Identity, Color.Blue).Draw();
        #endif
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
        pillarModel.Dispose();
    }
}
