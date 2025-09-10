# GoblinChess 🏰⚔️

A decentralized chess variant built on the Chromia blockchain using the Rell programming language. GoblinChess combines traditional chess mechanics with magical spells and enchantments, creating a unique strategic gaming experience where players can cast cards to alter the battlefield.

## 🎮 What is GoblinChess?

GoblinChess is an innovative chess variant that introduces magical elements to the classic game. Players not only move pieces according to traditional chess rules but can also play spell cards to enchant pieces, manipulate the board, and create strategic advantages.

### Key Features

- **Traditional Chess Foundation**: Standard chess pieces (King, Queen, Rook, Bishop, Knight, Pawn) with familiar movement patterns
- **Spell Card System**: 10 unique spell cards that can alter gameplay dynamics
- **Turn-Based Structure**: Each player rotation consists of 3 phases: Card → Move → Neutral
- **Multiple Game Modes**: Classic and Chaotic variants with different time controls
- **ELO Rating System**: Competitive ranking system for players
- **Decentralized**: Fully on-chain game state and logic

## 🃏 Spell Cards

### Enchantments
- **🐸 Frog**: Transform a piece temporarily, altering its movement capabilities
- **🗿 Turn to Stone**: Petrify a piece, making it immobile for several turns
- **🧪 Potion**: Enhance a piece with special abilities
- **⚔️ Knighted**: Grant knight-like movement to any piece
- **🦘 Jump**: Allow a piece to leap over obstacles

### Board Manipulation
- **🔄 Switch Place**: Swap positions of two pieces instantly
- **🧱 Wall**: Create barriers on the board to block movement
- **🌀 Portal**: Create magical gateways for piece teleportation

### Tactical Spells
- **💀 Resurrect**: Bring back a captured piece (delayed effect)
- **⏳ Double Move**: Make two moves in a single turn

## 🎯 Game Modes

### Classic Mode
- Traditional chess emphasis with few random events. 

### Chaotic Mode
- More unpredictable gameplay, since the result of every attack is determined by rolling dice. Primarily for kids or if you want to beat someone above your rating. 

## 🏗️ Architecture

This dApp is built using **Rell**, Chromia's domain-specific language for blockchain development. The architecture follows a modular design:

### Core Modules
- **Game Management**: Player registration, game creation, lobby system
- **Turn System**: Complex turn progression with card/move/neutral phases
- **Piece Logic**: Traditional chess piece behavior and movement validation
- **Card System**: Spell casting mechanics and effect resolution
- **Board State**: Position tracking and compressed game state storage
- **Check/Mate Detection**: Advanced chess rule validation, since the cards effect mate calculation.
- **Event System**: Game event logging and replay functionality

### Database Schema
- **Players**: User profiles with ELO ratings and statistics
- **Games**: Active and completed game states
- **Turns**: Granular turn-by-turn game progression
- **Pieces**: Individual piece tracking with enchantments
- **Cards**: Spell card plays and effects
- **Events**: Comprehensive game event logging

## 🚀 Developer Getting Started
You want to fork this repository, or just try to run it on your local computer.

### Prerequisites
- Chromia blockchain environment
- Rell compiler version 0.14.2 or higher

### Installation
For how to setup your environment you need to go to https://chromia.com.
You can also watch my videos explaining how I did it for this game: https://www.youtube.com/@GoblinChess_Olle

### Playing the Game

1. **Register as a Player**: Create your player profile with a unique username
2. **Join Lobby or Create Challenge**: Find opponents through the lobby system or direct challenges
3. **Play**: Alternate between playing spell cards and moving pieces
4. **Win Conditions**: Achieve checkmate, stalemate, or timeout victory

## 🔧 Operations
To get a feel for the API, we here describe the most common operations. A Chromia operation is something that updates the blockchain, and will therefore take some time (a fraction of a second) before the next block is built and the new data is visible.

### Player Operations
- `create_player(name)` - Register a new player
- `enter_lobby(name, game_type)` - Join matchmaking lobby, looking for someone within your rating range to play the given type of game.
- `create_challenge(opponent, game_type)` - Challenge specific player on a specific type of game

### Begin Game Operations
- `create_game_via_lobby(opponent, game_type)` - Start game from lobby
- `create_game_accept_challenge(challenge_id)` - Accept a challenge

### Running Game Operations
- `play_card(game_id, card_data, picks)` - Cast a spell card
- `move_peaceful/move_attack(game_id, move_data)` - Make chess moves
- `skip_neutral_move(game_id)` - Skip neutral phase. This needed to be forward compatible with version 2.0.

## 🔧 Queries 
Chromia makes a clear distinctions between operations and queries, where the latter won't update the blockchain, and will therefore return immediately.

### Query Operations
- `get_player_by_name(player_name)` - Get the player's pubkey from the name, if found
- `in_game(player_pubkey)` - Retrieve the current game for the player, if any
- `find_challenge(player_pubkey, timestamp)` - Returns a not-too-old challenge for the player, if any.

## 🏆 Competitive Features

- **ELO Rating System**: Dynamic skill-based matchmaking
- **Game Statistics**: Track wins, losses, draws, and performance
- **Replay System**: Full game reconstruction from event logs

## 🛠️ Development

### For Client Developers
This backend provides a complete API for building GoblinChess clients. The game state is fully deterministic and can be reconstructed from blockchain data. Being on a blockchain means that the data belongs to everyone and nobody, it's yours if you want to.

### For Rell Learners
This codebase serves as a tutorial for:
- Complex entity relationships in Rell
- State management in blockchain applications
- Game logic implementation
- Event-driven architecture
- Query optimization and how to reduce load on the database/blockchain.

## 📚 Technical Documentation
### Entity Relationship diagram
Chromia is unique among blockchain platforms since it builds on database tables. This is the ER-diagram for this game for example. 

![Entity Relationship Diagram](doc/img/rell_er.png)

### Turn Structure
Each player rotation consists of three turns:
1. **Card Turn**: Play a spell card (optional)
2. **Move Turn**: Make a chess move
3. **Neutral Turn**: Reserved for future neutral piece mechanics

There is a state machine validating that the progression from one turn to the next follows our rules called `verify_old_turn_type_new_turn_type()`:

[View turn validation logic →](src/turn/function_validation.rell)

### Randomness Handling
Handling random numbers is hard on the blockchain, since everything we save becomes public info. In this game we simply accept any random number from the client, but in the very end of the game we will verify all the sent random numbers in one go, by generating them just like the client did, using the client's seed. This works since both clients are required to provide their seeds at the end of the game, and if a player just shuts down their client they will lose. Had we stored the seeds early in the game, a player might try to fetch the opponent's seed and this way be able to generate the opponent's cards (which are supposed to be secret). This pic illustrates seed validation:  

![Game End Flow](doc/img/rell_game_over_flow.png)

### Main Menu
The Main Menu in my game client uses this flow, shown in the picture: 

![Game End Flow](doc/img/main_menu_state_machine.png)

To create a game is easy, but waiting for other players to accept an invitation means we need to do polling. This is because a blockchain is ignorant about the world outside, and cannot initiate contacts from server to client. Especially challenges are tricky, since both the player creating the challenge, and the player about to be challenged are polling for news at the same time. The picture below shows an example of a successful challenge: 

![Game End Flow](doc/img/rell_challenge_logic_happy_path.png)

The process is similar for the Lobby: 

![Game End Flow](doc/img/rell_lobby_logic_happy_path.png)

### Modules
We structure our Rell code into modules, and the idea is to encapsulate logic and keep the dependencies between modules to a minimum. In this game the module structure looks like this:

![Game End Flow](doc/img/rell_modules.png)

The "light modules" don't have any dependencies to entities, and are fast/easy to test automatically. The "heavy modules" likely require a full game to be tested properly. 

### Automatic Testing
The code has one big test that runs a complete game, from the first move to the inevitable checkmate, where we try to insert as many strange situations as possible:

[View turn validation logic →](src/test/test_all.rell)

## 🤝 Contributing

This codebase is open for specifically implementations of new game client. Should this become a popular game, having the data open from get-go will prevent the original developer (me) from raising prices or whatever evil things I might do in the future.

If you find a bug/security hole, please create a pull request.


## 📄 License

[License information to be added]

## 🔗 Links

- **Chromia Documentation**: https://docs.chromia.com/
- **Rell Language Reference**: https://docs.chromia.com/rell/language-features/
- **About the game**: http://goblinchess.com/

---

*Ready to cast your first spell? Join the battle and experience chess like never before!* ⚔️✨