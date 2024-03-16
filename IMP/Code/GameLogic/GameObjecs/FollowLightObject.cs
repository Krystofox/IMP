using System.Numerics;
using Game.Graphics;
using Raylib_cs;
using static Game.GameLogicThread;

namespace Game.GameLogic;

class FollowLight : IUpdatableObject
{
    public string Name => "FollowLight";
    Light light;
    Player player;
    public FollowLight()
    {
        player = (Player)GetGameLogicThread().GetUpdatableByName("Player");
        light = new Light(
            0,
            new Vector3(0, 0, 0),
            Vector3.Zero,
            Color.White
        );
    }

    public void Update()
    {
        light.Position = player.playerController.PlayerPosition + new Vector3(0, 0, 10);
        light.Update();
    }

    public void Dispose()
    {
        light.Dispose();
    }
}
