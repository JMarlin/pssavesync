using MemcardRex;

public record SaveRecord(string SaveName, string ProductCode) {
    public static Func<int, SaveRecord> FromCardSlot(ps1card card) =>
        slotIndex => new(
            string.Join("", card.saveIdentifier[slotIndex].Where(c => c != 0)),
            card.saveProdCode[slotIndex] );
}