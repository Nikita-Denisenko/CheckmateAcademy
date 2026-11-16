using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Infrastructure.Mappers
{
    public static class StudyChapterMapper
    {
        public static StudyChapter ToDomain(StudyChapterData data)
        {
            return new StudyChapter(
                data.Id,
                data.Title,
                data.Order,
                ChessBoardSetupMapper.ToDomain(data.InitialPosition),
                data.Moves.Select(StudyMoveNodeMapper.ToDomain));
        }

        public static StudyChapterData ToData(StudyChapter chapter)
        {
            return new StudyChapterData
            {
                Id = chapter.Id,
                Title = chapter.Title,
                Order = chapter.Order,
                InitialPosition = ChessBoardSetupMapper.ToData(chapter.InitialPosition),
                Moves = chapter.Moves
                    .Select(StudyMoveNodeMapper.ToData)
                    .ToList()
            };
        }
    }
}
