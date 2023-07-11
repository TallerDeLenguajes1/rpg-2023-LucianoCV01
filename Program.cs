using EspacioPersonaje;
using EspacioFabrica;
using EspacioPersonajesJson;
internal class Program
{
    private static void Main(string[] args)
    {
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
                    Console.WriteLine(listadoPersonajes[i].MostrarPersonaje());
                }      
                while (listadoPersonajes.Count != 1)
                {
                    var jugador1 = PersonajeAleatorio(listadoPersonajes);
                    listadoPersonajes.Remove(jugador1);
                    var jugador2 = PersonajeAleatorio(listadoPersonajes);
                    listadoPersonajes.Remove(jugador2);
                    listadoPersonajes.Add(Pelea(jugador1, jugador2));
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
                Console.WriteLine(listadoPersonajes[i].MostrarPersonaje());
            }
        }
    }
    public static int DanioProvocado(Personaje atacante, Personaje defensor)
    {
        Random rand = new();
        const int ajuste = 250;
        double ataque = atacante.Destreza * atacante.Fuerza * atacante.Nivel;
        int efectividad = rand.Next(1, 101);
        double defensa = defensor.Armadura * defensor.Velocidad;
        double danioProvocado = ((ataque * efectividad) - defensa) / ajuste;
        return ((int)Math.Round(danioProvocado, MidpointRounding.AwayFromZero));
    }
    public static Personaje PersonajeAleatorio(List<Personaje> listaPersonajes)
    {
        Random rand = new();
        return listaPersonajes[rand.Next(listaPersonajes.Count)];
    }
    public static Personaje Pelea(Personaje jugador1, Personaje jugador2)
    {
        int ronda = 1;
        int turno = 1;
        int danio;
        while (jugador1.Salud > 0 && jugador2.Salud >0)
        {
            Console.WriteLine("------> Ronda: "+ronda);
            Console.WriteLine(jugador1.MostrarPersonaje());
            Console.WriteLine(jugador2.MostrarPersonaje());
            if (turno == 1)
            {
                danio = DanioProvocado(jugador1, jugador2);
                jugador2.Salud -= danio;
                turno = 2;
            } else
            {
                danio = DanioProvocado(jugador2, jugador1);
                jugador1.Salud -= danio;
                turno = 1;
            }
            ronda++;
        }
        if (jugador1.Salud <= 0)
        {
            jugador2.Salud += 10;
            Console.WriteLine("Ganador: Jugador 2");
            Console.WriteLine(jugador2.MostrarPersonaje());
            return jugador2;
        } else
        {
            jugador1.Salud += 10;
            Console.WriteLine("Ganador: Jugador 1");
            Console.WriteLine(jugador1.MostrarPersonaje());
            return jugador1;
        }
    }
}