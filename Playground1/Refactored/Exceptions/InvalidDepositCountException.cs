namespace Playground1.Refactored.Exceptions;

public sealed class InvalidDepositCountException(string message): Exception(message);