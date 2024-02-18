using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Game.Develop;
class ConnectionWaiter
{
    static public void WaitingConnectionScreen()
    {
        EnableCursor();
        if(WindowShouldClose())
            Environment.Exit(0);
        BeginDrawing();
        ClearBackground(Color.White);
        DrawText("Waiting for connection",0,0,24,Color.Black);
        EndDrawing();
        WaitTime(0.033);
    }
}