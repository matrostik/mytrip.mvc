using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mytrip.Core.Models
{
    public class mtSQLError
    {
        public static string ShowSqlException(string connectionString)
        {
            string queryString = "EXECUTE NonExistantStoredProcedure";
            StringBuilder errorMessages = new StringBuilder();
            StringBuilder _errorMessages = new StringBuilder();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "<br/>" +
                            "Message: " + ex.Errors[i].Message + "<br/>" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "<br/>" +
                            "Source: " + ex.Errors[i].Source + "<br/>" +
                            "Procedure: " + ex.Errors[i].Procedure);
                        _errorMessages.Append(ex.Errors[i].Procedure);
                    }
                }
            }
            if (_errorMessages.Length > 1)
            {
                return errorMessages.ToString();
            }
          else
              return string.Empty;
        }


    }
    public class SqlSettingAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (CoreSetting.Development &&  mtSQLError.ShowSqlException(CoreSetting.connectionStringSQL()).Length>5)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                      new { controller = "Core", action = "Index"}));
            }
        }
    }
}
