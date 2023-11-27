using System.Numerics;
using System.Runtime.InteropServices;

namespace MapCompiler
{
    class Types
    {
        public struct Block
        {
            public string Texture;
            public Vector3 Position;
            public Vector3 Rotation;
            public Vector3 Size;
            public Vector2[] Uv;
        }

        public struct Prop
        {
            public string Name;
            public Vector3 Position;
            public Vector3 Rotation;
            public Vector3 Scale;
        }
    }
}