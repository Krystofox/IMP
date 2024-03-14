using Game.PhysicsMain;
using System.Diagnostics;
using Game.GameLogic;
using static Game.Graphics.GraphicsState;

namespace Game;
class GameLogicThread
{
    static GameLogicThread GLT_instance;
    InputHandler inputHandler = new InputHandler();
    private Physics physics;
    Thread thread;
    public List<IUpdatableObject> updatables = new List<IUpdatableObject>();
    public GameLogicThread()
    {
        GLT_instance = this;
        physics = new Physics();
        physics.Initialize();
    }

    public IUpdatableObject? GetUpdatableByName(string Name)
    {
        foreach (var u in updatables)
        {
            if(u.Name == Name)
                return u;
        }
        return null;
    }
    public static GameLogicThread GetGameLogicThread()
    {
        return GLT_instance;
    }
    public Stopwatch execTime = new Stopwatch();

    public void RunGameLogic()
    {
        execTime.Restart();
        physics.SimulationStep();
        thread = new Thread(GameLogic);
        thread.Start();
    }

    public void WaitForGameLogic()
    {
        thread.Join();
    }
    public void GameLogic()
    {
        GetStateL().dynamicObjects.Clear();
        GetStateL().uiObjects.Clear();
        for (int i = 0; i < updatables.Count; i++)
        {
            updatables[i].Update();
        }
        execTime.Stop();
    }

}

