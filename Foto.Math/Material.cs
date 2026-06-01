namespace Foto.Math;

public class Material
{
    public RGB DiffuseColor { get; set; }
    public float SpecularAmount { get; set; } //0-nieskonczonosc gdzie 0 = mat
    public float SpecularCoeff { get; set; } //0-nieskonczonosc (dla phonga)
    public float ReflectFraction { get; set; }
    
    public float RefractFraction { get; set; } //0-1
    public float RefractiveIndex { get; set; }

    public Material(RGB diffuseColor, float specularAmount, float specularCoeff, float reflectFraction)
    {
        DiffuseColor = diffuseColor;
        SpecularAmount = specularAmount;
        SpecularCoeff = specularCoeff;
        ReflectFraction = reflectFraction;
    }
    
    public Material(RGB diffuseColor, float specularAmount, float specularCoeff, float reflectFraction, float refractFraction, float refractiveIndex)
    {
        DiffuseColor = diffuseColor;
        SpecularAmount = specularAmount;
        SpecularCoeff = specularCoeff;
        ReflectFraction = reflectFraction;
        RefractFraction = refractFraction;
        RefractiveIndex = refractiveIndex;
    }
}