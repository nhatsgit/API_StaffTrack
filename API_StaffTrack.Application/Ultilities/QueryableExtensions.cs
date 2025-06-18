using System.Linq.Expressions;
using System.Reflection;

namespace API_StaffTrack.Application.Ultilities
{
    public static class QueryableExtensions
    {
        public static IOrderedQueryable<T> OrderByColumn<T>(this IQueryable<T> source, string columnPath)
            => source.OrderByColumnUsing(columnPath, "OrderBy");

        public static IOrderedQueryable<T> OrderByColumnDescending<T>(this IQueryable<T> source, string columnPath)
            => source.OrderByColumnUsing(columnPath, "OrderByDescending");

        public static IOrderedQueryable<T> ThenByColumn<T>(this IOrderedQueryable<T> source, string columnPath)
            => source.OrderByColumnUsing(columnPath, "ThenBy");

        public static IOrderedQueryable<T> ThenByColumnDescending<T>(this IOrderedQueryable<T> source, string columnPath)
            => source.OrderByColumnUsing(columnPath, "ThenByDescending");

        private static IOrderedQueryable<T> OrderByColumnUsing<T>(this IQueryable<T> source, string columnPath, string method)
        {
            var parameter = Expression.Parameter(typeof(T), "item");
            var member = columnPath.Split('.')
                .Aggregate((Expression)parameter, Expression.PropertyOrField);
            var keySelector = Expression.Lambda(member, parameter);
            var methodCall = Expression.Call(typeof(Queryable), method, new[]
                    { parameter.Type, member.Type },
                source.Expression, Expression.Quote(keySelector));

            return (IOrderedQueryable<T>)source.Provider.CreateQuery(methodCall);
        }


        public static IQueryable<string> SelectFromString<T>(this IQueryable<T> query, string column)
        {
            var parameter = Expression.Parameter(typeof(T), "e");
            var property = MakePropPath(parameter, column);

            if (property.Type != typeof(string))
            {
                if (property.Type != typeof(object))
                    property = Expression.Convert(property, typeof(object));

                property = Expression.Call(_toStringMethod, property);
            }

            var lambda = Expression.Lambda<Func<T, string>>(property, parameter);

            return query.Select(lambda);
        }

        private static Expression MakePropPath(Expression objExpression, string path)
        {
            return path.Split('.').Aggregate(objExpression, Expression.PropertyOrField);
        }

        private static MethodInfo _toStringMethod = typeof(Convert).GetMethods()
            .Single(m =>
                m.Name == nameof(Convert.ToString) && m.GetParameters().Length == 1 &&
                m.GetParameters()[0].ParameterType == typeof(object)
            );
    }
}
