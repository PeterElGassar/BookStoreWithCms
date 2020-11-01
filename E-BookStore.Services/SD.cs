using System;
using System.Collections.Generic;
using System.Text;

namespace E_BookStore.Services
{
    public static class SD
    {
        public const string Proc_CoverType_GetAll = "usp_GetCoverTypes";
        public const string Proc_CoverType_Get = "usp_GetCoverType";

        public const string Proc_CoverType_Delete = "usp_DeleteCoverType";

        public const string Proc_CoverType_Update = "usp_UpdateCoverType";
        public const string Proc_CoverType_Create = "usp_InsertCoverType";


        public const string Role_Admin = "Admin";
        public const string Role_Employee = "Employee";
        public const string Role_User_Comp = "Company Customer";
        public const string Role_User_Indi = "Individual User";

    }
}
