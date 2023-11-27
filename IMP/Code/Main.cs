using Game.Resources;
using Game.GameMap;
using Raylib_cs;

namespace Game;

class Main
{
    public Main()
    {
        RenderCore renderCore = new RenderCore();
        GameLogicThread gameLThread = new GameLogicThread();
        Map map = new Map("assets/Maps/dev_loading2");
        while (!Raylib.WindowShouldClose())
        {
            gameLThread.RunGameLogic();
            renderCore.RenderFrame();
            gameLThread.WaitForGameLogic();
        }
    }

}

