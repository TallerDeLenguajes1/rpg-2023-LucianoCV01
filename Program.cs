using EspacioPersonaje;
using EspacioFabrica;
using EspacioPersonajesJson;
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        const string nombreArchivoJson = "Personajes.json";
        PersonajesJson archivoJson = new();
        if (archivoJson.Existe(nombreArchivoJson))
        {
            var listadoPersonajes = archivoJson.LeerPersonajes(nombreArchivoJson);
            if (listadoPersonajes != null)
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("------ Personaje {0} ------", i);
                    MostrarPersonaje(listadoPersonajes[i]);
                }      
            }
        } else
        {
            FabricaDePersonajes crear = new();
            List<Personaje> listadoPersonajes = new();
            for (int i = 0; i < 10; i++)
            {
                var personaje = crear.crearPersonaje();
                listadoPersonajes.Add(personaje);
            }
            archivoJson.GuardarPersonajes(listadoPersonajes, nombreArchivoJson);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("------ Personaje {0} ------", i);
                MostrarPersonaje(listadoPersonajes[i]);
            }
        }
    }
    public static void MostrarPersonaje(Personaje persona)
    {
        // Datos
        Console.WriteLine(persona.Tipo);
        Console.WriteLine(persona.Nombre + " ---> " + persona.Apodo);
        Console.WriteLine(persona.FechaDeNacimiento);
        Console.WriteLine("Edad: " + persona.Edad);
        // Caracteristicas
        Console.WriteLine("Velocidad: " + persona.Velocidad);
        Console.WriteLine("Destreza: " + persona.Destreza);
        Console.WriteLine("Fuerza: " + persona.Fuerza);
        Console.WriteLine("Nivel: " + persona.Nivel);
        Console.WriteLine("Armadura: " + persona.Armadura);
        Console.WriteLine("Salud: " + persona.Salud);
    }
}
/*
2) Muestre por pantalla los datos y características de los personajes cargados.
*/