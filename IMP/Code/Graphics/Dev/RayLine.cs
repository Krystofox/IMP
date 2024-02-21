using System.Numerics;
using Game.Graphics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Game.Graphics.GraphicsState;
using static Game.Graphics.Shaders;

namespace Game.Graphics;
class RayLine : IDrawableObject
{
    int startX;
    int startY;
    int endX;
    int endY;
    Color color;
    
    public RayLine(int startX,int startY,int endX,int endY,Color color)
    {
        this.startX = startX;
        this.startY = startY;
        this.endX = endX;
        this.endY = endY;
        this.color = color;
    }
    public void Initialize()
    {
        
    }
    public void Draw()
    {
        GetStateL().uiObjects.Add(this);
    }
    unsafe public void OnDraw()
    {
        DrawLine(startX,startY,endX,endY,color);
    }

    public void Dispose()
    {

    }
}

