using Playground1.Refactored.Exceptions;

namespace Playground1.Refactored.Models.Banknotes;

internal abstract class BanknoteBase
{
    public abstract int Amount { get; }
    public abstract Denomination Denomination { get; }
    public int StoredCount { get; private set; }


    public void Deposit(int count)
    {
        if (count <= 0)
            Errors.ThrowArgument(nameof(count), "invalid count");

        StoredCount += count;
    }

    public void Withdraw(int count)
    {
        if (count <= 0)
            Errors.ThrowArgument(nameof(count), "invalid count");

        if (count > StoredCount)
            Errors.ThrowNoEnoughMoney();

        StoredCount -= count;
    }
}