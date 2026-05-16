namespace Foto.Math;

public struct Sample2D
{
    public readonly float X;
    public readonly float Y;

    public Sample2D(float x, float y)
    {
        X = x;
        Y = y;
    }
}

public static class Sampler
{
    private static Random _rng = new Random();

    public static List<Sample2D> MakeCenterSample()
    {
        return new List<Sample2D>{new Sample2D(0.5f, 0.5f)};
    }

    public static List<Sample2D> MakeRegularSample(int n)
    {
        var samples = new List<Sample2D>(n * n);
        for (int py = 0; py < n; ++py)
        {
            for (int px = 0; px < n; ++px)
            {
                float sampleX = (px + 0.5f) / n;
                float sampleY = (py + 0.5f) / n;
                samples.Add(new Sample2D(sampleX, sampleY));
            }
        }
        return samples;
    }
    
    public static List<Sample2D> MakeJitteredSamples(int n)
    {
        var samples = new List<Sample2D>(n * n);
        for (int py = 0; py < n; ++py)
        {
            for (int px = 0; px < n; ++px)
            {
                float sampleX = (px + (float)_rng.NextDouble()) / n;
                float sampleY = (py + (float)_rng.NextDouble()) / n;
                samples.Add(new Sample2D(sampleX, sampleY));
            }
        }
        return samples;
    }
}