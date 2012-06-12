namespace RIAServices.M2M.Utilities
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    internal static class ExpressionExtensions
    {
        #region Public Methods and Operators

        public static PropertyInfo GetProperty<TEntity, TProperty>(
            this Expression<Func<TEntity, TProperty>> propertySelector)
        {
            var expression = propertySelector.Body as MemberExpression
                             ?? ((UnaryExpression)propertySelector.Body).Operand as MemberExpression;
            if(expression == null)
            {
                throw new ArgumentNullException("propertySelector");
            }
            return (PropertyInfo)expression.Member;
        }

        #endregion
    }
}