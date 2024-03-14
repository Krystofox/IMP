using System.Numerics;
using static Game.GameLogicThread;


namespace Game.GameLogic;

class Puzzle5Object : IUpdatableObject
{
    public string Name => "Puzzle5";
    PressurePlateObject[,] plates = new PressurePlateObject[4, 4];
    bool[,] solution = {
        {false,false,false,true},
        {false,true,true,false},
        {false,false,false,false},
        {true,false,true,false},
    };
    GateObject gate1 = new GateObject(new Vector3(64f, -81f, 0f));
    public Puzzle5Object()
    {
        GetGameLogicThread().updatables.Add(gate1);
        GetGameLogicThread().updatables.Add(new SmallRockObject(new Vector3(-21f,-35f,0.5f)));
        GetGameLogicThread().updatables.Add(new SmallRockObject(new Vector3(35f,-31f,0.5f)));
        GetGameLogicThread().updatables.Add(new SmallRockObject(new Vector3(-18f,-67f,0.5f)));
        GetGameLogicThread().updatables.Add(new SmallRockObject(new Vector3(8f,-77f,0.5f)));
        GetGameLogicThread().updatables.Add(new SmallRockObject(new Vector3(-8f,-86f,0.5f)));
        for (int y = 0; y < 4; y++)
            for (int x = 0; x < 4; x++)
            {
                PressurePlateObject plate = new PressurePlateObject(new Vector3(58 + x*2, -65 - y*2, 0));
                plates[y,x] = plate;
                GetGameLogicThread().updatables.Add(plate);
            }
    }

    public void Update()
    {
        bool solved = true;
        for (int y = 0; y < 4; y++)
            for (int x = 0; x < 4; x++)
            {
                if(plates[y,x].IsPushed != solution[y,x])
                    solved = false;
            }
        gate1.IsOpen = solved;
    }

    public void Dispose()
    {
        
    }
}
