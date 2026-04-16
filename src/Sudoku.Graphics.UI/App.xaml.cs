namespace Sudoku.Graphics.UI;

/// <summary>
/// Interaction logic for App.xaml.
/// </summary>
public partial class App : Application
{
	/// <summary>
	/// Indicates the user preferences.
	/// </summary>
	internal static Preferences UserPreferences
	{
		get => (Preferences)Current.Resources["UserPreferences"];

		set => Current.Resources["UserPreferences"] = value;
	}
}
