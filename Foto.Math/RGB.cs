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
    
    public static RGB operator +(RGB c1, RGB c2)
    {
        return new RGB(c1.r + c2.r, c1.g + c2.g, c1.b + c2.b);
    }
    
    public RGB(float r, float g, float b)
    {
        this.r = r;
        this.g = g;
        this.b = b;
    }
}