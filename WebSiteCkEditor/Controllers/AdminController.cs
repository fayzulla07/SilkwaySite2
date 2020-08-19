using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteCkEditor.Repository;
using WebSiteCkEditor;
using WebSiteCkEditor.Models;
using SqlData;
using System.Data.SqlClient;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading;
using WebSiteCkEditor.Helpers;
using System.Threading.Tasks;

namespace WebSiteCkEditor.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {

        SqlGenerator<MenuContent> gen;
        SqlGenerator<UserRoleV> usr;
        SqlGenerator<Slider> sldr;
        SqlGenerator<Blogs> blog;

        LanguagesRepo lang;
        NewTypesRepo newt;
        NewsRepo nrpo;
        PagesRepo prpo;
        public AdminController()
        {
            lang = new LanguagesRepo();
            newt = new NewTypesRepo();
            nrpo = new NewsRepo();
            prpo = new PagesRepo();
            gen = new SqlGenerator<MenuContent>();
            usr = new SqlGenerator<UserRoleV>();
            sldr = new SqlGenerator<Slider>();
            blog = new SqlGenerator<Blogs>();

        }
        // GET: Admin
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string Login, string Pass)
        {
            if (ModelState.IsValid)
            {
                
                UserRoleV user = usr.GetData("select * from UserRoleV where UserName = @UserName and Pass = @Pass", new UserRoleV { UserName = Login, Pass = Pass }).FirstOrDefault();

                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    ClaimsIdentity claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                    claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String));
                    claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName, ClaimValueTypes.String));
                    claim.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                        "OWIN Provider", ClaimValueTypes.String));
                    if (user.RoleName != null)
                        claim.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleName, ClaimValueTypes.String));

                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    HttpCookie cookie = new HttpCookie("dvuhsivhihh");
                    cookie.Value = Coder.EncodeServerName(user.RoleName);
                    Response.Cookies.Add(cookie);
                    return RedirectToAction("News", "Admin");
                }
            }
            return View();
        }
        [AllowAnonymous]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            HttpCookie cookie = new HttpCookie("dvuhsivhihh");
            cookie.Value = "";
            Response.Cookies.Add(cookie);
            return Redirect("/Admin/Login");
        }
        public ActionResult News()
        {

            ViewBag.NewsTable = nrpo.GetByAny("ParentID", 0);
            ViewBag.NType = newt.GetAll();
            ViewBag.Lan = lang.GetAll();
            return View();
        }
        public ActionResult Pages()
        {
            AdminPagesModel m = new AdminPagesModel();
            m.lst_pages = prpo.GetByAny("ParentID", 0);
            m.lst_lang = lang.GetAll();
            return View(m);
        }
        public ActionResult CkEditorNews()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;

            AdminNewModel m = new AdminNewModel();
            m.lst_newtypes = newt.GetAll();
            m.lst_lang = lang.GetAll();
            return View(m);
        }
        public ActionResult CkEditoPages()
        {
            AdminPagesModel m = new AdminPagesModel();
            m.lst_lang = lang.GetAll();
            return View(m);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CkEditoPages(Pages item, int PagesId)
        {
            Pages val = item;
            if(val.Id == 0)
            {
                if (item.Id != PagesId)
                    val.ParentID = PagesId;
                var result = prpo.Add(item);
            }
            else
            {
                if (item.Id != PagesId)
                    val.ParentID = PagesId;
                var result = prpo.Update(val, false);
            }
            
            return Redirect("/admin/Pages");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CkEditorNews(News item, int NewsId)
        {
            News val = item;
            if (val.Id == 0)
            {
                if (item.Id != NewsId)
                    val.ParentID = NewsId;
                var result = nrpo.Add(val);
            }
            else
            {
                if(item.Id != NewsId)
                val.ParentID = NewsId;
                var result = nrpo.Update(val, false);
            }
           
            return Redirect("/admin/News");
        }
        [HttpPost]
        public ActionResult DeleteNews(int Id)
        {
            if(Id != 0)
            {
                var parentresult = nrpo.Get(Id);
                var result = nrpo.GetByAny("ParentID", Id);
                foreach (var item in result)
                {
                    nrpo.Delete(item.Id);
                }
                nrpo.Delete(parentresult.Id);
            }
           
            return Redirect("/admin/News");
        }
        [HttpPost]
        public ActionResult DeletePages(int Id)
        {
            if (Id != 0)
            {
                var parentresult = prpo.Get(Id);
                var result = prpo.GetByAny("ParentID", Id);
                foreach (var item in result)
                {
                    prpo.Delete(item.Id);
                }
                prpo.Delete(parentresult.Id);
            }

            return Redirect("/admin/News");
        }
        [HttpPost]
        public ActionResult EditNews(int Id,int Parent, int LangId, int NewType)
        {
            AdminNewModel m = new AdminNewModel();
            m.lst_newtypes = new List<NewTypes> { newt.Get((int)NewType) } ;
            m.lst_lang = new List<Languages> { lang.Get((int)LangId) };
            if (Id != 0)
            {
                m.NewsID = Id;
                var result = nrpo.GetAll();
                foreach (var item in result)
                {
                    if (item.Id == Id && item.LangID == LangId)
                    {
                        m.news = item;
                        break;
                    }
                    if (item.ParentID == Id && item.LangID ==LangId)
                    {
                        m.news = item;
                        break;
                    }
                }
            }
            return View("CkEditorNews", m);
        }

        [HttpPost]
        public ActionResult EditPages(int Id, int Parent, int LangId)
        {
            AdminPagesModel m = new AdminPagesModel();
            m.lst_lang = new List<Languages> { lang.Get((int)LangId) };
            if (Id != 0)
            {
                m.PagesID = Id;
                var result = prpo.GetAll();
                foreach (var item in result)
                {
                    if (item.Id == Id && item.LangID == LangId)
                    {
                        m.pages = item;
                        break;
                    }
                    if (item.ParentID == Id && item.LangID == LangId)
                    {
                        m.pages = item;
                        break;
                    }
                }
            }
            return View("CkEditoPages", m);
        }
        [HttpPost]
        public ActionResult FilterNews(int TypeId)
        {
            AdminPagesModel m = new AdminPagesModel();
            
            return View("CkEditoPages", m);
        }
        public ActionResult Menu()
        {
            AdminPagesModel m = new AdminPagesModel();
            ViewBag.rus = gen.GetById(2);
            ViewBag.eng = gen.GetById(3);
            ViewBag.kaz = gen.GetById(1);
            return View(m);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult MenuEdit(int id, string content, string prefix)
        {
            if(prefix == "kz")
            {
                gen.Update(new MenuContent { Id = id, LangID = 1, menuContent = content });
            }
            else if (prefix == "ru")
            {
                gen.Update(new MenuContent { Id = id, LangID = 2, menuContent = content });
            }
            else if (prefix == "en")
            {
                gen.Update(new MenuContent { Id = id, LangID = 3, menuContent = content });
            }
            return Redirect("/Admin/Menu");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Slider() // отображать на таблице
        {
            AdminSliderModel m = new AdminSliderModel();
            m.lst_slider = sldr.GetByAny("ParentID", 0);
            m.lst_lang = lang.GetAll();
            return View(m);
        }
        [HttpPost]
        public ActionResult SliderS(int Id, int Parent, int LangId) // из таблицы открыть редактирование
        {
            AdminSliderModel m = new AdminSliderModel();
            m.lst_lang = new List<Languages> { lang.Get((int)LangId) };
            if (Id != 0)
            {
                m.SliderID = Id;
                var result = sldr.GetAll();
                foreach (var item in result)
                {
                    if (item.Id == Id && item.LangID == LangId)
                    {
                        m.slider = item;
                        break;
                    }
                    if (item.ParentID == Id && item.LangID == LangId)
                    {
                        m.slider = item;
                        break;
                    }
                }
            }
            return View("SliderEdit", m);
        }
        public ActionResult SliderEdit() // открывает новую таблицу
        {
            AdminSliderModel m = new AdminSliderModel();
            m.lst_lang = lang.GetAll();
            return View(m);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SliderEdit(Slider item, int SlID) // создает новую таблицу
        {
            Slider val = item;
            if (val.Id == 0)
            {
                if (item.Id != SlID)
                    val.ParentID = SlID;
                var result = sldr.Add(val);
            }
            else
            {
                if (item.Id != SlID)
                    val.ParentID = SlID;
                var result = sldr.Update(val);
            }
            return Redirect("/admin/slider");
        }
        [HttpPost]
        public ActionResult SliderRemove(int Id)
        {
            if (Id != 0)
            {
                var parentresult = sldr.GetById(Id);
                var result = sldr.GetByAny("ParentID", Id);
                foreach (var item in result)
                {
                    nrpo.Delete(item.Id);
                }
                nrpo.Delete(parentresult.Id);
            }
            return Redirect("/Admin/Slider");
        }
         // --------------------------Pres-------------------------------- //
        public ActionResult PresidentQuestions()
        {
            var bl = blog.GetData("select * from Blogs as b where b.IsPres = 1");
            return View(bl);
        }
        [HttpGet]
        public ActionResult PresQuesEdit(int id)
        {
            var bl = blog.GetById(id);
            return View(bl);
        }
        [HttpPost]
        public async Task<ActionResult> PresQuesEdit(Blogs b, string send)
        {
            if(send == "1")
            {
                MailService.EmailService mail = new MailService.EmailService();
                await mail.SendEmailAsync(b.Mail, "swiu.kz message!", b.Answer);
            }
            else
            {
                blog.GetData("UPDATE Blogs SET  [FirstName] = @FirstName,[LastName] = @LastName,[Mail] = @Mail,[Question] = @Question,[Answer] = @Answer,[IsVisible] = @IsVisible  WHERE Id = @Id and IsPres = 1", b);
            }
            return RedirectToAction("PresidentQuestions");
        }
        // --------------------------Pres-------------------------------- //
        // --------------------------Rector-------------------------------- //
        public ActionResult RectorQuestions()
        {
            var bl = blog.GetData("select * from Blogs as b where b.IsPres = 0");
            return View(bl);
        }
        [HttpGet]
        public ActionResult RectorQuesEdit(int id)
        {
            var bl = blog.GetById(id);
            return View(bl);
        }
        [HttpPost]
        public async Task<ActionResult> RectorQuesEdit(Blogs b, string send)
        {
            if (send == "1")
            {
                MailService.EmailService mail = new MailService.EmailService();
                await mail.SendEmailAsync(b.Mail, "swiu.kz message!", b.Answer);
            }
            else
            {
                blog.GetData("UPDATE Blogs SET [FirstName] = @FirstName,[LastName] = @LastName,[Mail] = @Mail,[Question] = @Question,[Answer] = @Answer,[IsVisible] = @IsVisible  WHERE Id = @Id and IsPres = 0", b);
            }
            return RedirectToAction("RectorQuestions");
        }
        // --------------------------Rector-------------------------------- //
    }
}