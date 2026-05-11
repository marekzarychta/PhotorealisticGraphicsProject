namespace Foto.Math;

public struct Triangle
{
    private Vector3[] vertices;
    private Vector3[] normals;
    public Triangle()
    {
        vertices = new Vector3[3];
        normals = new Vector3[3];
    }

    public Triangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        vertices = new Vector3[3];
        vertices[0] = p1;
        vertices[1] = p2;
        vertices[2] = p3;
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
    }

    public bool Intersects(Ray ray, out Vector3 result)
    {
        result = new Vector3();
        
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

        if (t > 1e-6f)
        {
            result = ray.origin + ray.direction * t;
            return true;
        }

        return false;
    }
}