namespace Game.GameLogic;

interface IUpdatableObject : IDisposable
{
    uint Id { get; }
    string Name { get; }
    void Update();

}
