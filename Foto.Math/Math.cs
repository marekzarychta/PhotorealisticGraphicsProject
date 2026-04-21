namespace Foto.Math;

public static class MathFunctions
{
    public static float RadiansToDegrees(float radians)
    {
        return radians * 180 / MathF.PI;
    }
    public static float AngleBetweenVectors(Vector3 v1, Vector3 v2)
    {
        return RadiansToDegrees(MathF.Acos((v1.Dot(v2)) / (v1.Length() * v2.Length())));
    }
}