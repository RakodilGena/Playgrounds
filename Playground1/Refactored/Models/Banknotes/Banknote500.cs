namespace Playground1.Refactored.Models.Banknotes;

internal sealed class Banknote500 : BanknoteBase
{
    public override int Amount => 500;
    public override Denomination Denomination => Denomination._500;
}