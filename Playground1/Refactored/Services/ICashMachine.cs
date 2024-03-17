using Playground1.Refactored.Exceptions;
using Playground1.Refactored.Models;

namespace Playground1.Refactored.Services;

public interface ICashMachine
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="denomination"></param>
    /// <param name="count"></param>
    /// <exception cref="InvalidDepositCountException"></exception>
    void Deposit(Denomination denomination, int count);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="desiredAmount"></param>
    /// <returns></returns>
    /// <exception cref="NoEnoughMoneyException"></exception>
    WithdrawalDto Withdraw(int desiredAmount);
}