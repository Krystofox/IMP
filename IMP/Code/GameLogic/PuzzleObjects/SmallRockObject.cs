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

namespace Game.GameLogic;

class SmallRockObject : IUpdatableObject
{
    public uint Id { get; private set; }
    public string Name => "SmallRock";
    ModelSmallRock smallRockModel = new ModelSmallRock();
    BodyHandle colisionMesh;
    public Vector3 Position;
    public SmallRockObject(Vector3 position)
    {
        Id = GetNewID();
        GetGResources().lazyObjects.Add(smallRockModel);

        Box box = new Box(1, 1, 1);
        BodyInertia intertia = box.ComputeInertia(1f);
        Physics phys = GetPhysics();
        colisionMesh = phys.simulation.Bodies.Add(BodyDescription.CreateDynamic(position, intertia, phys.simulation.Shapes.Add(box), 0f));
    }

    public void Update()
    {
        Position = GetPhysics().simulation.Bodies[colisionMesh].Pose.Position;
        smallRockModel.Position = Position;
        smallRockModel.Draw();
        new ColisionMeshD(Position,new Vector3(1,1,1),GetPhysics().simulation.Bodies[colisionMesh].Pose.Orientation,Color.Blue).Draw();
    }

    public void Dispose()
    {
        GetGameLogicThread().updatables.Remove(this);
    }
}
