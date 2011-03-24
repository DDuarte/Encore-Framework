using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Trinity.Core.Reflection
{
    public static class ReflectionUtility
    {
        [SuppressMessage("Microsoft.Design", "CA1004", Justification = "The use of type parameter T is intended.")]
        public static int GetEnumValueCount<T>()
        {
            var type = typeof(T);
            if (!type.IsEnum)
                throw new ArgumentException("The type of T is not an enumeration.");

            return Enum.GetValues(type).Length;
        }

        public static T GetEnumMaxValue<T>()
        {
            var type = typeof(T);
            if (!type.IsEnum)
                throw new ArgumentException("The type of T is not an enumeration.");

            return ((T[])Enum.GetValues(type)).Max();
        }

        public static MethodInfo MethodOf(Expression<Action> expr)
        {
            var body = expr.Body as MethodCallExpression;

            if (body == null)
                throw new ArgumentException("Expression is not a method call.", "expr");

            return body.Method;
        }

        public static ConstructorInfo ConstructorOf<T>(Expression<Func<T>> expr)
        {
            var body = expr.Body as NewExpression;

            if (body == null)
                throw new ArgumentException("Expression is not an object construction operation.", "expr");

            return body.Constructor;
        }

        public static PropertyInfo PropertyOf<T>(Expression<Func<T>> expr)
        {
            var body = expr.Body as MemberExpression;

            if (body == null)
                throw new ArgumentException("Expression must be a member expression.", "expr");

            var member = body.Member as PropertyInfo;

            if (member == null)
                throw new ArgumentException("Member expression is not a property.", "expr");

            return member;
        }

        public static FieldInfo FieldOf<T>(Expression<Func<T>> expr)
        {
            var body = expr.Body as MemberExpression;

            if (body == null)
                throw new ArgumentException("Expression must be a member expression.", "expr");

            var member = body.Member as FieldInfo;

            if (member == null)
                throw new ArgumentException("Member expression is not a field.", "expr");

            return member;
        }
    }
}
