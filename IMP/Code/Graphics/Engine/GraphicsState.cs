using System.Numerics;
using BepuPhysics;
using Game.Graphics;
using Raylib_cs;

namespace Game.Graphics;
class GraphicsState
{
    static GraphicsState[] states;
    static int logicState = 0;
    static int renderState = 1;
    public static void Setup()
    {
        states =
        [
            new GraphicsState(),
            new GraphicsState()
        ];
    }
    public static GraphicsState GetStateL()
    {
        return states[logicState];
    }
    public static GraphicsState GetStateR()
    {
        return states[renderState];
    }
    public static void SwitchStates()
    {
        (logicState,renderState) = (renderState,logicState);
    }

    public List<IDrawableObject> staticObjects = new List<IDrawableObject>();
    public List<IDrawableObject> dynamicObjects = new List<IDrawableObject>();
    public List<IDrawableObject> uiObjects = new List<IDrawableObject>();
    public Camera3D camera3D = new Camera3D()
    {
        //Position = new Vector3(5,-10,50),
        Position = new Vector3(5,-5,5),
        Target = new Vector3(0,0,0),
        Up = new Vector3(0,0,1),
        FovY = 45,
        Projection = CameraProjection.Perspective
    };

}

