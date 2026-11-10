using CooperativeProcurement.Core.Interfaces;
using CooperativeProcurement.Core.Models;
using CooperativeProcurement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CooperativeProcurement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly IPurchaseOrderRepository _repository;

        public PurchaseOrdersController()
        {
            _repository = new JsonPurchaseOrderRepository();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseOrder>>> GetAll()
        {
            var orders = await _repository.GetAllAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseOrder>> GetById(Guid id)
        {
            var order = await _repository.GetByIdAsync(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<PurchaseOrder>> Create([FromBody] PurchaseOrder order)
        {
            var createdOrder = await _repository.CreateAsync(order);
            return CreatedAtAction(nameof(GetById), new { id = createdOrder.Id }, createdOrder);
        }
    }
}