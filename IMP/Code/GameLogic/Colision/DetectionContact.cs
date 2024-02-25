using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;
using Game.PhysicsMain;
using static Game.PhysicsMain.Physics;


namespace Game.GameLogic;

class DetectionContact : IContactDetection
{
    StaticHandle colisionMesh;
    public Vector3 Position;

    public uint Id { get; private set; }

    public DetectionContact()
    {
        Id = (uint)IContactDetection.contactDetections.Count;
        IContactDetection.contactDetections.Add(this);
        Physics phys = GetPhysics();
        Box box = new Box(2, 2, 2);
        RigidPose pose = new RigidPose
        {
            Position = new Vector3(0,0,0)
        };
        colisionMesh = phys.simulation.Statics.Add(new StaticDescription(pose, phys.simulation.Shapes.Add(box)));
        ref var sProperties = ref phys.bodyProperties.Allocate(colisionMesh);
        sProperties = new ColisionObjectProperties();
        sProperties.DetectionObject = true;
        sProperties.ContactDetection = Id;
        
        //ref var sProperties = ref phys.bodyProperties.Allocate(colisionMesh);
        //sProperties.DetectionObject = true;

    }

    public void Update()
    {

    }

    public void Dispose()
    {

    }

    public void Contact(BodyHandle contactObject)
    {
        
    }
}
