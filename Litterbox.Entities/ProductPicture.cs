using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Litterbox.Entities
{
    public class ProductPicture : BaseEntity
    {
        public int ProductID { get; set; }
        public int PictureID { get; set; }

        public virtual Picture Picture { get; set; }
    }
}