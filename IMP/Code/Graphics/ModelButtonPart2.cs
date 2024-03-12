using System.Numerics;
using Game.Graphics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Game.Graphics.GraphicsState;
using static Game.Graphics.Shaders;


namespace Game.Graphics;
class ModelButtonPart2 : IDrawableObject
{
    public Model model;
    public Matrix4x4 transform;
    public Vector3 Position = new Vector3(0,0,0);
    public Quaternion Orientation = Quaternion.Identity;
    
    public ModelButtonPart2()
    {

    }
    unsafe public void Initialize()
    {
        model = LoadModel("assets/Models/ButtonPart2/buttonpart2_model.m3d");
        Texture2D texture = LoadTexture("assets/Models/ButtonPart2/buttonpart2_diffuse.png");
        SetMaterialTexture(ref model,0,MaterialMapIndex.Diffuse,ref texture);
        model.Materials[0].Shader = GetShaders().lighting;
        transform = model.Transform;
    }
    public void Draw()
    {
        GetStateL().dynamicObjects.Add(this);
    }
    unsafe public void OnDraw()
    {
        Matrix4x4 translation = Raymath.MatrixTranslate(Position.X,Position.Y,Position.Z);
        Matrix4x4 rotation = Matrix4x4.CreateFromQuaternion(Orientation);
        translation = Matrix4x4.Multiply(translation,rotation);
        DrawMesh(model.Meshes[0],model.Materials[0],translation);
    }

    public void Dispose()
    {
        UnloadModel(model);
    }
}

