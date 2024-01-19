using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;
using BepuUtilities;
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
        if (IsKeyDown(KeyboardKey.KEY_W))
            movementVector += new Vector2(1, 0);
        if (IsKeyDown(KeyboardKey.KEY_S))
            movementVector += new Vector2(-1, 0);
        if (IsKeyDown(KeyboardKey.KEY_A))
            movementVector += new Vector2(0, 1);
        if (IsKeyDown(KeyboardKey.KEY_D))
            movementVector += new Vector2(0, -1);
        return movementVector;
    }

    Vector2 oldMousePos;
    public Vector2 GetLookVector()
    {
        Vector2 lookVector = (GetMousePosition() - oldMousePos);
        if(lookVector != Vector2.Zero)
            lookVector = Vector2.Normalize(lookVector);
        oldMousePos = GetMousePosition();
        return lookVector;
    }

    public bool GetJump()
    {
        if (IsKeyDown(KeyboardKey.KEY_SPACE))
            return true;
        return false;
    }
}
