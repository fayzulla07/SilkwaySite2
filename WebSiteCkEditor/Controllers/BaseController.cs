using SqlData;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebSiteCkEditor.Models;
using WebSiteCkEditor.Repository;
using WebSiteCkEditor.Repository.RepositoryView;
namespace WebSiteCkEditor.Controllers
{
    public abstract class BaseController : Controller
    {
        protected abstract string lang { get; set; }

        SqlData.SqlGenerator<Blogs> blog;
        SqlData.SqlGenerator<MenuContentV> gen;
        SqlData.SqlGenerator<SliderV> sldr;
        NewsVRepo repo;
        NewsRepo nrpo;
        PagesVRepo pvrpo;
        public BaseController()
        {
            repo = new NewsVRepo();
            nrpo = new NewsRepo();
            pvrpo = new PagesVRepo();
            gen = new SqlData.SqlGenerator<MenuContentV>();
            sldr = new SqlGenerator<SliderV>();
            blog = new SqlData.SqlGenerator<Blogs>();
        }
        public ActionResult Index()
        {
            ClientModel m = new ClientModel();

            m.lst_slider = sldr.GetData("select * from SliderV where Prefix = @Prefix", new SliderV { Prefix = lang });
            m.lst_slider = m.lst_slider.OrderBy(u => u.Priority);
            if (lang != null)
            {
                var news = repo.GetDataSql("select TOP 6 * from NewsV where Prefix = @Prefix and TypeName = 'Новости' order by Id desc", new NewsV { Prefix = lang });
                m.lst_newsv = news;
                m.lst_newads = SetAds();
            }
            else
            {
                return Redirect("/" + lang);
            }
            return View("~/Views/home/Index.cshtml", m);
        }


        public ActionResult News(int id)
        {
            if (id != 0)
            {
                ClientModel m = new ClientModel();

                if (lang != null)
                {
                    var result = repo.GetDataSql("select * from NewsV where (Id = @Id or ParentID = @ParentID) and Prefix = @Prefix", new NewsV { Id = id, ParentID = id, Prefix = lang }).FirstOrDefault();
                    if (result != null)
                    {
                        m.lst_newads = SetAds();
                        m.lst_newsv = new List<NewsV>() { result };
                        //SetCulture();
                    }
                    else
                    {
                        return Redirect("/" + lang);
                    }
                }
                else
                {
                    return Redirect("/" + lang);
                }
                return View("~/Views/home/news.cshtml", m);
            }
            else
            {

                return Redirect("/" + lang);
            }

        }


        public ActionResult allNews()
        {
            ClientModel m = new ClientModel();

            if (!String.IsNullOrEmpty(lang))
            {
                m.lst_newads = SetAds();
                var result = repo.GetDataSql("select * from NewsV where Prefix = @Prefix and TypeName = 'Новости' order by Id desc;", new NewsV { Prefix = lang });
                m.lst_newsv = result;
            }
            else
            {
                m.lst_newads = SetAds();
                var result = repo.GetDataSql("select * from NewsV where Prefix = @Prefix and TypeName = 'Новости' order by Id desc;", new NewsV { Prefix = lang });
                m.lst_newsv = result;
            }
            return View("~/Views/home/allnews.cshtml", m);
        }

        public ActionResult Page(int id)
        {

            if (id != 0)
            {
                ClientModel m = new ClientModel();

                if (lang != null)
                {
                    var result = pvrpo.GetDataSql("select * from PagesV where (Id = @Id or ParentID = @ParentID) and Prefix = @Prefix", new PagesV { Id = id, ParentID = id, Prefix = lang }).FirstOrDefault();
                    if (result != null)
                    {
                        m.lst_newads = SetAds();
                        m.lst_pages = new List<PagesV>() { result };
                        //SetCulture();
                    }
                    else
                    {
                        return Redirect("/" + lang);
                    }
                }
                else
                {
                    return Redirect("/" + lang);
                }
                return View("~/Views/home/page.cshtml", m);
            }
            else
            {

                return Redirect("/" + lang);
            }
        }
        private void SetCulture()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
        }
        private IEnumerable<NewsV> SetAds()
        {
            return repo.GetDataSql("select TOP 4 * from NewsV where Prefix = @Prefix and TypeName = 'Объявления' order by Id", new NewsV { Prefix = lang });
        }


        private ActionResult Autorize()
        {

            return View();
        }

        public ActionResult AdsLearnMore(int id)
        {

            if (id != 0)
            {
                ClientModel m = new ClientModel();

                if (lang != null)
                {
                    var result = repo.GetDataSql("select * from NewsV where (Id = @Id or ParentID = @ParentID) and Prefix = @Prefix", new NewsV { Id = id, ParentID = id, Prefix = lang }).FirstOrDefault();
                    if (result != null)
                    {
                        m.lst_newads = SetAds();
                        m.lst_newsv = new List<NewsV>() { result };
                        //SetCulture();
                    }
                    else
                    {
                        return Redirect("/" + lang);
                    }
                }
                else
                {
                    return Redirect("/" + lang);
                }
                return View("~/Views/home/AdsLearnMore.cshtml", m);
            }
            else
            {

                return Redirect("/" + lang);
            }
        }

        public ActionResult Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return Redirect("/");
            }
            string Query = query;

            if (string.IsNullOrEmpty(query))
            {
                Query = "";
            }
            ClientModel m = new ClientModel();

            if (lang != null)
            {
                m.lst_newads = SetAds();
                var result = repo.GetDataSql("select Id, Title, [Desc], ParentID, TypeName, Prefix from NewsV where ([Desc] LIKE '%'+ @Title + '%' or Title LIKE '%' + @Title + '%') and Prefix = @Prefix;", new NewsV { Title = Query, Prefix = lang });
                var result2 = pvrpo.GetDataSql("select Id, Title, [Desc], ParentID, Prefix from PagesV where ([Desc] LIKE '%'+ @Title+ '%' or Title LIKE '%'+ @Title +'%') and Prefix = @Prefix;", new NewsV { Title = Query, Prefix = lang });
                if (result != null)
                {
                    m.lst_newads = SetAds();
                    m.lst_pages = new List<PagesV>(result2);
                    m.lst_newsv = new List<NewsV>(result);
                }
                else
                {
                    return View("~/Views/home/search.cshtml", m);
                }
            }
            else
            {
                return Redirect("/" + lang);
            }
            return View("~/Views/home/search.cshtml", m);


        }
        // --------------------------Blogs---------------------------------------
        // GET: Blogs
        public ActionResult President()
        {
            return View("~/Views/Blogs/President.cshtml");
        }
        public ActionResult AskPresident()
        {
            return View("~/Views/Blogs/AskPresident.cshtml");
        }
        public ActionResult AnswerPresident()
        {
            var result = blog.GetData("select * from Blogs as b where b.IsVisible = 1 and b.IsPres = 1 order by b.RegDT desc");
            return View("~/Views/Blogs/AnswerPresident.cshtml", result);
        }
        [HttpPost]
        public async Task<ActionResult> AskPresident(Blogs bl, string captchaValid)
        {
            if (captchaValid != (string)Session["code"])
            {
                ViewBag.err = WebSiteCkEditor.Resources.ClientLang.ValidatePres;
            }
            if (!ModelState.IsValid)
            {
                ViewBag.err = WebSiteCkEditor.Resources.ClientLang.ValidatePres;
            }
            else
            {
                int i = blog.Add(bl);
                if (i != 0)
                {
                    ViewBag.err = WebSiteCkEditor.Resources.ClientLang.ValidatePres1;
                    string IsSend = ConfigurationManager.AppSettings["PresidentIsSend"];
                    string PresidentMail = ConfigurationManager.AppSettings["PresidentMail"];
                    if (IsSend == "1")
                    {
                        MailService.EmailService mail = new MailService.EmailService();
                        await mail.SendEmailAsync(PresidentMail, "swiu.kz message from user!", bl.Question);
                    }

                }
                else
                    ViewBag.err = WebSiteCkEditor.Resources.ClientLang.ValidatePresRepeat;
            }
            return View("~/Views/Blogs/AskPresident.cshtml", bl);
        }
        // --------------------------Rector---------------------------------------
        // GET: Blogs
        public ActionResult Rector()
        {
            return View("~/Views/Blogs/Rector.cshtml");
        }
        public ActionResult AskRector()
        {
            return View("~/Views/Blogs/AskRector.cshtml");
        }
        public ActionResult AnswerRector()
        {
            var result = blog.GetData("select * from Blogs as b where b.IsVisible = 1 and b.IsPres = 0 order by b.RegDT desc");
            return View("~/Views/Blogs/AnswerRector.cshtml", result);
        }
        [HttpPost]
        public ActionResult AskRector(Blogs bl, string captchaValid)
        {
            if (captchaValid != (string)Session["code"])
            {
                ViewBag.err = WebSiteCkEditor.Resources.ClientLang.ValidatePres;
            }
            if (!ModelState.IsValid)
            {
                ViewBag.err = WebSiteCkEditor.Resources.ClientLang.ValidatePres;
            }
            else
            {
                int i = blog.Add(bl);
                if (i != 0)
                {
                    // MailService.EmailService mail = new MailService.EmailService();
                    ViewBag.err = WebSiteCkEditor.Resources.ClientLang.ValidatePres1;
                    // await mail.SendEmailAsync("ctrl_13@list.ru", "swiu.kz message from user!", bl.Question);
                }

                else
                    ViewBag.err = WebSiteCkEditor.Resources.ClientLang.ValidatePresRepeat;
            }
            return View("~/Views/Blogs/AskRector.cshtml", bl);
        }
    }
}