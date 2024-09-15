namespace RogueGambit.Managers;

public partial class TurnManager : Node2D
{
    private GameStateManager _gameStateManager;
    private Sprite2D _turnSprite;
    private Dictionary<PieceOwner, string> TextureMap { get; set; }

    public override void _Ready()
    {
        GD.Print("...TurnManager ready.");
        InitializeNodes();
        _turnSprite.Position = new Vector2(50 + BoardConstants.BoardEndX, BoardConstants.BoardEndY / 2);
        _turnSprite.Visible = false;
        BuildTextureMap();
    }

    private void InitializeNodes()
    {
        _turnSprite = GetNode<Sprite2D>("TurnSprite2D");
        _gameStateManager = GetNode<GameStateManager>("/root/MainScene/GameStateManager");
    }

    public void UpdateTurn()
    {
        UpdateTurnSprite();
    }

    public void AdvanceTurn()
    {
        _gameStateManager.GameState.CurrentTurn = _gameStateManager.GameState.CurrentTurn == PieceOwner.Player
            ? PieceOwner.Ai
            : PieceOwner.Player;
        UpdateTurn();
    }

    public void SetTurn(PieceOwner owner)
    {
        _gameStateManager.GameState.CurrentTurn = owner;
        UpdateTurn();
    }

    private void UpdateTurnSprite()
    {
        _turnSprite.Texture = GD.Load<Texture2D>(TextureMap[_gameStateManager.GameState.CurrentTurn]);
        _turnSprite.Visible = true;
    }

    private void BuildTextureMap()
    {
        TextureMap = new Dictionary<PieceOwner, string>
        {
            { PieceOwner.Player, "res://Assets/UI/turn-white.png" },
            { PieceOwner.Ai, "res://Assets/UI/turn-black.png" }
        };
    }
}