using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Infrastructure.Mappers
{
    public static class StudyMapper
    {
        public static Study ToDomain(StudyData data)
        {
            return new Study(
                data.Id,
                data.UserId,
                data.Name,
                data.Description,
                data.Chapters.Select(StudyChapterMapper.ToDomain));
        }

        public static StudyData ToData(Study study)
        {
            return new StudyData
            {
                Id = study.Id,
                UserId = study.UserId,
                Name = study.Name,
                Description = study.Description,
                Chapters = study.Chapters
                    .Select(StudyChapterMapper.ToData)
                    .ToList()
            };
        }
    }
}
