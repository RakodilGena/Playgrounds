namespace Playground1.Raw;

internal sealed class Withdrawal
{
    public Dictionary<Nominal, int> Amounts { get; }

    public Withdrawal()
    {
        Amounts = new Dictionary<Nominal, int>();
    }
}