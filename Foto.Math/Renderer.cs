using System;
using System.Collections.Generic;

namespace Foto.Math;

public class Renderer
{
    private RayTracer tracer;

    public Renderer(RayTracer tracer)
    {
        this.tracer = tracer;
    }

    public void RenderScene(string filename, ICamera camera, Scene scene, List<Sample2D> samples, int width, int height)
    {
        Console.WriteLine($"\nRozpoczynam renderowanie: {filename}...");
        
        Image image = new Image(width, height);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float rAccum = 0;
                float gAccum = 0;
                float bAccum = 0;

                foreach (var sample in samples)
                {
                    Ray ray = camera.GenerateRay(x, y, width, height, sample.X, sample.Y);
                    
                    RGB color = tracer.Trace(ray, scene);
                    
                    rAccum += color.r;
                    gAccum += color.g;
                    bAccum += color.b;
                }

                float sampleCount = samples.Count;
                float finalR = rAccum / sampleCount;
                float finalG = gAccum / sampleCount;
                float finalB = bAccum / sampleCount;

                image.SetPixel(x, y, finalR, finalG, finalB);
            }
        }

        image.SaveToPPM(filename);
        Console.WriteLine($"Zapisano do: {filename}");
    }
}