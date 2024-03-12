using System.Numerics;
using BepuUtilities;
using Game.Graphics;
using Raylib_cs; 
using static Game.GameLogicThread;
using static Game.GameResources;

namespace Game.GameLogic;

class Puzzle2Object : IUpdatableObject
{
    public uint Id { get; private set; }
    public string Name => "Puzzle2";
    ButtonObject button1 = new ButtonObject(new Vector3(32,-35f,0f));
    GateObject gate1 = new GateObject(new Vector3(33f,-42f,0f),new Vector3(0,0,0));
    GateObject gate2 = new GateObject(new Vector3(33f,-45f,0f),new Vector3(0,0,0));
    public Puzzle2Object()
    {
        Id = GetNewID();
        GetGameLogicThread().updatables.Add(new SmallRockObject(new Vector3(33,-38f,0.5f)));
        GetGameLogicThread().updatables.Add(button1);
        GetGameLogicThread().updatables.Add(gate1);
        GetGameLogicThread().updatables.Add(gate2);
    }

    public void Update()
    {
        gate1.IsOpen = button1.Triggered;     
        gate2.IsOpen = !button1.Triggered;
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
