namespace Foto.Math;

public interface ILight
{
    RGB LightColor {get; set;}

    RGB GetDiffuse(Vector3 cameraPosition, IntersectionInfo intersectionInfo);
    RGB GetSpecular(Vector3 cameraPosition, IntersectionInfo intersectionInfo);
    bool IsInShadow(IntersectionInfo intersectionInfo, Scene scene);
}