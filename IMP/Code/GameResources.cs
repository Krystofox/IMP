using Game.Graphics;

namespace Game;
class GameResources
{
    public List<IDrawableObject> lazyObjects = new List<IDrawableObject>();

    static GameResources instance;

    public GameResources()
    {
        instance = this;
    }
    public static GameResources GetGResources()
    {
        return instance;
    }

    public void LazyLoadObjects()
    {
        for (int i = lazyObjects.Count - 1; i >= 0; i--)
        {
            lazyObjects[i].Initialize();
            lazyObjects.RemoveAt(i);
        }
    }


}

