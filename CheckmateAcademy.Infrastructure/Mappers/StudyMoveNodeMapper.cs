using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Infrastructure.Mappers
{
    public static class StudyMoveNodeMapper
    {
        public static StudyMoveNode ToDomain(StudyMoveNodeData data)
        {
            return new StudyMoveNode(
                data.Id,
                StudyMoveMapper.ToDomain(data.Move),
                data.NextMoves.Select(ToDomain));
        }

        public static StudyMoveNodeData ToData(StudyMoveNode node)
        {
            return new StudyMoveNodeData
            {
                Id = node.Id,
                Move = StudyMoveMapper.ToData(node.Move),
                NextMoves = node.NextMoves
                    .Select(ToData)
                    .ToList()
            };
        }
    }
}
