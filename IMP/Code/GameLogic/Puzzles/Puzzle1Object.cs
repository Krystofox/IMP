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
    List<uint> updetables = new List<uint>();
    public Puzzle1Object()
    {
        Id = GetNewID();

        GetGameLogicThread().updatables.Add(new SmallRockObject(new Vector3(10,-2.5f,0.5f)));
        updetables.Add(GameLogicThread.LastID);
    }

    public void Update()
    {

        //Console.WriteLine(playerController.PlayerPosition);
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
