namespace EDeals.Catalog.Application.ApplicationExtensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Traverse<T>(this IEnumerable<T> items, Func<T, IEnumerable<T>> childSelector)
        {
            if (items == null)
                yield break;

            var stack = new Stack<T>(items);
            var visited = new HashSet<T>();

            while (stack.Any())
            {
                var next = stack.Pop();
                if (next == null || !visited.Add(next))
                    continue;

                yield return next;

                var children = childSelector(next);
                if (children != null)
                {
                    foreach (var child in children)
                    {
                        if (child != null)
                            stack.Push(child);
                    }
                }
            }
        }
    }
}
