using System.Reflection;
using System.Runtime.CompilerServices;
using Game.Resources.RsTypes;

namespace Game.Resources;
class ResourceLoader
{
    // REWRITE
    public static ResourceData LoadResources(string resourceFile)
    {
        ResourceData rData = new ResourceData();
        using (StreamReader read = new StreamReader(resourceFile))
        {
            string line;
            while ((line = read.ReadLine()) != null)
            {
                if(line == "")
                    break;

                Console.WriteLine(line);
                string[] args = line.Split(";");

                switch (args[0])
                {
                    case "RsModel":
                        AddLoad(rData.rsModels,new RsModel(),args);
                        break;
                    default:
                        Console.WriteLine("Resource type not found");
                        break;
                }
                
            }
        }
        return rData;
    }

    private static void AddLoad<T>(List<T> l,T rs,string[] args) where T : RsType
    {
        rs.Load(args);
        l.Add(rs);
    }
}

