using System.Diagnostics;
using Game.Graphics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Game.Graphics.GraphicsState;
using System.Numerics;

namespace Game;
class RenderCore
{
    public static int screenWidth = 1920;
    public static int screenHeight = 1080;
    Shaders shaders;
    public RenderCore()
    {
        GraphicsState.Setup();
        // Initialize window
        SetConfigFlags(ConfigFlags.ResizableWindow);
        InitWindow(screenWidth, screenHeight, "IMP");

        //!!!OpenGL rendering functions can be only called on the same thread that created the window
        // Implement Forward+ shading
        shaders = new Shaders();
    }
    public Stopwatch execTime = new Stopwatch();
    public void RenderFrame()
    {
        execTime.Restart();
        DrawFrame();
        execTime.Stop();
    }

    unsafe public void DrawFrame()
    {
        // Its working, HOW?! (Thread Race condition)
        // Mabey has small chance for catastrophic failure
        GraphicsState gState = GetStateR();

        SetShaderValue(
                shaders.lighting,
                shaders.lighting.Locs[(int)ShaderLocationIndex.VectorView],
                gState.camera3D.Position,
                ShaderUniformDataType.Vec3
            );

        BeginDrawing();
        ClearBackground(Color.White);
        BeginMode3D(gState.camera3D);
        for (int i = 0; i < gState.dynamicObjects.Count; i++)
        {
            gState.dynamicObjects[i].OnDraw();
        }
        EndMode3D();
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

