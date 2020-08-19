using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteCkEditor.Models
{
    public class ClientModel
    {
        public IEnumerable<NewsV> lst_newsv { get; set; }
        public IEnumerable<PagesV> lst_pages { get; set; }
        public IEnumerable<NewsV> lst_newads { get; set; }
        public bool ShowSlide { get; set; } = true;


        public IEnumerable<SliderV> lst_slider { get; set; }
        public SliderV slider = new SliderV();

    }
}