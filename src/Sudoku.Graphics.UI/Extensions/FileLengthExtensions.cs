namespace System;

/// <summary>
/// Provides extension members on <see cref="long"/> instances.
/// </summary>
internal static class FileLengthExtensions
{
	/// <param name="this">The current instance.</param>
	extension(long @this)
	{
		/// <summary>
		/// Converts the current file length value into string.
		/// </summary>
		/// <returns>The string.</returns>
		public string ToFileLengthString()
		{
			var kb = @this / 1024;
			var mb = @this / 1024 / 1024;
			var gb = @this / 1024 / 1024 / 1024;
			var tb = @this / 1024 / 1024 / 1024 / 1024;
			return (tb, gb, mb, kb) switch
			{
				(0, 0, 0, 0) => "0 B",
				(0, 0, 0, _) => $"{kb} KB",
				(0, 0, _, _) => $"{mb} MB",
				(0, _, _, _) => $"{gb} GB",
				_ => $"{tb} TB"
			};
		}
	}
}
