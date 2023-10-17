namespace DineFine.DataObjects.Models;

public class IngredientCreateModel : BaseCreateModel
{
    public string Name { get; set; } = null!;
}

public class IngredientUpdateModel : BaseUpdateModel
{
    public string? Name { get; set; }
}

public class IngredientViewModel : BaseViewModel
{
    public string Name { get; set; } = null!;
}