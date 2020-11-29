using Cursos_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            string NewPassword = GeneratePassword();
            byte[] RandomSalt = GenerateLongRandomSalt();
            byte[] HashedPassword = GenerateHashedPassword(NewPassword, RandomSalt);

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


        #region Access Generator
        public static string GeneratePassword(
            int requiredLength = 8,
            int requiredUniqueChars = 4,
            bool requireDigit = true,
            bool requireLowercase = true,
            bool requireNonAlphanumeric = true,
            bool requireUppercase = true)
        {
            string[] randomChars = new[] {
                "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
                "abcdefghijkmnopqrstuvwxyz",    // lowercase
                "0123456789",                   // digits
                "!@$?_-"                        // non-alphanumeric
                };
            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (requireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (requireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (requireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (requireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < requiredLength
                || chars.Distinct().Count() < requiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }

        public static byte[] GenerateLongRandomSalt()
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            return salt;
        }

        public static byte[] GenerateHashedPassword(string Password, byte[] Salt)
        {
            return Hash(Encoding.UTF8.GetBytes(Password), Salt);
        }

        public static byte[] Hash(byte[] Password, byte[] Salt)
        {
            // Salt is appended to the password to defend against dictionary attacks or brute force.
            byte[] SaltedPassword = Password.Concat(Salt).ToArray();

            return new SHA256Managed().ComputeHash(SaltedPassword);
        }
        #endregion
    }
}
