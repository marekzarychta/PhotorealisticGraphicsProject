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



