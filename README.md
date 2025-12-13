

![GoblinChess](doc/img/2k_logo_banner_1920x348.png)

A decentralized chess variant built on the Chromia blockchain using the Rell programming language. GoblinChess combines traditional chess mechanics with magical cards.

## 📋 Table of Contents

- [🎮 What is GoblinChess?](#-what-is-goblinchess)
  - [Key Features](#key-features)
- [👥 Who is this for?](#-who-is-this-for)
  - [For Client Developers](#for-client-developers)
  - [For Rell Learners](#for-rell-learners)
- [🎯 Gameplay](#-gameplay)
  - [Spell Cards](#spell-cards)
    - [Enchantments](#enchantments)
    - [Board Manipulation](#board-manipulation)
    - [Delayed Spells](#delayed-spells)
  - [Game Modes](#game-modes)
    - [Classic Mode](#classic-mode)
    - [Chaotic Mode](#chaotic-mode)
  - [Playing the Game](#playing-the-game)
- [🔧 API](#-api)
  - [Operations](#operations)
    - [Initial Operations](#initial-operations)
    - [Game Creating Operations](#game-creating-operations)
    - [Running Game Operations](#running-game-operations)
  - [Queries](#queries)
    - [Sample Queries](#sample-queries)
- [🚀 Developer Getting Started](#-developer-getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [📚 Technical Documentation](#-technical-documentation)
  - [Entity Relationship diagram](#entity-relationship-diagram)
  - [Modules](#modules)
    - [Core Modules](#core-modules)
  - [Turn Structure](#turn-structure)
  - [Randomness Handling](#randomness-handling)
  - [Main Menu](#main-menu)
  - [Polling](#polling)
  - [Automatic Testing](#automatic-testing)
- [❓ QnA](#-qna)
  - [Tokens?](#tokens)
  - [Why put games on the blockchain?](#why-put-games-on-the-blockchain)
  - [Why Chromia?](#why-chromia)
  - [How do I report problems?](#how-do-i-report-problems)
- [📄 License](#-license)
- [🔗 Links](#-links)



## 🎮 What is GoblinChess?

GoblinChess is an innovative chess variant that introduces magical elements to the classic game. Players not only move pieces according to traditional chess rules but can also play spell cards to enchant pieces, manipulate the board, and create strategic advantages.

### Key Features
- **Traditional Chess Foundation**: In the beginning of the game only standard chess pieces exist on the board
- **Spell Card System**: 10 spell cards that can alter gameplay, each player starts with five random cards
- **Multiple Game Modes**: Classic and Chaotic variants with different time controls
- **ELO Rating System**: Standard chess ranking system for anyone playing online
- **Modern Chess Rules**: Supports everything you'd expect from competitive chess, e.g. en passant, repetition draw (3x), stalemate etc 
- **Replay System**: Full game reconstruction from event logs
- **Decentralized**: Fully on-chain. Nothing stored off-chain


## 👥 Who is this for? 
Why are you reading this?

### For Client Developers
This backend provides a complete API for building GoblinChess clients. Being on a blockchain means that the data belongs to everyone and nobody, it's yours if you want to.

### For Rell Learners
This codebase serves as a tutorial for:
- Real-world entity relationships in Rell
- State management in blockchain applications (nothing can be kept in memory between two requests)
- Turn-based game logic 
- Query optimization / how to reduce load on the database/blockchain
- How to keep future random numbers secret 

There are plenty of comments around the code, it is meant to be one big tutorial.


## 🎯 Gameplay 
A quick overview of the rules and features.

### Spell Cards
GoblinChess is chess-with-cards. The more twisted the cards are, the harder it is to implement them without bugs!

#### Enchantments
- **🐸 Frog**: Transform a piece temporarily, altering its movement capabilities
- **🗿 Turn to Stone**: Petrify a piece, making it immobile for 3 turns
- **🧪 Potion**: Enhance a piece with extra hitpoints (only works for Chaos mode)
- **⚔️ Knighted**: Grant knight-like movement to any piece
- **🦘 Jump**: Allow a piece to leap over obstacles

#### Board Manipulation
- **🔄 Switch Place**: Swap positions of two pieces instantly
- **🧱 Wall**: Create barriers on the board to block movement
- **🌀 Portal**: Create a permanent magical gateway for piece teleportation

#### Delayed Spells
- **💀 Resurrect**: Bring back a captured piece (delayed effect)
- **⏳ Double Move**: Make two moves in a single turn


###  Game Modes
The game can be played in one of two modes:

#### Classic Mode
- Traditional chess emphasis on few random events and a lot of strategy. 

#### Chaotic Mode
- More unpredictable gameplay, since the result of every attack is determined by rolling dice. Introduces a big portion of luck, making far-reaching calculation hopeless. 


### Playing the Game
The gameplay consists of four main phases:
1. **Register as a Player**: Create your player profile with a unique username
2. **Join Lobby or Create Challenge**: Find opponents through the lobby system or direct challenges
3. **Play**: Alternate between playing spell cards and moving pieces
4. **Win Conditions**: Achieve checkmate, stalemate, timeout victory or something else. There are ten ways to end the game, similar to modern chess.




## 🔧 API
A Chromia dApp exposes a host of REST endpoints, divided into "operations" and "queries":

### Operations
To get a feel for the API we here describe the most common operations. A "Chromia operation" is something that updates the blockchain, and will therefore take some time (a fraction of a second) before the next block is built and the new data is visible.

#### Initial Operations
- `create_player(name)` - Register a new player
- `enter_lobby(name, game_type)` - Join matchmaking lobby, looking for someone within your rating range to play the given type of game.
- `create_challenge(opponent, game_type)` - Challenge specific player on a specific type of game

#### Game Creating Operations
- `create_game_via_lobby(opponent, game_type)` - Start game from lobby
- `create_game_accept_challenge(challenge_id)` - Accept a challenge

#### Running Game Operations
- `play_card(game_id, card_data, picks)` - Cast a spell card
- `move_peaceful/move_attack(game_id, move_data)` - Make chess moves
- `skip_neutral_move(game_id)` - Skip neutral phase. This needed to be forward compatible with version 2.0.

### Queries 
A Chromia query won't update the blockchain, and will therefore return data immediately to the caller:

#### Sample Queries
- `get_player_by_name(player_name)` - Get the player's pubkey from the name, if found
- `in_game(player_pubkey)` - Retrieve the current game for the player, if any
- `find_challenge(player_pubkey, timestamp)` - Returns a not-too-old challenge for the player, if any.




## 🚀 Developer Getting Started
You can try out this code by cloning the repo and and start a Chromia node on your local computer.

### Prerequisites
- PostgreSQL

### Installation
You will need to install these Chromia-specific tools:
- Chromia blockchain environment, but only the `CHR` tool is needed.
- An editor with a Rell plugin is nice (Rell version 0.14.2 or higher), unless you like to read code in black-n-white?

For how to setup your environment you can visit: https://docs.chromia.com/ .
You can also watch videos explaining how we did it for this game: https://www.youtube.com/@GoblinChess_Olle

After you have cloned this repository to your local disk, you can start the node like this (Win):
```
> cd <cloned repository folder>
> %CHR%\chr.bat node start

Starting node with pubkey: 0324653EAC434488002CC06BBFB7F10FE18991E35F9FE4302DBEA6D2353DC0AB1C
INFO  2025-12-13 12:41:13.603 - [main] BaseApiInfrastructure Starting REST API on port 7740 and path /
INFO  2025-12-13 12:41:13.804 - [main] RestApi Rest API is listening on port 7740 and is attached on /
INFO  2025-12-13 12:41:13.804 - [main] BaseApiInfrastructure Starting Debug API on port 7750
INFO  2025-12-13 12:41:13.865 - [main] DebugApi Debug API listening on port 7750 and were given 7750, attached on /
Starting blockchain goblinchess_dapp with brid 8C84FB6713F4FC5B6884C393622D834ABC77375C04AA1D366DBB0A39BAB86DA2 on id 0
INFO  2025-12-13 12:41:19.410 [chain-id=0] - [main] BaseBlockchainProcessManager Starting of blockchain: 0
INFO  2025-12-13 12:41:20.154 [chain-id=0] - [main] SqlInit Initializing database (chain_iid = 0)
INFO  2025-12-13 12:41:21.054 [bc-rid=8C:6DA2, chain-id=0] - [main] EBFTSynchronizationInfrastructure I am alone signer
INFO  2025-12-13 12:41:21.098 [bc-rid=8C:6DA2, chain-id=0] - [main] NettyPeerConnector Node started listening on messaging port 9870
INFO  2025-12-13 12:41:21.149 [bc-rid=8C:6DA2, chain-id=0] - [main] BaseBlockchainProcessManager startBlockchain() - Blockchain has been started: ValidatorBlockchainProcess:RUNNING, blockchain RID: 8C84FB6713F4FC5B6884C393622D834ABC77375C04AA1D366DBB0A39BAB86DA2, signers: [0324653EAC434488002CC06BBFB7F10FE18991E35F9FE4302DBEA6D2353DC0AB1C]
Node is initialized
INFO  2025-12-13 12:41:21.237 [bc-rid=8C:6DA2, chain-id=0, block-rid=19:C711] - [0-BaseBlockDatabaseWorker] BaseBlockchainEngine Block is finalized: 48 ms, 0 net tps, 0 gross tps, delay: 0 ms, height: 0, accepted txs: 0, rejected txs: 0, root-hash: 46AF9064F12528CAD6A7C377204ACD0AC38CDC6912903E7DAB3703764C8DD5E5, block-rid: 1946AF63951DD0C057A22DE6F196E11A8AB8A1A9D1CFA058047E5B9FC656C711, prev-block-rid: 8C84FB6713F4FC5B6884C393622D834ABC77375C04AA1D366DBB0A39BAB86DA2
INFO  2025-12-13 12:41:21.284 [bc-rid=8C:6DA2, chain-id=0, block-rid=19:C711] - [0-BaseBlockDatabaseWorker] BaseBlockManager Committed block 1946AF63951DD0C057A22DE6F196E11A8AB8A1A9D1CFA058047E5B9FC656C711
```
If it looks something like this you have succeeded.


## 📚 Technical Documentation

### Entity Relationship diagram
Chromia is unique among blockchain platforms since it builds on database tables, called "entities". There are also "structs", and "enums" which work just like in other programming languages. This is the ER-diagram for the game: 

![Entity Relationship Diagram](doc/img/rell_er.png)

### Modules
The archtecture is based on the entity-relationship concept (from relational databases), but the logic is structured into modules, whereas each module holds a mix of entities, structs, enums and functions. Just like for programming in general, the idea is to encapsulate logic and keep the dependencies between modules to a minimum. In this game the module structure looks like this:

![Game End Flow](doc/img/rell_modules.png)

The "light modules" don't have any dependencies to entities. They are fast/easy to test automatically. The "heavy modules" are harder to unit test, since they will need extensive preparation of data before each test.

#### Core Modules
Below the most important modules are listed:
- **Main**: All operations and queries are defined in here. It's the entry point to the dApp.
- **Game**: Contains the entities `game`, `player` and the connection table `player_game`. Also handles various statistics for finished games.  
- **Challenge/Lobby**: These are the only two ways to create a new game.
- **Turn**: Turn progression with card/move/neutral phases.
- **Piece**: Controls the life-cycle of the pieces. Contains enities like `alive`, `limbo` and `dead`. 
- **Move**: Traditional chess piece movement validation, with extra rules for enchanted pieces.
- **Card**: Spell casting mechanics and effect resolution.
- **Board**: Keeping the board in memory during an operation is usually the most performant alternative. We use the `board_positions_all` struct to pass this information around.
- **Check**: Chess rule validation expanding on the traditional ones, since the cards (especially portals) affect the checkmate calculation.
- **Event**: Events are immutable, and can be used for replay functionality. This means old games can be analyzed by looking at (only) the event entities.


### Turn Structure
Each player rotation consists of three turns:
1. **Card Turn**: Play a spell card (optional)
2. **Move Turn**: Make a chess move
3. **Neutral Turn**: Reserved for future neutral piece mechanics OR a double move.

There is a state machine validating that the progression from one turn to the next follows our rules called `verify_old_turn_type_new_turn_type()`:

[View turn validation logic →](src/turn/function_validation.rell)

### Randomness Handling
Handling random numbers is hard on the blockchain, since everything we save becomes public info. In this game we simply accept any random number from the client, but in the very end of the game we will verify all the sent random numbers in one go, by generating them just like the client did, using the client's seed. This works since both clients are required to provide their seeds at the end of the game, and if a player just shuts down their client prematurely they will lose due to the missing seed. 

> **⚠️ Security Note**
> 
> Had we stored the seeds early in the game, a player might try to fetch the opponent's seed and this way be able to generate the opponent's cards (which are supposed to be secret). 

This pic illustrates the seed validation process:  

![Game End Flow](doc/img/rell_game_over_flow.png)

### Main Menu
The Main Menu of the first game client uses this flow, shown in the picture below. The red arrows can be ignored, since they only become relevant if there is an unexpected error happening on the Chromia node. Note that wherever we are - as long as we have "gone online" - we can always accept an incoming challenge, this way beginning a new game. 

![Game End Flow](doc/img/main_menu_state_machine.png)

### Polling
To create a game is easy, but waiting for other players to accept an invitation means we need to do polling. This is because a blockchain is ignorant about the world outside, and cannot initiate contacts from "server" (=node) to client. Especially challenges are tricky, since both the player creating the challenge, and the player about to be challenged are polling for news at the same time. The picture below shows an example of a successful challenge: 

![Create a Game via Challenge](doc/img/rell_challenge_logic_happy_path.png)

The process is similar for the Lobby: 

![Create a Game via Lobby](doc/img/rell_lobby_logic_happy_path.png)


### Automatic Testing
The code has one big test that runs a complete game, from the first move to the inevitable checkmate, where we try to insert as many strange situations as possible. You can run the tests via (Win):
```
> %CHR%\chr.bat test
```
The actual test is here: 

[View test code →](src/test/test_all.rell)

 We could have created more isolated tests, with unique database setup for each one. The benefit of this method is that we'll get an exact error message for every single test. But we didn't have time to do that in this project, instead we just setup a full game and tried to catch as many situations as possible inside this test-game. This is one way to do it, not necessarily the best one.

## ❓ QnA

### Tokens?
Yes this is a blockchain, but there are no tokens in this game. The commercial gaming-client can be bought for a few bucks, that is the only income-generating part of the project.

### Why put games on the blockchain?
The principle is openness: open source, open data. For on-chain games you (the reader) are free to implement a new game client, fork this code and/or fork the blockchain. Should this become a popular game, having the data open from the get-go will prevent the original developer from raising prices or whatever evil things they might do in the future. Keeping everything in the open is a solid strategy, prevents vendor lock-in.

### Why Chromia?
Chromia is a great platform for developing software, no matter if you are using tokens or not. The multi-chain approach makes performance problems a breeze. There are no transaction fees, making everything much easier. Disclaimer: Olle is a Core developer working for Chromaway, the company behind Chromia.

### How do I report problems?
If you find a bug/security hole, please create a pull request or email info _at_ goblinchess (dot) com. 

## 📄 License

MIT License

Copyright (c) 2025 Panaq AB, Olle Kullberg

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

## 🔗 Links

- **Chromia Documentation**: https://docs.chromia.com/
- **Rell Language Reference**: https://docs.chromia.com/rell/language-features/
- **About the game**: http://goblinchess.com/

---

*Ready to cast your first spell? Join the battle and experience chess like never before!* ⚔️✨