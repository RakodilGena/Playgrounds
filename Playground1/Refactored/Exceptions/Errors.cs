using System.Diagnostics.CodeAnalysis;

namespace Playground1.Refactored.Exceptions;

internal static class Errors
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="paramName"></param>
    /// <param name="message"></param>
    /// <exception cref="ArgumentException"></exception>
    [DoesNotReturn]
    public static void ThrowArgument(string? paramName, string? message)
        => throw new ArgumentException(message, paramName);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <exception cref="NoEnoughMoneyException"></exception>
    [DoesNotReturn]
    public static void ThrowNoEnoughMoney(string message = "cash machine has no enough money")
        => throw new NoEnoughMoneyException(message);



    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <exception cref="InvalidDepositCountException"></exception>
    [DoesNotReturn]
    public static void ThrowInvalidDepositCount(string message = "invalid deposit count")
        => throw new InvalidDepositCountException(message);
}