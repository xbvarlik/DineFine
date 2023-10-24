using DineFine.DataObjects.Models;

namespace DineFine.Accessor.Mappings;

public static class CustomerReviewMapper
{
    public static CustomerReviewViewModel? ToViewModel(this CustomerReviewCreateModel? model)
    {
        if (model == null)
            return null;
        
        return new CustomerReviewViewModel
        {
            CustomerId = model.CustomerId,
            Rating = model.Rating,
            Review = model.Review
        };
    }
    
    public static void ToUpdatedViewModel(this CustomerReviewViewModel? viewModel, CustomerReviewUpdateModel? updateModel)
    {
        if (updateModel == null || viewModel == null)
            return;
        
        viewModel.CustomerId = updateModel.CustomerId ?? viewModel.CustomerId;
        viewModel.Rating = updateModel.Rating ?? viewModel.Rating;
        viewModel.Review = updateModel.Review ?? viewModel.Review;
    }
}