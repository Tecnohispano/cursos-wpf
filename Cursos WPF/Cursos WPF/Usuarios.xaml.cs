using Cursos_WPF.Model;
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

            foreach (UserType _UserType in TecnohispanoDb.UserTypes)
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


            string TipoUsuario = CmbTipoUsuario.SelectedItem.ToString();

            // 1. Validar datos correctos
            if (!ValidarForm())
            {
                return;
            }

            // 2. Crear modelo Usuario
            string NewPassword = Helpers.AccesosHelper.GeneratePassword();
            byte[] RandomSalt = Helpers.AccesosHelper.GenerateLongRandomSalt();
            byte[] HashedPassword = Helpers.AccesosHelper.GenerateHashedPassword(NewPassword, RandomSalt);

            User NewUser = new User()
            {
                TypeId = 2, // TODO: Obtener TypeId correcto
                Name = NombreCompleto,
                Email = Correo.Text,
                Username = Correo.Text,
                Salt = RandomSalt,
                HashedPassword = HashedPassword,
                Active = true
            };

            // 3. Save()
            TecnohispanoDb.Users.Add(NewUser);
            TecnohispanoDb.SaveChanges();

            // 4. TODO: Enviar contrase a correo de Usuario

            // Mostrar mensaje de exito
            MessageBox.Show("Usuario agregado exitosamente",
                        "Nuevo usuario",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);

            // 6. Refresh window
            Usuarios NewUsuarios = new Usuarios();
            NewUsuarios.Show();

            this.Close();
        }


        /// <summary>
        /// This function validates all the user form.
        /// </summary>
        /// <returns></returns>
        private bool ValidarForm()
        {
            int maxLenght = Contrasenia.MaxLength;

            if (Contrasenia.Password.Length < 8)
            {
                MessageBox.Show("Escribe una contrasenia mas larga.",
                            "Error datos",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);

                return false;
            }

            if (CmbTipoUsuario.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un tipo de usuario",
                            "Error datos",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);

                return false;
            }

            // TODO: Agrar mas validaciones

            return true;
        }
    }
}
