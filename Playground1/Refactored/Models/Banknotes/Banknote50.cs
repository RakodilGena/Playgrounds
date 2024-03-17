namespace Playground1.Refactored.Models.Banknotes;

internal sealed class Banknote50 : BanknoteBase
{
    public override int Amount => 50;
    public override Denomination Denomination => Denomination._50;
}