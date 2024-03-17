using System.Diagnostics.CodeAnalysis;

namespace Playground1.Raw;

internal sealed class Bankomat : IBankomat
{
    private readonly Dictionary<Nominal, int> _storage;

    public Bankomat()
    {
        _storage = new Dictionary<Nominal, int>
        {
            { Nominal.N1000, 0 },
            { Nominal.N500, 0 },
            { Nominal.N100, 0 },
            { Nominal.N50, 0 },
        };
    }

    public void Add(int count, Nominal nominal)
    {
        int current = _storage[nominal];
        _storage[nominal] = current + count;
    }

    private static int NominalToInt(Nominal nom)
        => nom switch
        {
            Nominal.N50 => 50,
            Nominal.N100 => 100,
            Nominal.N500 => 500,
            //Nominal.N1000
            _ => 1000
        };

    public Withdrawal Withdraw(int amount)
    {
        var withdrawal = new Withdrawal();

        var sortedKeys =
            _storage.Keys.Select(k => (Key: k, Val: NominalToInt(k)))
                .OrderByDescending(k => k.Val);

        foreach (var banknote in sortedKeys)
        {
            var desiredCount = amount / banknote.Val;

            var countToWithDraw = Math.Min(_storage[banknote.Key], desiredCount);

            int withdrawAmount = banknote.Val * desiredCount;

            withdrawal.Amounts[banknote.Key] += countToWithDraw;

            amount -= withdrawAmount;
        }

        if (amount is 0)
        {
            foreach (var k in withdrawal.Amounts)
            {
                _storage[k.Key] -= k.Value;
            }
            
            return withdrawal;
        }

        ThrowNoEnoughMoney();
        return null;
    }

    //todo to throw helper
    [DoesNotReturn]
    private static void ThrowNoEnoughMoney() => throw new NoEnoughMoneyException();
}