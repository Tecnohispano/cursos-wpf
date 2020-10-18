using Cursos_WPF.Model;
using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace Cursos_WPF
{
    /// <summary>
    /// Interaction logic for Cursos.xaml
    /// </summary>
    public partial class Cursos : Window
    {
        private TecnohispanoEntities TecnohispanoDb;
        private static readonly Regex ONLY_NUMBERS = new Regex("[^0-9]+");

        public Cursos()
        {
            InitializeComponent();
            TecnohispanoDb = new TecnohispanoEntities();
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

        /// <summary>
        /// This function prevents to enter text on the only-number fields.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LimiteParticipantes_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            return !ONLY_NUMBERS.IsMatch(text);
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 2. Apply validations
                if (StartDate.SelectedDate == null)
                {
                    MessageBoxResult message = MessageBox.Show("Por favor ingresa una fecha de inicio",
                        "Confirmation",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);

                    return;
                }

                // TODO: Add more validations.

                // 1. Get all data from the form
                Course NewCourse = new Course()
                {
                    CourseId = 3,
                    Name = Nombre.Text,
                    Link = Enlace.Text,
                    StartDate = StartDate.SelectedDate.Value,
                    EndDate = EndDate.SelectedDate.Value,
                    StartTime = TimeSpan.Parse(StartTime.Text),
                    EndTime = TimeSpan.Parse(EndTime.Text),
                    IsMonday = ChkLunes.IsChecked.Value,
                    IsTuesday = ChkMartes.IsChecked.Value,
                    IsWednesday = ChkMiercoles.IsChecked.Value,
                    IsThursday = ChkJueves.IsChecked.Value,
                    IsFriday = ChkViernes.IsChecked.Value,
                    IsSaturday = ChkSabado.IsChecked.Value,
                    IsSunday = ChkDomingo.IsChecked.Value,
                    ParticipantsLimit = Int32.Parse(LimiteParticipantes.Text)
                };

                // 3. Save to database
                TecnohispanoDb.Courses.Add(NewCourse);
                TecnohispanoDb.SaveChanges();

                MessageBoxResult SuccessMessage = MessageBox.Show("Curso agregado exitosamente",
                        "Curso agregado",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);

                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
