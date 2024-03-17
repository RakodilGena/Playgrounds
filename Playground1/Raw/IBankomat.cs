namespace Playground1.Raw;

internal interface IBankomat
{
    void Add(int count, Nominal nominal);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="amount"></param>
    /// <exception cref="NoEnoughMoneyException"></exception>
    Withdrawal Withdraw(int amount);
}