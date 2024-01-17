using System.Numerics;
using Game.Graphics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Game.Graphics.GraphicsState;
using static Raylib_cs.Rlgl;

namespace Game.Graphics;
class RayGrid : IDrawableObject
{
    int slices;
    int spacing;

    public RayGrid(int slices, int spacing)
    {
        this.slices = slices;
        this.spacing = spacing;
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
        //EDITED RAYLIB FUNCTION ( Draw on Z axis )
        int halfSlices = slices / 2;

        Begin(DrawMode.LINES);
        for (int i = -halfSlices; i <= halfSlices; i++)
        {
            if (i == 0)
            {
                Color3f(0.5f, 0.5f, 0.5f);
                Color3f(0.5f, 0.5f, 0.5f);
                Color3f(0.5f, 0.5f, 0.5f);
                Color3f(0.5f, 0.5f, 0.5f);
            }
            else
            {
                Color3f(0.75f, 0.75f, 0.75f);
                Color3f(0.75f, 0.75f, 0.75f);
                Color3f(0.75f, 0.75f, 0.75f);
                Color3f(0.75f, 0.75f, 0.75f);
            }

            
            /*Vertex3f((float)i * spacing, 0.0f, (float)-halfSlices * spacing);
            Vertex3f((float)i * spacing, 0.0f, (float)halfSlices * spacing);

            Vertex3f((float)-halfSlices * spacing, 0.0f, (float)i * spacing);
            Vertex3f((float)halfSlices * spacing, 0.0f, (float)i * spacing);*/

            Vertex3f((float)i * spacing, (float)-halfSlices * spacing, 0);
            Vertex3f((float)i * spacing, (float)halfSlices * spacing, 0);

            Vertex3f((float)-halfSlices * spacing, (float)i * spacing, 0);
            Vertex3f((float)halfSlices * spacing, (float)i * spacing, 0);
        }
        End();
    }

    public void Dispose()
    {

    }
}

