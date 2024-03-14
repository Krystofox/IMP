using System.Numerics;
using Game.Graphics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Game.Graphics.GraphicsState;
using static Game.Graphics.Shaders;


namespace Game.Graphics;
class StaticModel : IDrawableObject
{
    public Model model;
    public Matrix4x4 Transform;
    string Model;
    
    public StaticModel(string model,Vector3 position,Vector3 rotation,Vector3 scale)
    {
        Matrix4x4 translationM = Raymath.MatrixTranslate(position.X,position.Y,position.Z);
        Matrix4x4 rotationM = Matrix4x4.CreateFromYawPitchRoll(-rotation.Y, -rotation.X, rotation.Z);
        Matrix4x4 scaleM = Raymath.MatrixScale(scale.X,scale.Y,scale.Z);
        Transform = Raymath.MatrixMultiply(rotationM,translationM);
        Transform = Raymath.MatrixMultiply(scaleM,Transform);
        Model = model;
    }
    unsafe public void Initialize()
    {
        model = HelperFunctions.LoadModel(Model);
        model.Materials[0].Shader = GetShaders().lighting;
    }
    unsafe public void OnDraw()
    {
        DrawMesh(model.Meshes[0],model.Materials[0],Transform);
    }

    public void Dispose()
    {
        UnloadModel(model);
    }
}

