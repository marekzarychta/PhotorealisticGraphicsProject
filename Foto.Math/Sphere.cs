using System.Drawing;

namespace Foto.Math;

public struct Sphere : ISceneObject
{
    public Vector3 center;
    public float radius;
    public RGB Color { get; set; }
    public Material Material { get; set; }
    public Sphere(Vector3 center, float radius)
    {
        this.center = center;
        this.radius = radius;
    }

    public Sphere(Vector3 center, float radius, RGB color)
    {
        this.center = center;
        this.radius = radius;
        this.Color = color;
    }

    public Sphere(Vector3 center, float radius, Material material)
    {
        this.center = center;
        this.radius = radius;
        this.Material = material;
        this.Color = material.DiffuseColor;
    }
    
    public bool Hit(Ray ray, float t_min, float t_max, out IntersectionInfo intersection)
    {
        intersection = new IntersectionInfo();
        Vector3 oc = ray.origin - center;
        
        float a = ray.direction.Dot(ray.direction);
        
        float b = 2 *  oc.Dot(ray.direction);
        
        float c = oc.Dot(oc) - radius * radius;
        
        float discriminant = b * b - (4 * a * c);
        
        //1 punkt
        if (discriminant == 0)
        {
            float t0 = -b / (2 * a);
            intersection.Point = ray.origin + ray.direction * t0;
            intersection.T = t0;
            intersection.Normal = (intersection.Point - center).Normalize();
            intersection.ObjectHit = this;
            return true;
        }
        //2 punkty
        else if (discriminant > 0)
        {
            float temp = (-b - MathF.Sqrt(discriminant)) / (2 * a);

            if (temp < t_max && temp > t_min)
            {
                intersection.Point = ray.origin + ray.direction * temp;
                intersection.T = temp;
                intersection.Normal = (intersection.Point - center).Normalize();
                intersection.ObjectHit = this;
                return true;
            }
            
            temp = (-b + MathF.Sqrt(discriminant)) / (2 * a);
            if (temp < t_max && temp > t_min)
            {
                intersection.Point = ray.origin + ray.direction * temp;
                intersection.T = temp;
                
                intersection.Normal = (intersection.Point - center).Normalize();
                intersection.ObjectHit = this;
                return true;
            }
        }
        //0 punktow
        return false;
    }
    
    public override string ToString() => "[" + center + ", " + radius + "]";
}