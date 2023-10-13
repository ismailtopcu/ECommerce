using AutoMapper;
using ECommerce.BusinessLayer.Abstract;
using ECommerce.DtoLayer.Dtos.Comment;
using ECommerce.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentDto commentDto)
        {
            commentDto.CreatedDate = DateTime.Now;
            var comment = _mapper.Map<Comment>(commentDto);
            await _commentService.TInsertAsync(comment);
            return Ok("Yorum eklendi");
        }
        [HttpGet]
        public IActionResult GetComments()
        {
            var commentList =  _commentService.TGetCommentsIncluded();
            return Ok(commentList);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCommentsByProduct(int productId)
        {
            var commentList = await _commentService.TGetListByFilter(x => x.ProductId == productId);
            return Ok(commentList);
        }
    }
}
