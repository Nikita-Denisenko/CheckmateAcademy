using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Domain.ValueObjects.Game;

namespace CheckmateAcademy.Infrastructure.Mappers
{
    public static class ChessBoardSetupMapper
    {
        public static ChessBoardSetup ToDomain(ChessBoardSetupData data)
        {
            return new ChessBoardSetup(
                data.Pieces.Select(PieceSetupMapper.ToDomain));
        }

        public static ChessBoardSetupData ToData(ChessBoardSetup setup)
        {
            return new ChessBoardSetupData
            {
                Pieces = setup.Pieces
                    .Select(PieceSetupMapper.ToData)
                    .ToList()
            };
        }
    }
}
