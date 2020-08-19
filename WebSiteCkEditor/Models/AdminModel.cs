using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteCkEditor.Models
{
    public class AdminNewModel
    {
        public News news = new News();
        public NewTypes newtypes = new NewTypes();
        public Languages Language = new Languages();

        public IEnumerable<News> lst_news { get; set; }
        public IEnumerable<NewTypes> lst_newtypes { get; set; }
        public IEnumerable<Languages> lst_lang { get; set; }
        public int NewsID { get; set; }
        public int IsNew { get; set; }


    }

    public class AdminPagesModel
    {
        public Pages pages = new Pages();

        public Languages Language = new Languages();


        public IEnumerable<Pages> lst_pages { get; set; }
        public IEnumerable<Languages> lst_lang { get; set; }

        public int PagesID { get; set; }
        public int IsNew { get; set; }
    }
    public class AdminSliderModel
    {
        public IEnumerable<Languages> lst_lang { get; set; }
        public IEnumerable<Slider> lst_slider { get; set; }
        public Slider slider = new Slider();
        public int SliderID { get; set; }
    }

}