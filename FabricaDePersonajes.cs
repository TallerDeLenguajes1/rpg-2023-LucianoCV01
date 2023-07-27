using EspacioPersonaje;
namespace EspacioFabrica
{
    public class FabricaDePersonajes
    {
        string[] tipoPersonaje = {"Brumoso", "Humano", "Inquisidor de Acero", "Nacido de la Bruma"};
        List<string> nombres = new List<string>
        {
            "Kaladin Stormblessed",
            "Shallan Davar",
            "Dalinar Kholin",
            "Szeth-son-son-Vallano",
            "Jasnah Kholin",
            "Adolin Kholin",
            "Lift",
            "Renarin Kholin",
            "Navani Kholin",
            "Taravangian"
        };
        List<string> apodos = new List<string>
        {
            "Portador de la Llave",
            "Ladrona de Palabras",
            "El Renacido",
            "La Cuchilla de la Verdad",
            "La Hereje",
            "La Espada",
            "La Ladrona de Comida",
            "El Profeta",
            "La Ingeniosa",
            "El Rey de los Tontos"
        };
        public Personaje crearPersonaje()
        {
            Personaje nuevoPersonaje = new();
            Random rand = new();
            // Datos
            nuevoPersonaje.Tipo = tipoPersonaje[rand.Next(tipoPersonaje.Length)];
            int numeroAleatorio = rand.Next(nombres.Count);
            nuevoPersonaje.Nombre = nombres[numeroAleatorio];
            nuevoPersonaje.Apodo = apodos[numeroAleatorio];
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
        public List<Personaje> ListaPersonajesAleatorios(int cantidadPersonajes)
        {
            int i = 0;
            List<Personaje> listaPersonajes = new();
            while (i < cantidadPersonajes)
            {
                var personaje = crearPersonaje();
                if (personaje.Nombre != null && personaje.Apodo != null)
                {
                    nombres.Remove(personaje.Nombre);
                    apodos.Remove(personaje.Apodo);
                }
                listaPersonajes.Add(personaje); 
                i++;
            }
            return listaPersonajes;
        }
    }
}