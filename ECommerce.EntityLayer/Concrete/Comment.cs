using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.EntityLayer.Concrete
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public string CommentText { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
