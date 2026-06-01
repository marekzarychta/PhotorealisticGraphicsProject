using System.Security.Cryptography;
using Foto.Math;


/*#region Zad1
/*Zadania 1-5#1#
Console.WriteLine("\t======ZAD 2======");
Vector3 v1 = new Vector3(0,3,0);
Vector3 v2 = new Vector3(5,5,0);

Vector3 zad1_1 = v1 + v2;
Console.WriteLine("Suma v1 i v2: " + zad1_1);
Vector3 zad1_2 = v2 + v1;
Console.WriteLine("Suma v2 i v1: " + zad1_2);

Console.WriteLine("\t======ZAD 3======");
Console.WriteLine("Kąt pomiędzy "+v1+" a "+v2+": "+MathFunctions.AngleBetweenVectors(v1,v2));

Console.WriteLine("\t======ZAD 4,5======");
Vector3 v3 = new Vector3(4, 5, 1);
Vector3 v4 = new Vector3(4, 1, 3);
Vector3 v5 = v3.Cross(v4);
Console.WriteLine("Wektor prostopadły do [4,5,1] i [4,1,3]: " + v5 + "\nPo normalizacji: "+v5.Normalize());

Console.WriteLine("\t======ZAD 7======");
Sphere S = new Sphere(new Vector3(0, 0, 0), 10);
Console.WriteLine("Sphere S: " + S);

Console.WriteLine("\t======ZAD 8======");
Ray R1 = new Ray(new Vector3(0,0,-20), (S.center - new Vector3(0,0,-20)).Normalize(), 100);
Console.WriteLine("Ray R1: " + R1);

Console.WriteLine("\t======ZAD 9======");
Ray R2 = new Ray(R1.origin, new Vector3(0, 1, 0), 100);
Console.WriteLine("Ray R2: " + R2);

Console.WriteLine("\t======ZAD 10======");
Vector3 res1 = new Vector3();
Vector3 res2 = new Vector3();
Console.WriteLine("Przecięcie S z R1: " + S.Hit(R1, 0.001f, R1.distance, out res1));
Console.WriteLine("Przecięcie S z R2: " + S.Hit(R2, 0.001f, R2.distance, out res2));

Console.WriteLine("\t======ZAD 11======");
Console.WriteLine("S przecina się z R1 w punkcie: "+res1);
Console.WriteLine("S przecina się z R2 w punkcie: "+res2);

Console.WriteLine("\t======ZAD 12======");
Ray R3 = new Ray(new Vector3(0,10,10),(new Vector3 (0,0,-1)),100);
Console.WriteLine("Ray R3: " + R3);
Console.WriteLine("Przecięcie S  z R3: " + S.Hit(R3, 0.001f, R3.distance, out Vector3 point));
Console.WriteLine(point);

Console.WriteLine("\t======ZAD 13======");
float angleRad = 45.0f * MathF.PI / 180.0f;
Vector3 normalVector = new Vector3(0, angleRad, angleRad);
Plane P = new Plane(normalVector, new Vector3(0, 0, 0));
Console.WriteLine("Normalna: " + P.normal);
Console.WriteLine("Kąt między normalną a osią Y: " + MathFunctions.AngleBetweenVectors(normalVector,new Vector3(0,1,0)));
Console.WriteLine("Kąt między normalną a osią Z: " + MathFunctions.AngleBetweenVectors(normalVector,new Vector3(0,0,1)));

Console.WriteLine("\t======ZAD 14======");
Console.WriteLine("Czy istnieje przecięcie P z R2: "+ P.Intersects(R2, out Vector3 intersection));
Console.WriteLine("W punkcie: "+intersection);

Console.WriteLine("\t======ZAD 15======");
Vector3 A = new Vector3(0, 0, 0);
Vector3 B = new Vector3(1, 0, 0);
Vector3 C = new Vector3(0, 1, 0);

Triangle tri = new Triangle(A, B, C);

Console.WriteLine("======1.======");
Vector3 P1 = new Vector3(-1, 0.5f, 0);
Vector3 P2 = new Vector3(1, 0.5f, 0);
Ray P1P2 = new Ray(P2,(P1 - P2),100.0f);

Console.WriteLine("Czy linia P1P2 przecina trójkąt: " + tri.Intersects(P1P2, out _));

Console.WriteLine("======2.======");
P1 =  new Vector3(2, -1, 0);
P2 =  new Vector3(2, 2, 0);
P1P2 = new Ray(P2,(P1 - P2),100.0f);
Console.WriteLine("Czy linia P1P2 przecina trójkąt: " + tri.Intersects(P1P2, out _));

Console.WriteLine("======3.======");
P1 =  new Vector3(0, 0, -1);
P2 =  new Vector3(0, 0, 1);
P1P2 = new Ray(P2,(P1 - P2),100.0f);
Console.WriteLine("Czy linia P1P2 przecina trójkąt: " + tri.Intersects(P1P2, out _));


Console.WriteLine("\t======TEST======");
Sphere sfera = new Sphere(new Vector3(0,0,0),10);
Ray testray = new Ray(new Vector3(20,0,0),new Vector3(-20,0,0), 100.0f);
Console.WriteLine("Czy ray przecina sfere: "+ sfera.Hit(testray, 0.1f, 100.0f, out Vector3 result));
Console.WriteLine(result);

#endregion*/

/*#region Zad2

Sphere sfera1 = new Sphere(new Vector3(12, 0, -40), 10.0f, new RGB(1.0f, 0.0f, 0.0f));

Sphere sfera2 = new Sphere(new Vector3(-12, 0, -40), 10.0f, new RGB(0.0f, 1.0f, 0.0f));

Scene scena = new Scene(new RGB(0.0f, 0.0f, 0.0f));

scena.Add(sfera1);
scena.Add(sfera2);

int width = 512;
int height = 512;


CameraPerspective cameraPersp =
    new CameraPerspective(new Vector3(0, 0, 0), new Vector3(0, 0, -1), new Vector3(0, 1, 0), 1.0f, 45.0f);

CameraOrthographic cameraOrtho =
    new CameraOrthographic(new Vector3(0,0,0), new Vector3(0,0,-1), new Vector3(0, 1, 0), 1.0f, 45.0f);

RayTracer tracer = new RayTracer();


Console.WriteLine("Kompiluję uruchamiam.");

Renderer renderer = new Renderer(tracer);



var samples1spp = Sampler.MakeCenterSample();
var samples2x2 = Sampler.MakeRegularSample(4);

renderer.RenderScene("orthographic_1spp.ppm", cameraOrtho, scena, samples1spp, width, height);

renderer.RenderScene("perspective_1spp.ppm", cameraPersp, scena, samples1spp, width, height);

renderer.RenderScene("perspective_2x2_aa.ppm", cameraPersp, scena, samples2x2, width, height);

#endregion*/

#region Zad3

Scene scena = new Scene(new RGB(0.1f, 0.1f, 0.1f));

Material reflectMat = new Material(new RGB(0.0f, 0.0f, 0.0f), 1.0f, 5.0f, 1.0f);
Material refractMat = new Material(new RGB(0.0f, 0.0f, 0.0f), 1.0f, 5.0f, 0.25f, 0.75f, 1.5f);

Material sufitMat = new Material(new RGB(0.8f, 0.8f, 0.8f), 0f, 0f, 0f);
Material lewyMat = new Material(new RGB(1.0f, 0f, 0f), 0f, 0f, 0f);
Material prawyMat = new Material(new RGB(0f, 1.0f, 0f), 0f, 0f, 0f);


Sphere sfera = new Sphere(new Vector3(-1, -2, -1), 1.0f, reflectMat);
scena.Add(sfera);

Sphere sfera2 = new Sphere(new Vector3(1, -2f, 1), 1.0f, refractMat);
scena.Add(sfera2);

Plane podloga = new Plane(new Vector3(0,1,0), new Vector3(0,-3,0), sufitMat);
scena.Add(podloga);

Plane sufit = new Plane(new Vector3(0,-1,0), new Vector3(0,3,0), sufitMat);
scena.Add(sufit);

Plane lewa = new Plane(new Vector3(1,0,0), new Vector3(-3,0,0), lewyMat);
scena.Add(lewa);

Plane prawa = new Plane(new Vector3(-1, 0, 0), new Vector3(3, 0, 0), prawyMat);
scena.Add(prawa);

Plane tylna = new Plane(new Vector3(0, 0, 1), new Vector3(0, 0, -3), sufitMat);
scena.Add(tylna);

AreaLight miekkieSwiatlo =
    new AreaLight(new Vector3(0.0f, 2.9f, 0.0f), new RGB(0.7f,0.7f,0.7f), 1f, new Vector3(0, -1, 0), 16);

scena.AddLight(miekkieSwiatlo);

/*
PointLight pointlight = new PointLight(new Vector3(0, 7, 2), new RGB(0.8f,0.8f,0.8f), 1F, 0.05f, 0.01f);
scena.AddLight(pointlight);
*/




CameraPerspective cameraPersp2 =
    new CameraPerspective(new Vector3(0, 0, 10), new Vector3(0, 0, -1), new Vector3(0, 1, 0), 1.0f, 45.0f);

RayTracer tracer = new RayTracer();
Renderer renderer = new Renderer(tracer);

var samples2x2 = Sampler.MakeRegularSample(2);

renderer.RenderScene("test.ppm", cameraPersp2, scena, samples2x2, 512, 512);

#endregion