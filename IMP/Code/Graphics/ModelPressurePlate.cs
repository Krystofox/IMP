using System.Numerics;
using Game.Graphics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Game.Graphics.GraphicsState;
using static Game.Graphics.Shaders;


namespace Game.Graphics;
class ModelPressurePlate : IDrawableObject
{
    public Model model;
    public Matrix4x4 transform;
    public Vector3 Position = new Vector3(0,0,0);
    public Vector3 Rotation = new Vector3(0,0,0);
    public ModelPressurePlate()
    {

    }
    unsafe public void Initialize()
    {
        model = LoadModel("assets/Models/PressurePlate/pressureplate_model.m3d");
        Texture2D texture = LoadTexture("assets/Models/PressurePlate/pressureplate_diffuse.png");
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
        Matrix4x4 rotation = Matrix4x4.CreateFromYawPitchRoll(-Rotation.Y, -Rotation.X, Rotation.Z);
        translation = Matrix4x4.Multiply(translation,rotation);
        DrawMesh(model.Meshes[0],model.Materials[0],translation);
    }

    public void Dispose()
    {
        UnloadModel(model);
    }
}

