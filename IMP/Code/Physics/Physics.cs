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
    public static Physics physics;
    public void Initialize()
    {
        physics = this;
        bufferPool = new BufferPool();
        simulation = Simulation.Create(bufferPool, new NarrowPhaseCallbacks(), new PoseIntegratorCallbacks(new Vector3(0, -150, 0)), new SolveDescription(8, 1));
        threadDispatcher = new ThreadDispatcher(Environment.ProcessorCount);
    }

    public void SimulationStep()
    {
        simulation.Timestep(0.01f, threadDispatcher);
    }

    public void Dispose()
    {
        simulation.Dispose();
        threadDispatcher.Dispose();
        bufferPool.Clear();
    }
}

