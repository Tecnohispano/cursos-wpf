using Cursos_WPF.Model;
using Cursos_WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Cursos_WPF
{
    /// <summary>
    /// Interaction logic for Cursos.xaml
    /// </summary>
    public partial class Cursos : Window
    {
        private readonly TecnohispanoEntities TecnohispanoDb;
        private static readonly Regex ONLY_NUMBERS = new Regex("[^0-9]+");

        /// <summary>
        /// Constructor.
        /// It creates a new TecnohispanoEntities so database operations can be performed.
        /// </summary>
        public Cursos()
        {
            InitializeComponent();
            TecnohispanoDb = new TecnohispanoEntities();

            CursosList.ItemsSource = GetCourses();
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
            if (!ONLY_NUMBERS.IsMatch(e.Text))
            {
                e.Handled = false;
            } else
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// This function creates a new Course. 
        /// It includes the corresponding validations and error messages handling.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 1. Apply validations
                if (!ValidForm())
                {
                    return;
                }

                // 2. Get all data from the form
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

                // 4. Refresh current window
                Cursos NewCursos = new Cursos();
                NewCursos.Show();

                this.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This function validates the form and returns True when all fields are correct.
        /// </summary>
        /// <returns></returns>
        private bool ValidForm()
        {
            if (Nombre.Text.Length == 0)
            {
                MessageBox.Show("Por favor ingresa un nombre para el curso",
                        "Campo faltante",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);

                return false;
            }

            if (Enlace.Text.Length == 0)
            {
                MessageBox.Show("Por favor ingresa un enlace para el curso",
                        "Campo faltante",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);

                return false;
            }

            bool IsURLValid = Uri.TryCreate(Enlace.Text, UriKind.Absolute, out Uri uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!IsURLValid)
            {
                MessageBox.Show("Por favor ingresa un enlace correcto",
                        "Campo faltante",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);

                return false;
            }

            if (StartDate.SelectedDate == null)
            {
                MessageBox.Show("Por favor ingresa una fecha de inicio",
                        "Campo faltante",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);

                return false;
            }

            if (EndDate.SelectedDate == null)
            {
                MessageBox.Show("Por favor ingresa una fecha de fin",
                        "Campo faltante",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);

                return false;
            }

            if (StartDate.SelectedDate > EndDate.SelectedDate)
            {
                MessageBox.Show("El periodo de fechas no es correcto",
                        "Campo faltante",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);

                return false;
            }

            if (StartTime.Text.Length == 0)
            {
                MessageBox.Show("Por favor ingresa una hora de inicio para el curso",
                        "Campo faltante",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);

                return false;
            }

            if (EndTime.Text.Length == 0)
            {
                MessageBox.Show("Por favor ingresa una hora de fin para el curso",
                        "Campo faltante",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);

                return false;
            }

            return true;
        }


        private List<CourseViewModel> GetCourses()
        {
            try
            {
                List<CourseViewModel> Courses = new List<CourseViewModel>();

                foreach (Course Curso in TecnohispanoDb.Courses)
                {
                    CourseViewModel _Curso = new CourseViewModel()
                    {
                        CourseId = Curso.CourseId,
                        Name = Curso.Name,
                        Link = Curso.Link,
                        StartDate = Curso.StartDate,
                        EndDate = Curso.EndDate,
                        //StartTime = StartTime,
                        //EndTime = EndTime,
                        IsMonday = Curso.IsMonday,
                        IsTuesday = Curso.IsTuesday,
                        IsWednesday = Curso.IsWednesday,
                        IsThursday = Curso.IsThursday,
                        IsFriday = Curso.IsFriday,
                        IsSaturday = Curso.IsSaturday,
                        IsSunday = Curso.IsSunday,
                        ParticipantsLimit = Curso.ParticipantsLimit
                    };

                    Courses.Add(_Curso);
                }

                return Courses;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
