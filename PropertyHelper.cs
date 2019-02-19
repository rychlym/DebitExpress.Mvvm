using System;
using System.Linq.Expressions;

namespace DebitExpress.Mvvm
{
    public class PropertyHelper
    {
        public static string GetPropertyName<T>(Expression<Func<T>> propertyLambda)
        {
            if (!(propertyLambda.Body is MemberExpression me))
                throw new ArgumentException(
                    "You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");

            return me.Member.Name;
        }
    }
}