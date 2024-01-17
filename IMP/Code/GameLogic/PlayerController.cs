using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;
using BepuUtilities;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Game.PhysicsMain.Physics;
using static Game.GameLogic.InputHandler;
using Game.PhysicsMain;


namespace Game.GameLogic;

class PlayerController
{
    public Vector3 PlayerPosition;
    BodyHandle colisionMesh;
    public PlayerController()
    {
        Box box = new Box(1, 1, 2);
        BodyInertia intertia = box.ComputeInertia(1f);
        Physics phys = GetPhysics();
        colisionMesh = phys.simulation.Bodies.Add(BodyDescription.CreateDynamic(new Vector3(0, 0, 2), intertia, phys.simulation.Shapes.Add(box), 0f));
    }
    public void Update()
    {
        Physics phys = GetPhysics();
        float multiplier = 0.1f;
        Vector2 movementV = GetInputHandler().GetMovementVector() * multiplier;
        phys.simulation.Bodies[colisionMesh].ApplyLinearImpulse(new Vector3(movementV.X, movementV.Y, 0));
        PlayerPosition = phys.simulation.Bodies[colisionMesh].Pose.Position;
        Console.WriteLine(PlayerPosition);
    }

}
