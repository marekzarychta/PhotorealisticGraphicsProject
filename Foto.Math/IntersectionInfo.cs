namespace Foto.Math;

public struct IntersectionInfo
{
    public float T { get; set; }
    public Vector3 Point { get; set; }
    public Vector3 Normal { get; set; }
    public ISceneObject ObjectHit { get; set; }
}