using System.Numerics;
using static Game.GameLogicThread;


namespace Game.GameLogic;

class Puzzle1Object : IUpdatableObject
{
    public string Name => "Puzzle1";
    ButtonObject button1 = new ButtonObject(new Vector3(26,-21f,0f));
    GateObject gate1 = new GateObject(new Vector3(27f,-30f,0f));
    public Puzzle1Object()
    {
        GetGameLogicThread().updatables.Add(button1);
        GetGameLogicThread().updatables.Add(gate1);
    }

    public void Update()
    {
        gate1.IsOpen = button1.Triggered;
    }

    public void Dispose()
    {

    }
}
