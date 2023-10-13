using ECommerce.DtoLayer.Dtos.AccountDto;
using ECommerce.DtoLayer.Dtos.Comment;
using ECommerce.EntityLayer.Concrete;
using ECommerce.PresentationLayer.Models;
using ECommerce.PresentationLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.PresentationLayer.ViewComponents
{
    public class CommentsForProducts : ViewComponent
    {
        private readonly IApiService _apiService;

        public CommentsForProducts(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var values = await _apiService.GetTableData<ResultCommentDto>("https://localhost:7175/api/Comment/GetCommentsByProduct?productId=" + id);
            if (values != null) 
            {
				ViewBag.count = values.Count;
                List<CommentsByProductViewModel> viewModels = new ();
                foreach(var item in values)
                {
                    CommentsByProductViewModel viewModel = new CommentsByProductViewModel()
                    {
                        CommentText = item.CommentText,
                        User = await _apiService.GetData<ResultUserDto>("https://localhost:7175/api/User/GetOneUserById/" + item.UserId)
                    };
                    viewModels.Add(viewModel);
				};
                ViewBag.id = id;
                return View(viewModels);
                
			}
            ViewBag.id = id;
            ViewBag.count = 0;
			return View(values);
        }
    }
}
