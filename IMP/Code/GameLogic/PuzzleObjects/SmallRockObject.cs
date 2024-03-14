using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;
using Raylib_cs;
using Game.PhysicsMain;
using static Game.PhysicsMain.Physics;
using Game.Graphics;
using static Game.GameResources;

namespace Game.GameLogic;

class SmallRockObject : IUpdatableObject
{
    public string Name => "SmallRock";
    ModelSmallRock smallRockModel = new ModelSmallRock();
    BodyHandle colisionMesh;
    public Vector3 Position;
    public SmallRockObject(Vector3 position)
    {
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
        #if HITBOX
            new ColisionMeshD(Position,new Vector3(1,1,1),GetPhysics().simulation.Bodies[colisionMesh].Pose.Orientation,Color.Blue).Draw();
        #endif
    }

    public void Dispose()
    {
        smallRockModel.Dispose();
    }
}
