using System.Windows;

namespace Cursos_WPF
{
    /// <summary>
    /// Interaction logic for Usuarios.xaml
    /// </summary>
    public partial class Usuarios : Window
    {
        public Usuarios()
        {
            InitializeComponent();
        }


        /// <summary>
        /// This function opens the Home window and closes the current one.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Inicio_Click(object sender, RoutedEventArgs e)
        {
            MainWindow InicioWindow = new MainWindow();
            InicioWindow.Show();

            this.Close();
        }


        /// <summary>
        /// This function opens the Cursos window and closes the current one.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cursos_Click(object sender, RoutedEventArgs e)
        {
            Cursos CursosWindow = new Cursos();
            CursosWindow.Show();

            this.Close();
        }
    }
}
