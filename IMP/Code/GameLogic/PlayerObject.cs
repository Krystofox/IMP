using System.Numerics;
using BepuUtilities;
using Game.Graphics;
using Raylib_cs;
using static Game.GameLogicThread;
using static Game.GameResources;

namespace Game.GameLogic;

class Player : IUpdatableObject
{
    public uint Id { get; private set; }
    public string Name => "Player";
    public PlayerController playerController = new PlayerController();
    ModelPlayer playerModel = new ModelPlayer();
    public Player()
    {
        Id = GetNewID();
        GetGResources().lazyObjects.Add(playerModel);
    }

    public void Update()
    {
        playerController.Update();
        playerModel.Position = playerController.PlayerPosition;
        playerModel.Rotation = playerController.PlayerRotation;
        playerModel.Draw();
        //Console.WriteLine(playerController.PlayerPosition);
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
