namespace Sudoku.Serialization;

/// <summary>
/// Represents a type that supports serialization and deserialization operation with inheritance.
/// </summary>
/// <typeparam name="T">The type of value.</typeparam>
[JsonConverter(typeof(InheritedJsonConverterFactory))]
public sealed class Inherited<T> where T : notnull
{
	/// <summary>
	/// Represents property name of referenced path to be serialized and deserialized.
	/// </summary>
	internal const string InheritedReferencedPropertyName = "$ref";

	/// <summary>
	/// Represents property name of value to be serialized and deserialized.
	/// </summary>
	internal const string ValuePropertyName = "value";

	/// <summary>
	/// Indicates binding flags on property members while resolving value.
	/// </summary>
	private const BindingFlags DefaultBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;


	/// <summary>
	/// Indicates the reference path of a property.
	/// </summary>
	private readonly string? _reference;

	/// <summary>
	/// Indicates the backing value.
	/// </summary>
	private readonly T? _value;


	/// <summary>
	/// Initializes an <see cref="Inherited{T}"/> instance via the specified property path.
	/// </summary>
	/// <param name="reference">The referenced property path.</param>
	private Inherited(string reference)
	{
		_reference = reference;
		_value = default;
	}

	/// <summary>
	/// Initializes an <see cref="Inherited{T}"/> instance via the specified value.
	/// </summary>
	/// <param name="value">The value.</param>
	private Inherited(T value)
	{
		_value = value;
		_reference = null;
	}


	/// <summary>
	/// Indicates whether the instance has a real value.
	/// </summary>
	[MemberNotNullWhen(true, nameof(_value))]
	[MemberNotNullWhen(false, nameof(_reference))]
	public bool HasValue => _reference is null;

	/// <summary>
	/// Indicates the referenced property path.
	/// </summary>
	/// <exception cref="InvalidOperationException">Throws when instance holds a value.</exception>
	public string Reference => HasValue ? throw new InvalidOperationException("This instance holds a value.") : _reference;

	/// <summary>
	/// Indicates the target value.
	/// </summary>
	/// <exception cref="InvalidOperationException">Throws when instance is a reference.</exception>
	public T? Value => HasValue ? _value : throw new InvalidOperationException("This instance is a reference.");


	/// <inheritdoc/>
	public override string ToString() => HasValue ? $"{nameof(Value)}: {_value}" : $"{nameof(Reference)}: {_reference}";

	/// <summary>
	/// Resolve using a resolver function. The resolver should map a reference string to another <see cref="Inherited{T}"/>
	/// (or <see langword="null"/> if not found). Will follow reference chain up to <paramref name="maxDepth"/>
	/// and will throw on cycles or missing.
	/// </summary>
	/// <typeparam name="TOwner">The type of <paramref name="owner"/>.</typeparam>
	/// <param name="owner">The owner instance.</param>
	/// <param name="maxDepth">The max depth. By default it's 2.</param>
	/// <exception cref="InvalidOperationException">
	/// Throws when at least one invalid case is encountered:
	/// <list type="bullet">
	/// <item>Maximum resolution depth exceeded (May cause a cycle)</item>
	/// <item>Cycle detected when resolving a certain reference</item>
	/// <item>Reference is not found by resolver.</item>
	/// </list>
	/// </exception>
	public T Resolve<TOwner>(TOwner owner, int maxDepth = 2) where TOwner : notnull
	{
		const string message_MaxDepthReached = "Maximum resolution depth exceeded.";

		if (HasValue)
		{
			return _value;
		}

		var visited = new HashSet<string>(StringComparer.Ordinal);
		var currentReference = Reference;
		var ownerType = owner.GetType();
		var depth = 0;
		while (true)
		{
			depth++;
			if (depth > maxDepth)
			{
				throw new InvalidOperationException(message_MaxDepthReached);
			}
			if (!visited.Add(currentReference))
			{
				throw new InvalidOperationException(message_CycleDetected(currentReference));
			}

			var propertyInfo = ownerType.GetProperty(currentReference, DefaultBindingFlags)
				?? throw new InvalidOperationException(message_MemberNotFound(currentReference, ownerType));
			var propertyValue = propertyInfo.GetValue(owner)
				?? throw new InvalidOperationException(message_MemberIsNull(currentReference));
			switch (propertyValue)
			{
				case Inherited<T> inherited:
				{
					if (inherited.HasValue)
					{
						return inherited._value;
					}
					currentReference = inherited.Reference;
					break;
				}
				case T value:
				{
					return value;
				}
				default:
				{
					throw new InvalidOperationException(message_MemberTypeIsInvalid(currentReference));
				}
			}
		}


		static string message_CycleDetected(string currentReference)
			=> $"Cycle detected while resolving reference '{currentReference}'.";

		static string message_MemberNotFound(string currentReference, Type ownerType)
			=> $"Property '{currentReference}' not found on type '{ownerType.Name}'.";

		static string message_MemberIsNull(string currentReference) => $"Property '{currentReference}' is null.";

		static string message_MemberTypeIsInvalid(string currentReference)
			=> $"Property '{currentReference}' is not neither 'string' nor '{nameof(Inherited<>)}<{typeof(T).Name}>'.";
	}

	/// <summary>
	/// Returns the real value if the current instance has a value; otherwise, <see langword="default"/>(<typeparamref name="T"/>).
	/// </summary>
	/// <returns>The real value or <see langword="default"/>(<typeparamref name="T"/>).</returns>
	public T? GetValueOrDefault() => HasValue ? _value : default;


	/// <summary>
	/// Creates an <see cref="Inherited{T}"/> instance via the specified property name.
	/// </summary>
	/// <param name="propertyName">The name of property to reference.</param>
	/// <returns>An <see cref="Inherited{T}"/> instance.</returns>
	public static Inherited<T> FromMemberName(string propertyName) => new(propertyName);

	/// <summary>
	/// Creates an <see cref="Inherited{T}"/> instance via the specified value.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>An <see cref="Inherited{T}"/> instance.</returns>
	public static Inherited<T> FromValue(T value) => new(value);
}

/// <summary>
/// Represents a JSON converter factory instance of type <see cref="Inherited{T}"/>.
/// </summary>
/// <seealso cref="Inherited{T}"/>
file sealed class InheritedJsonConverterFactory : JsonConverterFactory
{
	/// <inheritdoc/>
	public override bool CanConvert(Type typeToConvert)
		=> typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(Inherited<>);

	/// <inheritdoc/>
	public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
	{
		var innerType = typeToConvert.GetGenericArguments()[0];

		// Special-case for string to avoid ambiguity.
		return innerType == typeof(string)
			? Activator.CreateInstance<InheritedStringConverter>()
			: (JsonConverter?)Activator.CreateInstance(typeof(InheritedJsonConverter<>).MakeGenericType(innerType), [options]);
	}
}

/// <summary>
/// Represents a JSON converter factory instance of type <see cref="Inherited{T}"/>.
/// </summary>
/// <param name="_options">The options.</param>
/// <seealso cref="Inherited{T}"/>
file sealed class InheritedJsonConverter<T>(JsonSerializerOptions _options) : JsonConverter<Inherited<T>> where T : notnull
{
	/// <inheritdoc/>
	public override Inherited<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		// If token is string -> treat as reference name.
		if (reader.TokenType == JsonTokenType.String)
		{
			return Inherited<T>.FromMemberName(reader.GetString()!);
		}

		// If token is null.
		if (reader.TokenType == JsonTokenType.Null)
		{
			// Treat as value == default(T).
			reader.Read();
			return Inherited<T>.FromValue(default!);
		}

		// Otherwise deserialize as T.
		return Inherited<T>.FromValue(JsonSerializer.Deserialize<T>(ref reader, _options)!);
	}

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, Inherited<T> value, JsonSerializerOptions options)
	{
		if (value.HasValue)
		{
			// Serialize the inner value as if it were the property itself.
			JsonSerializer.Serialize(writer, value.Value, _options);
			return;
		}

		writer.WriteStringValue(value.Reference);
	}
}

/// <summary>
/// Special converter for <see cref="Inherited{T}"/> of <see cref="string"/> to avoid ambiguity.
/// </summary>
file sealed class InheritedStringConverter : JsonConverter<Inherited<string>>
{
	/// <inheritdoc/>
	public override Inherited<string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		const string message_CannotKnowStringMeans = "We cannot know whether the string value is a real value or a property referenced.";
		const string message_CannotKnowNullMeans = "We cannot know what null means.";
		const string message_ExpectedStringOrObjectInstance = $"Expected string or object for {nameof(Inherited<>)}<string>.";

		if (reader.TokenType is var defaultTokenType and (JsonTokenType.String or JsonTokenType.Null))
		{
			// Ambiguous raw string.
			reader.Read();
			throw new AmbiguousMatchException(defaultTokenType == JsonTokenType.String ? message_CannotKnowStringMeans : message_CannotKnowNullMeans);
		}

		if (reader.TokenType != JsonTokenType.StartObject)
		{
			throw new JsonException(message_ExpectedStringOrObjectInstance);
		}

		var (value, reference) = (default(string), default(string));
		var namingPolicy = options.PropertyNamingPolicy ?? JsonNamingPolicy.CamelCase;
		while (reader.Read())
		{
			if (reader.TokenType == JsonTokenType.EndObject)
			{
				break;
			}
			if (reader.TokenType != JsonTokenType.PropertyName)
			{
				continue;
			}

			var propertyName = namingPolicy.ConvertName(reader.GetString()!);
			reader.Read();
			if (propertyName == namingPolicy.ConvertName(Inherited<string>.InheritedReferencedPropertyName))
			{
				reference = reader.GetString();
			}
			else if (propertyName == namingPolicy.ConvertName(Inherited<string>.ValuePropertyName))
			{
				value = reader.TokenType == JsonTokenType.Null ? null : reader.GetString();
			}
			else
			{
				switch (options.UnmappedMemberHandling)
				{
					case JsonUnmappedMemberHandling.Skip:
					{
						reader.Skip();
						break;
					}
					case JsonUnmappedMemberHandling.Disallow:
					{
						throw new JsonException(message_InvalidPropertyName(propertyName));
					}
					default:
					{
						throw new JsonException(message_UndefinedUnmappedMemberBehavior(options));
					}
				}
			}
		}

		return reference is null ? Inherited<string>.FromValue(value ?? string.Empty) : Inherited<string>.FromMemberName(reference);


		static string message_InvalidPropertyName(string propertyName) => $"Invalid property name '{propertyName}'.";

		static string message_UndefinedUnmappedMemberBehavior(JsonSerializerOptions options)
			=> $"'{nameof(options)}.{nameof(options.UnmappedMemberHandling)}' holds an undefined value '{options.UnmappedMemberHandling}'.";
	}

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, Inherited<string> value, JsonSerializerOptions options)
	{
		var valuePropertyString = (options.PropertyNamingPolicy ?? JsonNamingPolicy.CamelCase).ConvertName(nameof(value));
		if (value.HasValue)
		{
			writer.WriteStartObject();
			writer.WriteString(valuePropertyString, value.Value);
			writer.WriteEndObject();
			return;
		}

		writer.WriteStartObject();
		writer.WriteString(Inherited<string>.InheritedReferencedPropertyName, value.Reference);
		writer.WriteEndObject();
	}
}
