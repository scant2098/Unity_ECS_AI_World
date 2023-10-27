using System;
using UnityEngine;
using ZFramework;
using ZFramework.IO;

namespace JH_ECS
{
    public struct EVector3 : IEquatable<EVector3>
    {
        public float x;
        public float y;
        public float z;
        public EVector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public EVector3(EVector3 vector)
        {
            this.x = vector.x;
            this.y = vector.y;
            this.z = vector.z;
        }
        public bool Equals(EVector3 other)
        {
            return x == other.x && y == other.y && z == other.z;
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            return obj is EVector3 other && Equals(other);
        }
        public static EVector3 operator *(EVector3 a, EVector3 b)
        {
            return new EVector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }
        public static EVector3 operator *(float a, EVector3 b)
        {
            return new EVector3(a* b.x, a* b.y, a* b.z);
        }
        public static EVector3 operator +(EVector3 a, EVector3 b)
        {
            return new EVector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }
        public static EVector3 operator -(EVector3 a, EVector3 b)
        {
            return new EVector3(a.x - b.x, a.y - b.y, a.z - b.z);
        }
        public override string ToString()
        {
            return $"({x},{y},{z})";
        }

        public static readonly EVector3 zero = new EVector3(0f, 0f, 0f);
        public static readonly EVector3 forward = new EVector3(1f, 0f, 0f);
        public static readonly EVector3 right = new EVector3(0f, 0f, 1f);
        public static readonly EVector3 one = new EVector3(1f, 1f, 1f);
    }
    public struct PositionComponent:IComponent,IStorage
    {
        public EVector3 position;

       public PositionComponent(EVector3 vec)
       {
           position = new EVector3(vec);
       }
       public IComponent Default
       {
           get { return new PositionComponent(EVector3.one); }
       }
    }
}