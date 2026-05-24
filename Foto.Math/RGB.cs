namespace Foto.Math;

public struct RGB
{
    public float r,g,b;

    public static RGB operator *(RGB color, float value)
    {
        return new RGB(color.r * value, color.g * value, color.b * value);
    }
    public static RGB operator *(float value, RGB color)
    {
        return new RGB(color.r * value, color.g * value, color.b * value);
    }
    
    public RGB(float r, float g, float b)
    {
        this.r = r;
        this.g = g;
        this.b = b;
    }
}