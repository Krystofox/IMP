using System.Numerics;
using Raylib_cs;
using Game.Graphics;
using static Game.GameResources;
using static Game.GameLogic.AudioHandler;

namespace Game.GameLogic;

class ButtonObject : IUpdatableObject
{
    public string Name => "Button";
    ModelButtonPart1 buttonPart1 = new ModelButtonPart1();
    ModelButtonPart2 buttonPart2 = new ModelButtonPart2();
    Vector3 Position;
    DetectionContact buttonTrigger;
    public ButtonObject(Vector3 position)
    {
        Position = position;
        GetGResources().lazyObjects.Add(buttonPart1);
        GetGResources().lazyObjects.Add(buttonPart2);

        buttonTrigger = new DetectionContact(position, new Vector3(0, 0, 0), new Vector3(2, 2, 10), true);
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
                GetAudioHandler().PlaySoundM("bop");
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
        #if HITBOX
            new ColisionMeshD(Position, new Vector3(2, 2, 10), Quaternion.Identity, Color.Blue).Draw();
        #endif
    }


    public void Dispose()
    {
        buttonPart1.Dispose();
        buttonPart2.Dispose();
    }
}
