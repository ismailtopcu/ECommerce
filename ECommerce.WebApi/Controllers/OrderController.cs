using AutoMapper;
using ECommerce.BusinessLayer.Abstract;
using ECommerce.DtoLayer.Dtos.Order;
using ECommerce.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;

namespace ECommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrders() 
        {
            var values = await _orderService.TGetAllOrdersIncluded();
            return Ok(values);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllOrdersFinished()
        {
            var values =  await _orderService.TGetAllOrdersIncluded();
            values = values.Where(x=>x.OrderFinished == true).ToList();
            return Ok(values);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetOrdersByUserIdFinished(int id)
        {
            var values = await _orderService.TGetAllOrdersIncluded();
            values = values.Where(x => x.UserId == id && x.OrderFinished == true).ToList();
            return Ok(values);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetOrdersByUserIdActive(int id)
        {
            var values = await _orderService.TGetAllOrdersIncluded();
            values = values.Where(x => x.UserId == id && x.OrderFinished == false).ToList();
            return Ok(values);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetOrdersByUserId(int id)
        {
            var values = await _orderService.TGetAllOrdersIncluded();
            values = values.Where(x => x.UserId == id).ToList();
            return Ok(values);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetOrderByOrderId(int id)
        {
            var values = await _orderService.TGetByIdAsync(id);
            return Ok(values);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateOrder(CreateOrderDto createOrderDto)
        {
            await _orderService.TInsertAsync(_mapper.Map<Order>(createOrderDto));
            return Ok("Sepet Oluşturuldu");
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> FinishOrder(int id)
        {
            var list = await _orderService.TGetAllOrdersIncluded();
            var value = list.Where(x => x.Id == id).FirstOrDefault();
            value.OrderDate = DateTime.Now;
            value.OrderFinished = true;
            decimal amount = 0;
            foreach (var item in value.OrderDetails)
            {
                amount += item.Quantity * item.UnitPrice;
            }
            value.TotalAmount = amount;
            await _orderService.TUpdateAsync(value);
            return Ok("Sipariş Oluşturuldu");
        }
    }
}
