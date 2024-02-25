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
using static Game.Graphics.GraphicsState;


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

    float jumpInterval = 0f;
    float jumpIntervalLen = 0.5f;
    float rot;
    Vector2 lookVector = new Vector2(0, 1);
    public void Update()
    {
        MouseLock();
        Physics phys = GetPhysics();

        float multiplier = GetFrameTime() * 20;

        Vector2 movementV = GetInputHandler().GetMovementVector();
        if (movementV != Vector2.Zero)
            movementV = Vector2.Normalize(movementV);


        if (GetInputHandler().GetJump())
        {
            if (jumpInterval > jumpIntervalLen)
            {
                jumpInterval = 0;
                var hitHandler = new RayCastStruct.HitHandler { };
                phys.simulation.RayCast(PlayerPosition, new Vector3(0, 0, -2f), 1, ref hitHandler, 0);
                if (hitHandler.Hit.Hit)
                {
                    phys.simulation.Bodies[colisionMesh].ApplyLinearImpulse(new Vector3(0, 0, 10));
                }
            }
        }
        else
            jumpInterval += GetFrameTime();

        lookVector += (movementV - lookVector) * GetFrameTime();

        Vector2 lookVectorN = lookVector;
        if (lookVectorN != Vector2.Zero)
            lookVectorN = Vector2.Normalize(lookVectorN);

        //new RayLine(1920 / 2, 1080 / 2, Convert.ToInt32(1920 / 2 + lookVectorN.X * 100), Convert.ToInt32(1080 / 2 - lookVectorN.Y * 100), Color.Red).Draw();

        Vector2 up = new Vector2(0, 1);
        rot = GetVecAngle(up, new Vector2(-lookVectorN.X, -lookVectorN.Y)) * -2;
        PlayerRotation = Raymath.QuaternionFromEuler(0, 0, rot);

        if (GetInputHandler().GetActionButton())
        {
            var hitHandler = new RayCastStruct.HitHandler { };
            phys.simulation.RayCast(PlayerPosition + new Vector3(lookVectorN.X, lookVectorN.Y, 0.5f), new Vector3(0, 0, -1f), 1, ref hitHandler, 0);
            if (hitHandler.Hit.Hit)
            {
                if(hitHandler.Hit.Collidable.Mobility == CollidableMobility.Dynamic)
                    phys.simulation.Bodies[hitHandler.Hit.Collidable.BodyHandle].Pose.Position = PlayerPosition + new Vector3(lookVectorN.X*1.5f, lookVectorN.Y*1.5f, 0.5f);
            }
        }
        new RayLine3D(PlayerPosition + new Vector3(lookVectorN.X, lookVectorN.Y, 0.5f), PlayerPosition + new Vector3(lookVectorN.X, lookVectorN.Y, -0.5f), Color.Red).Draw();

        // Quick Bypass for orientation lock -- implement using constraints
        phys.simulation.Bodies[colisionMesh].Pose.Orientation = PlayerRotation;
        phys.simulation.Bodies[colisionMesh].ApplyLinearImpulse(new Vector3(movementV.X * multiplier, movementV.Y * multiplier, 0));
        PlayerPosition = phys.simulation.Bodies[colisionMesh].Pose.Position;

        GetStateL().camera3D.Target = PlayerPosition;
        GetStateL().camera3D.Position = PlayerPosition + new Vector3(0, -7, 10);
        

        new ColisionMeshD(PlayerPosition, new Vector3(1, 1, 2), phys.simulation.Bodies[colisionMesh].Pose.Orientation, Color.Blue).Draw();
    }

    static float GetVecAngle(Vector2 a, Vector2 b)
    {
        return MathF.Atan2(b.Y - a.Y, b.X - a.X);
    }

}
