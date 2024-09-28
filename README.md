# Rogue Gambit

A chess implemntation in Godot featuring a split node-model system and Godot scenes to make all parts of the chess board extensible.
The Github version of this code may be used freely for non-commercial purposes. At some point this will stop receiving updates as the game develops further.

During development, some parts of the code may be commented out or return simply to test features. The piece ownership checker, for example, may just return true to facilitate development.

## Features

- [x] Classic chess ruleset
- [x] Custom piece movement system
- [ ] Support for special moves (castling, en passant, pawn promotion)
- [x] Chained move capabilities for complex piece movements
- [x] Cross-platform compatibility (Windows, macOS, Linux)
- [ ] Chess AI
- [ ] Game playback
- [ ] UI and menus
- [ ] Fen string support
- [ ] UCI support
- [x] Friendly fire checker
- [ ] Unit tests
- [ ] Type and instance/reference safety checks with logging
- [x] Slide moves
- [ ] Jump-style moves (technically working due to vector addition but requires more work)
- [x] Move rotation on the fly using vectors
- [ ] Support for moves at non-cardinal angles

## Getting Started

### Prerequisites

- Godot Engine 4.x (Download from [Godot's official website](https://godotengine.org/))
- Dotnet 8.0.203 (Download from [Microsoft](https://dotnet.microsoft.com/))

### Installation

1. Clone the repository:
2. Open Godot Engine
3. Click "Import"
4. Navigate to the cloned repository and select the `project.godot` file
5. Click "Open"

### Running the Game

- Press the "Play" button in the Godot editor, or
- Export the game for your target platform and run the executable
- The game currently boots directly into a chess game.

## Development

This game is built using Godot 4.x and GDScript. Key components include:
- A dependency injection system for easily registering and injecting our handlers, logic, and factories.
- An extensible system for Pieces and BoardSquares.
> Models (`PieceModel` and `BoardSquareModel`) are separate from the actual instance of a node. They can be updated on their own and can then use an UpdateNode() function to pass on changes or create a new instance. Instances are also aware of their parent, allowing flexible behaviour when it comes to handling them.
- Managers/Handlers which each exist as on the scene tree to facilitate communication with Godot. Each has its own interaction with the game (the boardManager and PieceManager also have children that are board squares and pieces respectively)
- A flexible MoveSet system that allows attributes for moves. This should, in future, allow for much more interesting piece moves.
- A Vector2 based system for holding positions. Those vectors are simply multiplied by the grid square size to achieve board positioning.


## Building

To build the game for different platforms:

1. Open the project in Godot
2. Go to Project > Export
3. Select your target platform (Windows, macOS, Linux, etc.)
4. Click "Export Project"

## Contributing

- This is a personal project to test myself and potentially make a fun game out of it. If you have ideas or would like to help out then reach out to me.


## Acknowledgments

- [Sebasian Lague](https://www.youtube.com/@SebastianLague)'s chess coding adventures
- [Tom7](https://www.youtube.com/watch?v=DpXy041BIlA)'s Weird Chess Algorithms 
