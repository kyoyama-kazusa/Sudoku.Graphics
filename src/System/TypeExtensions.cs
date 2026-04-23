namespace System;

/// <summary>
/// Provides extension members on <see cref="Type"/>.
/// </summary>
/// <seealso cref="Type"/>
public static class TypeExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="Type"/> instances.
	/// </summary>
	/// <param name="this">The current instance.</param>
	extension(Type @this)
	{
		/// <summary>
		/// Indicates whether the type has a callable parameterless constructor or not.
		/// </summary>
		public bool HasParameterlessConstructor
			=> Array.Exists(
				@this.GetConstructors(BindingFlags.Public | BindingFlags.Instance),
				static c => c.GetParameters().Length == 0
			);
	}
}
