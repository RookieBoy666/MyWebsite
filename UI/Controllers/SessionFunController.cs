



using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Utilities;

namespace WebController
{
    public class SessionFunController : Controller
    {
        private readonly IHttpContextAccessor _context;
        public IHttpContextAccessor _accessor;
        public SessionFunController( IHttpContextAccessor context)
        {
            _context = context;
        }

        public class LoginModel
        {
            public string Name { get; set; }
            public string Password { get; set; }
            public string token { get; set; }
        }

        /// <summary>
        /// 不是所有的数据都能自动绑定到方法参数上,比如Header就需要制定FromHeaderAttribute.这里测试Header提交的参数并不能进行模型绑定
        /// </summary>
        [HttpPost]
        public IActionResult PostHeader([FromHeader]String name, [FromHeader]String password, [FromHeader]String token)// [FromForm]LoginModel model)
        {
            LoginModel model = new LoginModel();
            model.Name = name;
            model.Password = password;
            model.token = token;
            ReqMsg msg = new ReqMsg();
            msg.Success = true;
            try
            {

            }
            catch(Exception ex)
            {
                msg.Success = false;
                msg.ErrorMsg = "登录失败!";
                //CreLog log = new CreLog();
                CreLog.Error(ex);
            }

            CreLog.Info(name);
            //return Json(msg, System.Web.Mvc.JsonRequestBehavior.AllowGet);

            //return Content("hello " + msg);


            //return "gdgdfgdg";

            return Content("hello " + name + "密码:" + model.Password+model.token);
        }


        //[HttpPost]
        //public ActionResult Login(string uid, string pwd)
        //{
        //    //  var form = ControllerContext.RequestContext.HttpContext.Request.Params;


        //   // var context = _context.HttpContext.Request.Headers.ToList();

        //    //var context = _context.HttpContext.Request.Form;
        //    //string aaa = context["uid"];

        //    Stream stream = HttpContext.Request.Body;
        //    byte[] buffer = new byte[HttpContext.Request.ContentLength.Value];
        //    stream.Read(buffer, 0, buffer.Length);
        //    string content = Encoding.UTF8.GetString(buffer);



        //    //IFormCollection form = HttpContext.Request.Form;
        //    //    string method = form["method"];


        //    ReqMsg msg = new ReqMsg();
        //    msg.Success = true;
        //    //try
        //    //{


        //    //    var user = UserService.Login(uid, pwd);
        //    //    if (user != null)
        //    //    {
        //    //        string localIp = IpHelper.GetRemoteIP();

        //    //        if (!string.IsNullOrEmpty(user.Unit.Ip))
        //    //        {
        //    //            string[] ipArray = user.Unit.Ip.Split(new char[] { ',' });
        //    //            bool flag = false;
        //    //            foreach (string item in ipArray)
        //    //            {
        //    //                flag = flag || IpHelper.ipIsValid(item, localIp);
        //    //            }
        //    //            if (!flag)
        //    //            {
        //    //                msg.Success = false;
        //    //                msg.ErrorMsg = "ip受限制，此账号与ip已记录!";
        //    //                AddLog(uid, user.Name, user.Unit.Id, user.Unit.Description, "ip受限制，此账号与ip已记录!", false);
        //    //            }
        //    //            else
        //    //            {
        //    //                if (HasPermisson(user))
        //    //                {
        //    //                    msg.Success = true;
        //    //                    msg.Result = user.Id + "," + user.Name + "," + user.Unit.Id + "," + user.Unit.Description;
        //    //                    AddLog(uid, user.Name, user.Unit.Id, user.Unit.Description, "带有ip的验证登录成功", true);
        //    //                    SessionHelper.Add(SessionHelper.SESSION_USER, user);
        //    //                    SessionHelper.Add("PKI", "0");
        //    //                }
        //    //                else
        //    //                {
        //    //                    AddLog(uid, user.Name, user.Unit.Id, user.Unit.Description, "无权限登录此系统", true);
        //    //                    msg.ErrorMsg = "很抱歉，您没有登录此子系统的权限，请与系统管理员联系！";
        //    //                    msg.Success = false;
        //    //                }
        //    //            }
        //    //        }
        //    //        else
        //    //        {
        //    //            if (HasPermisson(user))
        //    //            {
        //    //                msg.Success = true;
        //    //                msg.Result = user.Id + "," + user.Name + "," + user.Unit.Id + "," + user.Unit.Description;
        //    //                AddLog(uid, user.Name, user.Unit.Id, user.Unit.Description, "无验证登录成功", true);
        //    //                SessionHelper.Add(SessionHelper.SESSION_USER, user);
        //    //                SessionHelper.Add("PKI", "0");
        //    //            }
        //    //            else
        //    //            {
        //    //                AddLog(uid, user.Name, user.Unit.Id, user.Unit.Description, "无权限登录此系统", true);
        //    //                msg.ErrorMsg = "很抱歉，您没有登录此子系统的权限，请与系统管理员联系！";
        //    //                msg.Success = false;
        //    //            }
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        AddLog(uid, "", "", "", "登录失败", false);
        //    //        msg.Success = false;
        //    //        msg.ErrorMsg = "登录失败";
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    msg.Success = false;
        //    //    msg.ErrorMsg = "登录失败!";
        //    //    CreLog.Error(ex);
        //    //}
        //    return Json(msg, System.Web.Mvc.JsonRequestBehavior.AllowGet);

        //}
    }
}
