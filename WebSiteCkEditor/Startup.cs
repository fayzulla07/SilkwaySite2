using System;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CKSource.CKFinder.Connector.Config;
using CKSource.CKFinder.Connector.Core.Builders;
using CKSource.CKFinder.Connector.Host.Owin;
using CKSource.FileSystem;
using CKSource.FileSystem.Local;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using SqlData;

[assembly: OwinStartup(typeof(WebSiteCkEditor.Startup))]

namespace WebSiteCkEditor
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Cs.CsStr = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;
            //регистрируем файловую систему для коннектора

            FileSystemFactory.RegisterFileSystem<CKSource.FileSystem.Local.LocalStorage>();


            //объявляем маршрут в приложении и сопоставляем его с коннектором
            //клиентская JS-библиотека CKFinder ожидает увидеть коннектор именно по этому маршруту
            app.Map("/ckfinder/connector", SetupConnector);


            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Admin/Login"),
            });
        }

        //объявляем метод по настройке и инициализации коннектора
        private static void SetupConnector(IAppBuilder app)
        {
            //создаем экземпляры необходимых классов
            var connectorFactory = new OwinConnectorFactory();
            var connectorBuilder = new ConnectorBuilder();
            var customAuthenticator = new CKFinderAuthenticator();



            connectorBuilder
                .LoadConfig() //подгружаем конфигурацию из файла Web.config
                .SetAuthenticator(customAuthenticator) //устанавливаем ранее определенный аутентификатор
                .SetRequestConfiguration((request, config) => { config.LoadConfig(); }); //определяем конфигурацию для каждого отдельного запроса

          

            //создаем экземпляр коннектора
            var connector = connectorBuilder.Build(connectorFactory);
            
            //добавляем коннектор в pipeline
            app.UseConnector(connector);


        }

      
    }
}
