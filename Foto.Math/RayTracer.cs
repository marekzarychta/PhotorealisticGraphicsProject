namespace Foto.Math;

public class RayTracer
{
    private const int MaxDepth = 3;

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

        RGB totalLight = iInfo.ObjectHit.Material.DiffuseColor * 0.1f; // Ambient

        foreach (var light in scene.Lights)
        {
            if (!light.IsInShadow(iInfo, scene))
            {
                totalLight += light.GetDiffuse(ray.origin, iInfo);
                totalLight += light.GetSpecular(ray.origin, iInfo);
            }
        }

        float reflectFraction = iInfo.ObjectHit.Material.ReflectFraction;
        if (reflectFraction > 0.0f)
        {
            Vector3 v = ray.direction;
            Vector3 n = iInfo.Normal;
            Vector3 reflectionDir = v - (n * 2.0f * v.Dot(n));

            Ray reflectionRay = new Ray(iInfo.Point + (iInfo.Normal * 0.001f), reflectionDir);

            RGB reflectionColor = TraceRecursive(reflectionRay, scene, depth - 1);

            totalLight = (totalLight * (1.0f - reflectFraction)) + (reflectionColor * reflectFraction);
        }

        return totalLight;
    }
}