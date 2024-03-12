using System.Diagnostics;
using Game.Graphics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using Game.Develop;

namespace Game;

class Main
{
    GameResources gameResources = new GameResources();
    RenderCore renderCore = new RenderCore();
    GameLogicThread logicThread = new GameLogicThread();
    Process currentProcess;
    public Main()
    {
        //Map map = new Map("assets/Maps/dev_loading2");
        GameLogic.MapLoader.LoadMap("dev_blend2");
        lastTime = GetTime();

        logicThread.RunGameLogic();
        logicThread.WaitForGameLogic();

        while (!WindowShouldClose())
        {
            SetPerformanceStats();
            gameResources.LazyLoadObjects();
            GraphicsState.SwitchStates();
            logicThread.RunGameLogic();
            renderCore.RenderFrame();
            logicThread.WaitForGameLogic();
            FrameControl();

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.WaitForFullGCComplete();
        }
    }

    private void SetPerformanceStats()
    {
        renderCore.performanceStats.logicExec = logicThread.execTime.ElapsedMilliseconds;
        renderCore.performanceStats.renderExec = renderCore.execTime.ElapsedMilliseconds;
        renderCore.performanceStats.fps = GetFPS();
        renderCore.performanceStats.memoryAloc = Math.Round(Process.GetCurrentProcess().WorkingSet64 * 0.000001 - 100, 5);
    }

    double targetFrameTime = (double)1 / 144;
    //double targetFrameTime = 0;
    double lastTime = 0;
    private void FrameControl()
    {
        double frameTime = GetTime() - lastTime;
        if (frameTime < targetFrameTime)
            WaitTime(targetFrameTime - frameTime);
        lastTime = GetTime();
    }

}