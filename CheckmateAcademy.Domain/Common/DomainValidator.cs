namespace CheckmateAcademy.Domain.Common
{
    public static class DomainValidation
    {
        public static void PositiveId(int value, string parameterName)
        {
            if (value < 1)
                throw new ArgumentException(
                    "Id must be a positive integer.",
                    parameterName);
        }

        public static void PositiveOrder(int value, string parameterName)
        {
            if (value < 1)
                throw new ArgumentException(
                    "Order must be a positive integer.",
                    parameterName);
        }

        public static void NotEmptyString(
            string? value,
            string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(
                    "Value cannot be null or whitespace.",
                    parameterName);
        }

        public static void NotNull<T>(
            T? value,
            string parameterName)
            where T : class
        {
            if (value is null)
                throw new ArgumentNullException(parameterName);
        }

        public static List<T> OrderedCollection<T>(
            IEnumerable<T>? items,
            string parameterName,
            Func<T, int> orderSelector,
            bool allowEmpty = true)
            where T : class
        {
            if (items is null)
            {
                if (allowEmpty)
                    return [];

                throw new ArgumentNullException(parameterName);
            }

            var list = items.ToList();

            if (!allowEmpty && list.Count == 0)
                throw new ArgumentException(
                    "Collection cannot be empty.",
                    parameterName);

            if (list.Any(item => item is null))
                throw new ArgumentException(
                    "Collection cannot contain null elements.",
                    parameterName);

            if (list.Count == 0)
                return list;

            if (orderSelector(list[0]) != 1)
                throw new ArgumentException(
                    "First element must have order 1.",
                    parameterName);

            for (var i = 1; i < list.Count; i++)
            {
                if (orderSelector(list[i]) !=
                    orderSelector(list[i - 1]) + 1)
                {
                    throw new ArgumentException(
                        "Collection elements must have sequential order.",
                        parameterName);
                }
            }

            return list;
        }
    }
}
