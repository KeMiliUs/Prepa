using System.ComponentModel.DataAnnotations;

namespace Prepa.Models
{
    public class Pupil
    {
        [Key]
        public int id { get; set; }
        
        public int GroupId { get; set; }
        public string name { get; set; }

        public string? wish { get; set; }

        public Pupil? recipient { get; set; }

    }

}
