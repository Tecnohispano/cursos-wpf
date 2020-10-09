namespace Cursos_WPF.ViewModel
{
    class UserViewModel
    {
        public int UserId { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public byte[] HashedPassword { get; set; }
        public byte[] Salt { get; set; }
        public bool Active { get; set; }
    }
}
