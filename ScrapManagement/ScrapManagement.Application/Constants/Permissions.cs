using System.Collections.Generic;

namespace ScrapManagement.Application.Constants
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.Create",
                $"Permissions.{module}.View",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
            };
        }

        public static class Dashboard
        {
            public const string View = "Permissions.Dashboard.View";
            public const string Create = "Permissions.Dashboard.Create";
            public const string Edit = "Permissions.Dashboard.Edit";
            public const string Delete = "Permissions.Dashboard.Delete";
        }

        public static class Scraps
        {
            public const string View = "Permissions.Scraps.View";
            public const string Create = "Permissions.Scraps.Create";
            public const string Edit = "Permissions.Scraps.Edit";
            public const string Delete = "Permissions.Scraps.Delete";
        }

        public static class Users
        {
            public const string View = "Permissions.Users.View";
            public const string Create = "Permissions.Users.Create";
            public const string Edit = "Permissions.Users.Edit";
            public const string Delete = "Permissions.Users.Delete";
        }

        public static class ScrapTypes
        {
            public const string View = "Permissions.ScrapTypes.View";
            public const string Create = "Permissions.ScrapTypes.Create";
            public const string Edit = "Permissions.ScrapTypes.Edit";
            public const string Delete = "Permissions.ScrapTypes.Delete";
        }
    }
}