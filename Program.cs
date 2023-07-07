using EspacioPersonaje;
using EspacioFabrica;
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        FabricaDePersonajes crear = new();
        Personaje per1 = crear.crearPersonaje();
        Console.WriteLine(per1.Tipo);
    }
}