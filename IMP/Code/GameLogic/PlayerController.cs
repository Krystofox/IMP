using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;
using BepuUtilities;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Game.PhysicsMain.Physics;
using static Game.GameLogic.InputHandler;
using Game.PhysicsMain;
using Game.Graphics;
using System.Reflection.Metadata;


namespace Game.GameLogic;

class PlayerController
{
    public Vector3 PlayerPosition;
    public Quaternion PlayerRotation;
    BodyHandle colisionMesh;
    public PlayerController()
    {
        Box box = new Box(1, 1, 2);
        BodyInertia intertia = box.ComputeInertia(1f);
        Physics phys = GetPhysics();
        colisionMesh = phys.simulation.Bodies.Add(BodyDescription.CreateDynamic(new Vector3(0, 0, 2), intertia, phys.simulation.Shapes.Add(box), 0f));
        DisableCursor();
    }

    bool mouseLock = true;
    public void MouseLock()
    {
        if (IsKeyPressed(KeyboardKey.P))
        {
            mouseLock = !mouseLock;
            if (mouseLock)
            {
                DisableCursor();
            }
            else
                EnableCursor();
        }

    }

    bool jumped = false;
    float rot;
    Vector2 lookV = new Vector2(0,1);
    public void Update()
    {
        MouseLock();
        Physics phys = GetPhysics();
        float multiplier = 0.1f;
        Vector2 movementV = GetInputHandler().GetMovementVector();
        if (GetInputHandler().GetJump())
        {
            if (!jumped)
            {
                jumped = true;
                phys.simulation.Bodies[colisionMesh].ApplyLinearImpulse(new Vector3(0, 0, 10));
            }
        }
        else
            jumped = false;
        if (mouseLock)
        {
            Vector2 lookVector = GetInputHandler().GetLookVector();
            if (lookVector != Vector2.Zero)
            {
                lookV = lookVector*20;
                float rotSpeed = 50;
                //lookV = Vector2.Clamp(lookV,Vector2.One*-rotSpeed,Vector2.One*rotSpeed);
                Vector2 up = new Vector2(0, 0);
                // Not working
                rot = GetVecAngle(up,Vector2.Normalize(new Vector2(-lookV.X,-lookV.Y)));
                PlayerRotation = Raymath.QuaternionFromEuler(0, 0, rot*2);
            }


        }

        // Quick Bypass for orientation lock -- implement using constraints
        phys.simulation.Bodies[colisionMesh].Pose.Orientation = PlayerRotation;
        float rotat = rot;
        //Console.WriteLine(rotat);
        var ca = MathF.Cos(rotat);
        var sa = MathF.Sin(rotat);
        Vector2 move = new Vector2(ca*movementV.X - sa*movementV.Y, sa*movementV.X + ca*movementV.Y);
        Vector2 moveN = Vector2.Zero;
        if(move != Vector2.Zero)
            moveN = Vector2.Normalize(move) * multiplier;
        new ColisionMeshD(new Vector3(moveN.X,moveN.Y,0)*10,new Vector3(0.5f,0.5f,0.5f),Quaternion.Zero,Color.Violet).Draw();
        new ColisionMeshD(new Vector3(lookV.X,lookV.Y,0)/10,new Vector3(0.5f,0.5f,0.5f),Quaternion.Zero,Color.Violet).Draw();
        //phys.simulation.Bodies[colisionMesh].ApplyLinearImpulse(new Vector3(moveN.X, moveN.Y, 0));
        PlayerPosition = phys.simulation.Bodies[colisionMesh].Pose.Position;
        new ColisionMeshD(PlayerPosition, new Vector3(1, 1, 2), phys.simulation.Bodies[colisionMesh].Pose.Orientation, Color.Blue).Draw();
    }

    float GetVecAngle(Vector2 a, Vector2 b)
    {
        return MathF.Atan2(b.Y - a.Y, b.X - a.X);
    }

}
