namespace Sudoku.Graphics.UI.Configuration;

/// <summary>
/// Provides handling on type <see cref="Inherited{T}"/>.
/// </summary>
/// <seealso cref="Inherited{T}"/>
internal static class Inherited
{
	/// <summary>
	/// Resolves a property <c>instance.Property</c>,
	/// and directly returns the result after called <c>instance.Property.Resolve(instance)</c>.
	/// </summary>
	/// <typeparam name="T">The type of result value.</typeparam>
	/// <param name="source">The source expression (lambda expression).</param>
	/// <returns>The target value.</returns>
	/// <exception cref="ArgumentException">Throws when the source is not the normal format <c>() => instance.Property</c>.</exception>
	/// <exception cref="InvalidOperationException">Throws when the target type cannot find method <c>Resolve</c>.</exception>
	/// <seealso cref="App.UserPreferences"/>
	public static T ResolveProperty<T>(Expression<Func<Inherited<T>>> source) where T : notnull
	{
		const string message_TargetLambdaMustBeSpecifiedFormat = "The target lambda expression must be formatted as '() => instance.Property'.";
		const string message_MethodCannotBeFound = $"Cannot find for method '{nameof(Inherited<>)}.{nameof(Inherited<>.Resolve)}<T>(T, int)'.";
		const BindingFlags methodBindingFlags = BindingFlags.Instance | BindingFlags.Public;

		if (source.Body is not MemberExpression { Expression: { } instanceExpression, Type: var propertyType } memberExpression)
		{
			throw new ArgumentException(message_TargetLambdaMustBeSpecifiedFormat, nameof(source));
		}

		var methodInfo = propertyType.GetMethod(nameof(Inherited<>.Resolve), methodBindingFlags)?.MakeGenericMethod(typeof(Preferences))
			?? throw new InvalidOperationException(message_MethodCannotBeFound);
		var callExpression = Expression.Call(memberExpression, methodInfo, instanceExpression, Expression.Constant(2));
		return Expression.Lambda<Func<T>>(callExpression).Compile()();
	}
}
