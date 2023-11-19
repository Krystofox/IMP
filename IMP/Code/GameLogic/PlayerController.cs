using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;
using BepuUtilities;
using Raylib_cs;
using static Raylib_cs.Raylib;


namespace Game.GameLogic;

class PlayerController
{
    //REWRITE to Raylib 5.0 new input system
    /*
    public Vector3 playerLocation = new Vector3(0, 0, 0);
    public Vector3 playerRotation = new Vector3(1, 0, 0);
    public Vector3 cameraOffset = new Vector3(0, 0f, 0);
    public bool hookControls = true;

    Vector3 playerSize = new Vector3(2, 6, 2);
    float jumpHeight = 40f;
    float speed = 1f;

    BodyHandle colisionMesh;

    public PlayerController()
    {
        var shape = new Box(playerSize.X, playerSize.Y, playerSize.Z);
        cameraOffset.Y = playerSize.Y - playerSize.Y / 2;
        var shapeInertia = shape.ComputeInertia(1f);
        colisionMesh = physics.simulation.Bodies.Add(BodyDescription.CreateDynamic(new Vector3(0, 15, 0), shapeInertia, physics.simulation.Shapes.Add(shape), 0f));
        oldMousePos = GetMousePosition();
    }

    public void Update()
    {
        Input();
        if (IsKeyPressed(KeyboardKey.KEY_P))
            hookControls = !hookControls;
        if (hookControls)
        {

            MouseInput();

            if (!IsCursorHidden())
                DisableCursor();
        }
        else
        {
            if (IsCursorHidden())
                EnableCursor();
        }
        Main.camera.position = playerLocation + cameraOffset;
        Main.camera.target = playerLocation + cameraOffset + playerRotation;
    }

    bool jump = false;
    unsafe public void Jump()
    {
        if (IsKeyPressed(KeyboardKey.KEY_M))
        {
            physics.simulation.Bodies[colisionMesh].ApplyLinearImpulse(new Vector3(0, jumpHeight * 10, 0));
            return;
        }

        if (!IsKeyDown(KeyboardKey.KEY_SPACE))
        {
            jump = false;
            return;
        }

        if (!jump)
        {
            var hitHandler = new PhysicsMain.RayCastStruct.HitHandler { };
            physics.simulation.RayCast(playerLocation, new Vector3(0, -1f, 0), 1, ref hitHandler, 0);
            if (hitHandler.Hit.Hit)
            {
                physics.simulation.Bodies[colisionMesh].ApplyLinearImpulse(new Vector3(0, jumpHeight, 0));
                jump = true;
            }

        }
    }

    float xRotation = 0;
    float yRotation = 0;

    Vector2 oldMousePos = new Vector2(0, 0);
    public void MouseInput()
    {
        Vector2 mouse = GetMousePosition() - oldMousePos;
        oldMousePos = GetMousePosition();
        xRotation -= mouse.X * GetFrameTime() / 20;
        yRotation -= mouse.Y * GetFrameTime() / 20;
        yRotation = Math.Clamp(yRotation, -1.5f, 1.5f);
        Quaternion rotation = QuaternionEx.CreateFromYawPitchRoll(xRotation, 0, yRotation);
        QuaternionEx.Transform(new Vector3(1, 0, 0), rotation, out playerRotation);
    }


    public void Input()
    {
        Jump();
        Vector2 movementVector = new Vector2(0, 0);
        if (IsKeyDown(KeyboardKey.KEY_W))
            movementVector += new Vector2(1, 0);
        if (IsKeyDown(KeyboardKey.KEY_S))
            movementVector += new Vector2(-1, 0);
        if (IsKeyDown(KeyboardKey.KEY_A))
            movementVector += new Vector2(0, 1);
        if (IsKeyDown(KeyboardKey.KEY_D))
            movementVector += new Vector2(0, -1);



        Vector2 forward = new Vector2(playerRotation.X, playerRotation.Z);
        Vector2 right = new Vector2(playerRotation.Z, -playerRotation.X);
        movementVector = forward * movementVector.X + right * movementVector.Y;

        //Normalize
        if (movementVector != Vector2.Zero)
            movementVector = Vector2.Normalize(movementVector);

        movementVector = movementVector * speed;

        physics.simulation.Bodies[colisionMesh].ApplyLinearImpulse(new Vector3(movementVector.X, 0, movementVector.Y));
        //Console.WriteLine(Physics.simulation.Bodies[colisionMesh].Velocity.Linear);
        playerLocation = physics.simulation.Bodies[colisionMesh].Pose.Position + new Vector3(0, -playerSize.Y / 2, 0);
        //Console.WriteLine(Physics.simulation.Bodies[colisionMesh].Velocity.Linear.Length());
        physics.simulation.Bodies[colisionMesh].Pose.Orientation = Quaternion.Zero;
    }*/

}
