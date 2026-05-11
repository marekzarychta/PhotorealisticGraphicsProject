namespace Foto.Math;

public struct Plane
{
    public Vector3 normal;
    public Vector3 point;
    public float distance;

    public Plane(Vector3 normal, Vector3 point)
    {
        this.normal = normal.Normalize();
        this.point = point;
        this.distance = this.normal.Dot(point);
    }

    public Plane(Vector3 normal, float distance)
    {
        this.normal = normal.Normalize();
        this.point = normal * distance;
        this.distance = distance;
    }

    public bool Intersect3(Plane p2, Plane p3, out Vector3 result)
    {
        result = new Vector3();
        float denominator = normal.Dot(p2.normal.Cross(p3.normal));

        if (MathF.Abs(denominator) < 1e-6f)
        {
            return false;
        }

        Vector3 temp1 = p2.normal.Cross(p3.normal) * distance;
        Vector3 temp2 = p3.normal.Cross(normal) * p2.distance;
        Vector3 temp3 = normal.Cross(p2.normal) * p3.distance;

        result = (temp1 + temp2 + temp3) / denominator;
        return true;
    }

    public bool Intersects(Ray ray, out Vector3 result)
    {
        result = new Vector3();
        float denom = normal.Dot(ray.direction);

        if (MathF.Abs(denom) > 1e-6f)
        {
            float t = (point - ray.origin).Dot(normal) / denom;
            if (t >= 0)
            {
                result = ray.origin + ray.direction * t;
                return true;
            }
        }

        return false;
    }
}