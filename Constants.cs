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
            public const string GlobalAdmin = "global_admin";
            public const string GlobalAdminDescription = "Application administrator across all accounts";

            public const string AccountAdmin = "account_admin";
            public const string AccountAdminDescription = "Account admin for specific account(s) only";

            public const string ReportViewer = "report_viewer";
            public const string ReportViewerDescription = "User can access reporting data";

            public const string Manager = "manager";
            public const string ManagerDescription = "User is a manager of an employee";

            public const string Employee = "employee";
            public const string EmployeeDescription = "User is an employee in an account";
        }
    }
}
