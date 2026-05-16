namespace Foto.Math;

public class CameraOrthographic : CameraBase
{
    private float _viewScale;

    public CameraOrthographic(Vector3 eye, Vector3 target, Vector3 up, float aspectRatio, float viewScale) 
        : base(eye, target, up, aspectRatio)
    {
        _viewScale = viewScale;
    }

    public override Ray GenerateRay(int x, int y, int imageWidth, int imageHeight, float sampleX, float sampleY)
    {
        float u = (x + sampleX) / imageWidth;
        float v = (y + sampleY) / imageHeight;

        float screenX = (2.0f * u - 1.0f) * AspectRatio * _viewScale;
        float screenY = (1.0f - 2.0f * v) * _viewScale;

        Vector3 origin = Eye + (Right * screenX) + (TrueUp * screenY);
        return new Ray(origin, Forward);
    }
}