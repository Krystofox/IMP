using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;
using System.Runtime.CompilerServices;
using BepuPhysics.Trees;


namespace Game.PhysicsMain;

class RayCastStruct
{
    public struct RayHit
    {
        public Vector3 Normal;
        public float T;
        public CollidableReference Collidable;
        public bool Hit;
    }
    public unsafe struct HitHandler : IRayHitHandler
    {
        public RayHit Hit;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AllowTest(CollidableReference collidable)
        {
            if (collidable.BodyHandle.Value == 0 && collidable.Mobility == CollidableMobility.Dynamic)
                return false;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AllowTest(CollidableReference collidable, int childIndex)
        {
            return true;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnRayHit(in RayData ray, ref float maximumT, float t, in Vector3 normal, CollidableReference collidable, int childIndex)
        {
            maximumT = t;
            Hit.Normal = normal;
            Hit.T = t;
            Hit.Collidable = collidable;
            Hit.Hit = true;
        }
    }
}

