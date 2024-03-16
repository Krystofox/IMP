using System.Numerics;
using Game.Graphics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Game.Graphics.GraphicsState;
using static Game.Graphics.Shaders;


namespace Game.Graphics;
class Intro : IDrawableObject
{
    Texture2D logo;
    RenderTexture2D renderTexture;
    public int Alpha = 0;
    public int AlphaBG = 255;
    public Intro()
    {

    }
    unsafe public void Initialize()
    {
        logo = LoadTexture("assets/Images/logo.png");
        renderTexture = LoadRenderTexture(GetScreenWidth(),GetScreenHeight());
    }
    public void Draw()
    {
        GetStateL().uiObjects.Add(this);
    }
    unsafe public void OnDraw()
    {
        DrawRectangle(0,0,GetScreenWidth(),GetScreenHeight(),new Color(0,0,0,AlphaBG));
        BeginTextureMode(renderTexture);
        int fontSize = 80;
        Vector2 textSize = MeasureTextEx(RenderCore.PixelFont, "IMP", fontSize, 2);
        Vector2 center = new Vector2(GetScreenWidth() / 2, GetScreenHeight()/2);
        DrawTextEx(RenderCore.PixelFont, "IMP", new Vector2(center.X - textSize.X/2, center.Y - textSize.Y/2-50), fontSize, 2, Color.White);
        int scale = 5;
        DrawTextureEx(logo, new Vector2(center.X - logo.Width*scale/2, center.Y - logo.Height*scale/2 + 100),0,scale, Color.White);
        EndTextureMode();

        DrawTextureRec(renderTexture.Texture,new Rectangle(0,0,renderTexture.Texture.Width,-renderTexture.Texture.Height),new Vector2(0,0),new Color(255,255,255,Alpha));
    }

    public void Dispose()
    {

    }
}

