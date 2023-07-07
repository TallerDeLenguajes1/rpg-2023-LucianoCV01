using System.Text.Json;
using EspacioPersonaje;
namespace EspacioPersonajesJson
{
    public class PersonajesJson
    {
        public void GuardarPersonajes(List<Personaje> listaPersonajes, string nombreArchivo)
        {
            string personajesJson = JsonSerializer.Serialize(listaPersonajes);
            using (FileStream archivoAbierto = new(nombreArchivo, FileMode.OpenOrCreate))
            {
                using (StreamWriter archivoEscribir = new(archivoAbierto))
                {
                    archivoEscribir.WriteLine(personajesJson);
                    archivoEscribir.Close();
                }
            }
        }
        public List<Personaje>? LeerPersonajes(string nombreArchivo)
        {
            string documentoJson;
            using (FileStream archivoAbierto = new(nombreArchivo, FileMode.Open))
            {
                using (StreamReader archivoLeer = new(archivoAbierto))
                {
                    documentoJson = archivoLeer.ReadToEnd();
                    archivoLeer.Close();
                }   
            }
            var listadoPersonajes = JsonSerializer.Deserialize<List<Personaje>>(documentoJson);
            return listadoPersonajes;
        }
        public bool Existe(string nombreArchivo)
        {
            if (File.Exists(nombreArchivo))
            {
                FileInfo fileInfo = new(nombreArchivo);
                return fileInfo.Length > 0;
            }
            return false;
        }
    }
}