using ElasticSearchHelper.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Nest;
using System;

namespace ElasticSearchHelper.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IElasticSearchService service;

        public TestController(IElasticSearchService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult> Connection()
        {
            var res = await service.TestConnectionAsync();
            return Ok(res.DebugInformation);
        }

        [HttpGet("keyword")]
        public async Task<ActionResult<List<Order>>> SearchFunc(string keyword)
        {
            List<Order> orders = [];
            Func<QueryContainerDescriptor<Order>, QueryContainer> query = q => q.MultiMatch(
                m => m.Fields(
                    f => f.Field(f => f.ProjectText)).Query(keyword));
            var res = await service.SearchAsync(query);
            foreach (var order in res)
            {
                orders.Add(order);
            }
            return Ok(orders);
        }
        
        [HttpGet("keyword")]
        public async Task<ActionResult> SearchDescriptor(string keyword)
        {
            List<Order> orders = [];
            var search = new SearchDescriptor<Order>();
            search.Query(q => q.MultiMatch(
                m => m.Fields(
                    f => f.Field(f => f.ProjectText)).Query(keyword)));

            var res = await service.SearchAsync(search);
            foreach (var order in res)
            {
                orders.Add(order);
            }
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> CreateIndex()
        {
            var descriptor = new CreateIndexDescriptor(nameof(People).ToLower()).Map<People>(p => p.AutoMap());
            var res= await service.CreateIndexAsync(nameof(People).ToLower(), descriptor);
            return Ok(res.DebugInformation);
        }
        [HttpPost]
        public async Task<ActionResult> Index()
        {
            People person = new()
            {
                Id = 1,
                Name = "Jon Doe",
                Description = "Software Engineer",
                Email = "john.doe@example.com",
                Phone = "123-456-7890"
            };
            
            var res = await service.IndexAsync(person, nameof(People).ToLower());
            return Ok(res.DebugInformation);
        }
        [HttpPost]
        public async Task<ActionResult> Update()
        {
            People person = new()
            {
                Id = 1,
                Name = "J D",
                Description = "Software",
                Email = "jo.oe@example.com",
                Phone = "13-46-90"
            };
            var res = await service.UpdateAsync(person, nameof(People).ToLower(),1);
            return Ok(res.DebugInformation);
        }
        [HttpPost]
        public async Task<ActionResult> Bulk()
        {
            var bulkDescriptor = new BulkDescriptor()
                .Index<People>(i => i.Document(new People { Id = 3, Name = "Alice" }).Id(Guid.NewGuid()))
                .Index<People>(i => i.Document(new People { Id = 2, Name = "Bob" }).Id(Guid.NewGuid()))
                .Delete<People>(d => d.Id(1));

            var res = await service.BulkAsync(bulkDescriptor, nameof(People).ToLower());
            return Ok(res.DebugInformation);
        }
    }
}
