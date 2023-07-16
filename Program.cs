using EspacioPersonaje;
using EspacioFabrica;
using EspacioPersonajesJson;
using EspacioMensajes;
internal class Program
{
    private static void Main(string[] args)
    {
        Mensajes expresiones = new();
        Console.Write("\u001b[38;2;242;199;119m{0}\u001b[0m", expresiones.titulo); // Color: #F2C777
        Console.WriteLine("\u001b[38;2;166;119;78m{0}\u001b[0m", expresiones.subtitulo); // Color: #A6774E
        string? numero;
        do
        {
        Console.WriteLine("------> ELIJA EL MODO DE JUEGO: ");
        Console.WriteLine(expresiones.modoJuego);
        numero = Console.ReadLine();
        } while (numero != "1" && numero != "2");

        const int cantidadPersonajes = 10;
        const string nombreArchivoJson = "Personajes.json";
        const string nombreJsonPersonajes = "PersonajesMistborn.json";
        PersonajesJson archivoJson = new();
        List<Personaje>? listadoPersonajes = new();

        if (numero == "1")
        {
            listadoPersonajes = archivoJson.LeerPersonajes(nombreJsonPersonajes);
        } else
        {
            if (archivoJson.Existe(nombreArchivoJson))
            {
                listadoPersonajes = archivoJson.LeerPersonajes(nombreArchivoJson);
            } else
            {
                FabricaDePersonajes crear = new();
                listadoPersonajes = crear.CargarListaPersonajesAleatorios(cantidadPersonajes);
                archivoJson.GuardarPersonajes(listadoPersonajes, nombreArchivoJson);
            }    
        }
        // Mejorar Segmentacion
        if (listadoPersonajes != null)
        {
            Console.Clear();
            for (int i = 0; i < cantidadPersonajes; i++)
            {
                Console.WriteLine("╔═════════════════ Personaje {0} ════════════════╗", i);
                Console.WriteLine(listadoPersonajes[i].MostrarPersonaje());
                Console.WriteLine("╚══════════════════════════════════════════════╝");     
            } 
            
            Personaje? jugador1=null;
            if (numero == "1")
            {
                Console.WriteLine("ELIJA SU PERSONAJE: ");
                string? numPersonaje = Console.ReadLine();
                if (numPersonaje != null)
                {
                    int numPersona = int.Parse(numPersonaje);             
                    jugador1 = listadoPersonajes[numPersona];
                    listadoPersonajes.Remove(jugador1);
                }
            } else
            {
                jugador1 = PersonajeAleatorio(listadoPersonajes);
                listadoPersonajes.Remove(jugador1);
            }    
            Console.Clear();
            if (jugador1 !=null)
            { 
                int flag = 1;
                while (listadoPersonajes.Count != 1 && flag==1)
                {
                    Console.Clear();
                    var jugador2 = PersonajeAleatorio(listadoPersonajes);
                    listadoPersonajes.Remove(jugador2);
                    var ganador = Pelea(jugador1, jugador2);
                    if (ganador != jugador1)
                    {
                        Console.WriteLine("Perdedor");
                        flag=0;
                    }
                    Console.ReadKey();
                }    
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