namespace System;

/// <summary>
/// Provides extension members on <see cref="MathF"/>.
/// </summary>
/// <seealso cref="MathF"/>
public static class MathFExtensions
{
	extension(MathF)
	{
		/// <inheritdoc cref="Math.Clamp(float, float, float)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Clamp(float value, float min, float max) => Math.Clamp(value, min, max);
	}
}
