namespace Game.Graphics;

interface IDrawableObject : IDisposable
{
    void Initialize();
    void OnDraw();

}