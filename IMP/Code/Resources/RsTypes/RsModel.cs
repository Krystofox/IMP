using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Game.Resources.RsTypes;
class RsModel : RsType
{
    public Model model;
    unsafe public void Load(string[] args)
    {
        string modelFpath = "assets/Models/" + args[1] + "/";
        model = LoadModel(modelFpath + args[1] + ".m3d");
        if(File.Exists(modelFpath + args[1] + "_normal.png")){
            Image image = LoadImage(modelFpath + args[1] + "_normal.png");
            model.Materials[0].Maps[(int)MaterialMapIndex.MATERIAL_MAP_DIFFUSE].Texture = LoadTextureFromImage(image);
            UnloadImage(image);
        }
            
    }
}


