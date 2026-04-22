using System.Security.Cryptography;
using Foto.Math;

/*Zadania 1-5*/
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
Console.WriteLine("Przecięcie S z R1: " + S.Hit(R1, 0.001f, R1.distance));
Console.WriteLine("Przecięcie S z R2: " + S.Hit(R2, 0.001f, R2.distance));

Console.WriteLine("\t======ZAD 11======");
// Console.WriteLine("S przecina się z R1 w punkcie: "+);

Console.WriteLine("\t======ZAD 12======");
Ray R3 = new Ray(new Vector3(0,0,0),(new Vector3 (15,15,0)),100);
Console.WriteLine("Ray R3: " + R3);
Console.WriteLine("Przecięcie S  z R3: " + S.Hit(R3, 0.001f, R3.distance));