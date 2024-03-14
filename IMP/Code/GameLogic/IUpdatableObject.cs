namespace Game.GameLogic;

interface IUpdatableObject : IDisposable
{
    string Name { get; }
    void Update();

}
