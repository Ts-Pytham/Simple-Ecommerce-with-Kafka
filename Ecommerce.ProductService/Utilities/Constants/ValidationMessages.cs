namespace Ecommerce.ProductService.Utilities.Constants;

public class ValidationMessages
{
    public const string CategoryNotFound = "Category not found";
    public const string ProductNotFound = "Product not found";
    public const string ProductNotInCategory = "Product does not belong to the category";
    public const string ProductNotInStock = "Product is not in stock";
    public const string ProductQuantityGreaterThanZero = "Product quantity must be greater than zero";
    public const string PriceGreaterThanZero = "Price must be greater than zero";
}
