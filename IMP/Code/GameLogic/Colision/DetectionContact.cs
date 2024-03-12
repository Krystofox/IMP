using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;
using Game.PhysicsMain;
using static Game.PhysicsMain.Physics;


namespace Game.GameLogic;

class DetectionContact
{
    StaticHandle colisionMesh;

    public DetectionContact(Vector3 Position,Vector3 Rotation,Vector3 Size, bool AllowCollision = false)
    {
        Physics phys = GetPhysics();
        Box box = new Box(Size.X,Size.Y,Size.Z);
        RigidPose pose = new RigidPose
        {
            Position = Position,
            Orientation = Quaternion.CreateFromYawPitchRoll(Rotation.Y, Rotation.X, Rotation.Z)
        };
        colisionMesh = phys.simulation.Statics.Add(new StaticDescription(pose, phys.simulation.Shapes.Add(box)));
        ref var sProperties = ref phys.bodyProperties.Allocate(colisionMesh);
        sProperties = new ColisionObjectProperties
        {
            AllowCollision = AllowCollision,
            DetectionObject = true,
            DetectedContact = false,
            DetectedAction = false
        };
    }

    public bool ContactDetected()
    {
        Physics phys = GetPhysics();
        ref var sProperties = ref phys.bodyProperties[colisionMesh];
        if (sProperties.DetectedContact)
        {
            sProperties.DetectedContact = false;
            return true;
        }
        return false;
    }

    public bool ActionDetected()
    {
        Physics phys = GetPhysics();
        ref var sProperties = ref phys.bodyProperties[colisionMesh];
        if (sProperties.DetectedAction)
        {
            sProperties.DetectedAction = false;
            return true;
        }
        return false;
    }
}
