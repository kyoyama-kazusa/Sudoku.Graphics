namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates background fill color.
	/// </summary>
	public Inherited<SerializableColor> BackgroundFillColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultFillColor));
}
