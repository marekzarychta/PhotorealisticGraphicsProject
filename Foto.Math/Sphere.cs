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

    public bool Hit(Ray ray, float t_min, float t_max, out Vector3 result)
    {
        result = new Vector3();
        Vector3 oc = ray.origin - center;
        
        float a = ray.direction.Dot(ray.direction);
        
        float b = 2 *  oc.Dot(ray.direction);
        
        float c = oc.Dot(oc) - radius * radius;
        
        float discriminant = b * b - (4 * a * c);

        //1 punkt
        if (discriminant == 0)
        {
            float t0 = -b / (2 * a);
            result = ray.origin + ray.direction * t0;
            return true;
        }
        //2 punkty
        else if (discriminant > 0)
        {
            float temp = (-b - MathF.Sqrt(discriminant)) / (2 * a);

            if (temp < t_max && temp > t_min)
            {
                result = ray.origin + ray.direction * temp;
                return true;
            }
            
            temp = (-b + MathF.Sqrt(discriminant)) / (2 * a);
            if (temp < t_max && temp > t_min)
            {
                result = ray.origin + ray.direction * temp;
                return true;
            }
        }
        //0 punktow
        return false;
    }
    
    public override string ToString() => "[" + center + ", " + radius + "]";
}