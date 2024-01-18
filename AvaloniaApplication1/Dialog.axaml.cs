using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AvaloniaApplication1;

//Ventana nueva que actua como Dialog para añadir un coche al pulsar el boton Crear
public partial class Dialog : Window
{
    //Diccionario que almacena los datos del coche
    public Dictionary<String, String> Coche
    {
        get;
    }
    
    //Constructor de la clase Dialog que inicializa los atributos
    public Dialog()
    {
        InitializeComponent();
        Coche = new Dictionary<String, String>();
    }
    
    //Constructor que toma un objeto Coche, para utilizar el boton Modificar
    public Dialog(Coche coche) : this()
    {
        //Establece los valores iniciales en el Dialog en base al objeto Coche recibido
        tbMarca.Text = coche.Marca;
        tbModelo.Text = coche.Modelo;
        tbPrecio.Text = coche.Precio;
        tbGama.Text = coche.Gama;
        tbClase.Text = coche.Clase;
        tbMotor.Text = coche.TipoMotor;
    }
    
    //Getters y Setters de los atributos
    public string Marca => tbMarca.Text;
    public string Modelo => tbModelo.Text;
    public float Precio => float.Parse(tbPrecio.Text);
    public char Gama => tbGama.Text.FirstOrDefault();
    public string Clase => tbClase.Text;
    public string Motor => tbMotor.Text;

    //Metodo para el boton Aceptar que guarda los datos en el diccionario
    private void BtnAceptar_OnClick(object? sender, RoutedEventArgs e)
    {
        Coche["Marca"] = tbMarca.Text;
        Coche["Modelo"] = tbModelo.Text;
        Coche["Precio"] = tbPrecio.Text;
        Coche["Gama"] = tbGama.Text.FirstOrDefault().ToString();
        Coche["Clase"] = tbClase.Text;
        Coche["Motor"] = tbMotor.Text;
        
        //Pulsar este boton establece el result en true
        Close(true);
    }
    
    //Metodo para el boton Cancelar
    private void BtnCancelar_OnClick(object? sender, RoutedEventArgs e)
    {
        //Pulsar este boton establece el result en false
        Close(false);
    }
}