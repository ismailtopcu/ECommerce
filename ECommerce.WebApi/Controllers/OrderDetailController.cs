using AutoMapper;
using ECommerce.BusinessLayer.Abstract;
using ECommerce.DtoLayer.Dtos.OrderDetail;
using ECommerce.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;
        private readonly IMapper _mapper;

        public OrderDetailController(IOrderDetailService orderDetailService, IMapper mapper)
        {
            _orderDetailService = orderDetailService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllOrderDetails()
        {
            var orderDetails = await _orderDetailService.TGetListAsync();
            return Ok(orderDetails);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetailById(int id)
        {
            var orderDetail = await _orderDetailService.TGetByIdAsync(id);
            return Ok(orderDetail);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            var orderDetail = await _orderDetailService.TGetByIdAsync(id);
            await _orderDetailService.TDeleteAsync(orderDetail);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddOrderDetail(CreateOrderDetailDto createOrderDetailDto)
        {
            var value = _mapper.Map<OrderDetail>(createOrderDetailDto);
            await _orderDetailService.TInsertAsync(value);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOrderDetail(UpdateOrderDetailDto updateOrderDetailDto)
        {
            var value = _mapper.Map<OrderDetail>(updateOrderDetailDto);
            await _orderDetailService.TUpdateAsync(value);
            return Ok();
        }
    }
}
