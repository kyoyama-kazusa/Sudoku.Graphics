namespace System;

/// <summary>
/// Provides extension members on <see cref="Environment"/>.
/// </summary>
/// <seealso cref="Environment"/>
public static class EnvironmentExtensions
{
	/// <inheritdoc cref="EnvironmentExtensions"/>
	extension(Environment)
	{
		/// <summary>
		/// Represents desktop path.
		/// </summary>
		public static string DesktopPath => Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
	}
}
