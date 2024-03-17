namespace Playground1.Refactored.Models.Banknotes;

internal sealed class Banknote100 : BanknoteBase
{
    public override int Amount => 100;
    public override Denomination Denomination => Denomination._100;
}