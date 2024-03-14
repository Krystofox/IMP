using static Game.GameLogicThread;
using Game.Graphics;
using static Game.Graphics.GraphicsState;

namespace Game.GameLogic;

class MapObject : IUpdatableObject
{
    //!!!! REMOVE DEPRECATED ADD STATICS TO GRAPHICS STATE
    public string Name => "Map";
    public List<IDrawableObject> staticDraws = new List<IDrawableObject>();
    public MapObject()
    {

    }

    public void Update()
    {
        foreach (var sd in staticDraws)
        {
            GetStateL().dynamicObjects.Add(sd);
        }
    }

    public void Dispose()
    {
        foreach (var s in staticDraws)
            s.Dispose();
    }
}
