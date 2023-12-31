﻿namespace DineFine.DataObjects.Models;

public class BaseCreateModel { }
public class BaseUpdateModel { }

public class BaseViewModel
{
    public int Id { get; set; }
}
public class BaseQueryFilterModel { }

public class BaseCosmosQueryFilterModel : BaseQueryFilterModel
{
    public string PartitionKey { get; set; } = null!;
}