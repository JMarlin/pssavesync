using System.ComponentModel.DataAnnotations;

namespace PSaveSync {

    public class Block {

        [Key]
        public int Id { get; set; }

        public int Index { get; set; }
        public int SaveId { get; set; }
        public byte[] Data { get; set; } = new byte[1024 * 8];

        public Save Save { get; set; } = new();
    }
}