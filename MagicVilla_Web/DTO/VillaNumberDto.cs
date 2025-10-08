namespace MagicVilla_Web.Dto
{
    public class VillaNumberDto
    {
        public int Id { get; set; } // it should'v be a villaNo , but for the sake of the IEntity Interface 
        public int VillaId { get; set; }
        public string SpecialDetail { get; set; } = string.Empty;
        public VillaDto Villa { get; set; }
    }
}
