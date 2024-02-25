using BepuPhysics;

namespace Game.GameLogic;

interface IContactDetection : IDisposable
{
    uint Id { get; }
    void Contact(BodyHandle contactObject);
    public static List<IContactDetection> contactDetections = new List<IContactDetection>();

}
