using System.Numerics;
using Game.Graphics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Game.Graphics.GraphicsState;
using static Game.Graphics.Shaders;

namespace Game.Graphics;
class RayPlane : IDrawableObject
{
    Model model;
    public Vector3 Position = new Vector3(0,0,0);
    float width;
    float height;
    int resX;
    int resY;
    
    public RayPlane(float width,float height,int resX,int resY)
    {
        this.width = width;
        this.height = height;
        this.resX = resX;
        this.resY = resY;
    }
    unsafe public void Initialize()
    {
        model = LoadModelFromMesh(GenMeshPlane(width,height,resX,resY));
        model.Materials[0].Shader = GetShaders().lighting;
    }
    public void Draw()
    {
        GetStateL().dynamicObjects.Add(this);
    }
    unsafe public void OnDraw()
    {
        DrawModelEx(model,Vector3.Zero,new Vector3(1,0,0),90,Vector3.One,Color.RED);
    }

    public void Dispose()
    {

    }
}

