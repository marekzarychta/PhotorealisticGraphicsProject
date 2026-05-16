namespace Foto.Math;

public abstract class CameraBase : ICamera
{
    protected Vector3 Eye, Target, Up;
    protected Vector3 Forward, Right, TrueUp;
    protected float AspectRatio;

    public CameraBase(Vector3 eye, Vector3 target, Vector3 up, float aspectRatio)
    {
        Eye = eye;
        Target = target;
        Up = up;
        AspectRatio = aspectRatio;
        
        Forward = (Target - Eye).Normalize();
        Right = Forward.Cross(Up).Normalize();
        TrueUp = Right.Cross(Forward).Normalize();
    }

    public abstract Ray GenerateRay(int x, int y, int imageWidth, int imageHeight, float sampleX, float sampleY);
}