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
	public Inherited(string reference)
	{
		ArgumentNullException.ThrowIfNull(reference);
		_reference = reference;
		_value = default;
	}

	/// <summary>
	/// Initializes an <see cref="Inherited{T}"/> instance via the specified value.
	/// </summary>
	/// <param name="value">The value.</param>
	public Inherited(T value)
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
	/// <param name="maxDepth">The max depth. By default it's 64.</param>
	/// <exception cref="InvalidOperationException">
	/// Throws when at least one invalid case is encountered:
	/// <list type="bullet">
	/// <item>Maximum resolution depth exceeded (May cause a cycle)</item>
	/// <item>Cycle detected when resolving a certain reference</item>
	/// <item>Reference is not found by resolver.</item>
	/// </list>
	/// </exception>
	public T Resolve<TOwner>(TOwner owner, int maxDepth = 64) where TOwner : notnull
	{
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
				throw new InvalidOperationException("Maximum resolution depth exceeded.");
			}
			if (!visited.Add(currentReference))
			{
				throw new InvalidOperationException($"Cycle detected while resolving reference '{currentReference}'.");
			}

			var propertyInfo = ownerType.GetProperty(currentReference, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
				?? throw new InvalidOperationException($"Property '{currentReference}' not found on type '{ownerType.Name}'.");
			var propertyValue = propertyInfo.GetValue(owner)
				?? throw new InvalidOperationException($"Property '{currentReference}' is null.");
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
					throw new InvalidOperationException($"Property '{currentReference}' is not Inherited<{typeof(T).Name}>.");
				}
			}
		}
	}

	/// <summary>
	/// Returns the real value if the current instance has a value; otherwise, <see langword="default"/>(<typeparamref name="T"/>).
	/// </summary>
	/// <returns>The real value or <see langword="default"/>(<typeparamref name="T"/>).</returns>
	public T? GetValueOrDefault() => HasValue ? _value : default;
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
			return new(reader.GetString()!);
		}

		// If token is null.
		if (reader.TokenType == JsonTokenType.Null)
		{
			// treat as value == default(T).
			reader.Read();
			return new(default(T)!);
		}

		// Otherwise deserialize as T.
		return new(JsonSerializer.Deserialize<T>(ref reader, _options)!);
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
		if (reader.TokenType == JsonTokenType.String)
		{
			// Ambiguous raw string => treat as concrete string value.
			return new(reader.GetString()!);
		}

		if (reader.TokenType == JsonTokenType.Null)
		{
			reader.Read();
			return new(null!);
		}

		if (reader.TokenType != JsonTokenType.StartObject)
		{
			throw new JsonException($"Expected string or object for {nameof(Inherited<>)}<string>.");
		}

		var value = default(string);
		var reference = default(string);
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

			var propertyName = reader.GetString()!;
			reader.Read();
			if (propertyName == Inherited<string>.InheritedReferencedPropertyName)
			{
				reference = reader.GetString();
			}
			else if (propertyName == Inherited<string>.ValuePropertyName)
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
						throw new JsonException($"Invalid property name '{propertyName}'.");
					}
					default:
					{
						throw new JsonException(
							$"'{nameof(options)}.{nameof(options.UnmappedMemberHandling)}' holds an undefined value '{options.UnmappedMemberHandling}'."
						);
					}
				}
			}
		}

		return reference is null ? new(value ?? string.Empty) : new(reference);
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
