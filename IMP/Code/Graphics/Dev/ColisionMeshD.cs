using System.Numerics;
using Game.Graphics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Game.Graphics.GraphicsState;
using static Game.Graphics.Shaders;
using static Game.GameResources;

namespace Game.Graphics;
class ColisionMeshD : IDrawableObject
{
    Vector3 position;
    Vector3 size;
    Quaternion orientation;
    Color color;
    
    static Model model;
    static bool loaded = false;
    
    public ColisionMeshD(Vector3 position,Vector3 size,Quaternion orientation,Color color)
    {
        this.position = position;
        this.size = size;
        this.color = color;
        this.orientation = orientation;
        GetGResources().lazyObjects.Add(this);
    }
    public void Initialize()
    {
        if(!loaded)
        {
            model = LoadModelFromMesh(GenMeshCube(1,1,1));
            loaded = true;
        }
    }
    public void Draw()
    {
        GetStateL().dynamicObjects.Add(this);
    }
    unsafe public void OnDraw()
    {
        Rlgl.EnableWireMode();
        Matrix4x4 translation = Raymath.MatrixTranslate(position.X,position.Y,position.Z);
        Matrix4x4 scale = Raymath.MatrixScale(size.X,size.Y,size.Z);
        Matrix4x4 rotation = Matrix4x4.CreateFromQuaternion(orientation);
        translation = Matrix4x4.Multiply(translation,rotation);
        translation = Matrix4x4.Multiply(translation,scale);
        model.Materials[0].Maps[(int)MaterialMapIndex.Diffuse].Color = color;
        DrawMesh(model.Meshes[0],model.Materials[0],translation);

        Rlgl.DisableWireMode();
    }

    public void Dispose()
    {

    }
}

