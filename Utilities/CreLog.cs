using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
namespace Utilities
{
    public class CreLog
    {

      

        static string path = MyServiceProvider.ServiceProvider.GetRequiredService<IHostingEnvironment>().WebRootPath;
  
        static FileLogHelper logerError = new FileLogHelper(@path + "\\Log\\<yyyy-MM-dd>trace.log");
        static FileLogHelper logerInfo = new FileLogHelper(@path + "\\Log\\<yyyy-MM-dd>info.log");


        //static string webRootPath = _hostingEnvironment.WebRootPath;
        // string contentRootPath = _hostingEnvironment.ContentRootPath;

        // return Content(webRootPath + "\n" + contentRootPath);
        //        这里要注意区分Web根目录 和 内容根目录的区别：

        //Web根目录是指提供静态内容的根目录，即asp.net core应用程序根目录下的wwwroot目录

        //内容根目录是指应用程序的根目录，即asp.net core应用的应用程序根目录

        //static string path = Microsoft.AspNetCore.Http.HttpContextHttpContext.Current.Server.MapPath("~");

        public CreLog()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void Info(string info)
        {

            logerInfo.WriteLineWithTime(info);
        }
        public static void Error(string message, Exception ex)
        {

            logerError.WriteLineWithTime(message + ex.ToString());
        }

        public static void Error(Exception ex)
        {

            logerError.WriteLineWithTime(ex.ToString());
        }
    }
}