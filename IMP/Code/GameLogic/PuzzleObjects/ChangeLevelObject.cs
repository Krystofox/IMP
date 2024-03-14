using System.Numerics;

namespace Game.GameLogic;

class ChangeLevelObject : IUpdatableObject
{
    public string Name => "ChangeLevel";
    DetectionContact moveTrigger;
    string Level;
    public ChangeLevelObject(Vector3 position,Vector3 scale,string level)
    {
        Level = level;
        moveTrigger = new DetectionContact(position, new Vector3(0,0,0), scale, false);
    }
    public void Update()
    {
        if(moveTrigger.ContactDetected())
            MapLoader.ChangeLevelLazy(Level);
    }

    public void Dispose()
    {
        
    }
}
