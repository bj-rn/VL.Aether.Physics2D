using Stride.Core.Mathematics;
using nkast.Aether.Physics2D.Common;

using Vector2  = Stride.Core.Mathematics.Vector2;
using AetherVector2 = nkast.Aether.Physics2D.Common.Vector2;
using System.Runtime.InteropServices;
using System.Numerics;

namespace Aether.Physics2D;

public static class Utils
{

    public static Transform ToAetherTransform(this Matrix matrix)
    {
        var pos = matrix.TranslationVector.XY().ToAetherVector2();
        var angle = (float)Math.Atan2(matrix.M12, matrix.M11);
        
        return new Transform(pos, angle);
    }


    public static Vector2 ToVector2(this AetherVector2 vector)
    {
        return new Vector2(vector.X, vector.Y);
    }

    public static AetherVector2 ToAetherVector2(this Vector2 vector)
    {
        return new AetherVector2(vector.X, vector.Y);
    }

    // https://graphicdna.blogspot.com/2014/03/fast-casting-of-c-structs-with-no.html

    [StructLayout(LayoutKind.Explicit)]
    internal struct Union
    {
        [FieldOffset(0)]
        public Vector2 VectorA;
        [FieldOffset(0)]
        public AetherVector2 VectorB;

        public static Union StaticRef = new Union();

        public static Vector2 ToVector2(AetherVector2 vector)
        {
            StaticRef.VectorB = vector;
            return StaticRef.VectorA;
        }
        public static AetherVector2 ToAetherVector2(Vector2 vector)
        {
            StaticRef.VectorA = vector;
            return StaticRef.VectorB;
        }

    }

    public static AetherVector2 ToAetherVector2Union(this Vector2 vector)
    {
        return Union.ToAetherVector2(vector);
    }


    public static Vector2 ToVector2Union(this AetherVector2 vector)
    {
        return Union.ToVector2(vector);
    }


}