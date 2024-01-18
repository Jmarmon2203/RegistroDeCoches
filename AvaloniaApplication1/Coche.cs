using Avalonia.Media.Imaging;

namespace AvaloniaApplication1;

//Clase coche que almacena los datos de un coche
public class Coche
{
    //Atributos que tendra el coche
    //Uso de get y set para poder acceder a los atributos
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public string Precio { get; set; }
    public string Gama { get; set; }
    public string Clase { get; set; }
    public string TipoMotor { get; set; }
    public Bitmap Imagen { get; set; }
    public string Eslogan { get; set; } 
    
    //Constructor de la clase Coche que inicializa los atributos
    public Coche(string marca, string modelo, string precio, string gama, string clase, string tipoMotor, Bitmap imagen, string eslogan)
    {
        Marca = marca;
        Modelo = modelo;
        Precio = precio;
        Gama = gama;
        Clase = clase;
        TipoMotor = tipoMotor;
        Imagen = imagen;
        Eslogan = eslogan;
    }
}
