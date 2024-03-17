namespace Playground1.Refactored.Models.Banknotes;

internal sealed class Banknote1000 : BanknoteBase
{
    public override int Amount => 1000;
    public override Denomination Denomination => Denomination._1000;
}