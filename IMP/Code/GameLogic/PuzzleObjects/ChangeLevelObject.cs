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
using System.Reflection.Metadata;

namespace Game.GameLogic;

class ChangeLevelObject : IUpdatableObject
{
    public uint Id { get; private set; }
    public string Name => "ChangeLevel";
    DetectionContact moveTrigger;
    string Level;
    public ChangeLevelObject(Vector3 position,Vector3 scale,string level)
    {
        Id = GetNewID();
        Level = level;
        moveTrigger = new DetectionContact(position, new Vector3(0,0,0), scale, false);
    }
    public void Update()
    {
        if(moveTrigger.ContactDetected())
            MapLoader.ChangeLevel(Level);
        
    }

    public void Dispose()
    {
        
    }
}
