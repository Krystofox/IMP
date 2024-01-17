using System.Numerics;
using BepuUtilities;
using Game.Graphics;
using Raylib_cs;
using static Game.GameResources;

namespace Game.GameLogic;

class Player
{
    PlayerController playerController = new PlayerController();
    ModelPlayer playerModel = new ModelPlayer();
    public Player()
    {
        GetGResources().lazyObjects.Add(playerModel);
    }

    public void Update()
    {
        playerController.Update();
        playerModel.Position = playerController.PlayerPosition;
        playerModel.Draw();
        //Console.WriteLine(playerController.PlayerPosition);
    }
}
