namespace Sudoku.Graphics;

/// <summary>
/// Represents exporting options.
/// </summary>
public sealed class CanvasExportingOptions : IOptionsProvider<CanvasExportingOptions>
{
	/// <summary>
	/// Indicates the default options.
	/// </summary>
	public static readonly CanvasExportingOptions Default = new();


	/// <summary>
	/// Indicates the quality. Range 0..100. Default 80.
	/// </summary>
	public int Quality { get; init; } = 80;


	/// <inheritdoc/>
	static CanvasExportingOptions IOptionsProvider<CanvasExportingOptions>.DefaultInstance => Default;


	/// <inheritdoc/>
	public void WriteTo(string path, JsonSerializerOptions? options = null)
	{
		var json = JsonSerializer.Serialize(this, options);
		File.WriteAllText(path, json);
	}


	/// <inheritdoc/>
	public static CanvasExportingOptions ReadFrom(string path, JsonSerializerOptions? options = null)
	{
		var json = File.ReadAllText(path);
		return JsonSerializer.Deserialize<CanvasExportingOptions>(json, options)!;
	}
}
