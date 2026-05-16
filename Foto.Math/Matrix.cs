using System.Text;

namespace Foto.Math;

public struct Matrix4x4
{
    public float[] entries = new float[16];
    public Matrix4x4(float[] entries)
    {
        this.entries = entries;
    }

    public Matrix4x4()
    {
        for (int i = 0; i < entries.Length; i++)
        {
            entries[i] = 0.0f;
        }
    }
    
    public Matrix4x4(ref readonly float f){
        for (int i = 0; i < 16; i++)
        {
            entries[i] = f;
        }
    }
    
    
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        int count = 0;
        sb.Append("[\n");
        foreach (float f in entries)
        {
            count++;
            
            sb.Append(f);
            if (count % 4 == 0)
            {
                sb.Append("\n");
            }
            else
            {
                sb.Append(',');
            }
        }
        sb.Append("]");
        return sb.ToString();
    }
}

public struct Matrix3x3
{
    public float[] entries = new float[9];

    public Matrix3x3()
    {
    }
    public Matrix3x3(float[] entries)
    {
        this.entries = entries;
    }

    public Matrix3x3(ref readonly float f)
    {
        for (int i = 0; i < 9; i++)
        {
            entries[i] = f;
        }
    }
    
}