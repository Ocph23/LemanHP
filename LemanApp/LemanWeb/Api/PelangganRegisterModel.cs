namespace LemanWeb.Api
{
    public class PelangganRegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Nama { get; set; }
        public string Telepon { get; set; }
        public string Alamat { get; set; }
        public System.DateTime TanggalDaftar { get; set; }
        public string UserId { get; set; }
    }
}