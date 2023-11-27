using Raylib_cs;
using Game.PhysicsMain;
using static Raylib_cs.Raylib;

namespace Game;
class GameLogicThread
{
    Physics physics;
    Thread thread;
    public GameLogicThread()
    {
        physics = new Physics();
        physics.Initialize();
        
    }

    public void RunGameLogic()
    {
        thread = new Thread(GameLogic);
        thread.Start();
    }

    public void WaitForGameLogic()
    {
        thread.Join();
    }
    public void GameLogic()
    {
        physics.SimulationStep();
    }
    
}

