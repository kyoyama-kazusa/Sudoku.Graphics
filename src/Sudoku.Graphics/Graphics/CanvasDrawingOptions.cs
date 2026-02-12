namespace Sudoku.Graphics;

/// <summary>
/// Represents drawing options.
/// </summary>
public sealed class CanvasDrawingOptions : IOptionsProvider<CanvasDrawingOptions>
{
	/// <summary>
	/// Indicates the default instance.
	/// </summary>
	public static readonly CanvasDrawingOptions Default = new();


	/// <summary>
	/// Indicates corner radius ratio of rounded rectangle of border lines, relative to cell size. By default it's 0.12.
	/// </summary>
	[ResourceKey("GridBorderRoundedRectangleCornerRadius")]
	public Inherited<Scale> GridBorderRoundedRectangleCornerRadius { get; set; } = Inherited<Scale>.FromValue(.12M);

	/// <summary>
	/// Indicates stroke thickness ratio of thin lines, relative to cell size. By default it's 0.0225.
	/// </summary>
	[ResourceKey("ThinLineWidth")]
	public Inherited<Scale> ThinLineWidth { get; set; } = Inherited<Scale>.FromValue(.0225M);

	/// <summary>
	/// Indicates stroke thickness ratio of thick lines, relative to cell size. By default it's 0.09.
	/// </summary>
	[ResourceKey("ThickLineWidth")]
	public Inherited<Scale> ThickLineWidth { get; set; } = Inherited<Scale>.FromValue(.09M);

	/// <summary>
	/// Represents background color. By default it's equivalent value of color <see cref="SKColors.White"/>.
	/// </summary>
	/// <seealso cref="SKColors.White"/>
	[ResourceKey("BackgroundColor")]
	public Inherited<SerializableColor> BackgroundColor { get; set; } = Inherited<SerializableColor>.FromValue(SKColors.White);

	/// <summary>
	/// Represents thin line color. By default it's equivalent value of color <see cref="SKColors.Black"/>.
	/// </summary>
	/// <seealso cref="SKColors.Black"/>
	[ResourceKey("ThinLineColor")]
	public Inherited<SerializableColor> ThinLineColor { get; set; } = Inherited<SerializableColor>.FromValue(SKColors.Black);

	/// <summary>
	/// Represents thick line color. By default it's same value inherited from property <see cref="ThinLineColor"/>.
	/// </summary>
	/// <seealso cref="ThinLineColor"/>
	[ResourceKey("ThickLineColor")]
	public Inherited<SerializableColor> ThickLineColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(ThinLineColor));

	/// <summary>
	/// Represents grid line template to be drawn. By default it will be initialized like
	/// <c><see langword="new"/> <see cref="StandardLineTemplate"/>(3)</c>.
	/// </summary>
	/// <seealso cref="StandardLineTemplate"/>
	[ResourceKey("GridLineTemplate")]
	public LineTemplate GridLineTemplate { get; set; } = new StandardLineTemplate(3);

	/// <summary>
	/// Represents JSudoku color set. The color set contains 27 different colors.
	/// </summary>
	[ResourceKey("JSudokuColorSet")]
	public Inherited<SerializableColorSet> JSudokuColorSet { get; set; } = Inherited<SerializableColorSet>.FromValue(
		[
			new(255, 187, 187), // Red
			new(187, 255, 187), // Green
			new(187, 187, 255), // Purple
			new(255, 255, 187), // Yellow
			new(255, 187, 255), // Pink
			new(187, 255, 255), // Skyblue
			new(187, 187, 187), // Gray
			new(255, 221, 221), // Light red
			new(221, 255, 221), // Light green
			new(221, 221, 255), // Light purple
			new(255, 255, 221), // Light yellow
			new(255, 221, 255), // Light pink
			new(221, 255, 255), // Light skyblue
			new(221, 221, 221), // Light gray
			new(255, 153, 153), // Dark red
			new(153, 255, 153), // Dark green
			new(153, 153, 255), // Dark purple
			new(255, 255, 153), // Dark yellow
			new(255, 153, 255), // Dark pink
			new(153, 255, 255), // Dark skyblue
			new(153, 153, 153), // Dark gray
			new(255, 119, 119), // Deep dark red
			new(119, 255, 119), // Deep dark green
			new(119, 119, 255), // Deep dark purple
			new(255, 255, 119), // Deep dark yellow
			new(255, 119, 255), // Deep dark pink
			new(119, 255, 255), // Deep dark skyblue
			new(119, 119, 119), // Deep dark gray
		]
	);


	/// <inheritdoc/>
	static CanvasDrawingOptions IOptionsProvider<CanvasDrawingOptions>.DefaultInstance => Default;


	/// <inheritdoc/>
	public void WriteTo(string path, JsonSerializerOptions? options = null)
	{
		var json = JsonSerializer.Serialize(this, options);
		File.WriteAllText(path, json);
	}


	/// <inheritdoc/>
	public static CanvasDrawingOptions ReadFrom(string path, JsonSerializerOptions? options = null)
	{
		var json = File.ReadAllText(path);
		return JsonSerializer.Deserialize<CanvasDrawingOptions>(json, options)!;
	}
}
