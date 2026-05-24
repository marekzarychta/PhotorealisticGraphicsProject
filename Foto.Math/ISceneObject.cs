namespace Foto.Math;

public interface ISceneObject
{
    RGB Color { get; set; }
    
    Material Material { get; set; }

    public bool Hit(Ray ray, float t_min, float t_max, out IntersectionInfo intersection);
}