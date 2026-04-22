namespace Foto.Math;

public struct Ray
{
    public Vector3 origin;
    public Vector3 direction;
    public Vector3 destination;
    public float distance;
    public Ray(Vector3 origin, Vector3 direction)
    {
        this.origin = origin;
        this.direction = direction;
    }

    public Ray(Vector3 origin, Vector3 direction, float distance)
    {
        this.origin = origin;
        this.direction = direction;
        this.distance = distance;
    }
    
    public override string ToString() => "[Origin: " + origin + ", Destination: " + direction + ", Distance: " + distance + "]";
}