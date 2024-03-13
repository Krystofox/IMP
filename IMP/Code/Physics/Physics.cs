using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;
using BepuPhysics.CollisionDetection;
using BepuPhysics.Constraints;
using BepuUtilities;
using BepuUtilities.Memory;
using System.Globalization;

namespace Game.PhysicsMain;

class Physics : IDisposable
{
    // Change UP vector to Z+
    public BufferPool bufferPool;
    public Simulation simulation;
    public ThreadDispatcher threadDispatcher;
    static Physics physInstance;
    public CollidableProperty<ColisionObjectProperties> bodyProperties;
    public void Initialize()
    {
        physInstance = this;
        bufferPool = new BufferPool();
        bodyProperties = new CollidableProperty<ColisionObjectProperties>();
        simulation = Simulation.Create(bufferPool, new NarrowPhaseCallbacks() { Properties = bodyProperties }, new PoseIntegratorCallbacks(new Vector3(0, 0, -10f)), new SolveDescription(8, 1));
        threadDispatcher = new ThreadDispatcher(Environment.ProcessorCount);
    }
    public static Physics GetPhysics()
    {
        return physInstance;
    }

    public void SimulationStep()
    {
        simulation.Timestep(0.01f, threadDispatcher);
        //simulation.IncrementallyOptimizeDataStructures(threadDispatcher);
    }

    public void Dispose()
    {
        simulation.Dispose();
        threadDispatcher.Dispose();
        bufferPool.Clear();
        bodyProperties.Dispose();
    }
}

