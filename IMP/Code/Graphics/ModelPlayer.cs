using System.Numerics;
using Game.Graphics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Game.Graphics.GraphicsState;
using static Game.Graphics.Shaders;


namespace Game.Graphics;
class ModelPlayer : IDrawableObject
{
    public Model model;
    public Matrix4x4 transform;
    public Vector3 Position = new Vector3(0,0,0);
    public Quaternion Rotation;
    
    public ModelPlayer()
    {

    }
    unsafe public void Initialize()
    {
        model = LoadModel("assets/Models/PlayerModel/playerModel.m3d");
        Texture2D texture = LoadTexture("assets/Models/PlayerModel/playerTexture.png");
        SetMaterialTexture(ref model,0,MaterialMapIndex.Diffuse,ref texture);
        model.Materials[0].Shader = GetShaders().lighting;
        transform = model.Transform;
    }
    public void Draw()
    {
        //GetStateL().dynamicObjects.Add((ModelPlayer)Clone());
        GetStateL().dynamicObjects.Add(this);
    }
    unsafe public void OnDraw()
    {
        Matrix4x4 translation = Raymath.MatrixTranslate(Position.X,Position.Y,Position.Z);
        Matrix4x4 rotation = Matrix4x4.CreateFromQuaternion(Rotation);
        
        DrawMesh(model.Meshes[0],model.Materials[0],Raymath.MatrixMultiply(rotation,translation));
    }

    public void Dispose()
    {
        UnloadModel(model);
    }
}

