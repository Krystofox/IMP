using Game.Graphics;
using static Game.GameResources;
using static Game.GameLogicThread;
using System.Numerics;

namespace Game.GameLogic;

class EndingObject : IUpdatableObject
{
    public string Name => "Ending";
    Ending ending = new Ending();
    DetectionContact moveTrigger;
    Player player;
    public EndingObject(Vector3 position,Vector3 scale)
    {
        player = (Player)GetGameLogicThread().GetUpdatableByName("Player");
        GetGResources().lazyObjects.Add(ending);
        moveTrigger = new DetectionContact(position, new Vector3(0, 0, 0), scale, false);
    }
    public void Update()
    {
        if (moveTrigger.ContactDetected())
        {
            player.playerController.PlayerLock = true;
            if (ending.Alpha < 255)
                ending.Alpha++;
            ending.Draw();
        }
    }

    public void Dispose()
    {

    }
}
