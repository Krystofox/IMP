using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;
using static Game.GameLogicThread;
using Game.PhysicsMain;
using static Game.PhysicsMain.Physics;
using Game.Graphics;
using static Game.GameResources;
using System.Globalization;
using static Game.Graphics.GraphicsState;

namespace Game.GameLogic;

class MapLoader
{
    static bool changeLevel = false;
    static string changedLevel = "";
    public static void ChangeLevelLazy(string level)
    {
        changeLevel = true;
        changedLevel = level;
    }
    public static void ChangeLevelCheck()
    {
        if(!changeLevel)
            return;
        changeLevel = false;

        /*foreach (var u in GetGameLogicThread().updatables)
            u.Dispose();*/
        GetGameLogicThread().updatables.Clear();
        ClearStaticObjects();
        GetPhysics().Dispose();
        GetPhysics().Initialize();

        LoadMap(changedLevel);
    }
    public static void LoadMap(string map_path)
    {
        string[] lines = File.ReadAllLines("assets/Maps/" + map_path + "/map.dump");
        int offset = 0;
        int chunkEnd = offset + Convert.ToInt32(lines[offset]);
        for (offset++; offset <= chunkEnd; offset++)
        {
            string[] p = lines[offset].Split(';');
            int o = 0;
            StaticModel s = new StaticModel(p[o++], GetVector(p[o++], p[o++], p[o++]), GetVector(p[o++], p[o++], p[o++]), GetVector(p[o++], p[o++], p[o++]));
            AddStaticObject(s);
        }

        chunkEnd = offset + Convert.ToInt32(lines[offset]);
        for (offset++; offset <= chunkEnd; offset++)
        {
            string[] p = lines[offset].Split(';');
            int o = 0;
            FoliageDrawable s = new FoliageDrawable(p[o++], GetVector(p[o++], p[o++], p[o++]), GetVector(p[o++], p[o++], p[o++]), GetVector(p[o++], p[o++], p[o++]));
            GetGResources().lazyObjects.Add(s);
            AddStaticObject(s);
        }

        chunkEnd = offset + Convert.ToInt32(lines[offset]);
        for (offset++; offset <= chunkEnd; offset++)
        {
            string[] p = lines[offset].Split(';');
            int o = 0;
            Vector3 position = GetVector(p[o++], p[o++], p[o++]);
            Vector3 rotation = GetVector(p[o++], p[o++], p[o++]);
            Vector3 scale = GetVector(p[o++], p[o++], p[o++]);
            Physics phys = GetPhysics();
            RigidPose pose = new RigidPose
            {
                Position = position,
                Orientation = Quaternion.CreateFromYawPitchRoll(rotation.Y, rotation.X, rotation.Z)
            };
            StaticHandle handle = phys.simulation.Statics.Add(new StaticDescription(pose, phys.simulation.Shapes.Add(new Box(scale.X, scale.Y, scale.Z))));
            ref var sProperties = ref phys.bodyProperties.Allocate(handle);
            sProperties = new ColisionObjectProperties
            {
                AllowCollision = true,
                DetectionObject = false,
                DetectedContact = false,
                DetectedAction = false
            };
        }

        switch (map_path)
        {
            case "dev_blend":
                Player player = new Player();
                //player.playerController.SetPlayerPosition(new Vector3(0, 0, 2));
                player.playerController.SetPlayerPosition(new Vector3(21f, -51f, 0f));
                GetGameLogicThread().updatables.Add(player);
                GetGameLogicThread().updatables.Add(new SmallRockObject(new Vector3(10, -2.5f, 0.5f)));
                GetGameLogicThread().updatables.Add(new Puzzle1Object());
                GetGameLogicThread().updatables.Add(new Puzzle2Object());
                GetGameLogicThread().updatables.Add(new Puzzle3Object());
                GetGameLogicThread().updatables.Add(new FollowLight());
                GetGameLogicThread().updatables.Add(new ChangeLevelObject(new Vector3(20, -45, 0), new Vector3(2, 2, 2), "dev_blend2"));
                GetGameLogicThread().updatables.Add(new IntroObject());

                break;
            case "dev_blend2":
                Player player2 = new Player();
                player2.playerController.SetPlayerPosition(new Vector3(0, 0, 2));
                GetGameLogicThread().updatables.Add(player2);
                GetGameLogicThread().updatables.Add(new Puzzle4Object());
                GetGameLogicThread().updatables.Add(new Puzzle5Object());
                GetGameLogicThread().updatables.Add(new NormalLight(new Vector3(0,0,1)));
                GetGameLogicThread().updatables.Add(new EndingObject(new Vector3(61,-84,0),new Vector3(6,4,10)));
                break;
        }
    }

    static Vector3 GetVector(string a, string b, string c)
    {
        return new Vector3(float.Parse(a, CultureInfo.InvariantCulture), float.Parse(b, CultureInfo.InvariantCulture), float.Parse(c, CultureInfo.InvariantCulture));
    }
}
