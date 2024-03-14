using System.Numerics;
using static Game.GameLogicThread;

namespace Game.GameLogic;

class Puzzle3Object : IUpdatableObject
{
    public string Name => "Puzzle3";
    ButtonObject button1 = new ButtonObject(new Vector3(28, -55f, 0f));
    ButtonObject button2 = new ButtonObject(new Vector3(45, -52f, 4f));
    GateObject gate1 = new GateObject(new Vector3(21f, -52f, 0f));
    PillarObject pillar1 = new PillarObject(new Vector3(35, -52f, 0f));
    PillarObject pillar2 = new PillarObject(new Vector3(37, -52f, 0f));
    PillarObject pillar3 = new PillarObject(new Vector3(39, -52f, 0f));
    public Puzzle3Object()
    {
        GetGameLogicThread().updatables.Add(button1);
        GetGameLogicThread().updatables.Add(button2);
        GetGameLogicThread().updatables.Add(gate1);
        GetGameLogicThread().updatables.Add(pillar1);
        GetGameLogicThread().updatables.Add(pillar2);
        GetGameLogicThread().updatables.Add(pillar3);
    }

    public void Update()
    {
        pillar1.IsOpen = button1.Triggered;
        pillar2.IsOpen = button1.Triggered;
        pillar3.IsOpen = button1.Triggered;
        gate1.IsOpen = button2.Triggered;
    }

    public void Dispose()
    {

    }
}
