namespace Sudoku.Graphics.UI;

public partial class MainWindow
{
	/// <summary>
	/// The static constructor of this type.
	/// </summary>
	static MainWindow()
	{
		ItemOperationHandlerFactory = [];
		foreach (var type in typeof(MainWindow).Assembly.GetTypes())
		{
			if (type.GetCustomAttribute<OperationHandlerAttribute>() is { SupportedItemType: var itemType }
				&& type.HasParameterlessConstructor)
			{
				ItemOperationHandlerFactory.Add(itemType, () => (OperationHandler)Activator.CreateInstance(type)!);
			}
		}

		BitmapEncoderFactory = new(StringComparer.OrdinalIgnoreCase)
		{
			{ ".jpg", static () => new JpegBitmapEncoder() },
			{ ".jpeg", static () => new JpegBitmapEncoder() },
			{ ".png", static () => new PngBitmapEncoder() },
			{ ".bmp", static () => new BmpBitmapEncoder() }
		};

		SerializerOptions = new()
		{
			WriteIndented = true,
			AllowTrailingCommas = false,
			IncludeFields = false,
			IgnoreReadOnlyFields = true,
			IgnoreReadOnlyProperties = true,
			AllowDuplicateProperties = false,
			RespectNullableAnnotations = true,
			RespectRequiredConstructorParameters = true,
			PropertyNameCaseInsensitive = false,
			AllowOutOfOrderMetadataProperties = true,
			IndentCharacter = ' ',
			IndentSize = 2,
			MaxDepth = 8,
			NewLine = "\r\n",
			Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
			PreferredObjectCreationHandling = JsonObjectCreationHandling.Populate,
			ReadCommentHandling = JsonCommentHandling.Skip,
			UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement,
			DefaultIgnoreCondition = JsonIgnoreCondition.Never,
			UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip,
			NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.AllowNamedFloatingPointLiterals,
			TypeInfoResolver = JsonTypeInfoResolver.Combine(
				JsonTypeInfoResolver.Create<Template>(),
				JsonTypeInfoResolver.Create<Item>(),
				new DefaultJsonTypeInfoResolver()
			),
			Converters =
			{
				new BitArrayJsonConverter(),
				new JsonStringEnumConverter()
			}
		};
	}
}
