namespace Sudoku.Graphics.UI.Services;

public interface IImageGeneratorService
{
	ImageSource? Generate(ICreateCanvasDialogService dialogService);
}
