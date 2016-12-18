using System;
using System.ComponentModel.DataAnnotations;

namespace Multi_language.Common.Enums
{
    [Flags]
    public enum ERoleLevels
    {
        [Display(Name = "User Permissions")]
        UserPermissions = 1,

        [Display(Name = "Role Manage Permissions")]
        RoleManagePermissions = 8,

        [Display(Name = "Backup Permissions")]
        BackupPermissions = 9,

        [Display(Name = "Administrator Permissions")]
        AdminPermissions = 10
    }
}
