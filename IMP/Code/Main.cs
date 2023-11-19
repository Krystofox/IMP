using Raylib_cs;

namespace Game;

class Main
{
    public Main()
    {
        MainThread mainThread = new MainThread();
        RenderThread renderThread = new RenderThread();

        while (!Raylib.WindowShouldClose())
        {
            mainThread.GameLogic();
            renderThread.RenderFrame();
            //renderThread.WaitForRenderThread();
        }
    }

}

