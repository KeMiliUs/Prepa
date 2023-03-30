namespace Prepa.Models
{
    public class OnePupilTossDto
    {
        public int id { get; set; }
        public string name { get; set; }

        public string? wish { get; set; }

        public RecipientPupilDto? recipient { get; set; }
    }
}
