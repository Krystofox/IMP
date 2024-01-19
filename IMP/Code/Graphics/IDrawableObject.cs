namespace Game.Graphics;

interface IDrawableObject : IDisposable,ILazyLoad
{
    void OnDraw();

}