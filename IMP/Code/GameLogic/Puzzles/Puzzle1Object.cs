using System.Numerics;
using BepuUtilities;
using Game.Graphics;
using Raylib_cs; 
using static Game.GameLogicThread;
using static Game.GameResources;

namespace Game.GameLogic;

class Puzzle1Object : IUpdatableObject
{
    public uint Id { get; private set; }
    public string Name => "Puzzle1";
    ButtonObject button1 = new ButtonObject(new Vector3(26,-21f,0f));
    GateObject gate1 = new GateObject(new Vector3(27f,-30f,0f),new Vector3(0,0,0));
    public Puzzle1Object()
    {
        Id = GetNewID();
        GetGameLogicThread().updatables.Add(button1);
        GetGameLogicThread().updatables.Add(gate1);
    }

    public void Update()
    {
        //Console.WriteLine(trigger1.ContactDetected());
        //Console.WriteLine(playerController.PlayerPosition);
        gate1.IsOpen = button1.Triggered;
            
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
