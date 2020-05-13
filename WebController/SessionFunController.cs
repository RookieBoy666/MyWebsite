
using Microsoft.AspNetCore.Http;
using System;
using System.Web.Mvc;
using Utilities;

namespace WebController
{
    public class SessionFunController : Controller
    {
        public IHttpContextAccessor _accessor;
        public SessionFunController(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        [HttpPost]
        public ActionResult Login(string uid, string pwd)
        {
            //  var form = ControllerContext.RequestContext.HttpContext.Request.Params;


            var httpcontext = _accessor.HttpContext;
           
        //IFormCollection form = HttpContext.Request.Form;
        //    string method = form["method"];


            ReqMsg msg = new ReqMsg();
            msg.Success = true;
            //try
            //{


            //    var user = UserService.Login(uid, pwd);
            //    if (user != null)
            //    {
            //        string localIp = IpHelper.GetRemoteIP();

            //        if (!string.IsNullOrEmpty(user.Unit.Ip))
            //        {
            //            string[] ipArray = user.Unit.Ip.Split(new char[] { ',' });
            //            bool flag = false;
            //            foreach (string item in ipArray)
            //            {
            //                flag = flag || IpHelper.ipIsValid(item, localIp);
            //            }
            //            if (!flag)
            //            {
            //                msg.Success = false;
            //                msg.ErrorMsg = "ip受限制，此账号与ip已记录!";
            //                AddLog(uid, user.Name, user.Unit.Id, user.Unit.Description, "ip受限制，此账号与ip已记录!", false);
            //            }
            //            else
            //            {
            //                if (HasPermisson(user))
            //                {
            //                    msg.Success = true;
            //                    msg.Result = user.Id + "," + user.Name + "," + user.Unit.Id + "," + user.Unit.Description;
            //                    AddLog(uid, user.Name, user.Unit.Id, user.Unit.Description, "带有ip的验证登录成功", true);
            //                    SessionHelper.Add(SessionHelper.SESSION_USER, user);
            //                    SessionHelper.Add("PKI", "0");
            //                }
            //                else
            //                {
            //                    AddLog(uid, user.Name, user.Unit.Id, user.Unit.Description, "无权限登录此系统", true);
            //                    msg.ErrorMsg = "很抱歉，您没有登录此子系统的权限，请与系统管理员联系！";
            //                    msg.Success = false;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            if (HasPermisson(user))
            //            {
            //                msg.Success = true;
            //                msg.Result = user.Id + "," + user.Name + "," + user.Unit.Id + "," + user.Unit.Description;
            //                AddLog(uid, user.Name, user.Unit.Id, user.Unit.Description, "无验证登录成功", true);
            //                SessionHelper.Add(SessionHelper.SESSION_USER, user);
            //                SessionHelper.Add("PKI", "0");
            //            }
            //            else
            //            {
            //                AddLog(uid, user.Name, user.Unit.Id, user.Unit.Description, "无权限登录此系统", true);
            //                msg.ErrorMsg = "很抱歉，您没有登录此子系统的权限，请与系统管理员联系！";
            //                msg.Success = false;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        AddLog(uid, "", "", "", "登录失败", false);
            //        msg.Success = false;
            //        msg.ErrorMsg = "登录失败";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    msg.Success = false;
            //    msg.ErrorMsg = "登录失败!";
            //    CreLog.Error(ex);
            //}
            return Json(msg, JsonRequestBehavior.AllowGet);

        }
    }
}
