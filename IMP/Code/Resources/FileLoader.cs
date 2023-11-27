using System.Collections;

namespace Game.Resources;
class FileLoader
{
    public static Type MatchType(string str,Type typeClass)
    {
        Type[] structs = typeClass.GetNestedTypes();
        foreach (var type in structs)
        {
            if (type.Name == str)
                return type;
        }
        throw new Exception("Type not found");
    }


    public static int ReadInt(ref byte[] arr, ref int offset)
    {

        int Int = BitConverter.ToInt32(arr, offset);
        offset += 4;
        return Int;
    }
    public static IList createList(Type myType)
    {
        Type genericListType = typeof(List<>).MakeGenericType(myType);
        return (IList)Activator.CreateInstance(genericListType);
    }
}

