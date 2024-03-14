using System.Numerics;
using Game.Graphics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Game.Graphics.GraphicsState;
using static Game.Graphics.Shaders;


namespace Game.Graphics;
class Ending : IDrawableObject
{
    Texture2D logo;
    RenderTexture2D renderTexture;
    public int Alpha = 0;
    public Ending()
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
        DrawRectangle(0,0,GetScreenWidth(),GetScreenHeight(),new Color(0,0,0,Alpha));
        BeginTextureMode(renderTexture);
        int fontSize = 50;
        //ADD CZECH SYMBOLS
        string text = "Děkuji za zahraní";
        Vector2 textSize = MeasureTextEx(GetFontDefault(), text, fontSize, 2);
        Vector2 center = new Vector2(GetScreenWidth() / 2, GetScreenHeight()/2);
        DrawTextEx(GetFontDefault(), text, new Vector2(center.X - textSize.X/2, center.Y - textSize.Y/2-50), fontSize, 2, Color.White);
        EndTextureMode();

        DrawTextureRec(renderTexture.Texture,new Rectangle(0,0,renderTexture.Texture.Width,-renderTexture.Texture.Height),new Vector2(0,0),new Color(255,255,255,Alpha));
    }

    public void Dispose()
    {

    }
}

