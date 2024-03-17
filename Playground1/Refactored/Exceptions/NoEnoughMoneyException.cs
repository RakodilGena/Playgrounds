namespace Playground1.Refactored.Exceptions;

public sealed class NoEnoughMoneyException(string message): Exception(message);