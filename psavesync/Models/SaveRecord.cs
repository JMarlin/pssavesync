using MemcardRex;

public record SaveRecord(string SaveName, string ProductCode, byte[] FirstBlockData) {
    public static Func<int, SaveRecord> FromCardSlot(ps1card card) =>
        slotIndex => new(
            string.Join("", card.saveIdentifier[slotIndex].Where(c => c != 0)),
            card.saveProdCode[slotIndex],
            Enumerable.Range(0, 8192).Select(i => card.saveData[slotIndex - 1, i]).ToArray() );
}