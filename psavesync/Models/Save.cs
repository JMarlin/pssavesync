using System.ComponentModel.DataAnnotations;

namespace PSaveSync {

    public class Save {

        [Key]
        public int Id { get; set; }

        public string ProductCode { get; set; } = "";
        public string Identifier { get; set; } = "";
        public DateTime WriteDate { get; set; }
        public bool IsRetired { get; set; } = false;

        public ICollection<Block> Blocks { get; set; } = new List<Block>();
    }
}