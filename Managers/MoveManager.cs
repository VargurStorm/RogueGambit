namespace RogueGambit.Managers;

public partial class MoveManager : Node2D
{
    public PieceModel SelectedPiece { get; private set; }

    public override void _Ready()
    {
        GD.Print("...MoveManager ready.");
    }

    public void SelectPiece(PieceModel piece)
    {
        SelectedPiece = piece;
        piece.Instance.SelectedSprite.Visible = true;
        GD.Print("Selected piece: " + piece);
    }

    public void DeselectPiece()
    {
        SelectedPiece.Instance.SelectedSprite.Visible = false;
        SelectedPiece = null;
        GD.Print("Deselected piece: " + SelectedPiece);
    }

    public void ToggleSelectedPiece(PieceModel piece)
    {
        if (SelectedPiece == null)
        {
            SelectPiece(piece);
        }
        else if (SelectedPiece == piece)
        {
            DeselectPiece();
        }
        else
        {
            DeselectPiece();
            SelectPiece(piece);
        }
    }

    public void MovePiece(PieceModel piece, Vector2 targetPosition)
    {
        piece.GridPosition = targetPosition;
    }
}