using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using AutoMapProblem.DTO;
using AutoMapProblem.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoMapProblem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly MyContext _db;

        public CategoryController(IMapper mapper, MyContext db)
        {
            _mapper = mapper;
            _db = db;
        }

        [HttpGet]
        public async Task<List<CategoryTree>> GetCategories()
        {
            var cats = _db.Set<Category>().Include("Categories.CategoryTopics").Where(x => x.ParentCategory == null).ToList();

            return _mapper.Map<List<CategoryTree>>(cats);
        }

        [HttpPost]
        public async Task UpdateCategories(List<CategoryTree> categoryTrees)
        {
            foreach (var cat in categoryTrees)
            {
                _db.Set<Category>().Persist(_mapper).InsertOrUpdate(typeof(CategoryTree), cat);
            }
            await _db.SaveChangesAsync();
        }

        [HttpPut]
        public async Task Init()
        {
            foreach(var c in _db.Set<Category>())
            {
                _db.Set<Category>().Remove(c);
            }
            await _db.SaveChangesAsync();

            var projectId = Guid.NewGuid();
            var cat1 = new CategoryTree
            {
                ProjectId = projectId,
                CategoryId = Guid.NewGuid(),
                Name = "Test 1",
                Order = 0                
            };
            var cat11 = new CategoryTree
            {
                ProjectId = projectId,
                CategoryId = Guid.NewGuid(),
                Name = "Test 1.1",
                Order = 0,
                ParentCategoryId = cat1.CategoryId
            };
            var cat12 = new CategoryTree
            {
                ProjectId = projectId,
                CategoryId = Guid.NewGuid(),
                Name = "Test 1.2",
                Order = 1,
                ParentCategoryId = cat1.CategoryId
            };
            var topic1 = new CategoryTopicList()
            {
                ProjectId = projectId,
                CategoryId = cat11.CategoryId,
                TopicId = Guid.NewGuid()
            };

            cat1.Categories.Add(cat11);
            cat1.Categories.Add(cat12);
            cat11.Topics.Add(topic1);

            var catList = new List<CategoryTree>() { cat1 };

            await UpdateCategories(catList);
        }
    }
}