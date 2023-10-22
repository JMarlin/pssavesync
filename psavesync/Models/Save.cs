using System.ComponentModel.DataAnnotations;
using MemcardRex;

namespace PSaveSync {

    public class Save {

        [Key]
        public int Id { get; set; }

        public string ProductCode { get; set; } = "";
        public string FileName { get; set; } = "";
        public DateTime WriteDate { get; set; }
        public bool IsRetired { get; set; } = false;

        public ICollection<Block> Blocks { get; set; } = new List<Block>();

        public Save() {}

        public Save(ps1card sourceCard, int directoryBlockIndex) {

            WriteDate = DateTime.UtcNow;
            ProductCode = sourceCard.saveProdCode[directoryBlockIndex];
            Blocks = Block.FromChain(sourceCard, directoryBlockIndex);
        }
    }
}