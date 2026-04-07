using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class ProductsModel : PageModel
{
    // Binds the form values to this property on POST
    [BindProperty]
    public ProductViewModel Product { get; set; }

    public void OnGet()
    {
    }
}

// Simple model class representing a product
public class ProductViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public int StockQuantity { get; set; }
}
