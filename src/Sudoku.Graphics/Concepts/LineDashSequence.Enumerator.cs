namespace Sudoku.Concepts;

public partial struct LineDashSequence
{
	/// <summary>
	/// Represents an enumerator object that iterates on each <see cref="float"/> values.
	/// </summary>
	/// <param name="values">The <see cref="float"/> values.</param>
	public ref struct Enumerator(ReadOnlySpan<float> values) : IEnumerator<float>
	{
		/// <summary>
		/// Indicates the backing values.
		/// </summary>
		private readonly ReadOnlySpan<float> _values = values;

		/// <summary>
		/// Indicates the current index.
		/// </summary>
		private int _index = -1;


		/// <inheritdoc/>
		public readonly float Current => _values[_index];

		/// <inheritdoc/>
		readonly object IEnumerator.Current => Current;


		/// <inheritdoc/>
		public bool MoveNext() => ++_index < _values.Length;

		/// <inheritdoc/>
		readonly void IDisposable.Dispose() { }

		/// <inheritdoc/>
		[DoesNotReturn]
		readonly void IEnumerator.Reset() => throw new NotImplementedException();
	}
}
