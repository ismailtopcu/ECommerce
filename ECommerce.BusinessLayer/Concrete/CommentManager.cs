using ECommerce.BusinessLayer.Abstract;
using ECommerce.DataAccessLayer.Abstract;
using ECommerce.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BusinessLayer.Concrete
{
    public class CommentManager : ICommentService
    {
        private readonly ICommentDal _commentDal;

        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public async Task TDeleteAsync(Comment t)
        {
           await _commentDal.DeleteAsync(t);
        }

        public async Task<Comment> TGetByIdAsync(int id)
        {
            return await _commentDal.GetByIdAsync(id);
        }

        public List<Comment> TGetCommentsIncluded()
        {
            return _commentDal.GetCommentsIncluded();
        }

        public async Task<List<Comment>> TGetListAsync()
        {
            return await _commentDal.GetListAsync();
        }

        public Task<List<Comment>> TGetListByFilter(Expression<Func<Comment, bool>> filter)
        {
            return _commentDal.GetListByFilter(filter);
        }

        public async Task TInsertAsync(Comment t)
        {
            await _commentDal.InsertAsync(t);
        }

        public async Task TUpdateAsync(Comment t)
        {
            await _commentDal.UpdateAsync(t);
        }
    }
}
