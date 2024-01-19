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

class MapObject : IUpdatableObject
{
    Player player = new Player();
    RayPlane plane = new RayPlane(50,50,1,1);
    Light light;
    public MapObject()
    {
        Physics phys = GetPhysics();
        phys.simulation.Statics.Add(new StaticDescription(new Vector3(0,0,-1f), phys.simulation.Shapes.Add(new Box(100,100,2))));
        GetGResources().lazyObjects.Add(plane);
        light = new Light(
            0,
            LightType.Point,
            new Vector3(0, 0, 10),
            Vector3.Zero,
            Color.WHITE
        );

        GetGameLogicThread().updatables.Add(new LanternObject());
    }

    public void Update()
    {
        player.Update();
        plane.Draw();
    }

    public void Dispose()
    {
        GetGameLogicThread().updatables.Remove(this);
    }
}
