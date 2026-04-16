
namespace System.Text.Json.Serialization.Metadata;

/// <summary>
/// Provides a way to create an <see cref="IJsonTypeInfoResolver"/> instance.
/// </summary>
/// <seealso cref="IJsonTypeInfoResolver"/>
public static class ResolverDerivedTypeFactory
{
	/// <summary>
	/// Provides <see langword="static"/> members for <see cref="IJsonTypeInfoResolver"/>.
	/// </summary>
	extension(JsonTypeInfoResolver)
	{
		/// <summary>
		/// Creates an <see cref="IJsonTypeInfoResolver"/> instance to be serialized / deserialized,
		/// that can replace traditional <see cref="JsonPolymorphicAttribute"/> and <see cref="JsonDerivedTypeAttribute"/> usages.
		/// </summary>
		/// <typeparam name="T">The type of instances that may be used in serialization.</typeparam>
		/// <returns>An <see cref="IJsonTypeInfoResolver"/> instance.</returns>
		/// <seealso cref="JsonPolymorphicAttribute"/>
		/// <seealso cref="JsonDerivedTypeAttribute"/>
		public static IJsonTypeInfoResolver Create<T>() where T : class => new PolymorphicTypeInfoResolver<T>();
	}
}

/// <summary>
/// Represents a file-local polymorphic type information resolver.
/// </summary>
/// <typeparam name="T">The desired base type.</typeparam>
file sealed class PolymorphicTypeInfoResolver<T> : IJsonTypeInfoResolver where T : class
{
	/// <summary>
	/// The fallback resolver.
	/// </summary>
	private readonly IJsonTypeInfoResolver _fallback = new DefaultJsonTypeInfoResolver();


	/// <inheritdoc/>
	public JsonTypeInfo? GetTypeInfo(Type type, JsonSerializerOptions options)
	{
		if (type == typeof(T))
		{
			var info = _fallback.GetTypeInfo(type, options);
			if (info is null)
			{
				return null;
			}

			info.PolymorphismOptions = new()
			{
				TypeDiscriminatorPropertyName = "$type",
				IgnoreUnrecognizedTypeDiscriminators = true
			};
			foreach (var derived in
				from assembly in AppDomain.CurrentDomain.GetAssemblies()
				from t in assembly.GetTypes()
				where t.IsAssignableTo(typeof(T)) && !t.IsAbstract
				select t)
			{
				info.PolymorphismOptions.DerivedTypes.Add(new(derived, derived.Name));
			}
			return info;
		}

		return null;
	}
}
