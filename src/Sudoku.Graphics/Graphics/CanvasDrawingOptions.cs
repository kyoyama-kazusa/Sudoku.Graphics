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
	/// Indicates font name of text that will be drawn with big size, like given and modifiable digits.
	/// By default it's <c>"Arial"</c>.
	/// </summary>
	public Inherited<string> BigTextFontName { get; set; } = Inherited<string>.FromValue("Arial");

	/// <summary>
	/// Indicates font name of text that will be drawn with small size, like candidates.
	/// By default it's derived from property <see cref="BigTextFontName"/>.
	/// </summary>
	/// <seealso cref="BigTextFontName"/>
	public Inherited<string> SmallTextFontName { get; set; } = Inherited<string>.FromPropertyName(nameof(BigTextFontName));

	/// <summary>
	/// Indicates font weight of text that will be drawn with big size, like given and modifiable digits.
	/// By default it's <see cref="SKFontStyleWeight.Normal"/>.
	/// </summary>
	public Inherited<SKFontStyleWeight> BigTextFontWeight { get; set; } =
		Inherited<SKFontStyleWeight>.FromValue(SKFontStyleWeight.Normal);

	/// <summary>
	/// Indicates font weight of text that will be drawn with small size, like candidates.
	/// By default it's derived from property <see cref="BigTextFontWeight"/>.
	/// </summary>
	/// <seealso cref="BigTextFontWeight"/>
	public Inherited<SKFontStyleWeight> SmallTextFontWeight { get; set; } =
		Inherited<SKFontStyleWeight>.FromPropertyName(nameof(BigTextFontWeight));

	/// <summary>
	/// Indicates font width of text that will be drawn with big size, like given and modifiable digits.
	/// By default it's <see cref="SKFontStyleWidth.Normal"/>.
	/// </summary>
	public Inherited<SKFontStyleWidth> BigTextFontWidth { get; set; } = Inherited<SKFontStyleWidth>.FromValue(SKFontStyleWidth.Normal);

	/// <summary>
	/// Indicates font width of text that will be drawn with small size, like candidates.
	/// By default it's derived from property <see cref="BigTextFontWidth"/>.
	/// </summary>
	/// <seealso cref="BigTextFontWidth"/>
	public Inherited<SKFontStyleWidth> SmallTextFontWidth { get; set; } =
		Inherited<SKFontStyleWidth>.FromPropertyName(nameof(BigTextFontWidth));

	/// <summary>
	/// Indicates font size of text that will be drawn in with big size, like given and modifiable digits.
	/// The scale value is related to a cell size.
	/// By default it's <c>0.75</c>.
	/// </summary>
	public Inherited<Scale> BigTextFontSizeScale { get; set; } = Inherited<Scale>.FromValue(.75M);

	/// <summary>
	/// Indicates font size of text that will be drawn in with small size, like candidates.
	/// By default it's derived from property <see cref="BigTextFontSizeScale"/>.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The scale value is related to a cell size; however, different-sized grids may produce different-ranged candidate values.
	/// </para>
	/// <para>
	/// Drawing API may use square root of size to split a cell into multiple subcells, in order to show them.
	/// For example, if a grid is 9 * 9 (of 81 cells), candidate values should be between 1 and 9 (both sides included),
	/// we can divide a cell into 3 * 3 subcells, in order to show all 9 digits.
	/// However, if a grid is 13 * 13 (of 169 cells), candidate values should be between 1 and 13,
	/// we should divide a cell into 4 * 4 subcells, in order to show all 13 digits.
	/// </para>
	/// <para>
	/// But, 3 (for 9 * 9 grids) or 4 (for 13 * 13 grids) can only be calculated from <see cref="PointMapper"/> instances,
	/// using properties <see cref="PointMapper.RowsCount"/> and <see cref="PointMapper.ColumnsCount"/>.
	/// Because we don't store any information on grid sizes here, we cannot know how to split the grid.
	/// Therefore, this property value is relative to cell itself.
	/// </para>
	/// </remarks>
	/// <seealso cref="BigTextFontSizeScale"/>
	/// <seealso cref="PointMapper.RowsCount"/>
	/// <seealso cref="PointMapper.ColumnsCount"/>
	public Inherited<Scale> SmallTextFontSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(BigTextFontSizeScale));

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
