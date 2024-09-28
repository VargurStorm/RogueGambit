using RogueGambit.Logic;
using RogueGambit.Logic.Interfaces;

public partial class MainScene : Node2D
{
	public override void _Ready()
	{
		GD.Print("...MainScene ready.");
	}

	public override void _EnterTree()
	{
		GD.Print("...MainScene entered tree.");
		RegisterServices();
	}

	private void RegisterServices()
	{
		RegisterService<IGameStateManager>(GetNode<GameStateManager>("/root/MainScene/GameStateManager"));
		RegisterService<IBoardManager>(GetNode<BoardManager>("/root/MainScene/BoardManager"));
		RegisterService<IInputManager>(GetNode<InputManager>("/root/MainScene/InputManager"));
		RegisterService<IMoveManager>(GetNode<MoveManager>("/root/MainScene/MoveManager"));
		RegisterService<IPieceManager>(GetNode<PieceManager>("/root/MainScene/PieceManager"));
		RegisterService<ITurnManager>(GetNode<TurnManager>("/root/MainScene/TurnManager"));

		RegisterService<IMoveLogic>(new MoveLogic());

		RegisterNodeFactory<BoardSquare>((BoardManager)GetService<IBoardManager>());
		RegisterNodeFactory<Piece>((PieceManager)GetService<IPieceManager>());
	}
}
