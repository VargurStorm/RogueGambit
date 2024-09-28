namespace RogueGambit.Logic;

public class MoveLogic : IMoveLogic
{
    [Inject] private IGameStateManager _gameStateManager;

    public void Initialize()
    {
        InjectDependencies(this);
    }


    public HashSet<Vector2> GetValidMoves(PieceModel piece)
    {
        var validMoves = new HashSet<Vector2>();
        var moveSet = piece.MoveSet;

        foreach (var move in moveSet.Moves)
        {
            var direction = RotateVector(move.Direction, piece.Rotation);
            var validRay = CastRay(piece.GridPosition, direction, move.Range, move.Attributes);

            validMoves.UnionWith(validRay);
        }

        foreach (var validMove in validMoves) GD.Print(validMove);

        return validMoves;
    }

    private Vector2 RotateVector(Vector2 vector, int degrees)
    {
        if (degrees == 0) return vector;
        degrees = (degrees % 360 + 360) % 360;

        switch (degrees)
        {
            case 90:  return new Vector2(-vector.Y, vector.X);
            case 180: return new Vector2(-vector.X, -vector.Y);
            case 270: return new Vector2(vector.Y, -vector.X);
        }

        var radians = degrees * (Mathf.Pi / 180);
        var cos = Mathf.Cos(radians);
        var sin = Mathf.Sin(radians);

        var x = vector.X * cos - vector.Y * sin;
        var y = vector.X * sin + vector.Y * cos;

        return new Vector2(
            Mathf.Abs(x) < 0.001f ? 0 : Mathf.Round(x),
            Mathf.Abs(y) < 0.001f ? 0 : Mathf.Round(y)
        );
    }

    private List<Vector2> CastRay(Vector2 origin, Vector2 direction, int distance, HashSet<MoveAttrib> attributes = null)
    {
        var pieces = _gameStateManager.GameState.Pieces;
        var board = _gameStateManager.GameState.BoardSquares;

        var isAttackOnly = attributes?.Contains(MoveAttrib.AttackOnly) ?? false;
        var isMoveOnly = attributes?.Contains(MoveAttrib.MoveOnly) ?? false;

        var validMoves = new List<Vector2>();
        var rayPos = origin;

        for (var i = 1; i <= distance; i++)
        {
            rayPos += direction;

            if (!board.ContainsKey(rayPos)) break; // Out of bounds, move is invalid and ray stops

            if (pieces.ContainsKey(rayPos))
            {
                if (FriendlyFire || pieces[rayPos].Owner != pieces[origin].Owner)
                    if (!isMoveOnly)
                        validMoves.Add(rayPos); // Found an enemy or friendly (with friendly fire), move is valid

                break; // Friendly piece without friendly fire, stop the ray
            }

            // Empty square
            if (!isAttackOnly) validMoves.Add(rayPos);
        }

        return validMoves;
    }
}