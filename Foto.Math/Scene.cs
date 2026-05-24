namespace Foto.Math;

public class Scene
{
    public List<ISceneObject> SceneObjects { get; set; } = new List<ISceneObject>();
    
    public List<ILight> Lights { get; set; } = new List<ILight>();
    public RGB BackgroundColor { get; set; }
    public Scene(RGB backgroundColor){
        BackgroundColor = backgroundColor;
    }

    public void Add(ISceneObject sceneObject)
    {
        SceneObjects.Add(sceneObject);
    }

    public void AddLight(ILight light)
    {
        Lights.Add(light);
    }
    
    public void Remove(ISceneObject sceneObject)
    {
        SceneObjects.Remove(sceneObject);
    }

    public bool Intersect(Ray ray, float tMin, float tMax, out IntersectionInfo intersectionInfo)
    {
        intersectionInfo = new IntersectionInfo();
        bool hitAnything = false;

        float closestT = tMax;

        foreach (var obj in SceneObjects)
        {
            if (obj.Hit(ray, tMin, closestT, out IntersectionInfo localIntersection))
            {
                    closestT = localIntersection.T;
                    intersectionInfo = localIntersection;
                    hitAnything = true;
            }
        }
        return hitAnything;
    }
    
}