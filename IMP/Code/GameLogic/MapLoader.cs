using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;
using BepuUtilities;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Game.GameLogicThread;
using Game.PhysicsMain;
using static Game.PhysicsMain.Physics;
using Game.Graphics;
using static Game.GameResources;
using System.Globalization;
using Game.GameLogic;

namespace Game.GameLogic;

class MapLoader
{
    public static void LoadMap(string map_path)
    {
        MapObject mapO = new MapObject();
        string[] lines = File.ReadAllLines("assets/Maps/" + map_path + "/map.dump");
        int offset = 0;
        int chunkEnd = offset + Convert.ToInt32(lines[offset]);
        for (offset++; offset <= chunkEnd; offset++)
        {
            Console.WriteLine(lines[offset]);
            string[] p = lines[offset].Split(';');
            int o = 0;
            StaticModel s = new StaticModel(p[o++],GetVector(p[o++],p[o++],p[o++]),GetVector(p[o++],p[o++],p[o++]),GetVector(p[o++],p[o++],p[o++]));
            GetGResources().lazyObjects.Add(s);
            mapO.staticDraws.Add(s);
        }

        chunkEnd = offset + Convert.ToInt32(lines[offset]);
        for (offset++; offset <= chunkEnd; offset++)
        {
            Console.WriteLine(lines[offset]);
            string[] p = lines[offset].Split(';');
            int o = 0;
            FoliageDrawable s = new FoliageDrawable(p[o++],GetVector(p[o++],p[o++],p[o++]),GetVector(p[o++],p[o++],p[o++]),GetVector(p[o++],p[o++],p[o++]));
            //StaticModel s = new StaticModel(p[o++],GetVector(p[o++],p[o++],p[o++]),GetVector(p[o++],p[o++],p[o++]),GetVector(p[o++],p[o++],p[o++]));
            GetGResources().lazyObjects.Add(s);
            mapO.staticDraws.Add(s);
        }

        chunkEnd = offset + Convert.ToInt32(lines[offset]);
        for (offset++; offset <= chunkEnd; offset++)
        {
            Console.WriteLine(lines[offset]);
            string[] p = lines[offset].Split(';');
            int o = 0;
            Vector3 position = GetVector(p[o++],p[o++],p[o++]);
            Vector3 rotation = GetVector(p[o++],p[o++],p[o++]);
            Vector3 scale = GetVector(p[o++],p[o++],p[o++]);
            Physics phys = GetPhysics();
            RigidPose pose = new RigidPose
            {
                Position = position,
                Orientation = Quaternion.CreateFromYawPitchRoll(rotation.Y, rotation.X, rotation.Z)
            };
            phys.simulation.Statics.Add(new StaticDescription(pose, phys.simulation.Shapes.Add(new Box(scale.X,scale.Y,scale.Z))));
        }


        GetGameLogicThread().updatables.Add(mapO);
        GetGameLogicThread().updatables.Add(new Player());
        GetGameLogicThread().updatables.Add(new SmallRockObject(new Vector3(10,-2.5f,0.5f)));
        GetGameLogicThread().updatables.Add(new Puzzle1Object());
        DetectionContact detectionObject = new DetectionContact();
        
    }

    static Vector3 GetVector(string a, string b, string c)
    {
        return new Vector3(float.Parse(a, CultureInfo.InvariantCulture), float.Parse(b, CultureInfo.InvariantCulture), float.Parse(c, CultureInfo.InvariantCulture));
    }
}
