using System.ComponentModel.DataAnnotations;

namespace Foto.Math;

public struct Vector3
{
    public float x, y, z;

    public Vector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    
    public static Vector3 operator +(Vector3 a, Vector3 b) => new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);

    public static Vector3 operator *(Vector3 a, float b) => new Vector3(a.x * b, a.y * b, a.z * b);

    public float Dot(Vector3 other) => x * other.x + y * other.y + z * other.z;
    
    public float Length() => MathF.Sqrt(x * x + y * y + z * z);
    
    public Vector3 Normalize() {
        float len = this.Length();
        if (len > 0) return this * (1.0f / len);
        else return new Vector3(0, 0, 0);
    }
    
    public override string ToString() => "[" + x + ", " + y + ", " + z + "]";
}