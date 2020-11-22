using Cursos_WPF.Model;
using System;
using System.Collections.Generic;
using System.Windows;

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
        /// This function gets all the user types.
        /// </summary>
        /// <returns></returns>
        private List<string> GetUserTypes()
        {
            List<string> UserTypes = new List<string>();

            foreach(UserType _UserType in TecnohispanoDb.UserTypes)
            {
                UserTypes.Add(_UserType.TypeName);
            }

            return UserTypes;
        }


        /// <summary>
        /// This function creates a new user using the Hashing SHA256 algorithms.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            // 0. Traerme los datos del formulario
            string NombreCompleto = PrimerNombre.Text + " " +
                                    SegundoNombre.Text + " " +
                                    ApellidoPaterno.Text + " " +
                                    ApellidoMaterno.Text;
            string CorreoElectronico = Correo.Text;
            string ContraseniaUsuario = Contrasenia.Password;
            string TipoUsuario = CmbTipoUsuario.SelectedItem.ToString();

            // 1. Validar datos correctos
            // 2. Crear modelo Usuario
            // 3. Save()
            // 4. Refresh window
        }
    }
}
