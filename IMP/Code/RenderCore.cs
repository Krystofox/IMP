using System.Diagnostics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Game;
class RenderCore
{
    public static int screenWidth = 1920;
    public static int screenHeight = 1080;
    public RenderCore()
    {
        // Initialize window
        SetConfigFlags(ConfigFlags.FLAG_WINDOW_RESIZABLE);
        InitWindow(screenWidth, screenHeight, "IMP");
        // TEMP
        SetWindowPosition(1920, 0);
        // _____________
        //SetTargetFPS(60);

        //!!!OpenGL rendering functions can be only called on the same thread that created the window
    }
    public Stopwatch execTime = new Stopwatch();
    public void RenderFrame()
    {
        execTime.Restart();
        DrawFrame();
        execTime.Stop();
    }

    public void DrawFrame()
    {
        BeginDrawing();
            ClearBackground(Color.WHITE);
        EndDrawing();
    }

}

