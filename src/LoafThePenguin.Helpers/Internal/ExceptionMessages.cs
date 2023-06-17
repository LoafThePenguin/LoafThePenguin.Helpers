namespace LoafThePenguin.Helpers.Internal;

internal static class ExceptionMessages
{
    public const string ARGUMENT_OUT_OF_RANGE_CANT_BE_LOWER = "Аргумент \"{0}\" не может быть меньше чем \"{1}\".";
    public const string ARGUMENT_OUT_OF_RANGE_CANT_BE_HIGHER = "Аргумент \"{0}\" не может быть больше чем \"{1}\".";
    public const string INDEX_CANT_BE_LOWER_TAHN_ZERO = "Индекс не может быть меньше нуля.";
    public const string OBSCURE_EXCEPTION_THROWN = "Было вызвано неизвестное исключение.";
    public const string NEED_AT_LEAST_A_MESSAGE = "Необходимо, хотябы, сообщение об ошибке.";
    public const string SEQUENCE_HAS_NULL_REFERENCE = "Последовательность \"{0}\" содержит пустые ссылки.";
    public const string OBJECT_DISPOSED = "Объект \"{0}\" типа \"{1}\" был освобождён.";
    public const string NULL_REFERENCE = "Объект \"{0}\" типа \"{1}\" оказался null.";
}
