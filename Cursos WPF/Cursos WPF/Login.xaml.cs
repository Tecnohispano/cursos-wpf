using Cursos_WPF.Model;
using System.Text;
using System.Windows;

namespace Cursos_WPF
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly TecnohispanoEntities TecnohispanoDb;

        public Login()
        {
            InitializeComponent();
            TecnohispanoDb = new TecnohispanoEntities();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            // 1. Validaciones
            if (!ValidarForm()) {
                return;
            }

            // 2. Get user from database and validate that exists
            User Usuario = ObtenerUsuario(CorreoElectronico.Text);

            if (Usuario.UserId == 0)
            {
                MessageBox.Show("No tienes una cuenta en Cursos WPF",
                    "Cuenta no encontrada",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            // 3. Generar hashed password y comparar
            if (!Helpers.AccesosHelper.ComparePasswords(Contrasenia.Password, Usuario.Salt, Usuario.HashedPassword))
            {
                MessageBox.Show("El nombre de usuario o contraseña son incorrectos",
                   "Error de inicio de sesión",
                   MessageBoxButton.OK,
                   MessageBoxImage.Warning);

                return;
            }

            // 3.1 Agregar credenciales a sesión

            // 4. Darle acceso si es correcto

            // 5. Refrescar
        }


        private bool ValidarForm()
        {
            if (CorreoElectronico.Text == null || CorreoElectronico.Text.Length == 0)
            {
                MessageBox.Show("Ingresa un correo electrónico",
                    "Dato requerido",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return false;
            }

            return true;
        }

        private User ObtenerUsuario(string CorreoElectronico)
        {
            User Resultado = new User();

            foreach (User _Usuario in TecnohispanoDb.Users)
            {
                if (_Usuario.Email == CorreoElectronico)
                {
                    Resultado = new User()
                    {
                        UserId = _Usuario.UserId,
                        TypeId = _Usuario.TypeId,
                        Name = _Usuario.Name,
                        Email = _Usuario.Email,
                        HashedPassword = _Usuario.HashedPassword,
                        Salt = _Usuario.Salt,
                        Active = _Usuario.Active
                    };

                    string temp = Encoding.UTF8.GetString(Resultado.HashedPassword, 0, Resultado.HashedPassword.Length);
                }
            }

            return Resultado;
        }
    }
}
