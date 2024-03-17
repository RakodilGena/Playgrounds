using System.Collections.Frozen;
using Playground1.Refactored.Exceptions;
using Playground1.Refactored.Models;
using Playground1.Refactored.Models.Banknotes;

namespace Playground1.Refactored.Services;

internal sealed class CashMachine : ICashMachine
{
    private readonly FrozenDictionary<Denomination, BanknoteBase> _storageDict;
    private readonly BanknoteBase[] _orderedStorage;

    // ReSharper disable once ConvertConstructorToMemberInitializers
    public CashMachine()
    {
        //reversed order.
        _orderedStorage =
        [
            new Banknote1000(),
            new Banknote500(),
            new Banknote100(),
            new Banknote50()
        ];

        _storageDict = _orderedStorage.ToFrozenDictionary(
            keySelector: banknote => banknote.Denomination);
    }

    public void Deposit(Denomination denomination, int count)
    {
        if (count <= 0)
            Errors.ThrowInvalidDepositCount();

        _storageDict[denomination].Deposit(count);
    }

    public WithdrawalDto Withdraw(int desiredAmount)
    {
        var withdrawals = new Dictionary<Denomination, int>(_orderedStorage.Length);

        foreach (var banknote in _orderedStorage)
        {
            var desiredCount = desiredAmount / banknote.Amount;

            var countToWithdraw = Math.Min(banknote.StoredCount, desiredCount);
            if (countToWithdraw is 0)
                continue;

            withdrawals[banknote.Denomination] = countToWithdraw;

            var withdrawAmount = banknote.Amount * countToWithdraw;
            desiredAmount -= withdrawAmount;
        }

        if (desiredAmount is not 0)
            Errors.ThrowNoEnoughMoney();

        foreach (var withdrawal in withdrawals)
        {
            _storageDict[withdrawal.Key].Withdraw(withdrawal.Value);
        }

        return new WithdrawalDto(Banknotes: withdrawals.ToFrozenDictionary());
    }
}