using AutoMapper;
using AutoMapper.EquivalencyExpression;
using AutoMapProblem.DTO;
using AutoMapProblem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMapProblem
{
    public class MyMapperProfile : Profile
    {
        public MyMapperProfile()
        {
            CreateMap<Category, CategoryTree>()
                .ForMember(mod => mod.Categories, x => x.MapFrom(ent => ent.Categories))
                .ForMember(mod => mod.Topics, x => x.MapFrom(ent => ent.CategoryTopics))
                .ForMember(mod => mod.ProjectId, x => x.MapFrom(ent => ent.ProjectId))
                .ForMember(mod => mod.CategoryId, x => x.MapFrom(ent => ent.CategoryId))
                //.EqualityComparison((x, y) => x.ProjectId == y.ProjectId && x.CategoryId == y.CategoryId)
            ;

            CreateMap<CategoryTree, Category>()
               .ForMember(dest => dest.Categories, x => x.MapFrom(src => src.Categories))
               .ForMember(dest => dest.ProjectId, x => x.MapFrom(src => src.ProjectId))
               .ForMember(dest => dest.CategoryId, x => x.MapFrom(src => src.CategoryId))
               .ForMember(dest => dest.Name, x => x.MapFrom(src => src.Name))
               .ForMember(dest => dest.ParentCategoryId, x => x.MapFrom(src => src.ParentCategoryId))
               .ForMember(dest => dest.Order, x => x.MapFrom(src => src.Order))
               .ForMember(dest => dest.ParentCategory, x => x.Ignore())
               .ForMember(dest => dest.CategoryTopics, x => x.MapFrom(src => src.Topics))
               //.EqualityComparison((x, y) => x.ProjectId == y.ProjectId && x.CategoryId == y.CategoryId)
            ;

            CreateMap<CategoryTopicList, CategoryTopic>()
                .ForMember(ent => ent.ProjectId, x => x.MapFrom(mod => mod.ProjectId))
                .ForMember(ent => ent.TopicId, x => x.MapFrom(mod => mod.TopicId))
                .ForMember(ent => ent.Order, x => x.MapFrom(mod => mod.Order))
                .ForMember(ent => ent.Category, x => x.Ignore())
                //.EqualityComparison((x, y) => x.ProjectId == y.ProjectId && x.CategoryId == y.CategoryId && x.TopicId == y.TopicId)
                ;

            CreateMap<CategoryTopic, CategoryTopicList>()
                .ForMember(dest => dest.ProjectId, x => x.MapFrom(src => src.ProjectId))
                .ForMember(dest => dest.TopicId, x => x.MapFrom(src => src.TopicId))
                .ForMember(dest => dest.Order, x => x.MapFrom(src => src.Order))
                //.EqualityComparison((x, y) => x.ProjectId == y.ProjectId && x.CategoryId == y.CategoryId && x.TopicId == y.TopicId)
                ;
            ;

        }
    }
}
