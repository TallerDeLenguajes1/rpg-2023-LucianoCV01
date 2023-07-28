using System.Net;
using System.Text.Json;
using EspacioPersonaje;
using EspacioFabrica;
using EspacioPersonajesJson;
using EspacioMensajes;
using EspacioFrutas;
internal class Program
{
    private static void Main(string[] args)
    {
        const string nombreArchivoJson = "Personajes.json";
        PersonajesJson archivoJson = new();
        const int cantidadPersonajes = 10;
        List<Personaje>? listadoPersonajes = new();
        if (archivoJson.Existe(nombreArchivoJson))
        {
            listadoPersonajes = archivoJson.LeerPersonajes(nombreArchivoJson);
        } else
        {
            FabricaDePersonajes crear = new();
            listadoPersonajes = crear.ListaPersonajesAleatorios(cantidadPersonajes);
            archivoJson.GuardarPersonajes(listadoPersonajes, nombreArchivoJson);
        }    

        Mensajes expresiones = new();
        string? opcionJuego;
        do
        {
            Console.WriteLine("\u001b[38;2;242;199;119m{0}\u001b[0m", expresiones.titulo); // Color: #F2C777
            Console.WriteLine("------> ELIJA EL MODO DE JUEGO: ");
            Console.WriteLine(expresiones.modoJuego);
            opcionJuego = Console.ReadLine();
        } while (opcionJuego != "1" && opcionJuego != "2");

        if (listadoPersonajes != null)
        {
            Console.Clear();
            Personaje? jugador1=null;
            if (opcionJuego == "1")
            {
                for (int i = 0; i < cantidadPersonajes; i++)
                {
                    Console.WriteLine("╔═════════════════ Personaje {0} ════════════════╗", i);
                    Console.WriteLine(listadoPersonajes[i].MostrarPersonaje());
                    Console.WriteLine("╚══════════════════════════════════════════════╝");     
                } 
                string? numPersonaje;
                do
                {
                    Console.WriteLine("ELIJA SU PERSONAJE: ");
                    numPersonaje = Console.ReadLine();
                } while (numPersonaje != "0" && numPersonaje != "1" && numPersonaje != "2" && numPersonaje != "3" && numPersonaje != "4" && numPersonaje != "5" && numPersonaje != "6" && numPersonaje != "7" && numPersonaje != "8" && numPersonaje != "9");
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
            
            if (jugador1 !=null)
            { 
                int flag = 1;
                while (listadoPersonajes.Count != 1 && flag==1)
                {
                    var jugador2 = PersonajeAleatorio(listadoPersonajes);
                    listadoPersonajes.Remove(jugador2);

                    Console.WriteLine("╔════════════════════ PELEA ═══════════════════╗");
                    Console.WriteLine(jugador1.MostrarPersonaje());
                    Console.WriteLine("        --------------- VS ---------------");
                    Console.WriteLine(jugador2.MostrarPersonaje());
                    Console.WriteLine("╚══════════════════════════════════════════════╝");

                    var ganador = Pelea(jugador1, jugador2);

                    Console.ReadKey();
                    if (ganador == jugador1)
                    {
                        Console.WriteLine("╔═════════════ GANADOR: JUGADOR 1 ═════════════╗");
                        Console.WriteLine(jugador1.MostrarPersonaje());
                        Console.WriteLine("╚══════════════════════════════════════════════╝");
                        Recompensa(jugador1);
                    } else
                    {
                        Console.WriteLine("╔═════════════ GANADOR: JUGADOR 2 ═════════════╗");
                        Console.WriteLine(jugador2.MostrarPersonaje());
                        Console.WriteLine("╚══════════════════════════════════════════════╝");
                        flag=0;
                    }
                }   
                if (flag == 0)
                {
                    Console.WriteLine("\u001b[38;2;217;37;37m{0}\u001b[0m", expresiones.gameOver); // color: #D92525
                } else
                {
                    Console.WriteLine("\u001b[38;2;3;166;60m{0}\u001b[0m", expresiones.ganasta); // color: #03A63C
                }
            }
            
        }
    }
    public static int DanioProvocado(Personaje atacante, Personaje defensor)
    {
        Random rand = new();
        const int ajuste = 250;
        double ataque = atacante.Destreza * atacante.Fuerza * atacante.Nivel;
        int efectividad = rand.Next(50, 101);
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
            Console.WriteLine($"╔═════════════════════ RONDA {ronda} ════════════════╗\n");
            if (turno == 1)
            {
                danio = DanioProvocado(jugador1, jugador2);
                Console.WriteLine($"           JUGADOR {turno} ATACA ---> {danio}\n");
                jugador2.Salud -= danio;
                turno = 2;
            } else
            {
                danio = DanioProvocado(jugador2, jugador1);
                Console.WriteLine($"           JUGADOR {turno} ATACA ---> {danio}\n");
                jugador1.Salud -= danio;
                turno = 1;
            }
            Console.WriteLine($" {jugador1.Apodo} ------> Salud: {jugador1.Salud}\n");
            Console.WriteLine($" {jugador2.Apodo} ------> Salud: {jugador2.Salud}\n");
            Console.WriteLine("╚══════════════════════════════════════════════╝");
            ronda++;
            Console.ReadKey();
            Console.Clear();
        }
        if (jugador1.Salud <= 0)
        {
            return jugador2;
        } else
        {
            return jugador1;
        }
    }
    public static List<Fruta>? ObtenerFrutas()
    {
        var url = $"https://www.fruityvice.com/api/fruit/all";
        var request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.ContentType = "application/json";
        request.Accept = "application/json";
        try
        {
            using (WebResponse response = request.GetResponse())
            {
                using (Stream strReader = response.GetResponseStream())
                {
                    if (strReader == null) return null;
                    using (StreamReader objReader = new StreamReader(strReader))
                    {
                        string responseBody = objReader.ReadToEnd();
                        List<Fruta>? listaFrutas = JsonSerializer.Deserialize<List<Fruta>>(responseBody);
                        return listaFrutas;
                    }
                }
            }
        }
        catch (WebException ex)
        {
             Console.WriteLine("Problemas de acceso a la API");
             return null;
        }
    }
    public static void Recompensa(Personaje ganador)
    {
        Random rand = new();
        string[] frase = new string[]
        {
            "   1. {fruta}: regenera la salud",
            "   2. {fruta}: aumenta la fuerza en 1",
            "   3. {fruta}: aumenta la armadura en 1"
        };
        var listaFrutas = ObtenerFrutas();

        Console.WriteLine("╔═════════════════ RECOMPENSA ═════════════════╗");
        for (int i = 0; i < 3; i++)
        {
            if (listaFrutas != null)
            {
                var fruta = listaFrutas[rand.Next(listaFrutas.Count)];
                listaFrutas.Remove(fruta);
                frase[i] = frase[i].Replace("{fruta}", fruta.name);
            }
            Console.WriteLine(frase[i]);
        }            
        Console.WriteLine("╚══════════════════════════════════════════════╝");
        string? numRecompensa;
        do
        {
            Console.WriteLine("------> ELIJA SU RECOMPENSA: ");
            numRecompensa = Console.ReadLine();
        } while (numRecompensa != "1" && numRecompensa != "2" && numRecompensa != "3");
        switch (numRecompensa)
        {
            case "1":
                ganador.Salud = 100;
                break;
            case "2":
                ganador.Fuerza += 1;
                break;
            case "3":
                ganador.Armadura += 1;
                break;    
        }
    }
}