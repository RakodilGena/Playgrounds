namespace Playground1.Refactored.Models;

public sealed record WithdrawalDto(
    IReadOnlyDictionary<Denomination, int> Banknotes);