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

namespace Game.GameLogic;

class MapObject : IUpdatableObject
{
    Player player = new Player();
    public MapObject()
    {
        Physics phys = GetPhysics();
        phys.simulation.Statics.Add(new StaticDescription(new Vector3(0,0,-0.5f), phys.simulation.Shapes.Add(new Box(100,100,1))));
    }

    public void Update()
    {
        player.Update();
        //new RayCube(new Vector3(0,0,-0.5f),100,100,1,new Color(250,250,250,255)).Draw();
        new RayGrid(100,1).Draw();
    }

    public void Dispose()
    {
        GetGameLogicThread().updatables.Remove(this);
    }
}
