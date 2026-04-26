namespace Foto.Math;

public struct Plane
{
    public Vector3 normal;
    public Vector3 point;
    public float distance;
    
    bool Intersect3(Plane p2,  Plane p3, Vector3 result)
    {
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
}