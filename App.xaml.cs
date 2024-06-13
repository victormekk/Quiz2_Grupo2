namespace Quiz2_Grupo2
{
    public partial class App : Application
    {
        public static Controllers.AutorController AutorController { get; private set; }
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            AutorController = new Controllers.AutorController();
        }
    }
}
