using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Game;
class RenderThread
{
    public static int screenWidth = 1920;
    public static int screenHeight = 1080;
    //Thread rThread;
    public RenderThread()
    {
        // Initialize window
        SetConfigFlags(ConfigFlags.FLAG_WINDOW_RESIZABLE);
        InitWindow(screenWidth, screenHeight, "IMP");
        // TEMP
        SetWindowPosition(1920, 0);
        // _____________
        SetTargetFPS(60);

        //!!!OpenGL rendering functions can be only called on the same thread that created the window
        //rThread = new Thread(DrawFrame);
    }

    public void RenderFrame()
    {
        //rThread.Start();
        DrawFrame();
    }
    public void WaitForRenderThread()
    {
        //TODO
        /*rThread.Join();
        rThread = new Thread(DrawFrame);*/
    }

    public void DrawFrame()
    {
        BeginDrawing();
            ClearBackground(Color.WHITE);
        EndDrawing();
    }

}

