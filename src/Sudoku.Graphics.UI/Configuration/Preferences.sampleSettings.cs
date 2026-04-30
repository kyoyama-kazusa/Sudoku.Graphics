namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates sample canvas cell size.
	/// </summary>
	public Inherited<float> SampleCanvasCellSize { get; set; } = Inherited<float>.FromValue(60);

	/// <summary>
	/// Indicates sample canvas margin.
	/// </summary>
	public Inherited<float> SampleCanvasMargin { get; set; } = Inherited<float>.FromValue(0);
}
