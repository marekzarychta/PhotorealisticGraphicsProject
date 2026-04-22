namespace Foto.Math;

public struct Sphere
{
    public Vector3 center;
    public float radius;
    public Sphere(Vector3 center, float radius)
    {
        this.center = center;
        this.radius = radius;
    }

    public bool Hit(Ray ray, float t_min, float t_max)
    {
        Vector3 oc = ray.origin - center;
        
        float a = ray.direction.Dot(ray.direction);
        float b = oc.Dot(ray.direction);
        float c = oc.Dot(oc) - radius * radius;
        
        float discriminant = b * b - a * c;
        if (discriminant > 0)
        {
            float temp = (-b + MathF.Sqrt(discriminant)) / a;

            if (temp < t_max && temp > t_min)
            {
                return true;
            }
            
            temp = (-b - MathF.Sqrt(discriminant)) / a;
            if (temp < t_max && temp > t_min)
            {
                return true;
            }
            
        }
        return false;
    }
    
    public override string ToString() => "[" + center + ", " + radius + "]";
}