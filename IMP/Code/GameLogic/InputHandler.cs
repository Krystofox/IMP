using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Game.GameLogic;

class InputHandler
{
    static InputHandler instance;
    public InputHandler()
    {
        instance = this;
    }
    public static InputHandler GetInputHandler()
    {
        return instance;
    }
    public Vector2 GetMovementVector()
    {
        Vector2 movementVector = new Vector2(0, 0);
        if (IsKeyDown(KeyboardKey.W))
            movementVector += new Vector2(0, 1);
        if (IsKeyDown(KeyboardKey.S))
            movementVector += new Vector2(0, -1);
        if (IsKeyDown(KeyboardKey.A))
            movementVector += new Vector2(-1, 0);
        if (IsKeyDown(KeyboardKey.D))
            movementVector += new Vector2(1, 0);
        return movementVector;
    }

    public bool GetJump()
    {
        return IsKeyDown(KeyboardKey.Space);
    }
    public bool GetActionButton()
    {
        return IsKeyDown(KeyboardKey.Enter);
    }

    Vector2 oldMousePos;
    public Vector2 GetLookVector()
    {
        Vector2 lookVector = GetMousePosition() - oldMousePos;
        if (lookVector != Vector2.Zero)
            lookVector = Vector2.Normalize(lookVector);
        oldMousePos = GetMousePosition();
        return lookVector;
    }
}
