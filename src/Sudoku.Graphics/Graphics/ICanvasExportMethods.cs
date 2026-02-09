namespace Sudoku.Graphics;

/// <summary>
/// Represents a canvas object that supports exporting methods.
/// </summary>
public interface ICanvasExportMethods
{
	/// <summary>
	/// Export the current canvas into target file.
	/// </summary>
	/// <param name="path">The file path. The extension specified will be used as output file format.</param>
	/// <param name="options">The options.</param>
	void Export(string path, CanvasExportingOptions? options = null);
}
