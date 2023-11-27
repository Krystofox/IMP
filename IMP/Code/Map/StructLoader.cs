using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Game.GameMap;

class StructLoader
{
    public static object CastToStruct(byte[] data, Type type)
    {
        FieldInfo[] fInfo = type.GetFields();
        int offset = 0;
        var structObj = Activator.CreateInstance(type);
        for (int i = 0; i < fInfo.Length; i++)
        {
            Type fType = fInfo[i].FieldType;
            if (fType == typeof(string))
            {
                string text = BytesToStr(ref data, ref offset);
                fInfo[i].SetValue(structObj, text);
                continue;
            }
            if (fType.IsArray)
            {
                Array array = BytesToArray(ref data, ref offset, fType.GetElementType());
                fInfo[i].SetValue(structObj, array);
                continue;
            }
            byte[] vData = new byte[Marshal.SizeOf(fType)];
            Array.Copy(data, offset, vData, 0, vData.Length);
            offset += vData.Length;
            var pData = GCHandle.Alloc(vData, GCHandleType.Pinned);
            var result = Marshal.PtrToStructure(pData.AddrOfPinnedObject(), fType);
            pData.Free();
            fInfo[i].SetValue(structObj, result);
        }
        return structObj;
    }
    public static string BytesToStr(ref byte[] bytes, ref int offset)
    {
        int len = BitConverter.ToInt32(bytes, offset);
        offset += 4;
        byte[] sub = new byte[len];
        Array.Copy(bytes, offset, sub, 0, len);
        string text = Encoding.Unicode.GetString(sub);
        offset += len;
        return text;
    }

    public static Array BytesToArray(ref byte[] bytes, ref int offset, Type type)
    {
        int len = BitConverter.ToInt32(bytes, offset);
        offset += 4;
        Array array = Array.CreateInstance(type, len);
        for (int i = 0; i < len; i++)
        {
            byte[] vData = new byte[Marshal.SizeOf(type)];
            Array.Copy(bytes, offset, vData, 0, vData.Length);
            offset += vData.Length;
            var pData = GCHandle.Alloc(vData, GCHandleType.Pinned);
            var result = Marshal.PtrToStructure(pData.AddrOfPinnedObject(), type);
            array.SetValue(result, i);
            pData.Free();
        }
        offset += len;
        return array;
    }

    public static byte[] CastToArray(object data, Type type)
    {
        FieldInfo[] fInfo = type.GetFields();
        List<byte> bytes = new List<byte>();
        for (int i = 0; i < fInfo.Length; i++)
        {
            var obj = fInfo[i].GetValue(data);
            byte[] arr = ObjToBytes(obj);
            bytes.AddRange(arr);
        }
        return bytes.ToArray();
    }

    public static byte[] ObjToBytes(object obj)
    {
        if (obj.GetType() == typeof(string))
            return StrToBytes((string)obj);
        if (obj.GetType().IsArray)
            return ArrToBytes((Array)obj);
        int size = Marshal.SizeOf(obj);
        byte[] result = new byte[size];
        IntPtr ptr = Marshal.AllocHGlobal(size);
        Marshal.StructureToPtr(obj, ptr, true);
        Marshal.Copy(ptr, result, 0, size);
        Marshal.FreeHGlobal(ptr);
        return result;
    }
    public static byte[] StrToBytes(string str)
    {
        List<byte> result = new List<byte>();
        byte[] bStr = Encoding.Unicode.GetBytes(str);
        result.AddRange(BitConverter.GetBytes(bStr.Length));
        result.AddRange(bStr);
        return result.ToArray();
    }
    public static byte[] ArrToBytes(Array arr)
    {
        List<byte> result = new List<byte>();
        result.AddRange(BitConverter.GetBytes(arr.Length));
        foreach (var o in arr)
        {
            result.AddRange(ObjToBytes(o));
        }

        return result.ToArray();
    }
}
