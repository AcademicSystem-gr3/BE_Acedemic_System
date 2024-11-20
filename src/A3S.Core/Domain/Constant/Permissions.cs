using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3S.Core.Domain.Constant
{
    public static class Permissions
    {
        public static class Dashboard
        {
            [Description("Xem Dashboard")]
            public const string view = "Permissions.Dashboard.View";
        }
        public static class Roles
        {
            [Description("Xem quyen")]
            public const string View = "Permissions.Roles.View";
            [Description("Tao moi quyen")]
            public const string Create = "Permissions.Roles.Create";
            [Description("Sua quyen")]
            public static string Edit = "Permissions.Roles.Edit";
            [Description("Xoa quyen")]
            public static string Delete = "Permissions.Roles.Delete";
        }
        public static class Users
        {
            [Description("Xem nguoi dung")]
            public const string View = "Permissions.Users.View";
            [Description("Tao moi nguoi dung")]
            public const string Create = "Permissions.Users.Create";
            [Description("Sua nguoi dung")]
            public static string Edit = "Permissions.Users.Edit";
            [Description("Xoa nguoi dung")]
            public static string Delete = "Permissions.Users.Delete";
        }
        public static class Answers
        {
            [Description("Xem cau tra loi")]
            public const string View = "Permissions.Answers.View";
            [Description("Tao cau tra loi")]
            public const string Create = "Permissions.Answers.Create";
            [Description("Sua cau tra loi")]
            public static string Edit = "Permissions.Answers.Edit";
            [Description("Xoa cau tra loi")]
            public static string Delete = "Permissions.Answers.Delete";
        }
        public static class Blogs
        {
            [Description("Xem bai viet")]
            public const string View = "Permissions.Blogs.View";
            [Description("Tao bai viet")]
            public const string Create = "Permissions.Blogs.Create";
            [Description("Sua bai viet")]
            public static string Edit = "Permissions.Blogs.Edit";
            [Description("Xoa bai viet")]
            public static string Delete = "Permissions.Blogs.Delete";
        }
        public static class Classes
        {
            [Description("Xem lop")]
            public const string View = "Permissions.Classes.View";
            [Description("Tao lop")]
            public const string Create = "Permissions.Classes.Create";
            [Description("Sua lop")]
            public static string Edit = "Permissions.Classes.Edit";
            [Description("Xoa lop")]
            public static string Delete = "Permissions.Classes.Delete";
        }
        public static class CommentBlogs
        {
            [Description("Xem binh luan")]
            public const string View = "Permissions.CommentBlogs.View";
            [Description("Tao binh luan")]
            public const string Create = "Permissions.CommentBlogs.Create";
            [Description("Sua binh luan")]
            public static string Edit = "Permissions.CommentBlogs.Edit";
            [Description("Xoa binh luan")]
            public static string Delete = "Permissions.CommentBlogs.Delete";
        }

        public static class FileContents
        {
            [Description("Xem file")]
            public const string View = "Permissions.FileContents.View";
            [Description("Tao file")]
            public const string Create = "Permissions.FileContents.Create";
            [Description("Sua file")]
            public static string Edit = "Permissions.FileContents.Edit";
            [Description("Xoa file")]
            public static string Delete = "Permissions.FileContents.Delete";
        }
        public static class Folders
        {
            [Description("Xem file")]
            public const string View = "Permissions.Folders.View";
            [Description("Tao file")]
            public const string Create = "Permissions.Folders.Create";
            [Description("Sua file")]
            public static string Edit = "Permissions.Folders.Edit";
            [Description("Xoa file")]
            public static string Delete = "Permissions.Folders.Delete";
        }
        public static class Homeworks
        {
            [Description("Xem bai tap")]
            public const string View = "Permissions.Homeworks.View";
            [Description("Tao bai tap")]
            public const string Create = "Permissions.Homeworks.Create";
            [Description("Sua bai tap")]
            public static string Edit = "Permissions.Homeworks.Edit";
            [Description("Xoa bai tap")]
            public static string Delete = "Permissions.Homeworks.Delete";
        }
    }
}
