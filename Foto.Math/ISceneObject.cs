namespace Foto.Math;

public interface ISceneObject
{
    RGB Color { get; set; }

    public bool Hit(Ray ray, float t_min, float t_max, out Vector3 result);
}