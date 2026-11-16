
using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Infrastructure.Mappers
{
    public static class GameMapper
    {
        public static Game ToDomain(GameData data)
        {
            var initialPosition = ChessBoardSetupMapper.ToDomain(
                data.InitialPosition);

            var game = new Game(
                data.Id,
                data.UserId,
                data.BotId,
                initialPosition);

            foreach (var moveData in data.Moves)
            {
                var move = GameMoveMapper.ToDomain(
                    moveData,
                    game.ChessBoard);

                game.MakeMove(move);
            }

            return game;
        }

        public static GameData ToData(Game game)
        {
            return new GameData
            {
                Id = game.Id,
                UserId = game.UserId,
                BotId = game.BotId,
                InitialPosition = ChessBoardSetupMapper.ToData(
                    game.InitialPosition),
                Moves = game.Moves
                    .Select(GameMoveMapper.ToData)
                    .ToList()
            };
        }
    }
}

