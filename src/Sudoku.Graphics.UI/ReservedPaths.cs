namespace Sudoku.Graphics.UI;

/// <summary>
/// Represents paths that are reserved by this program.
/// </summary>
internal static class ReservedPaths
{
	/// <summary>
	/// Indicates config directory path.
	/// </summary>
	public static readonly string ConfigFolderPath;

	/// <summary>
	/// Indicates config file path.
	/// </summary>
	public static readonly string ConfigFilePath;


	/// <summary>
	/// The static constructor of the current type.
	/// </summary>
	static ReservedPaths()
	{
		var myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
		ConfigFolderPath = Path.Combine(myDocuments, "SudokuGraphics");
		ConfigFilePath = Path.Combine(ConfigFolderPath, "config.json");
	}
}
