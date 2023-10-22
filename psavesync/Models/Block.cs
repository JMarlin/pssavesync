using System.ComponentModel.DataAnnotations;
using MemcardRex;

namespace PSaveSync {

    public class Block {

        public const int FRAME_SIZE = 128;
        public const int BLOCK_SIZE = FRAME_SIZE * 64;

        [Key]
        public int Id { get; set; }

        public int Index { get; set; }
        public int SaveId { get; set; }
        public byte[] Data { get; set; } = new byte[BLOCK_SIZE];

        public Save Save { get; set; } = new();

        public Block() {}

        public Block(ps1card card, int blockIndex) =>
            Data = Enumerable.Range(0, BLOCK_SIZE)
                .Select(i => card.saveData[blockIndex, i])
                .ToArray();

        private static int _SizeToBlockCount(int size) =>
            (int)Math.Ceiling((double)size/(double)BLOCK_SIZE);

        public static ICollection<Block> FromChain(ps1card card, int directoryBlockIndex) =>
            Enumerable.Range(0, _SizeToBlockCount(card.saveSize[directoryBlockIndex]))
                .Aggregate(
                    (directoryBlockIndex, Enumerable.Empty<Block>()),
                    ((int currentIndex, IEnumerable<Block> blocks) a, int i) =>
                        (card.headerData[a.currentIndex, 8], a.blocks.Append(new(card, a.currentIndex))),
                    ((int currentIndex, IEnumerable<Block> blocks) a) => a.blocks)
                .ToArray();
    }
}