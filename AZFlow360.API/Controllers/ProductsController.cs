using AZFlow360.Application.Features.Auth.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AZFlow360.API.Controllers
{
    [Authorize] // Yêu cầu xác thực cho tất cả các endpoint trong controller này
    public class ProductsController : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {
            var productId = await Mediator.Send(command);
            // Trả về response theo chuẩn RESTful
            return CreatedAtAction(nameof(GetById), new { id = productId }, command);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // Cần tạo GetProductByIdQuery trong lớp Application
            // var query = new GetProductByIdQuery { Id = id };
            // var product = await Mediator.Send(query);
            // return Ok(product);
            return Ok(new { Message = $"Endpoint to get product with Id {id}." });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Manager")] // Chỉ Admin hoặc Manager mới được xóa
        public async Task<IActionResult> Delete(int id)
        {
            // Cần tạo DeleteProductCommand trong lớp Application
            // await Mediator.Send(new DeleteProductCommand { Id = id });
            return NoContent(); // 204 No Content là response thành công cho việc xóa
        }
    }
}