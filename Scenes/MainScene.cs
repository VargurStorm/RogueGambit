using System.Runtime.CompilerServices;
using RogueGambit.Handlers;
using RogueGambit.Handlers.Interface;
using RogueGambit.Logic;
using BoardHandler = RogueGambit.Handlers.BoardHandler;

[assembly: InternalsVisibleTo("RogueGambit.Tests")]

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
		RegisterService<IGameStateManager>(GetNode<GameStateHandler>("/root/MainScene/GameStateHandler"));
		RegisterService<IBoardManager>(GetNode<BoardHandler>("/root/MainScene/BoardHandler"));
		RegisterService<IInputManager>(GetNode<InputHandler>("/root/MainScene/InputHandler"));
		RegisterService<IMoveManager>(GetNode<MoveHandler>("/root/MainScene/MoveHandler"));
		RegisterService<IPieceManager>(GetNode<PieceHandler>("/root/MainScene/PieceHandler"));
		RegisterService<ITurnManager>(GetNode<TurnHandler>("/root/MainScene/TurnHandler"));

		RegisterService<IMoveLogic>(new MoveLogic());

		RegisterNodeFactory<BoardSquare>((BoardHandler)GetService<IBoardManager>());
		RegisterNodeFactory<Piece>((PieceHandler)GetService<IPieceManager>());
	}
}
