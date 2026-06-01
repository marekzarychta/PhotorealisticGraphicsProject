using System;

namespace Foto.Math;

public class PointLight : ILight
{
    public Vector3 Position { get; set; }
    public RGB LightColor { get; set; }

    public float ConstAtten {  get; set; }
    public float LinearAtten {  get; set; }
    public float QuadraticAtten {  get; set; }

    public PointLight(Vector3 position, RGB lightColor,
        float constAtten = 1.0f, float linearAtten = 0.0f, float quadraticAtten = 0.0f)
    {
        Position = position;
        LightColor = lightColor;
        ConstAtten = constAtten;
        LinearAtten = linearAtten;
        QuadraticAtten = quadraticAtten;
    }

    public RGB GetDiffuse(Vector3 cameraPosition, IntersectionInfo intersectionInfo)
    {
        Vector3 lightVec = Position - intersectionInfo.Point;
        float distance = lightVec.Length();
        
        Vector3 L = lightVec.Normalize();
        Vector3 N = intersectionInfo.Normal.Normalize();

        float cosTheta = N.Dot(L);
        
        if (cosTheta <= 0.0f)
        {
            return new RGB(0, 0, 0);
        }

        float attenuation = 1.0f / (ConstAtten + (LinearAtten * distance) + (QuadraticAtten * distance * distance));

        RGB kd = intersectionInfo.ObjectHit.Material.DiffuseColor;

        float r = LightColor.r * kd.r * cosTheta * attenuation;
        float g = LightColor.g * kd.g * cosTheta * attenuation;
        float b = LightColor.b * kd.b * cosTheta * attenuation;

        return new RGB(r, g, b);
    }

    public RGB GetSpecular(Vector3 cameraPosition, IntersectionInfo intersectionInfo)
    {
        Vector3 lightVec = Position - intersectionInfo.Point;
        float distance = lightVec.Length();
        
        Vector3 L = lightVec.Normalize();
        Vector3 N = intersectionInfo.Normal.Normalize();

        float dotNL = N.Dot(L);
        if (dotNL <= 0.0f)
        {
            return new RGB(0, 0, 0);
        }

        Vector3 V = (cameraPosition - intersectionInfo.Point).Normalize();

        Vector3 R = (N * (2.0f * dotNL)) - L;
        R = R.Normalize();

        float cosAlpha = R.Dot(V);
        
        if (cosAlpha <= 0.0f)
        {
            return new RGB(0, 0, 0);
        }

        float shininess = intersectionInfo.ObjectHit.Material.SpecularCoeff;
        float specularFactor = MathF.Pow(cosAlpha, shininess);

        float attenuation = 1.0f / (ConstAtten + (LinearAtten * distance) + (QuadraticAtten * distance * distance));

        float ks = intersectionInfo.ObjectHit.Material.SpecularAmount;

        float r = LightColor.r * ks * specularFactor * attenuation;
        float g = LightColor.g * ks * specularFactor * attenuation;
        float b = LightColor.b * ks * specularFactor * attenuation;

        return new RGB(r, g, b);
    }
    
    public bool IsInShadow(IntersectionInfo intersectionInfo, Scene scene)
    {
        Vector3 lightVec = Position - intersectionInfo.Point;
        float distanceToLight = lightVec.Length();
        Vector3 shadowDirection = lightVec.Normalize();

        float bias = 0.001f;
        Vector3 safeStartPoint = intersectionInfo.Point + (intersectionInfo.Normal * bias);

        Ray shadowRay = new Ray(safeStartPoint, shadowDirection);
    
        float tMin = 0.001f;
        float tMax = distanceToLight - 0.001f; 

        return scene.Intersect(shadowRay, tMin, tMax, out IntersectionInfo shadowIntersection);
    }
}

public class AreaLight : ILight
{
    private PointLight baseLight;
    
    public Vector3 Position 
    { 
        get => baseLight.Position; 
        set => baseLight.Position = value; 
    }
    
    public RGB LightColor 
    { 
        get => baseLight.LightColor; 
        set => baseLight.LightColor = value; 
    }

    public float Radius { get; set; }
    public int SamplesCount { get; set; }

    private RGB originalColor;
    private Vector3 lightNormal;

    public AreaLight(Vector3 position, RGB lightColor, float radius, int samplesCount = 3)
    {
        baseLight = new PointLight(position, lightColor);
        Radius = radius;
        SamplesCount = samplesCount;
        originalColor = lightColor;
    }
    
    public AreaLight(Vector3 position, RGB lightColor, float radius, Vector3 lightNormal, int samplesCount = 3)
    {
        baseLight = new PointLight(position, lightColor);
        Radius = radius;
        SamplesCount = samplesCount;
        originalColor = lightColor;
        this.lightNormal = lightNormal;
    }

    public RGB GetDiffuse(Vector3 cameraPosition, IntersectionInfo intersectionInfo)
    {
        Vector3 dirToPoint = (intersectionInfo.Point - Position).Normalize();
        float cosEmission = dirToPoint.Dot(lightNormal);
        
        if (cosEmission <= 0.0f) return new RGB(0, 0, 0);

        RGB baseDiffuse = baseLight.GetDiffuse(cameraPosition, intersectionInfo);
        return baseDiffuse * cosEmission;
    }

    public RGB GetSpecular(Vector3 cameraPosition, IntersectionInfo intersectionInfo)
    {
        Vector3 dirToPoint = (intersectionInfo.Point - Position).Normalize();
        float cosEmission = dirToPoint.Dot(lightNormal);
        
        if (cosEmission <= 0.0f) return new RGB(0, 0, 0);

        RGB baseSpecular = baseLight.GetSpecular(cameraPosition, intersectionInfo);
        return baseSpecular * cosEmission;
    }

    public bool IsInShadow(IntersectionInfo intersectionInfo, Scene scene)
    {
        baseLight.LightColor = originalColor;

        int shadowHits = 0;
        int totalSamples = SamplesCount * SamplesCount;
        Vector3 centerPosition = Position;

        for (int x = 0; x < SamplesCount; x++)
        {
            for (int y = 0; y < SamplesCount; y++)
            {
                float offsetX = SamplesCount > 1 ? ((x / (float)(SamplesCount - 1)) - 0.5f) * 2.0f * Radius : 0;
                float offsetZ = SamplesCount > 1 ? ((y / (float)(SamplesCount - 1)) - 0.5f) * 2.0f * Radius : 0;

                Vector3 samplePosition = new Vector3(
                    centerPosition.x + offsetX,
                    centerPosition.y,
                    centerPosition.z + offsetZ
                );

                Vector3 lightVec = samplePosition - intersectionInfo.Point;
                float distanceToLight = lightVec.Length();
                Vector3 shadowDirection = lightVec.Normalize();



                Vector3 safeStartPoint = intersectionInfo.Point + (intersectionInfo.Normal * 0.001f);
                Ray shadowRay = new Ray(safeStartPoint, shadowDirection);

                if (scene.Intersect(shadowRay, 0.001f, distanceToLight - 0.001f, out _))
                {
                    shadowHits++;
                }
            }
        }

        float visibility = 1.0f - ((float)shadowHits / totalSamples);
        
        baseLight.LightColor = new RGB(
            originalColor.r * visibility, 
            originalColor.g * visibility, 
            originalColor.b * visibility
        );

        return false; 
    }
}