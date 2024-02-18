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

class LanternObject : IUpdatableObject
{
    public uint Id { get; private set; }
    public string Name => "Lantern";
    Light light;
    ModelLantern lanternModel = new ModelLantern();
    BodyHandle colisionMesh;
    public Vector3 Position;
    public LanternObject()
    {
        Id = GetNewID();
        GetGResources().lazyObjects.Add(lanternModel);
        light = new Light(
            1,
            LightType.Point,
            new Vector3(0, 0, 0),
            Vector3.Zero,
            Color.Yellow
        );

        Box box = new Box(1, 1, 1);
        BodyInertia intertia = box.ComputeInertia(1f);
        Physics phys = GetPhysics();
        colisionMesh = phys.simulation.Bodies.Add(BodyDescription.CreateDynamic(new Vector3(1, 1, 2), intertia, phys.simulation.Shapes.Add(box), 0f));
    }

    public void Update()
    {
        Position = GetPhysics().simulation.Bodies[colisionMesh].Pose.Position;
        lanternModel.Position = Position;
        lanternModel.Draw();
        light.Position = Position;
        light.Update();
        new ColisionMeshD(Position,new Vector3(1,1,1),GetPhysics().simulation.Bodies[colisionMesh].Pose.Orientation,Color.Blue).Draw();
    }

    public void Dispose()
    {
        GetGameLogicThread().updatables.Remove(this);
    }
}
