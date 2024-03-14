using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;
using Raylib_cs;
using Game.PhysicsMain;
using static Game.PhysicsMain.Physics;
using Game.Graphics;
using static Game.GameResources;

namespace Game.GameLogic;

class LanternObject : IUpdatableObject
{
    public string Name => "Lantern";
    Light light;
    ModelLantern lanternModel = new ModelLantern();
    BodyHandle colisionMesh;
    public Vector3 Position;
    public LanternObject(Vector3 position)
    {
        Position = position;
        GetGResources().lazyObjects.Add(lanternModel);
        light = new Light(
            1,
            LightType.Point,
            new Vector3(0, 0, 0),
            Vector3.Zero,
            new Color(255,255,0,255)
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
        #if HITBOX
            new ColisionMeshD(Position,new Vector3(1,1,1),GetPhysics().simulation.Bodies[colisionMesh].Pose.Orientation,Color.Blue).Draw();
        #endif
    }

    public void Dispose()
    {
        lanternModel.Dispose();
        light.Dispose();
    }
}
