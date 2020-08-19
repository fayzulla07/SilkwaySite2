using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteCkEditor.Models
{
    public class MenuContent
    {
        [Key]
        public int Id { get; set; } // int
        public string menuContent { get; set; } // nvarchar(max)
        public int? LangID { get; set; } // int
    }

    public class Languages
    {
        [Key]
        public int Id { get; set; } // int
        public string LangName { get; set; } // varchar(50)
        public byte[] Icon { get; set; } // varbinary(max)
        public string Prefix { get; set; } // varchar(5)
    }

    public class Menu
    {
        [Key]
        public int Id { get; set; } // int
        public string MenuName { get; set; } // varchar(100)
        public int? ParentID { get; set; } // int
        public int? PageID { get; set; } // int
        public int? LangID { get; set; } // int
    }

    public class News
    {
        [Key]
        public int Id { get; set; } // int
        public string Title { get; set; } // varchar(50)
        public string PhotoUrl { get; set; } // varchar(50)
        public string Desc { get; set; } // varchar(500)
        public string Content { get; set; } // varchar(max)
        public DateTime? RegDT { get; set; }
        public int LangID { get; set; } // int
        public int NewTypesID { get; set; } // int
        public int ParentID { get; set; } // int
    }

    public class NewTypes
    {
        [Key]
        public int Id { get; set; } // int
        public string TypeName { get; set; } // varchar(100)
        public string Desc { get; set; } // varchar(300)
        public int ShowLast { get; set; }
    }

    public class Pages
    {
        [Key]
        public int Id { get; set; } // int
        public string Title { get; set; } // varchar(50)
        public string Desc { get; set; } // varchar(500)
        public string Content { get; set; } // varchar(max)
        public DateTime? RegDT { get; set; } // datetime
        public int? LangID { get; set; } // int
        public int ParentID { get; set; } // int
    }

    public class Roles
    {
        [Key]
        public int Id { get; set; } // int
        public string RoleName { get; set; } // varchar(35)
    }

    public class Users
    {
        [Key]
        public int Id { get; set; } // int
        public string FIO { get; set; } // varchar(100)
        public string UserName { get; set; } // varchar(25)
        public string Pass { get; set; } // varchar(25)
        public int? RoleID { get; set; } // int
    }
    public class NewsLangs
    {
       public int NewsId { get; set; }
        public int LangId { get; set; }
    }

    public class Slider
    {
        [Key]
        public int Id { get; set; } // int
        public string Name { get; set; } // nvarchar(50)
        public string UrlAddress { get; set; } // nvarchar(500)
        public int Priority { get; set; } // int
        public int LangID { get; set; } // int
        public int ParentID { get; set; } // int
        public string PageUrl { get; set; } // nvarchar(500)
        public string Desc { get; set; } // nvarchar(500)
    }

    public class Blogs
    {
        [Key]
        public int Id { get; set; } // int

        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string FirstName { get; set; } // nvarchar(50)
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string LastName { get; set; } // nvarchar(50)

        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string Mail { get; set; } // varchar(50)

        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string Question { get; set; } // nvarchar(1000)
        public string Answer { get; set; } // nvarchar(1000)
        public bool IsVisible { get; set; } // bit
        public DateTime? RegDT { get; set; } // bit
        public bool IsPres { get; set; }
    }

}
