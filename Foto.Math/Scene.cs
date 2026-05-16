namespace Foto.Math;

public class Scene
{
    public List<ISceneObject> SceneObjects { get; set; } = new List<ISceneObject>();
    public RGB BackgroundColor { get; set; }
    public Scene(RGB backgroundColor){
        BackgroundColor = backgroundColor;
    }

    public void Add(ISceneObject sceneObject)
    {
        SceneObjects.Add(sceneObject);
    }
    public void Remove(ISceneObject sceneObject)
    {
        SceneObjects.Remove(sceneObject);
    }

    public bool Intersect(Ray ray, float tMin, float tMax, out ISceneObject hitObject)
    {
        hitObject = null;
        bool hitAnything = false;

        float closestT = tMax;

        foreach (var obj in SceneObjects)
        {
            if (obj.Hit(ray, tMin, tMax, out Vector3 result))
            {
                float t = (result - ray.origin).Length();
                if (t > tMin && t < closestT)
                {
                    closestT = t;
                    hitObject = obj;
                    hitAnything = true;
                }
            }
        }
        return hitAnything;
    }
    
}