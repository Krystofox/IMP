using System.Numerics;
using static Game.GameLogicThread;

namespace Game.GameLogic;

class Puzzle4Object : IUpdatableObject
{
    public string Name => "Puzzle4";
    PressurePlateObject[,] plates = new PressurePlateObject[3, 3];
    bool[,] solution = {
        {false,false,false},
        {false,true,false},
        {false,false,false}
    };
    GateObject gate1 = new GateObject(new Vector3(4.3f, -20f, 0f));
    public Puzzle4Object()
    {
        GetGameLogicThread().updatables.Add(gate1);
        GetGameLogicThread().updatables.Add(new SmallRockObject(new Vector3(-10,0f,0.5f)));
        GetGameLogicThread().updatables.Add(new SmallRockObject(new Vector3(7f,10f,0.5f)));
        GetGameLogicThread().updatables.Add(new SmallRockObject(new Vector3(22,0f,0.5f)));
        for (int y = 0; y < 3; y++)
            for (int x = 0; x < 3; x++)
            {
                PressurePlateObject plate = new PressurePlateObject(new Vector3(2 + x*2, -10 + y*2, 0));
                plates[x,y] = plate;
                GetGameLogicThread().updatables.Add(plate);
            }
    }

    public void Update()
    {
        bool solved = true;
        for (int y = 0; y < 3; y++)
            for (int x = 0; x < 3; x++)
            {
                if(plates[x,y].IsPushed != solution[x,y])
                    solved = false;
            }
        gate1.IsOpen = solved;
        
    }

    public void Dispose()
    {

    }
}
