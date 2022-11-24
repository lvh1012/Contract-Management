using API.DTOs;
using API.Model;
using API.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TService, TModel> : ControllerBase
        where TService : IBaseService<TModel>
        where TModel : BaseModel
    {
        protected readonly TService _service;

        public BaseController(TService service)
        {
            _service = service;
        }

        [HttpPost("GetData")]
        public async Task<IActionResult> GetData(Page page, [FromQuery] string query)
        {
            var result = await _service.GetData(page, query);
            return ResponseResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetById(id);
            return ResponseResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, TModel model)
        {
            var result = await _service.Update(id, model);
            return ResponseResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(TModel model)
        {
            var result = await _service.Insert(model);
            return ResponseResult(result);
        }

        [HttpPost("DeleteMultiple")]
        public async Task<IActionResult> DeleteMultiple(ListID listID)
        {
            var result = await _service.DeleteMultiple(listID);
            return ResponseResult(result);
        }

        [HttpPost("GetMultiple")]
        public async Task<IActionResult> GetMultiple(ListID listID)
        {
            var result = await _service.GetMultiple(listID);
            return ResponseResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.Delete(id);
            return ResponseResult(result);
        }

        protected IActionResult ResponseResult(object result)
        {
            return Ok(result);
        }
    }
}