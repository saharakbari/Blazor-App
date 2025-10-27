using AutoMapper;
using Domain.Entities;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper
{
    public class TaskItemMapper : Profile
    {
        public TaskItemMapper()
        {
            //CreateMap<TaskItem, TaskItemDTO>().ReverseMap();

            // Entity → DTO
            CreateMap<TaskItem, TaskItemDTO>()
                .ForMember(dest => dest.DueDate,
                           opt => opt.MapFrom(src => src.DueDate.HasValue
                                                       ? src.DueDate.Value.ToString("yyyy-MM-dd")
                                                       : null))
                .ForMember(dest => dest.CreatedAt,
                           opt => opt.MapFrom(src => src.CreatedAt.ToString("yyyy-MM-dd")));

            // DTO → Entity
            CreateMap<TaskItemDTO, TaskItem>()
                .ForMember(dest => dest.DueDate,
                           opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.DueDate)
                                                       ? DateTime.Parse(src.DueDate)
                                                       : (DateTime?)null))

               .ForMember(dest => dest.CreatedAt,
                           opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.CreatedAt.ToString())
                                                       ? DateTime.Parse(src.CreatedAt.ToString())
                                                       : DateTime.Now));

        }


    }
}

