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
using static Game.Graphics.GraphicsState;

namespace Game.GameLogic;

class MapObject : IUpdatableObject
{
    public uint Id { get; private set; }
    public string Name => "Map";
    //RayPlane plane = new RayPlane(50, 50, 1, 1);
    //RayGrid grid = new RayGrid(10,1);
    public List<IDrawableObject> staticDraws = new List<IDrawableObject>();
    public MapObject()
    {
        Id = GetNewID();
        Physics phys = GetPhysics();
        //phys.simulation.Statics.Add(new StaticDescription(new Vector3(0, 0, -1f), phys.simulation.Shapes.Add(new Box(100, 100, 2))));
        //GetGResources().lazyObjects.Add(plane);
    }

    public void Update()
    {
        //plane.Draw();
        foreach (var sd in staticDraws)
        {
            GetStateL().dynamicObjects.Add(sd);
        }
        //grid.Draw();
    }

    public void Dispose()
    {
        GetGameLogicThread().updatables.Remove(this);
    }
}
