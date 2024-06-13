namespace Quiz2_Grupo2.Views;

public partial class verAutores : ContentPage
{
    private Controllers.AutorController AutorController;
    private List<Models.Autor> autores;
    public verAutores()
    {
        InitializeComponent();
        AutorController = new Controllers.AutorController();
    }

    //Metodo que permite mostrar la lista mientras la pagina se esta mostrando o cargando
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Obtiene la lista de personas de la base de datos
        autores = await AutorController.getListAutor();

        // Coloca la lista en el collection view
        collectionView.ItemsSource = autores;
    }

    private void searchBar_SearchButtonPressed(object sender, EventArgs e)
    {
        BuscarAutores(searchBar.Text);
    }

    //Funcion para realizar una busqueda en la lista o base de datos
    private void BuscarAutores(string query)
    {

        //Usa LINQ (Language-Integrated Query) (es una característica en el framework .NET que proporciona una sintaxis estandarizada
        //y declarativa para consultar y manipular datos de diferentes tipos de fuentes, como colecciones,
        //bases de datos, XML, entre otros.) en una expresion tipo lambda para filtrar la informacion de la base 
        //de datos y mostrar los resultados basados en la busqueda.

        var results = autores
            .Where(author => author.Nombres?.ToLowerInvariant().Contains(query.ToLowerInvariant()) == true ||
                             author.Nacionalidad?.ToLowerInvariant().Contains(query.ToLowerInvariant()) == true)
            .ToList();

        collectionView.ItemsSource = new List<Models.Autor>(results);
    }

    private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(e.NewTextValue))
        {
            collectionView.ItemsSource = autores;
        }
    }

    private void btnRegresar_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}