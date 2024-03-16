using System.Diagnostics;
using Game.Graphics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Game.Graphics.GraphicsState;

namespace Game;
class RenderCore
{
    public static int screenWidth = 1920;
    public static int screenHeight = 1080;
    Shaders shaders;
    static public Font PixelFont;
    public RenderCore()
    {
        GraphicsState.Setup();
        SetConfigFlags(ConfigFlags.ResizableWindow);
        InitWindow(screenWidth, screenHeight, "IMP");
        shaders = new Shaders();
        PixelFont = LoadFontEx("assets/Font/uni05_53.ttf", 30, null, 480);
    }
    public Stopwatch execTime = new Stopwatch();
    public void RenderFrame()
    {
        execTime.Restart();
        DrawFrame();
        execTime.Stop();
    }

    float deltaT = 0;
    unsafe public void DrawFrame()
    {
        deltaT += GetFrameTime();
        GraphicsState gState = GetStateR();

        shaders.SetVectorView(gState.camera3D.Position);
        int deltaTloc = GetShaderLocation(shaders.foliage, "deltaT");
        SetShaderValue(shaders.foliage, deltaTloc, deltaT, ShaderUniformDataType.Float);


        BeginDrawing();
        ClearBackground(Color.Black);
        BeginMode3D(gState.camera3D);
        for (int i = 0; i < gState.staticObjects.Count; i++)
        {
            gState.staticObjects[i].OnDraw();
        }
        for (int i = 0; i < gState.dynamicObjects.Count; i++)
        {
            gState.dynamicObjects[i].OnDraw();
        }
        EndMode3D();

        for (int i = 0; i < gState.uiObjects.Count; i++)
        {
            gState.uiObjects[i].OnDraw();
        }

        DrawPerformanceStats();
        EndDrawing();
    }

    GraphRenderer graphRenderer = new GraphRenderer(100);
    double lastUpdateTime = 0;

    public struct PerformanceStats
    {
        public long logicExec;
        public long renderExec;
        public int fps;
        public double memoryAloc;
    }
    public PerformanceStats performanceStats = new PerformanceStats();
    private void DrawPerformanceStats()
    {
        DrawRectangle(10, 10, 200, 30 + 20 * 5, new Color(0, 0, 0, 20));
        DrawText($"Logic time: {performanceStats.logicExec} ms", 15, 15 + 20 * 0, 20, Color.Black);
        DrawText($"Render time: {performanceStats.renderExec} ms", 15, 15 + 20 * 1, 20, Color.Black);
        DrawText($"FPS: {performanceStats.fps}", 15, 15 + 20 * 2, 20, Color.Black);
        DrawText($"MEM: {performanceStats.memoryAloc} MB", 15, 15 + 20 * 3, 20, Color.Black);

        graphRenderer.AddValueAvg(GetFrameTime() * 10);
        if (lastUpdateTime + 0.1 < GetTime())
        {
            graphRenderer.UpdateValueAvg();
            lastUpdateTime = GetTime();
        }
        graphRenderer.Draw(15, 15 + 20 * 3, 200 - 15, 20 * 3, Color.Red);
    }

}

