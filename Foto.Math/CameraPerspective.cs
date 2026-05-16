namespace Foto.Math;

public class CameraPerspective : CameraBase
{
    private float _fovRadians;

    public CameraPerspective(Vector3 eye, Vector3 target, Vector3 up, float aspectRatio, float fovDegrees) 
        : base(eye, target, up, aspectRatio)
    {
        _fovRadians = fovDegrees * MathF.PI / 180.0f;
    }

    public override Ray GenerateRay(int x, int y, int imageWidth, int imageHeight, float sampleX, float sampleY)
    {
        float u = (x + sampleX) / imageWidth;
        float v = (y + sampleY) / imageHeight;

        float px = (2.0f * u - 1.0f) * AspectRatio * MathF.Tan(_fovRadians * 0.5f);
        float py = (1.0f - 2.0f * v) * MathF.Tan(_fovRadians * 0.5f);

        Vector3 direction = (Forward + (Right * px) + (TrueUp * py)).Normalize();
        return new Ray(Eye, direction);
    }
}