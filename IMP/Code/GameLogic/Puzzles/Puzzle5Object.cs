using System.Numerics;
using BepuUtilities;
using Game.Graphics;
using Raylib_cs;
using static Game.GameLogicThread;
using static Game.GameResources;

namespace Game.GameLogic;

class Puzzle5Object : IUpdatableObject
{
    public uint Id { get; private set; }
    public string Name => "Puzzle5";
    PressurePlateObject[,] plates = new PressurePlateObject[4, 4];
    bool[,] solution = {
        {false,false,false,false},
        {false,true,false,false},
        {false,false,false,false},
        {false,false,false,false},
    };
    GateObject gate1 = new GateObject(new Vector3(64f, -81f, 0f), new Vector3(0, 0, 0));
    public Puzzle5Object()
    {
        Id = GetNewID();
        GetGameLogicThread().updatables.Add(gate1);
        GetGameLogicThread().updatables.Add(new SmallRockObject(new Vector3(-21f,-35f,0.5f)));
        GetGameLogicThread().updatables.Add(new SmallRockObject(new Vector3(35f,-31f,0.5f)));
        GetGameLogicThread().updatables.Add(new SmallRockObject(new Vector3(-18f,-67f,0.5f)));
        GetGameLogicThread().updatables.Add(new SmallRockObject(new Vector3(8f,-77f,0.5f)));
        GetGameLogicThread().updatables.Add(new SmallRockObject(new Vector3(-8f,-86f,0.5f)));
        for (int y = 0; y < 4; y++)
            for (int x = 0; x < 4; x++)
            {
                PressurePlateObject plate = new PressurePlateObject(new Vector3(58 + x*2, -65 + y*2, 0), new Vector3(0, 0, 0));
                plates[x,y] = plate;
                GetGameLogicThread().updatables.Add(plate);
            }
    }

    public void Update()
    {
        bool solved = true;
        for (int y = 0; y < 4; y++)
            for (int x = 0; x < 4; x++)
            {
                if(plates[x,y].IsPushed != solution[x,y])
                    solved = false;
            }
        gate1.IsOpen = solved;
        
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
