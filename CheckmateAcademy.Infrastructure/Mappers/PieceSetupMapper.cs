using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Domain.Enums;
using CheckmateAcademy.Domain.ValueObjects.Game;

namespace CheckmateAcademy.Infrastructure.Mappers
{
    public static class PieceSetupMapper
    {
        public static PieceSetup ToDomain(PieceSetupData data)
        {
            return new PieceSetup
            {
                Type = Enum.Parse<PieceType>(data.Type, true),
                Color = Enum.Parse<Color>(data.Color, true),
                Position = new BoardPosition(data.Row, data.Column)
            };
        }

        public static PieceSetupData ToData(PieceSetup setup)
        {
            return new PieceSetupData
            {
                Type = setup.Type.ToString(),
                Color = setup.Color.ToString(),
                Row = setup.Position.Row,
                Column = setup.Position.Column
            };
        }
    }
}
