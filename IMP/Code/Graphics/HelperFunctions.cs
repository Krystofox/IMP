using Raylib_cs;
using static Raylib_cs.Raylib;


namespace Game.Graphics;
public class HelperFunctions
{
    public static Model LoadModel(string modelName)
    {
        string modelLower = modelName.ToLower();
        Model model = Raylib.LoadModel($"assets/Models/{modelName}/{modelLower}_model.m3d");
        string path = $"assets/Models/{modelName}/{modelLower}_diffuse.png";
        if(File.Exists(path))
        {
            Texture2D diffuse = LoadTexture(path);
            SetMaterialTexture(ref model,0,MaterialMapIndex.Diffuse,ref diffuse);
        }

        path = $"assets/Models/{modelName}/{modelLower}_normal.png";
        if(File.Exists(path))
        {
            Texture2D normal = LoadTexture(path);
            SetMaterialTexture(ref model,0,MaterialMapIndex.Normal,ref normal);
        }
        return model;
    }
}