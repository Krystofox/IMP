using Raylib_cs;
using static Raylib_cs.Raylib;
class ConnectionWaiter
{
    static public void WaitingConnectionScreen()
    {
        Raylib.EnableCursor();
        if(WindowShouldClose())
            Environment.Exit(0);
        BeginDrawing();
        ClearBackground(Color.WHITE);
        DrawText("Waiting for connection",0,0,24,Color.BLACK);
        EndDrawing();
        WaitTime(0.033);
    }
}