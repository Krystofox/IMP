using System.Numerics;
using Game.Graphics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Game.Graphics.GraphicsState;
using static Game.Graphics.Shaders;

namespace Game.Graphics;
class RayLine3D : IDrawableObject
{
    Vector3 startPos;
    Vector3 endPos;
    Color color;
    
    public RayLine3D(Vector3 startPos,Vector3 endPos,Color color)
    {
        this.startPos = startPos;
        this.endPos = endPos;
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
        DrawLine3D(startPos,endPos,color);
    }

    public void Dispose()
    {

    }
}

