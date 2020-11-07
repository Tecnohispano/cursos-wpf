using Cursos_WPF.Model;
using Cursos_WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

namespace Cursos_WPF
{
    /// <summary>
    /// Interaction logic for Usuarios.xaml
    /// </summary>
    public partial class Usuarios : Window
    {
        private readonly TecnohispanoEntities TecnohispanoDb;

        /// <summary>
        /// Constructor.
        /// It creates a new TecnohispanoEntities so database operations can be performed.
        /// </summary>
        public Usuarios()
        {
            InitializeComponent();
            TecnohispanoDb = new TecnohispanoEntities();

            CmbTipoUsuario.ItemsSource = GetUserTypes();
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


        /// <summary>
        /// This function returns all the available user Types.
        /// </summary>
        /// <returns></returns>
        private List<string> GetUserTypes()
        {
            List<string> UserTypes = new List<string>();

            try
            {
                foreach (UserType UserType in TecnohispanoDb.UserTypes)
                {
                    UserTypes.Add(UserType.TypeName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado. Detalles del error: " + ex.Message,
                            "Error",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
            }

            return UserTypes;
        }
    }
}
