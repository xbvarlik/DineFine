namespace DineFine.DataObjects.Models;

public class CategoryCreateModel : BaseCreateModel
{
    public string Name { get; set; } = null!;
}

public class CategoryUpdateModel : BaseUpdateModel
{
    public string? Name { get; set; } 
}

public class CategoryViewModel : BaseViewModel
{
    public string Name { get; set; } = null!;
}