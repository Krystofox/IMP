using System.Numerics;
using Game.Graphics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Game.Graphics.GraphicsState;
using static Game.Graphics.Shaders;
using System.Linq.Expressions;


namespace Game.Graphics;
class StaticModel : IDrawableObject
{
    public Model model;
    public Matrix4x4 transform;
    public Vector3 Position = new Vector3(0,0,0);
    string Model;
    
    public StaticModel(string model,Vector3 position,Vector3 rotation,Vector3 scale)
    {
        Position = position;
        Model = model;
    }
    unsafe public void Initialize()
    {
        string modelLower = Model.ToLower();
        model = LoadModel($"assets/Models/{Model}/{modelLower}_model.m3d");
        Texture2D normal = LoadTexture($"assets/Models/{Model}/{modelLower}_normal.png");
        SetMaterialTexture(ref model,0,MaterialMapIndex.Normal,ref normal);
        model.Materials[0].Shader = GetShaders().lighting;
        transform = model.Transform;
    }
    unsafe public void OnDraw()
    {
        Matrix4x4 translation = Raymath.MatrixTranslate(Position.X,Position.Y,Position.Z);
        DrawMesh(model.Meshes[0],model.Materials[0],translation);
    }

    public void Dispose()
    {
        UnloadModel(model);
    }
}

