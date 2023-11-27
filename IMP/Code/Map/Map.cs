using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Reflection.Metadata;
using Game.Resources;
using static Game.GameMap.Types;
namespace Game.GameMap;

class Map
{
    ResourceData mapResources;
    List<Prop> props = new List<Prop>();
    public Map(string mapLoc)
    {
        mapResources = ResourceLoader.LoadResources($"{mapLoc}/map.rf");
        Dictionary<Type, IList> mapData = MapLoader.LoadMapData($"{mapLoc}/map.data");
        props = (List<Prop>)mapData[typeof(Prop)];
    }

}