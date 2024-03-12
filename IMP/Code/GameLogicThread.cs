using Raylib_cs;
using Game.PhysicsMain;
using static Raylib_cs.Raylib;
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
        //Buffer with calculated matricies and locations for gpu
        //Precalculate as much things on logicthread
        //Send buffer to RenderCore
        physics = new Physics();
        physics.Initialize();
    }

    public static uint LastID = 0;
    public static uint GetNewID()
    {
        return LastID++;
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
        //MEMORY LEAKS IF SIMULATION STEP IS IN A ANOTHER THREAD!!!!
        physics.SimulationStep();
        thread = new Thread(GameLogic);
        thread.Start();
    }

    public void WaitForGameLogic()
    {
        thread.Join();
    }
    //Random rnd = new Random();
    public void GameLogic()
    {
        //Thread.Sleep(1000);
        GetStateL().dynamicObjects.Clear();
        GetStateL().uiObjects.Clear();
        for (int i = 0; i < updatables.Count; i++)
        {
            updatables[i].Update();
        }
        execTime.Stop();
    }

}

