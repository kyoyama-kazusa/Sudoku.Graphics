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
	extension(IJsonTypeInfoResolver)
	{
		/// <summary>
		/// Creates an <see cref="IJsonTypeInfoResolver"/> instance to be serialized / deserialized,
		/// that can replace traditional <see cref="JsonPolymorphicAttribute"/> and <see cref="JsonDerivedTypeAttribute"/> usages.
		/// </summary>
		/// <typeparam name="T">The type of instances that may be used in serialization.</typeparam>
		/// <returns>An <see cref="IJsonTypeInfoResolver"/> instance.</returns>
		/// <seealso cref="JsonPolymorphicAttribute"/>
		/// <seealso cref="JsonDerivedTypeAttribute"/>
		public static IJsonTypeInfoResolver Create<T>() where T : class
			=> new DefaultJsonTypeInfoResolver
			{
				Modifiers =
				{
					static typeInfo =>
					{
						if (typeInfo.Type != typeof(T))
						{
							return;
						}

						typeInfo.PolymorphismOptions = new() { IgnoreUnrecognizedTypeDiscriminators = true };
						foreach (var derived in
							from assembly in AppDomain.CurrentDomain.GetAssemblies()
							from type in assembly.GetTypes()
							where type.IsAssignableTo(typeof(T)) && !type.IsAbstract
							select type)
						{
							typeInfo.PolymorphismOptions.DerivedTypes.Add(new(derived, derived.Name));
						}
					}
				}
			};
	}
}
