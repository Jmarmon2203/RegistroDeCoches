using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;

namespace AvaloniaApplication1;

public partial class MainWindow : Window
{
    private List<Control> listaCoches; //Lista de los controles usados en el diseño
    private Bitmap[] logosCoches; //Array que guarda las imagenes de los logos
    private List<Coche> listaDeCoches; //Lista que usare para almacenar los datos de los coches creados
    private int indiceActual; //Indice del coche actual dentro de la lista
    private int totalCoches; //Indica la cantidad de coches creados
    
    public MainWindow()
    {
        //Inicializo los componentes
        InitializeComponent();
        
        listaCoches = new List<Control>(); //Inicializo la lista de controles
        logosCoches = new Bitmap[5]; //Inicializo el array de imagenes
        listaDeCoches = new List<Coche>(); //Inicializo la lista de coches
        indiceActual = 0; //Inicializo el indice
        totalCoches = 0; //icializo el total de coches
        
        //Cargo las imagenes de los logos dentro del array
        logosCoches[0] = new Avalonia.Media.Imaging.Bitmap("LogosCoches\\BMW.png");
        logosCoches[1] = new Avalonia.Media.Imaging.Bitmap("LogosCoches\\Mercedes.png");
        logosCoches[2] = new Avalonia.Media.Imaging.Bitmap("LogosCoches\\Nissan.png");
        logosCoches[3] = new Avalonia.Media.Imaging.Bitmap("LogosCoches\\Toyota.png");
        logosCoches[4] = new Avalonia.Media.Imaging.Bitmap("LogosCoches\\Volkswagen.png");
        
        //Cargo los controles dentro de la lista de los controles
        //Labels
        listaCoches.Add(lblMarca);
        listaCoches.Add(lblModelo);
        listaCoches.Add(lblPrecio);
        listaCoches.Add(lblGama);
        listaCoches.Add(lblMotor);
        listaCoches.Add(lblClase);
        listaCoches.Add(lblTitulo);
        listaCoches.Add(lblEslogan);
        listaCoches.Add(lblTotalRegistros);
        
        //TextBoxes
        listaCoches.Add(tbMarca);
        listaCoches.Add(tbModelo);
        listaCoches.Add(tbPrecio);
        listaCoches.Add(tbGama);
        listaCoches.Add(tbMotor);
        listaCoches.Add(tbClase);
        
        //Botones
        listaCoches.Add(btnAnterior);
        listaCoches.Add(btnSiguiente);
        listaCoches.Add(btnCrear);
        listaCoches.Add(btnEliminar);
        listaCoches.Add(btnModificar);
        
        //Elemento Imaage para los logos
        listaCoches.Add(imgLogo);
    }
    
    //Metodo para el boton Crear
    private async void BtnCrear_OnClick(object? sender, RoutedEventArgs e)
    {
        //Creo un objeto de tipo Dialog
        Dialog dialog = new Dialog();
        //Y creo un objeto result para saber si pulsamos aceptar o cancelar
        bool? result = await dialog.ShowDialog<bool?>(this);
        
        //Creo variables temporales para guardar los datos
        string marcaTemp = string.Empty;
        string modeloTemp = string.Empty;
        string precioTemp = string.Empty;
        string gamaTemp = string.Empty;
        string claseTemp = string.Empty;
        string motorTemp = string.Empty;
        string esloganTemp = string.Empty;
        Bitmap imagenTemp = null;
        
        //Creo un condicional en el que si se pulsa el boton de aceptar se ejecuta lo siguiente
        while (result.HasValue && result.Value)
        {
            //Creo un diccionario para guardar los datos
            Dictionary<string, string> cocheData = dialog.Coche;
            
            //En el atributo marcaTemp guardo la marca del coche y establezco tanto la imagen como el logo dependiendo de la marca introducida
            //Si la marca no esta registrada, salta un ShowMessageBox
            marcaTemp = cocheData["Marca"];
            if (marcaTemp == "BMW")
            {
                imagenTemp = logosCoches[0];
                esloganTemp = "Eslogan: El placer de conducir";
            }
            else if (marcaTemp == "Mercedes")
            {
                imagenTemp = logosCoches[1];
                esloganTemp = "Eslogan: Lo mejor o nada";
            }
            else if (marcaTemp == "Nissan")
            {
                imagenTemp = logosCoches[2];
                esloganTemp = "Eslogan: Esto es Nissan";
            }
            else if (marcaTemp == "Toyota")
            {
                imagenTemp = logosCoches[3];
                esloganTemp = "Eslogan: Let's Go Places";
            }
            else if (marcaTemp == "Volkswagen")
            {
                imagenTemp = logosCoches[4];
                esloganTemp = "Eslogan: Das Auto";
            }
            else
            {
                ShowMessageBox("Esa marca no está registrada");
                result = await dialog.ShowDialog<bool?>(this);
                continue;
            }
            
            //Guardo el modelo introducido
            modeloTemp = cocheData["Modelo"];
            
            //Guardo el precio introducido
            //Si el valor es negativo o un caracter no numerico, salta un ShowMessageBox
            precioTemp = cocheData["Precio"];
            if (float.TryParse(precioTemp, out float precio))
            {
                if (precio < 0)
                {
                    ShowMessageBox("El precio no puede ser negativo");
                    result = await dialog.ShowDialog<bool?>(this);
                    continue;
                }
            }
            else
            {
                ShowMessageBox("Introduzca un valor numérico para el precio");
                result = await dialog.ShowDialog<bool?>(this);
                continue;
            }
            
            //Guardo la gama introducida
            //Si la gama no es A, M o B salta un ShowMessageBox
            gamaTemp = cocheData["Gama"];
            if (gamaTemp != "A" && gamaTemp != "M" && gamaTemp != "B")
            {
                ShowMessageBox("Introduzca una gama correcta");
                result = await dialog.ShowDialog<bool?>(this);
                continue;
            }
            
            //Guardo la clase introducida
            claseTemp = cocheData["Clase"];
            
            //Guardo el motor introducido
            motorTemp = cocheData["Motor"];
            
            //Cierro el bucle
            break;
        }
        
        //Creo un condicional en el que si se pulsa el boton de aceptar se ejecuta lo siguiente
        if (result.HasValue && result.Value)
        {
            tbMarca.Text = marcaTemp;
            tbModelo.Text = modeloTemp;
            tbPrecio.Text = precioTemp;
            tbGama.Text = gamaTemp;
            tbClase.Text = claseTemp;
            tbMotor.Text = motorTemp;
            
            imgLogo.Source = imagenTemp;
            lblEslogan.Content = esloganTemp;

            Coche nuevoCoche = new Coche(marcaTemp, modeloTemp, precioTemp, gamaTemp, claseTemp, motorTemp, imagenTemp, esloganTemp);
            
            //Añado el nuevo coche a la lista
            listaDeCoches.Add(nuevoCoche);
            
            //Actualiza el indice y el total de coches
            indiceActual = listaDeCoches.Count - 1;
            totalCoches = listaDeCoches.Count;
            
            //Muestro el numero de registros que existen a medida que se vayan creando/eliminando
            lblTotalRegistros.Content = $"Registros: {totalCoches}";
            
            //Llama al metodo para MostrarCocheActual
            MostrarCocheActual();
        }
    }

    //Metodo que modifica un registro
    private async void BtnModificar_OnClick(object? sender, RoutedEventArgs e)
    {
        //Condicinal que comprueba si existen coches dentro de la lista
        if (totalCoches > 0 && indiceActual >= 0 && indiceActual < totalCoches)
        {
            //El metood modificar funciona como el metodo crear
            //Abre de nuevo el dialog, solo que con los datos que se introdujeron al pulsar el boton Crear con el objetivo de modificarlos
            Coche cocheActual = listaDeCoches[indiceActual];
            Dialog dialog = new Dialog(cocheActual);
            
            bool? result = await dialog.ShowDialog<bool?>(this);
            
            //Si se pulsa aceptar se ejecuta lo siguiente
            while (result.HasValue && result.Value)
            {
                //Creo un diccionario para guardar los datos
                Dictionary<string, string> cocheData = dialog.Coche;
                
                //Guardo todos los datos y compruebo que sean correctos
                //Que la marca sea una de las que estan registradas
                cocheActual.Marca = cocheData["Marca"];
                if (cocheActual.Marca == "BMW")
                {
                    cocheActual.Imagen = logosCoches[0];
                    cocheActual.Eslogan = "Eslogan: El placer de conducir";
                }
                else if (cocheActual.Marca == "Mercedes")
                {
                    cocheActual.Imagen = logosCoches[1];
                    cocheActual.Eslogan = "Eslogan: Lo mejor o nada";
                }
                else if (cocheActual.Marca == "Nissan")
                {
                    cocheActual.Imagen = logosCoches[2];
                    cocheActual.Eslogan = "Eslogan: Esto es Nissan";
                }
                else if (cocheActual.Marca == "Toyota")
                {
                    cocheActual.Imagen = logosCoches[3];
                    cocheActual.Eslogan = "Eslogan: Let's Go Places";
                }
                else if (cocheActual.Marca == "Volkswagen")
                {
                    cocheActual.Imagen = logosCoches[4];
                    cocheActual.Eslogan = "Eslogan: Das Auto";
                }
                else
                {
                    ShowMessageBox("Esa marca no está registrada");
                    result = await dialog.ShowDialog<bool?>(this);
                    continue;
                }
                
                cocheActual.Modelo = cocheData["Modelo"];
                
                //En el precio compruebo que sea un valor numerico y positivo
                cocheActual.Precio = cocheData["Precio"];
                if (float.TryParse(cocheActual.Precio, out float precio))
                {
                    if (precio < 0)
                    {
                        ShowMessageBox("El precio no puede ser negativo");
                        result = await dialog.ShowDialog<bool?>(this);
                        continue;
                    }
                }
                else
                {
                    ShowMessageBox("Introduzca un valor numérico para el precio");
                    result = await dialog.ShowDialog<bool?>(this);
                    continue;
                }
                
                //Que la gama sea A, M o B
                cocheActual.Gama = cocheData["Gama"];
                if (cocheActual.Gama != "A" && cocheActual.Gama != "M" && cocheActual.Gama != "B")
                {
                    ShowMessageBox("Introduzca una gama correcta");
                    result = await dialog.ShowDialog<bool?>(this);
                    continue;
                }

                cocheActual.Clase = cocheData["Clase"];
                
                cocheActual.TipoMotor = cocheData["Motor"];
                
                //Muestro el coche actual
                MostrarCocheActual();
                
                //Y por ultimo cierro el bucle
                break;
            }
        }
        //Sino existen registros de coches, muestra un mensaje
        else
        {
            ShowMessageBox("No hay registros para modificar.");
        }
    }
    
    //Metodo para eliminar un registro de coche
    private void BtnEliminar_OnClick(object? sender, RoutedEventArgs e)
    {
        //Condicinal que comprueba si existen coches dentro de la lista
        if (totalCoches > 0 && indiceActual >= 0 && indiceActual < totalCoches)
        {
            Coche cocheAEliminar = listaDeCoches[indiceActual];
            
            //Se elimina el registro
            listaDeCoches.RemoveAt(indiceActual);
            totalCoches = listaDeCoches.Count;
            
            //En este caso restaria 1 al numero de regsitros
            lblTotalRegistros.Content = $"Registros: {totalCoches}";
            
            //Si existe al menos un registro se ejecuta lo siguiente
            if (totalCoches > 0)
            {
                //Comprueba si el índice actual es mayor o igual al total de coches
                //Para asegurarse de que el índice actual esté dentro de los límites válidos después de la eliminación
                if (indiceActual >= totalCoches)
                {
                    //Esto establece el indice actual en la ultima posicion
                    indiceActual = totalCoches - 1;
                }
                //Y se muestra el registro actual despues de borrar el anterior
                MostrarCocheActual();
            }
            //En el caso de que no queden registros en la lista al borrar, se vaciaran los text boxes, el check box, la imagen y el eslogan
            else
            {
                LimpiarCampos();
            }
        }
        //Sino existen registros, se muestra un mensaje
        else
        {
            ShowMessageBox("No hay registros para eliminar.");
        }
    }
    
    //Metodo que muestra el coche anterior
    private void BtnAnterior_OnClick(object? sender, RoutedEventArgs e)
    {
        //Comprueba si el indice actual es mayor que 0, es decir, no es el primero
        if (indiceActual > 0)
        {
            //Retrocede un indice y muestra el coche anterior
            indiceActual--;
            MostrarCocheActual();
        }
    }
    
    //Metodo que muestra el coche siguiente
    private void BtnSiguiente_OnClick(object? sender, RoutedEventArgs e)
    {
        //Comprueba si el indice actual es menor que el total de coches -1, es decir, no es el ultimo
        if (indiceActual < listaCoches.Count - 1)
        {
            //Avanza un indice y muestra el coche siguiente
            indiceActual++;
            MostrarCocheActual();
        }
    }
    
    //Metodo que muestra el coche actual
    private void MostrarCocheActual()
    {
        //Condicinal que comprueba si existen coches dentro de la lista
        if (totalCoches > 0 && indiceActual >= 0 && indiceActual < totalCoches)
        {
            Coche cocheActual = listaDeCoches[indiceActual];
            tbMarca.Text = cocheActual.Marca;
            tbModelo.Text = cocheActual.Modelo;
            tbPrecio.Text = cocheActual.Precio;
            tbGama.Text = cocheActual.Gama;
            tbClase.Text = cocheActual.Clase;
            tbMotor.Text = cocheActual.TipoMotor;

            imgLogo.Source = cocheActual.Imagen;
            lblEslogan.Content = cocheActual.Eslogan;
        }
    }
    
    //Metodo que muestra el messageBox
    private void ShowMessageBox(string message)
    {
        //Creo un nuevo messageBox
        var messageBox = new Window
        {
            //Le asigno un textBlock para no poder editar su contenio
            //Y le pongo el titulo de mensaje
            Content = new TextBlock { Text = message },
            Title = "Mensaje",
            Width = 400,
            Height = 50
        };

        messageBox.ShowDialog(this);
    }
    
    //Metodo que limpia los campos al pulsar el boton eliminar
    private void LimpiarCampos()
    {
        tbMarca.Text = string.Empty;
        tbModelo.Text = string.Empty;
        tbPrecio.Text = string.Empty;
        tbGama.Text = string.Empty;
        tbClase.Text = string.Empty;
        tbMotor.Text = string.Empty;
        imgLogo.Source = null;
        lblEslogan.Content = "Eslogan:";
    }
    
    //Metodo para guardar los datos en un archivo
    private void BtnGuardar_OnClick(object? sender, RoutedEventArgs e)
    {
        //Con un try catch capturo la excepcion en el caso de que hubiese algun error al guardar los datos
        try
        {
            //Compruebo si no hay registros y en caso afirmativo muestra un mensaje
            if (totalCoches == 0)
            {
                ShowMessageBox("No hay datos para guardar.");
                return;
            }
            
            //Con un FileStream creo un archivo llamado database.data
            using (FileStream fileStream = new FileStream("database.data", FileMode.Create))
            {
                //Para escribirlos en binario uso un BinaryWriter
                using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
                {
                    //Y despues por cada dato lo convierte a binario para escribirlo en el archivo
                    binaryWriter.Write(totalCoches);
                    foreach (Coche coche in listaDeCoches)
                    {
                        binaryWriter.Write(coche.Marca);
                        binaryWriter.Write(coche.Modelo);
                        binaryWriter.Write(coche.Precio);
                        binaryWriter.Write(coche.Gama);
                        binaryWriter.Write(coche.Clase);
                        binaryWriter.Write(coche.TipoMotor);
                        binaryWriter.Write(coche.Eslogan);
                    }
                }
            }
            
            //Finalmente muestra un mensaje de que se han guardado correctamente
            ShowMessageBox("Datos guardados correctamente.");
        }
        //En caso de que salte la excepcion muestra el siguiente mensaje
        catch (Exception ex)
        {
            ShowMessageBox($"Error al guardar los datos: {ex.Message}");
        }
    }

    //Metodo para cargar los datos de un archivo
    private void BtnCargar_OnClick(object? sender, RoutedEventArgs e)
    {
        //Con un try catch capturo la excepcion en el caso de que hubiese algun error al cargar los datos
        try
        {
            //Compruebo que el archivo database.data exista
            if (File.Exists("database.data"))
            {
                //En este caso abre el archivo en caso de que si exista
                using (FileStream fileStream = new FileStream("database.data", FileMode.Open))
                {
                    //Con un BinaryReader lee los datos binarios
                    using (BinaryReader binaryReader = new BinaryReader(fileStream))
                    {
                        //Para finalmente leer todos los datos y mostrarlos 
                        totalCoches = binaryReader.ReadInt32();
                        listaDeCoches.Clear();
                        for (int i = 0; i < totalCoches; i++)
                        {
                            string marca = binaryReader.ReadString();
                            string modelo = binaryReader.ReadString();
                            string precio = binaryReader.ReadString();
                            string gama = binaryReader.ReadString();
                            string clase = binaryReader.ReadString();
                            string tipoMotor = binaryReader.ReadString();
                            string eslogan = binaryReader.ReadString();

                            Coche coche = new Coche(marca, modelo, precio, gama, clase, tipoMotor, GetLogoByMarca(marca), eslogan);
                            listaDeCoches.Add(coche);
                        }
                        
                        //Establece el indice actual en 0 y el numero de registros
                        indiceActual = 0;
                        lblTotalRegistros.Content = $"Registros: {totalCoches}";
                        
                        //Si hay datos, los muestra
                        if (totalCoches > 0)
                        {
                            MostrarCocheActual();
                        }
                        //Sino limpia los compos 
                        else
                        {
                            LimpiarCampos();
                        }
                        
                        //Muestra un mensaje de que se han cargado exitosamente
                        ShowMessageBox("Datos cargados correctamente.");
                    }
                }
            }
            //En el caso de que el archivo no exista, muestra el siguiente mensaje
            else
            {
                ShowMessageBox("No se encontró el archivo de datos.");
            }
        }
        //En caso de que salte la excepcion muestra el siguiente mensaje
        catch (Exception ex)
        {
            ShowMessageBox($"Error al cargar los datos: {ex.Message}");
        }
    }
    
    //Metodo para mostrar la imagen del logo que corresponda a su marca
    private Bitmap GetLogoByMarca(string marca)
    {
        switch (marca)
        {
            case "BMW":
                return logosCoches[0];
            case "Mercedes":
                return logosCoches[1];
            case "Nissan":
                return logosCoches[2];
            case "Toyota":
                return logosCoches[3];
            case "Volkswagen":
                return logosCoches[4];
            default:
                return null;
        }
    }
}