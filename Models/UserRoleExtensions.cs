namespace CapEx.Models;

public static class UserRoleExtensions
{
    public static string ToDisplayName(this UserRole role) => role switch
    {
        UserRole.Employee => "Employee",
        UserRole.DepartmentManager => "Department Manager",
        UserRole.FinanceDirector => "Finance Director",
        UserRole.CEO => "CEO",
        _ => role.ToString()
    };

    public static bool IsApprover(this UserRole role) => role != UserRole.Employee;
}
