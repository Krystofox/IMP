using System.Diagnostics;
using Game.GameLogic;
using Game.Graphics;
using static Raylib_cs.Raylib;

namespace Game;

class Main
{
    GameResources gameResources = new GameResources();
    RenderCore renderCore = new RenderCore();
    GameLogicThread logicThread = new GameLogicThread();
    Process currentProcess;
    public Main()
    {
        MapLoader.ChangeLevelLazy("dev_blend2");
        lastTime = GetTime();

        logicThread.RunGameLogic();
        logicThread.WaitForGameLogic();

        while (!WindowShouldClose())
        {
            SetPerformanceStats();
            gameResources.LazyLoadObjects();
            MapLoader.ChangeLevelCheck();
            GraphicsState.SwitchStates();
            logicThread.RunGameLogic();
            renderCore.RenderFrame();
            logicThread.WaitForGameLogic();
            FrameControl();
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
    double lastTime = 0;
    private void FrameControl()
    {
        double frameTime = GetTime() - lastTime;
        if (frameTime < targetFrameTime)
            WaitTime(targetFrameTime - frameTime);
        lastTime = GetTime();
    }

}