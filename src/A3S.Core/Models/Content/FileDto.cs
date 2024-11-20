using A3S.Core.Domain.Entities;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace A3S.Core.Models.Content
{
    public class FileDto
    {
        public required Guid FileId { get; set; }
        public string FileName { get; set; }
        public Guid FolderId { get; set; }
        public string FileUrl { get; set; }
        public string FileSize { get; set; }
        public required Guid CreatorId { get; set; }
        public string Tag { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<FileContent, FileDto>();
            }
        }
    }
}
