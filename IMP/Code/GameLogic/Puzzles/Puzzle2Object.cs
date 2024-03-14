using System.Numerics;
using static Game.GameLogicThread;


namespace Game.GameLogic;

class Puzzle2Object : IUpdatableObject
{
    public string Name => "Puzzle2";
    ButtonObject button1 = new ButtonObject(new Vector3(32,-35f,0f));
    GateObject gate1 = new GateObject(new Vector3(33f,-42f,0f));
    GateObject gate2 = new GateObject(new Vector3(33f,-45f,0f));
    public Puzzle2Object()
    {
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

    }
}
