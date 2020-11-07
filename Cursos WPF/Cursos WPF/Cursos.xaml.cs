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

        /// <summary>
        /// This function gets all the current courses used by the grid.
        /// </summary>
        /// <returns></returns>
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
                        StartDate = Curso.StartDate.ToLongDateString(),
                        EndDate = Curso.EndDate.ToLongDateString(),
                        StartTime = Curso.StartTime.ToString(),
                        EndTime = Curso.EndTime.ToString(),
                        IsMonday = Curso.IsMonday,
                        IsTuesday = Curso.IsTuesday,
                        IsWednesday = Curso.IsWednesday,
                        IsThursday = Curso.IsThursday,
                        IsFriday = Curso.IsFriday,
                        IsSaturday = Curso.IsSaturday,
                        IsSunday = Curso.IsSunday,
                        ParticipantsLimit = Curso.ParticipantsLimit,
                        Active = Curso.Active
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
                    ParticipantsLimit = Int32.Parse(LimiteParticipantes.Text),
                    Active = true
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
        /// This function fills out the form with a selected course to be edited.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CourseViewModel Curso = ((FrameworkElement)sender).DataContext as CourseViewModel;

                // 1. Find course in database
                Course Course = TecnohispanoDb.Courses.Find(Curso.CourseId);

                if (Course != null && Course.CourseId > 0)
                {
                    // 2. Fill out the form
                    CourseId.Text = Course.CourseId.ToString();
                    Nombre.Text = Course.Name;
                    Enlace.Text = Course.Link;
                    LimiteParticipantes.Text = Course.ParticipantsLimit.ToString();
                    StartDate.SelectedDate = Course.StartDate;
                    EndDate.SelectedDate = Course.EndDate;
                    StartTime.SelectedIndex = Course.StartTime.Hours;
                    EndTime.SelectedIndex = Course.EndTime.Hours;
                    ChkLunes.IsChecked = Course.IsMonday;
                    ChkMartes.IsChecked = Course.IsTuesday;
                    ChkMiercoles.IsChecked = Course.IsWednesday;
                    ChkJueves.IsChecked = Course.IsThursday;
                    ChkViernes.IsChecked = Course.IsFriday;
                    ChkSabado.IsChecked = Course.IsSaturday;
                    ChkDomingo.IsChecked = Course.IsSunday;

                    ChkActivo.IsChecked = Course.Active;

                    // 3. Show Edit and Cancel button and hide Add button. Shows Checkbox to indicate Status.
                    btnEditar.Visibility = Visibility.Visible;
                    btnCancelar.Visibility = Visibility.Visible;
                    btnAgregar.Visibility = Visibility.Hidden;
                    ChkActivo.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error al momento de buscar el curso.",
                                "Editar curso",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error inesperado. Intenta más tarde. Error: " + ex.Message,
                                "Editar curso",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// This function edits the selected course.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Editar_Confirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 1. Apply validations
                if (!ValidForm())
                {
                    return;
                }

                // 2. Find course in database
                Course Course = TecnohispanoDb.Courses.Find(Int32.Parse(CourseId.Text));

                // 3. Update all fields
                Course.Name = Nombre.Text;
                Course.Link = Enlace.Text;
                Course.StartDate = StartDate.SelectedDate.Value;
                Course.EndDate = EndDate.SelectedDate.Value;
                Course.StartTime = TimeSpan.Parse(StartTime.Text);
                Course.EndTime = TimeSpan.Parse(EndTime.Text);
                Course.IsMonday = ChkLunes.IsChecked.Value;
                Course.IsTuesday = ChkMartes.IsChecked.Value;
                Course.IsWednesday = ChkMiercoles.IsChecked.Value;
                Course.IsThursday = ChkJueves.IsChecked.Value;
                Course.IsFriday = ChkViernes.IsChecked.Value;
                Course.IsSaturday = ChkSabado.IsChecked.Value;
                Course.IsSunday = ChkDomingo.IsChecked.Value;
                Course.ParticipantsLimit = Int32.Parse(LimiteParticipantes.Text);
                Course.Active = ChkActivo.IsChecked.Value;

                // 4. Save changes to database
                TecnohispanoDb.SaveChanges();

                MessageBox.Show("Curso editado exitosamente",
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
                MessageBox.Show("Ha ocurrido un error inesperado. Por favor intenta más tarde. Error: " + ex.Message,
                        "Editar curso",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// This function clears the course form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Editar_Cancel_Click(object sender, RoutedEventArgs e)
        {
            // 1. Refresh current window to clear form.
            Cursos NewCursos = new Cursos();
            NewCursos.Show();

            this.Close();
        }

        /// <summary>
        /// This function deletes a selected course.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 1. Get the current course ID to be deleted.
                CourseViewModel Curso = ((FrameworkElement)sender).DataContext as CourseViewModel;

                // 2. Find the course in the database.
                Course Course = TecnohispanoDb.Courses.Find(Curso.CourseId);

                // TODO: Validate if the course has no users or any additional validation.

                MessageBoxResult DeleteMessage = MessageBox.Show("¿Estás seguro de querer eliminar el curso " + Course.Name + "?",
                            "Eliminar curso",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Warning);

                // 3. Delete the course from database when user selects Yes.
                if (DeleteMessage == MessageBoxResult.Yes)
                {
                    TecnohispanoDb.Courses.Remove(Course);
                    TecnohispanoDb.SaveChanges();
                }

                // 4. Refresh current window
                Cursos NewCursos = new Cursos();
                NewCursos.Show();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado. Detalles del error: " + ex.Message,
                            "Error",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
            }
        }
    }
}
