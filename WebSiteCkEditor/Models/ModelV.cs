using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteCkEditor.Models
{
    public class MenuContentV
    {
        [Key]
        public int Id { get; set; } // int
        public string menuContent { get; set; } // nvarchar(max)
        public string Prefix { get; set; } // int
    }
    public class NewsV
    {
        [Key]
        public int Id { get; set; } // int
        public string Title { get; set; } // nvarchar(100)
        public string Desc { get; set; } // nvarchar(500)
        public string Content { get; set; } // nvarchar(max)
        public DateTime? RegDT { get; set; } // datetime
        public string PhotoUrl { get; set; } // varchar(500)
        public string TypeName { get; set; } // varchar(100)
        public int ShowLast { get; set; } // int
        public int ParentID { get; set; } // int
        public int LangID { get; set; } // int
        public string Prefix { get; set; } // varchar(5)
    }

    public class PagesV
    {
        [Key]
        public int Id { get; set; } // int
        public string Title { get; set; } // nvarchar(100)
        public string Desc { get; set; } // nvarchar(500)
        public string Content { get; set; } // nvarchar(max)
        public DateTime? RegDT { get; set; } // datetime
        public int? ParentID { get; set; } // int
        public string Prefix { get; set; } // varchar(5)
    }

    public class UserRoleV
    {
        [Key]
        public int Id { get; set; } // int
        public string FIO { get; set; } // varchar(100)
        public string UserName { get; set; } // varchar(25)
        public string Pass { get; set; } // varchar(25)
        public string RoleName { get; set; } // varchar(35)
    }

    public class SliderV
    {
        [Key]
        public int Id { get; set; } // int
        public string Name { get; set; } // nvarchar(50)
        public string UrlAddress { get; set; } // nvarchar(300)
        public int Priority { get; set; } // int
        public string Prefix { get; set; } // int
        public string PageUrl { get; set; } // nvarchar(500)
        public string Desc { get; set; } // nvarchar(500)
    }

}