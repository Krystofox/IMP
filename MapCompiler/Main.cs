using System.Collections;
using System.Globalization;
using System.Numerics;
using System.Runtime.InteropServices;
using static MapCompiler.Types;
namespace MapCompiler
{
    class Main
    {
        public Main()
        {
            // Change UP vector to Z+
            if (Program.args.Length == 0)
            {
                Console.WriteLine("No dump param!");
                return;
            }
            string dumpPath = Path.GetFullPath(Program.args[0]);
            ParseDump(dumpPath);

            byte[] mapFile = CreateData(new object[] { blocks });
            string mapFilePath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(dumpPath), @"../map.data"));
            Console.WriteLine(mapFilePath);
            if (File.Exists(mapFilePath))
                File.Delete(mapFilePath);
            using var writer = new BinaryWriter(File.Create(mapFilePath));
            writer.Write(mapFile);
            Console.WriteLine("DONE");
        }
        byte[] CreateData(object[] objects)
        {
            List<byte> data = new List<byte>();
            for (int i = 0; i < objects.Length; i++)
            {
                
                List<byte> buffer = new List<byte>();
                IList list = (IList)objects[i];
                Type type = list.GetType().GetGenericArguments().Single();
                data.AddRange(StructLoader.StrToBytes(type.Name));
                for (int j = 0; j < list.Count; j++)
                {
                    byte[] arr = StructLoader.CastToArray(list[j], type);
                    byte[] lenght = BitConverter.GetBytes(arr.Length);
                    buffer.AddRange(lenght);
                    buffer.AddRange(arr);
                }
                data.AddRange(BitConverter.GetBytes(buffer.Count));
                data.AddRange(buffer);
            }
            return data.ToArray();
        }

        static List<Block> blocks = new List<Block>();

        void ParseDump(string dumpPath)
        {
            string[] textFromFile = File.ReadAllLines(dumpPath);
            foreach (string line in textFromFile)
            {
                string[] d = line.Split(";");
                switch (d[0])
                {
                    case ("BLOCK"):
                        int i = 1;
                        blocks.Add(new Block()
                        {
                            Texture = d[i++],
                            Position = ChangeWCords(GetVector(d[i++], d[i++], d[i++])),
                            Rotation = GetVector(d[i++], d[i++], d[i++]),
                            Size = ChangeWCords(GetSize(ref d,ref i,int.Parse(d[i++]))*10),
                            Uv = GetVector2Array(ref d,ref i,48)
                        });
                        break;
                }
            }

        }

        Vector3 ChangeWCords(Vector3 pos){
            return new Vector3(pos.Y,pos.Z,pos.X);
        }

        int AddToValue(int value, ref int to)
        {
            to += value;
            return value;
        }
        const int vecMultiplier = 1000;
        Vector3 GetVector(string a, string b, string c)
        {
            return new Vector3(float.Parse(a, CultureInfo.InvariantCulture), float.Parse(b, CultureInfo.InvariantCulture), float.Parse(c, CultureInfo.InvariantCulture)) * vecMultiplier;
        }
        Vector2[] GetVector2Array(ref string[] d, ref int i,int length){
            Vector2[] arr = new Vector2[length/2];
            for (int j = 0; j < arr.Length;j++)
            {
                arr[j] = new Vector2(float.Parse(d[i], CultureInfo.InvariantCulture), float.Parse(d[i+1], CultureInfo.InvariantCulture));
                i+=2;
            }
            return arr;
        }

        Vector3 GetSize(ref string[] d, ref int i,int length)
        {
            List<Vector3> vertices = new List<Vector3>();
            length = i+length*3;
            for (; i < length; i += 3)
            {
                vertices.Add(GetVector(d[i], d[i + 1], d[i + 2]));
            }
            Vector3 center = Vector3.Zero;
            for (int j = 0; j < vertices.Count; j++)
            {
                center += vertices[j];
            }
            center = center / vertices.Count;
            return Vector3.Abs(center - vertices[0]) / 6;
        }
    }

}