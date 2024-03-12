using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;
using BepuUtilities;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Game.GameLogicThread;
using Game.PhysicsMain;
using static Game.PhysicsMain.Physics;
using Game.Graphics;
using static Game.GameResources;

namespace Game.GameLogic;

class ButtonObject : IUpdatableObject
{
    public uint Id { get; private set; }
    public string Name => "Button";
    ModelButtonPart1 buttonPart1 = new ModelButtonPart1();
    ModelButtonPart2 buttonPart2 = new ModelButtonPart2();
    Vector3 Position;
    DetectionContact buttonTrigger;
    public ButtonObject(Vector3 position)
    {
        Id = GetNewID();
        Position = position;
        GetGResources().lazyObjects.Add(buttonPart1);
        GetGResources().lazyObjects.Add(buttonPart2);

        buttonTrigger = new DetectionContact(position, new Vector3(0, 0, 0), new Vector3(2, 2, 10), true);

        //Box box = new Box(1, 1, 1);
        //BodyInertia intertia = box.ComputeInertia(1f);
        //Physics phys = GetPhysics();
        //colisionMesh = phys.simulation.Bodies.Add(BodyDescription.CreateDynamic(position, intertia, phys.simulation.Shapes.Add(box), 0f));
        //colisionMesh = phys.simulation.Bodies.Add(BodyDescription.CreateKinematic(position, phys.simulation.Shapes.Add(box), -1));
    }
    public bool Triggered = false;
    bool waitForUnpress = false;
    public void Update()
    {
        if (buttonTrigger.ActionDetected())
        {
            waitForUnpress = true;
        }
        else
        {
            if (waitForUnpress)
            {
                Triggered = !Triggered;
                waitForUnpress = false;
            }
        }

        buttonPart1.Position = Position + new Vector3(0, 0, 1);
        buttonPart1.Draw();

        buttonPart2.Position = Position + new Vector3(0, 0, 1);
        if(Triggered)
        {
            buttonPart2.Orientation *= Quaternion.CreateFromYawPitchRoll(0, 0.01f, 0.01f);
            buttonPart2.Position = Position + new Vector3(0, 0, 2);
        }
        buttonPart2.Draw();

        new ColisionMeshD(Position, new Vector3(2, 2, 10), Quaternion.Identity, Color.Blue).Draw();
    }


    public void Dispose()
    {
        GetGameLogicThread().updatables.Remove(this);
    }
}
