using Raylib_cs;
using Game.PhysicsMain;
using static Raylib_cs.Raylib;
using System.Diagnostics;

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
    public Stopwatch execTime = new Stopwatch();

    public void RunGameLogic()
    {
        execTime.Restart();
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
        execTime.Stop();
    }
    
}

