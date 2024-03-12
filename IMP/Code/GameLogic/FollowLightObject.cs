using System.Numerics;
using BepuUtilities;
using Game.Graphics;
using Raylib_cs;
using static Game.GameLogicThread;
using static Game.GameResources;

namespace Game.GameLogic;

class FollowLight : IUpdatableObject
{
    public uint Id { get; private set; }
    public string Name => "FollowLight";
    Light light;
    Player player;
    public FollowLight()
    {
        Id = GetNewID();
        player = (Player)GetGameLogicThread().GetUpdatableByName("Player");
        light = new Light(
            0,
            LightType.Point,
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
        throw new NotImplementedException();
    }
}
