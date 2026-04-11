using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

public class ProductsModel : PageModel
{
    private readonly MyDbContext _context;
    public ProductsModel(MyDbContext context)
    {
        _context = context;
    }

    public List<Product> Products { get; set; } = new List<Product>();

    public void OnGet()
    {
        // Load products from EF Core
        Products = _context.Products.ToList();
    }

    // Binds the form values to this property on POST
    [BindProperty]
    public ProductViewModel Product { get; set; } = new ProductViewModel();
         // Simple model class representing a product
    public class ProductViewModel
    {
        [Required]
        [StringLength(100)] // Set maximum length
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 10000.00)] //Set a price range between 0.01$ and 10000.00$
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        [Range(0, 1000)] //Set a Stock Quantity range between 0$ and 10000$
        public int StockQuantity { get; set; }
    }
    // Async handler using EF Core SaveChangesAsync
    public async Task<IActionResult> OnPostAsync()
    {
        // Server-side validation , ModelState is from Data Annotations
        if (!ModelState.IsValid)
        {
            return Page(); // Return the page to show the validation errors
        }

        // Map view model to entity
        var productItem = new Product
        {
            Name = Product.Name,
            Price = Product.Price,
            Description = Product.Description,
            StockQuantity = Product.StockQuantity
        };

        // data processing logic 
        // Add the Entity to the context and save the changes
        await _context.Products.AddAsync(productItem);
        await _context.SaveChangesAsync();

        TempData["Message"] = "Product added successfully!"; // Feedback
        return RedirectToPage(); // Redirect to GET to avoid double entity post
    }
}
