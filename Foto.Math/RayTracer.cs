namespace Foto.Math;

public class RayTracer
{
    private const int MaxDepth = 4;

    public RGB Trace(Ray ray, Scene scene)
    {
        return TraceRecursive(ray, scene, MaxDepth);
    }

private RGB TraceRecursive(Ray ray, Scene scene, int depth)
{
    if (depth <= 0) return new RGB(0, 0, 0);

    float tMin = 0.001f;
    float tMax = float.MaxValue;

    if (!scene.Intersect(ray, tMin, tMax, out IntersectionInfo iInfo))
    {
        return scene.BackgroundColor;
    }

    Material mat = iInfo.ObjectHit.Material;
    RGB totalLight = mat.DiffuseColor * 0.25f; // Ambient

    if (mat.RefractFraction < 1.0f)
    {
        foreach (var light in scene.Lights)
        {
            if (!light.IsInShadow(iInfo, scene))
            {
                totalLight += light.GetDiffuse(ray.origin, iInfo);
                totalLight += light.GetSpecular(ray.origin, iInfo);
            }
        }
    }

    if (mat.ReflectFraction > 0.0f)
    {
        Vector3 v = ray.direction;
        Vector3 n = iInfo.Normal;
        Vector3 reflectionDir = (v - (n * 2.0f * v.Dot(n))).Normalize();

        Ray reflectionRay = new Ray(iInfo.Point + (iInfo.Normal * 0.001f), reflectionDir);
        RGB reflectionColor = TraceRecursive(reflectionRay, scene, depth - 1);

        totalLight = (totalLight * (1.0f - mat.ReflectFraction)) + (reflectionColor * mat.ReflectFraction);
    }

    if (mat.RefractFraction > 0.0f)
    {
        Vector3 normal = iInfo.Normal;
        Vector3 incident = ray.direction;
        
        float cosI = incident.Dot(normal);
        float eta;
        
        if (cosI < 0.0f)
        {
            
            cosI = -cosI;
            eta = 1.0f / mat.RefractiveIndex;
        }
        else
        {
            
            normal = normal * -1.0f;
            eta = mat.RefractiveIndex / 1.0f;
        }

        float k = 1.0f - eta * eta * (1.0f - cosI * cosI);

        if (k >= 0.0f) 
        {
            Vector3 refractDir = (incident * eta) + (normal * (eta * cosI - MathF.Sqrt(k)));
            refractDir = refractDir.Normalize();

            Ray refractRay = new Ray(iInfo.Point - (normal * 0.001f), refractDir);
            RGB refractColor = TraceRecursive(refractRay, scene, depth - 1);

            totalLight = (totalLight * (1.0f - mat.RefractFraction)) + (refractColor * mat.RefractFraction);
        }
        else 
        {
            Vector3 reflectionDir = (incident - (normal * 2.0f * incident.Dot(normal))).Normalize();
    
            Ray internalReflectionRay = new Ray(iInfo.Point - (normal * 0.001f), reflectionDir);
    
            RGB internalColor = TraceRecursive(internalReflectionRay, scene, depth - 1);
    
            totalLight = (totalLight * (1.0f - mat.RefractFraction)) + (internalColor * mat.RefractFraction);
        }
    }

    return totalLight;
}
}