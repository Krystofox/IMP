using Game.Graphics;
using static Game.GameResources;
using static Game.GameLogicThread;

namespace Game.GameLogic;

class IntroObject : IUpdatableObject
{
    public string Name => "Intro";
    Intro intro = new Intro();
    public IntroObject()
    {
        GetGResources().lazyObjects.Add(intro);
    }
    int animationPart = 0;
    int delay = 0;
    public void Update()
    {
        switch (animationPart)
        {
            case 0:
                intro.Alpha++;
                if (intro.Alpha == 255)
                {
                    animationPart++;
                    delay = 100;
                }
                break;
            case 1:
                delay--;
                if (delay == 0)
                {
                    animationPart++;
                }
                break;
            case 2:
                intro.Alpha--;
                intro.AlphaBG--;
                if (intro.Alpha == 0)
                {
                    GetGameLogicThread().updatables.Remove(this);
                }
                break;
        }
        intro.Draw();
    }

    public void Dispose()
    {

    }
}
