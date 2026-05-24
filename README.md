# C# Ray Tracing Image Generation

CPU-based Ray Tracing engine implemented in C#. This project focuses on realistic light simulation using the Phong reflection model, soft shadows, and recursive reflections.

## Features
* **Whitted-style Ray Tracing:** Supports recursive reflections for mirrors and polished surfaces.
* **Phong Illumination Model:** Implements realistic Diffuse and Specular lighting components.
* **Soft Shadows:** Utilizes Area Lights with stochastic sampling to produce physically accurate, soft-edged shadows.
* **Material System:** Configurable materials supporting various levels of shininess (specular coefficient) and reflectivity.
* **Geometric Primitives:** Built-in support for Spheres, Planes, and Triangles.
* **PPM Export:** Renders scenes directly into the Portable Pixmap (PPM) format.

## How it Works
The engine follows a standard ray tracing pipeline:
1.  **Ray Generation:** Camera rays are cast from the viewpoint into the scene.
2.  **Intersection Testing:** The engine calculates intersections between rays and scene primitives.
3.  **Lighting Calculation:**
    * For each hit, the engine computes light contributions from multiple sources.
    * 
    * **Shadow Rays:** The engine performs secondary ray-casting toward light sources to determine visibility.
4.  **Recursion:** For reflective materials, the engine casts secondary "reflection rays" to simulate mirror-like surfaces.

## Project Structure
* `Foto.Math`: Contains core math structures (`Vector3`, `Ray`, `RGB`, `IntersectionInfo`) and rendering primitives.
* `RayTracer`: The core engine responsible for traversing the scene and computing the final pixel color using recursive lighting calculations.
* `Scene`: Manages objects, lights, and rendering settings.

## Getting Started
To render a scene, configure your objects and materials in `Program.cs`:

```csharp
// Example: Creating a shiny red sphere
Material shinyRed = new Material(new RGB(1.0f, 0.0f, 0.0f), 1.0f, 100.0f, 0.5f);
Sphere sphere = new Sphere(new Vector3(0, 0, 0), 1.0f, shinyRed);
scena.Add(sphere);

// Render the scene
renderer.RenderScene("output.ppm", camera, scene, sampler, width, height);
