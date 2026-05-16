namespace Foto.Math;

public interface ICamera
{
    Ray GenerateRay(int x, int y, int imageWidth, int imageHeight, float sampleX, float sampleY);
}