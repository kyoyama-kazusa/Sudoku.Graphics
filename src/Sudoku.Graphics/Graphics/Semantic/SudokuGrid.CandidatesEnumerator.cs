namespace Sudoku.Graphics.Semantic;

public partial class SudokuGrid
{
	/// <summary>
	/// Represents an enumerator that can iterate on each candidate in this collection.
	/// </summary>
	/// <param name="_candidates">Indicates the backing candidates.</param>
	/// <param name="_digitsCount">The number of digits.</param>
	public ref struct CandidatesEnumerator(BitArray _candidates, int _digitsCount) :
		IEnumerator<CandidatePosition>,
		IEnumerable<CandidatePosition>
	{
		/// <summary>
		/// Indicates the number of digits in a row in a cell to draw.
		/// </summary>
		private readonly int _desiredSize = ((Absolute)_digitsCount).GetCandidatesCountInEachRow();

		/// <summary>
		/// Indicates the backing index.
		/// </summary>
		private int _currentIndex = -1;


		/// <inheritdoc cref="IEnumerator{T}.Current"/>
		public CandidatePosition Current { get; private set; }

		/// <inheritdoc/>
		readonly object IEnumerator.Current => Current;


		/// <inheritdoc/>
		public readonly CandidatesEnumerator GetEnumerator() => this;

		/// <inheritdoc/>
		public bool MoveNext()
		{
			for (var i = _currentIndex + 1; i < _candidates.Length; i++)
			{
				if (_candidates[i])
				{
					var cell = i / _digitsCount;
					var digit = i % _digitsCount;
					Current = new(cell, _desiredSize, digit);
					_currentIndex = i;
					return true;
				}
			}

			Current = default;
			return false;
		}

		/// <summary>
		/// Returns a <see cref="ReadOnlySpan{T}"/> of <see cref="CandidatePosition"/> instances immediately.
		/// </summary>
		/// <returns></returns>
		internal readonly ReadOnlySpan<CandidatePosition> GetCandidatePositionsImmediately() => GetCandidates().AsSpan();

		/// <inheritdoc/>
		readonly void IDisposable.Dispose()
		{
		}

		/// <inheritdoc/>
		[DoesNotReturn]
		readonly void IEnumerator.Reset() => throw new NotSupportedException();

		/// <inheritdoc/>
		readonly IEnumerator IEnumerable.GetEnumerator() => GetCandidates().GetEnumerator();

		/// <inheritdoc/>
		readonly IEnumerator<CandidatePosition> IEnumerable<CandidatePosition>.GetEnumerator() => GetCandidates().GetEnumerator();

		private readonly List<CandidatePosition> GetCandidates()
		{
			var result = new List<CandidatePosition>();
			for (var i = 0; i < _candidates.Length; i++)
			{
				if (_candidates[i])
				{
					var cell = i / _digitsCount;
					var digit = i % _digitsCount;
					result.Add(new(cell, _desiredSize, digit));
				}
			}
			return result;
		}
	}
}
