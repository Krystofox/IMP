using Game.Graphics;
using static Game.GameResources;

namespace Game.GameLogic;

class Player : IUpdatableObject
{
    public string Name => "Player";
    public PlayerController playerController = new PlayerController();
    ModelPlayer playerModel = new ModelPlayer();
    public Player()
    {
        GetGResources().lazyObjects.Add(playerModel);
    }

    public void Update()
    {
        playerController.Update();
        playerModel.Position = playerController.PlayerPosition;
        playerModel.Rotation = playerController.PlayerRotation;
        playerModel.Draw();
    }

    public void Dispose()
    {
        playerModel.Dispose();
    }
}
