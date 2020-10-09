using System.Windows;

namespace Cursos_WPF
{
    /// <summary>
    /// Interaction logic for Cursos.xaml
    /// </summary>
    public partial class Cursos : Window
    {
        public Cursos()
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
        /// This function opens the Usuarios window and closes the current one.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Usuarios_Click(object sender, RoutedEventArgs e)
        {
            Usuarios UsuariosWindow = new Usuarios();
            UsuariosWindow.Show();

            this.Close();
        }
    }
}
