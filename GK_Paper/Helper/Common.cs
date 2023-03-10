using GK_Paper.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GK_Paper.Helper
{
    public class Common
    {
        public Login_Model Get_Login_Details()
        {
            string login_string = HttpContext.Current.User.Identity.Name;
            Login_Model login_model = JsonConvert.DeserializeObject<Login_Model>(login_string);
            return login_model;
        }

    }
}