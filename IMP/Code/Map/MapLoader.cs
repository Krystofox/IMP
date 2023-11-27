using System.Collections;

namespace Game.GameMap;
class MapLoader
{
    public static Dictionary<Type, IList> LoadMapData(string mapFile)
    {
        int offset = 0;
        byte[] fileBytes = File.ReadAllBytes(mapFile);
        Dictionary<Type, IList> mapData = new Dictionary<Type, IList>();

        while (offset < fileBytes.Length - 1)
        {
            string typeStr = StructLoader.BytesToStr(ref fileBytes, ref offset);
            Type type = MatchType(typeStr);
            mapData.Add(type, CreateList(type));
            int typeEnd = offset + ReadInt(ref fileBytes, ref offset);
            while (offset < typeEnd)
            {
                int len = ReadInt(ref fileBytes, ref offset);
                byte[] bytes = new byte[len];
                Array.Copy(fileBytes, offset, bytes, 0, len);
                var obj = StructLoader.CastToStruct(bytes, type);
                mapData[type].Add(obj);
                offset += len;
            }
        }
        return mapData;
    }

    static Type MatchType(string str)
    {
        Type[] structs = typeof(Types).GetNestedTypes();
        foreach (var type in structs)
        {
            if (type.Name == str)
                return type;
        }
        throw new Exception("Type not found");
    }

    static int ReadInt(ref byte[] arr, ref int offset)
    {

        int Int = BitConverter.ToInt32(arr, offset);
        offset += 4;
        return Int;
    }
    static IList CreateList(Type myType)
    {
        Type genericListType = typeof(List<>).MakeGenericType(myType);
        return (IList)Activator.CreateInstance(genericListType);
    }
}