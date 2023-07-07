using EspacioPersonaje;
namespace EspacioFabrica
{
    public class FabricaDePersonajes
    {
        string[] tipoPersonaje = {"Brumoso", "Humano", "Inquisidor", "Kandra", "Koloss", "Nacido de la Bruma"};
        string[,] nombresYapodos = 
        {
            { "Kaladin Stormblessed", "Portador de la Llave" }, 
            { "Shallan Davar", "Ladrona de Palabras" },
            { "Dalinar Kholin", "El Renacido" },
            { "Szeth-son-son-Vallano", "La Cuchilla de la Verdad" },
            { "Jasnah Kholin", "La Hereje" },
            { "Adolin Kholin", "La Espada" },
            { "Lift", "La Ladrona de Comida" },
            { "Renarin Kholin", "El Profeta" },
            { "Navani Kholin", "La Ingeniosa" },
            { "Taravangian", "El Rey de los Tontos" }
        };
        public Personaje crearPersonaje()
        {
            Personaje nuevoPersonaje = new();
            Random rand = new();
            // Datos
            nuevoPersonaje.Tipo = tipoPersonaje[rand.Next(7)];
            int numeroAleatorio = rand.Next(10);
            nuevoPersonaje.Nombre = nombresYapodos[numeroAleatorio, 0];
            nuevoPersonaje.Apodo = nombresYapodos[numeroAleatorio, 1];
            nuevoPersonaje.Edad = rand.Next(0, 300);
            nuevoPersonaje.FechaDeNacimiento = calcularFechaDeNacimiento(nuevoPersonaje.Edad);
            // Caracteristicas
            nuevoPersonaje.Velocidad = rand.Next(1, 11);
            nuevoPersonaje.Destreza = rand.Next(1, 6);
            nuevoPersonaje.Fuerza = rand.Next(1, 11);
            nuevoPersonaje.Nivel = rand.Next(1, 11);
            nuevoPersonaje.Armadura = rand.Next(1, 11);
            nuevoPersonaje.Salud = 100;

            return nuevoPersonaje;
        }
        public DateTime calcularFechaDeNacimiento(int edad)
        {
            var FechaActual = DateTime.Today;
            var FechaNacimiento = FechaActual.AddYears(-edad);
            return FechaNacimiento;
        }
    }
}