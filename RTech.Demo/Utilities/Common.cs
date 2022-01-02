using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using RTech.Demo.Utilities;
using Riddhasoft.Services.User;
using System.Security.Cryptography;
using Riddhasoft.Entity.User;
using System.IO;
namespace RTech.Demo.Utilities
{
    public class Common
    {
        public static bool AjaxRequest
        {
            get
            {

                return true;
            }

        }
        public static bool ValidateToken(string token)
        {
            if (token != "")
            {
                SContext con = new SContext();
                return con.List().Data.Where(x => x.Token == token).FirstOrDefault() != null;
            }
            else
            {
                return false;
            }
        }

        public static string RequestToken { get { return HttpContext.Current.Request.Headers.GetValues("Token").FirstOrDefault() ?? ""; } }
        internal static T GetObjFromQueryString<T>() where T : new()
        {
            var obj = new T();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                if (Type.GetType(property.PropertyType.ToString()) == typeof(List<>))
                {
                    //var lo=GetObjFromQueryString<Type.GetType(property.PropertyType.ToString())>
                }
                else
                {
                    var valueAsString = HttpContext.Current.Request.QueryString[property.Name];

                    var value = Convert.ChangeType(valueAsString, Type.GetType(property.PropertyType.ToString()));

                    if (value == null)
                        continue;

                    property.SetValue(obj, value, null);
                }

            }
            return obj;
        }

        internal static string GetNepaliUnicodeNumber(string field)
        {
            /*१ २ ३ ४ ५ ६ ७ ८ ९ ० */
            string result = "";
            for (int i = 0; i < field.Length; i++)
            {
                switch (field[i])
                {
                    case '0':
                        result = result + "०";
                        break;
                    case '1':
                        result = result + "१";
                        break;
                    case '2':
                        result = result + "२";
                        break;
                    case '3':
                        result = result + "३";
                        break;
                    case '4':
                        result = result + "४";
                        break;
                    case '5':
                        result = result + "५";
                        break;
                    case '6':
                        result = result + "६";
                        break;
                    case '7':
                        result = result + "७";
                        break;
                    case '8':
                        result = result + "८";
                        break;
                    case '9':
                        result = result + "९";
                        break;

                    default:
                        result = result + field[i];
                        break;
                }
            }
            return result;
        }

        internal static void AddAuditTrail(string menuCode, string actionCode, DateTime systemTime, int userId, int targetId, string message)
        {
            SAuditTrial services = new SAuditTrial(menuCode, actionCode, systemTime, userId, targetId, message);
            services.Add();
        }
    }
}