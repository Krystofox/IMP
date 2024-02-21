using System.Numerics;
using Game.Graphics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Game.Graphics.GraphicsState;
using static Game.Graphics.Shaders;

namespace Game.Graphics;
class RayCube : IDrawableObject
{
    Vector3 position;
    float width;
    float height;
    float length;
    Color color;
    
    public RayCube(Vector3 position,float width,float height, float length,Color color)
    {
        this.position = position;
        this.width = width;
        this.height = height;
        this.length = length;
        this.color = color;
    }
    public void Initialize()
    {
        
    }
    public void Draw()
    {
        GetStateL().dynamicObjects.Add(this);
    }
    unsafe public void OnDraw()
    {
        DrawCube(position,width,height,length,color);
    }

    public void Dispose()
    {

    }
}

