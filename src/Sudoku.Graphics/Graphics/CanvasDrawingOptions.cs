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
	/// Indicates stroke thickness ratio of thin lines, relative to cell size. By default it's 0.015.
	/// </summary>
	[ResourceKey("ThinLineWidth")]
	public Inherited<Scale> ThinLineWidth { get; set; } = Inherited<Scale>.FromValue(.015M);

	/// <summary>
	/// Indicates stroke thickness ratio of thick lines, relative to cell size. By default it's 0.06.
	/// </summary>
	[ResourceKey("ThickLineWidth")]
	public Inherited<Scale> ThickLineWidth { get; set; } = Inherited<Scale>.FromValue(.06M);

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
