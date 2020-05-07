using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app_template
{
    public static class Constants
    {
        public static class Roles
        {
            public static string GlobalAdmin = "global_admin";
            public static string GlobalAdminDescription = "Application administrator across all accounts";

            public static string AccountAdmin = "account_admin";
            public static string AccountAdminDescription = "Account admin for specific account(s) only";

            public static string ReportViewer = "report_viewer";
            public static string ReportViewerDescription = "User can access reporting data";

            public static string Manager = "manager";
            public static string ManagerDescription = "User is a manager of an employee";

            public static string Employee = "employee";
            public static string EmployeeDescription = "User is an employee in an account";
        }
    }
}
