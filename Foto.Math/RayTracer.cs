namespace Foto.Math;

public class RayTracer
{
    public RGB Trace(Ray ray, Scene scene)
    {
        float tMin = 0.001f;
        float tMax = float.MaxValue;

        bool hit = scene.Intersect(ray, tMin, tMax, out IntersectionInfo intersectionInfo);

        if (!hit)
        {
            return scene.BackgroundColor;
        }
        float totalR = 0.0f;
        float totalG = 0.0f;
        float totalB = 0.0f;
        
        RGB ambientColor = intersectionInfo.ObjectHit.Material.DiffuseColor * 0.1f;
        totalR = ambientColor.r; totalG = ambientColor.g; totalB = ambientColor.b;

        foreach (var light in scene.Lights)
        {
            if (!light.IsInShadow(intersectionInfo, scene))
            {
                RGB diffuse = light.GetDiffuse(ray.origin, intersectionInfo);
                RGB specular = light.GetSpecular(ray.origin, intersectionInfo);

                totalR += diffuse.r + specular.r;
                totalG += diffuse.g + specular.g;
                totalB += diffuse.b + specular.b;
            }
        }

        return new RGB(totalR, totalG, totalB);
    }
}