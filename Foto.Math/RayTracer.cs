namespace Foto.Math;

public class RayTracer
{
    public RGB Trace(Ray ray, Scene scene)
    {
        float tMin = 0.001f;
        float tMax = float.MaxValue;

        bool hit = scene.Intersect(ray, tMin, tMax, out ISceneObject hitObject);

        if (hit)
        {
            return hitObject.Color;
        }

        return scene.BackgroundColor;
    }
}