﻿using Microsoft.EntityFrameworkCore;

namespace DineFine.DataObjects.Entities;

public class TableOfRestaurant : BaseEntity
{
    public int RestaurantId { get; set; }
    public int TableStatusId { get; set; }
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public virtual TableStatus? TableStatus { get; set; }
    public virtual Restaurant? Restaurant { get; set; }
    public int NumberOfSeats { get; set; }
}