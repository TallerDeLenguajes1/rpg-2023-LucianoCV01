namespace EspacioPersonaje
{
    public class Personaje
    {
        // Datos
        string? tipo;
        string? nombre;
        string? apodo;
        DateTime fechaDeNacimiento;
        int edad; // 0 a 300
        // Caracteristicas
        int velocidad; // 1 a 10
        int destreza; // 1 a 5
        int fuerza; // 1 a 10
        int nivel; // 1 a 10
        int armadura; // 1 a 10
        int salud; // 100

        public string? Tipo { get => tipo; set => tipo = value; }
        public string? Nombre { get => nombre; set => nombre = value; }
        public string? Apodo { get => apodo; set => apodo = value; }
        public DateTime FechaDeNacimiento { get => fechaDeNacimiento; set => fechaDeNacimiento = value; }
        public int Edad { get => edad; set => edad = value; }
        public int Velocidad { get => velocidad; set => velocidad = value; }
        public int Destreza { get => destreza; set => destreza = value; }
        public int Fuerza { get => fuerza; set => fuerza = value; }
        public int Nivel { get => nivel; set => nivel = value; }
        public int Armadura { get => armadura; set => armadura = value; }
        public int Salud { get => salud; set => salud = value; }

        public string MostrarPersonaje()
        {
            return $" '{Apodo}'\n {Nombre} ---> {Tipo}\n Velocidad: {Velocidad}\n Destreza: {Destreza}\n Fuerza: {Fuerza}\n Nivel: {Nivel}\n Armadura: {Armadura}\n Salud: {Salud}";
        }
    }
}