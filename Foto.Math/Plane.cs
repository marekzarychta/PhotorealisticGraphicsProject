namespace Foto.Math;

public struct Plane
{
    public Vector3 normal;
    public Vector3 point;
    public float distance;

    public Plane(Vector3 normal, Vector3 point, float distance)
    {
        this.normal = normal;
        this.point = point;
        this.distance = distance;
    }

    public bool Intersect3(Plane p2,  Plane p3, out Vector3 result)
    {
        result = new Vector3();
        float denominator = normal.Dot((p2.normal).Cross(p3.normal));

        if (denominator == 0.0f)
        {
            return false;
        }

        Vector3 temp1, temp2, temp3;
        
        temp1=(p2.normal.Cross(p3.normal))*distance; 
        temp2=(p3.normal.Cross(normal))*p2.distance;
        temp3=(normal.Cross(p2.normal))*p3.distance;
        
        result = (temp1+temp2+temp3)/(-denominator);
        return true;
    }

    public bool Intersects(Ray ray, out Vector3 result)
    {
        result = new Vector3();
        
        return false;
    }
    
}