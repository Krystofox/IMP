using Game.Resources;
using Game.GameMap;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Game;

class Main
{
    RenderCore renderCore = new RenderCore();
    GameLogicThread logicThread = new GameLogicThread();
    public Main()
    {
        
        Map map = new Map("assets/Maps/dev_loading2");
        lastTime = GetTime();
        while (!WindowShouldClose())
        {
            logicThread.RunGameLogic();
            renderCore.RenderFrame();
            logicThread.WaitForGameLogic();

            DrawPerformanceStats();
            FrameControl();
        }
    }

    private void DrawPerformanceStats()
    {
        DrawRectangle(10,10,200,30+20*2,new Color(0,0,0,20));
        DrawText($"Logic time: {logicThread.execTime.ElapsedMilliseconds} ms",15,15+20*0,20,Color.BLACK);
        DrawText($"Render time: {renderCore.execTime.ElapsedMilliseconds} ms",15,15+20*1,20,Color.BLACK);
        DrawText($"FPS: {GetFPS()}",15,15+20*2,20,Color.BLACK);
    }

    double targetFrameTime = (double)1/144;
    double lastTime = 0;
    private void FrameControl()
    {
        double frameTime = GetTime() - lastTime;
        if (frameTime < targetFrameTime)
            WaitTime(targetFrameTime - frameTime);
        lastTime = GetTime();
    }

}