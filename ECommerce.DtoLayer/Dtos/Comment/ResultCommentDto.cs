using ECommerce.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DtoLayer.Dtos.Comment
{
    public class ResultCommentDto
    {
        public int CommentId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public string CommentText { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
