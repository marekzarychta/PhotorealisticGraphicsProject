namespace Foto.Math;

public class Triangle : ISceneObject
{
    private Vector3[] vertices;
    private Vector3[] normals;
    
    public RGB Color { get; set; }
    
    public Material Material { get; set; }

    public Triangle()
    {
        vertices = new Vector3[3];
        normals = new Vector3[3]; 
        Color = new RGB();
    }

    public Triangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        vertices = new Vector3[3];
        vertices[0] = p1;
        vertices[1] = p2;
        vertices[2] = p3;
        normals = new Vector3[3]; 
        Color = new RGB();
    }

    public Triangle(Vector3 p1, Vector3 p2, Vector3 p3, Material material)
    {
        vertices = new Vector3[3];
        vertices[0] = p1;
        vertices[1] = p2;
        vertices[2] = p3;
        this.Material = material;
    }
    
    public Triangle(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 normal1, Vector3 normal2, Vector3 normal3)
    {
        vertices = new Vector3[3];
        vertices[0] = p1;
        vertices[1] = p2;
        vertices[2] = p3;
        
        normals = new Vector3[3];
        normals[0] = normal1;
        normals[1] = normal2;
        normals[2] = normal3;
        
        Color = new RGB();
    }

    public bool Hit(Ray ray, float t_min, float t_max, out IntersectionInfo intersection)
    {
        intersection = new IntersectionInfo();
        
        Vector3 edge1 = vertices[1] - vertices[0];
        Vector3 edge2 = vertices[2] - vertices[0];
        
        Vector3 h = ray.direction.Cross(edge2);
        float a = edge1.Dot(h);

        if (a > -1e-6f && a < 1e-6f)
        {
            return false;
        }

        float f = 1.0f / a;
        Vector3 s = ray.origin - vertices[0];
        
        float u = f * s.Dot(h);

        if (u < 0.0f || u > 1.0f)
        {
            return false;
        }

        Vector3 q = s.Cross(edge1);
        float v = f * ray.direction.Dot(q);

        if (v < 0.0f || u + v > 1.0f)
        {
            return false;
        }

        float t = f * edge2.Dot(q);

        if (t >= t_min && t <= t_max)
        {
            intersection.T = t;
            intersection.Point = ray.origin + ray.direction * t;

            Vector3 hitNormal;
            
            if (normals != null && (MathF.Abs(normals[0].x) > 0 || MathF.Abs(normals[0].y) > 0 || MathF.Abs(normals[0].z) > 0))
            {
                float w = 1.0f - u - v;
                hitNormal = (normals[0] * w) + (normals[1] * u) + (normals[2] * v);
            }
            else
            {
                hitNormal = edge1.Cross(edge2);
            }

            hitNormal = hitNormal.Normalize();

            if (ray.direction.Dot(hitNormal) > 0)
            {
                hitNormal = hitNormal * -1.0f;
            }

            intersection.Normal = hitNormal;
            intersection.ObjectHit = this;
            
            return true;
        }

        return false;
    }
}