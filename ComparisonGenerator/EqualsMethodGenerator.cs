/*
ComparisonGenerator - Generator of equals method dynamic

Written in 2014 by <Olga Miller> <olga.rgb@googlemail.com>
To the extent possible under law, the author(s) have dedicated all copyright and related and neighboring rights to this software to the public domain worldwide.
This software is distributed without any warranty.
You should have received a copy of the CC0 Public Domain Dedication along with this software. If not, see <http://creativecommons.org/publicdomain/zero/1.0/>.
*/

using System;
using System.Linq.Expressions;
using System.Reflection;

namespace ComparisonGenerator
{
	public class EqualsMethodGenerator
	{
		public Expression<Func<T, T, bool>> GenerateEqualsMethodExpression<T>()
		{
			Expression expression = Expression.Constant(true);

			ParameterExpression p1 = Expression.Parameter(typeof(T));
			ParameterExpression p2 = Expression.Parameter(typeof(T));

			foreach (PropertyInfo property in typeof(T).GetProperties())
			{
				Expression expr1 = Expression.Property(p1, property);
				Expression expr2 = Expression.Property(p2, property);

				expression = Expression.AndAlso(expression, Expression.Equal(expr1, expr2));
			}

			return Expression.Lambda<Func<T, T, bool>>(expression, p1, p2);
		}

		public Expression<Func<T, T, bool>> GenerateEqualsMethodExpressionRecurse<T>()
		{
			ParameterExpression paramL = Expression.Parameter(typeof(T));
			ParameterExpression paramR = Expression.Parameter(typeof(T));

			Expression expression = GetExpression<T>(paramL, paramR).Reduce();
			return Expression.Lambda<Func<T, T, bool>>(expression, paramL, paramR);
		}

		private Expression GetExpression<T>(ParameterExpression paramL, ParameterExpression paramR)
		{
			Expression expression = Expression.Constant(true);

			foreach (PropertyInfo property in typeof(T).GetProperties())
			{
				Expression exprL = Expression.Property(paramL, property);
				Expression exprR = Expression.Property(paramR, property);

				GetExpression(ref expression, property, exprL, exprR);
			}

			return expression;
		}

		private void GetExpression(ref Expression expression, PropertyInfo property, Expression exprL, Expression exprR)
		{
			if (!HasEqualsMethod(property))
			{
				PropertyInfo[] subProperties = property.PropertyType.GetProperties();

				foreach (PropertyInfo subProp in subProperties)
				{
					Expression subExprL = Expression.Property(exprL, subProp);
					Expression subExprR = Expression.Property(exprR, subProp);

					GetExpression(ref expression, subProp, subExprL, subExprR);
				}
			}
			else
			{
				expression = Expression.AndAlso(expression, Expression.Equal(exprL, exprR));
			}
		}

		private bool HasEqualsMethod(PropertyInfo property)
		{
			MethodInfo equalsMethod = property.PropertyType.GetMethod("Equals", new Type[1] { typeof(object) });

			return equalsMethod != null && IsOverride(equalsMethod);
		}

		private bool IsOverride(MethodInfo method)
		{
			return method.GetBaseDefinition().DeclaringType != method.DeclaringType;
		}
	}
}

