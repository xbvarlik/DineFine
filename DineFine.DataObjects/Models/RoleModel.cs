namespace DineFine.DataObjects.Models;

public class RoleCreateModel : BaseCreateModel
{
    public string Name { get; set; } = null!;
}

public class RoleUpdateModel : BaseUpdateModel
{
    public string? Name { get; set; } = null!;
}

public class RoleViewModel : BaseViewModel
{
    public string Name { get; set; } = null!;
}