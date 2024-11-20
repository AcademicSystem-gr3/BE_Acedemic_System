using A3S.Core.Domain.Entities;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace A3S.Core.Models.Content
{
    public class FolderDto
    {
        public required Guid FolderId { get; set; }
        public string FolderName { get; set; }
        public Guid? Parent { get; set; }
        public required Guid OwnerId { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public List<FileDto> Files { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Folder, FolderDto>();
            }
        }
    }
}
