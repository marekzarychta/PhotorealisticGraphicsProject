namespace Foto.Math;

public class Material
{
    public RGB DiffuseColor { get; set; }
    public float SpecularAmount { get; set; } //0-nieskonczonosc gdzie 0 = mat
    public float SpecularCoeff { get; set; } //0-nieskonczonosc (dla phonga)
    public float ReflectFraction { get; set; }

    public Material(RGB diffuseColor, float specularAmount, float specularCoeff, float reflectFraction)
    {
        DiffuseColor = diffuseColor;
        SpecularAmount = specularAmount;
        SpecularCoeff = specularCoeff;
        ReflectFraction = reflectFraction;
    }
}