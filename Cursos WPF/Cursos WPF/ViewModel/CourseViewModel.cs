using System;

namespace Cursos_WPF.ViewModel
{
    class CourseViewModel
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool IsMonday { get; set; }
        public bool IsTuesday { get; set; }
        public bool IsWednesday { get; set; }
        public bool IsThursday { get; set; }
        public bool IsFriday { get; set; }
        public bool IsSaturday { get; set; }
        public bool IsSunday { get; set; }
        public int ParticipantsLimit { get; set; }
        public bool Active { get; set; }
    }
}
