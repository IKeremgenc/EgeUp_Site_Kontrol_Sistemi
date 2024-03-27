using DataaccsessLayer.Table;

namespace EgeUpUI.Dtos.RegisterDto
{
    public class CreateNewUSerDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public int ConfirmeCode { get; set; }
        public int ReferanceCode { get; set; }
        public AppUser ReferencedBy { get; set; }
    }
}
