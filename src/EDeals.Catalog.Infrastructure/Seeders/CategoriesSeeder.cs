using EDeals.Catalog.Domain.Entities.ItemEntities;
using EDeals.Catalog.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace EDeals.Catalog.Infrastructure.Seeders
{
    public class CategoriesSeeder
    {
        private readonly AppDbContext _context;

        public CategoriesSeeder(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task AddTopCategories()
        {
            if (!_context.ProductsCategories.Any())
            {
                var categories = new List<ProductCategory>
                {
                    new ProductCategory
                    {
                        CategoryName = "Electronice",
                        Description = "Produse electronice",
                    },
                    new ProductCategory
                    {
                        CategoryName = "Telefoane Mobile si Tablete",
                        Description = "Telefoane Mobile si Tablete",
                    },
                    new ProductCategory
                    {
                        CategoryName = "Laptopuri si accesorii",
                        Description = "Laptopuri si accesorii",
                    },
                    new ProductCategory
                    {
                        CategoryName = "Calculatoare si accesorii",
                        Description = "Calculatoare si accesorii",
                    },
                    new ProductCategory
                    {
                        CategoryName = "Haine",
                        Description = "Haine",
                    },
                };

                await _context.AddRangeAsync(categories);
                await _context.SaveChangesAsync();
            }
        }
           
        public async Task AddSubcategories()
        {
            var electronicsCategory = await _context.ProductsCategories.Where(x => x.CategoryName == "Electronice").FirstOrDefaultAsync();
            var mobileCategory = await _context.ProductsCategories.Where(x => x.CategoryName == "Telefoane Mobile si Tablete").FirstOrDefaultAsync();
            var laptopCategory = await _context.ProductsCategories.Where(x => x.CategoryName == "Laptopuri si accesorii").FirstOrDefaultAsync();
            var pcCategory = await _context.ProductsCategories.Where(x => x.CategoryName == "Calculatoare si accesorii").FirstOrDefaultAsync();
            var fashionCategory = await _context.ProductsCategories.Where(x => x.CategoryName == "Haine").FirstOrDefaultAsync();

            if (laptopCategory != null && !_context.ProductsCategories.Any(x => x.ParentCategoryId == laptopCategory.Id))
            {
                var laptopsSubcategories = new List<ProductCategory>()
                {
                    new ProductCategory
                    {
                        CategoryName = "Laptopuri",
                        Description = "",
                        ParentCategory = laptopCategory
                    },
                    new ProductCategory
                    {
                        CategoryName = "Accesorii Laptop",
                        Description = "",
                        ParentCategory = laptopCategory
                    },
                    new ProductCategory
                    {
                        CategoryName = "Resigilate",
                        Description = "",
                        ParentCategory = laptopCategory
                    },
                    new ProductCategory
                    {
                        CategoryName = "Branduri de Top",
                        Description = "",
                        ParentCategory = laptopCategory
                    },
                };
                
                await _context.AddRangeAsync(laptopsSubcategories);
                await _context.SaveChangesAsync();

                var subSubcategories = new List<ProductCategory>
                {
                    new ProductCategory
                    {
                        CategoryName = "Laptopuri cu Windows",
                        Description = "Laptopuri cu Windows",
                        ParentCategory = laptopsSubcategories[0]
                    },
                    new ProductCategory
                    {
                        CategoryName = "Laptopuri Gaming",
                        Description = "Laptopuri Gaming",
                        ParentCategory = laptopsSubcategories[0]
                    },
                    new ProductCategory
                    {
                        CategoryName = "Laptopuri Apple",
                        Description = "Laptopuri Apple",
                        ParentCategory = laptopsSubcategories[0]
                    },
                    new ProductCategory
                    {
                        CategoryName = "Asus",
                        Description = "Asus",
                        ParentCategory = laptopsSubcategories[3]
                    },
                    new ProductCategory
                    {
                        CategoryName = "Lenovo",
                        Description = "Lenovo",
                        ParentCategory = laptopsSubcategories[3]
                    },
                    new ProductCategory
                    {
                        CategoryName = "HP",
                        Description = "HP",
                        ParentCategory = laptopsSubcategories[3]
                    },
                    new ProductCategory
                    {
                        CategoryName = "Apple",
                        Description = "Apple",
                        ParentCategory = laptopsSubcategories[3]
                    },
                    new ProductCategory
                    {
                        CategoryName = "Acer",
                        Description = "Acer",
                        ParentCategory = laptopsSubcategories[3]
                    },
                    new ProductCategory
                    {
                        CategoryName = "Genti laptop",
                        Description = "Genti laptop",
                        ParentCategory = laptopsSubcategories[1]
                    },
                    new ProductCategory
                    {
                        CategoryName = "Standuri/Coolere laptop",
                        Description = "Standuri/Coolere laptop",
                        ParentCategory = laptopsSubcategories[1]
                    },
                    new ProductCategory
                    {
                        CategoryName = "Hard disk-uri notebook",
                        Description = "Hard disk-uri notebook",
                        ParentCategory = laptopsSubcategories[1]
                    },
                };
                await _context.AddRangeAsync(subSubcategories);
                await _context.SaveChangesAsync();
            }
            
            if (mobileCategory != null && !_context.ProductsCategories.Any(x => x.ParentCategoryId == mobileCategory.Id))
            {
                var mobileSubcategories = new List<ProductCategory>()
                {
                    new ProductCategory
                    {
                        CategoryName = "Telefoane mobile si accesorii",
                        Description = "",
                        ParentCategory = mobileCategory
                    },
                    new ProductCategory
                    {
                        CategoryName = "Accesorii telefoane",
                        Description = "",
                        ParentCategory = mobileCategory
                    },
                    new ProductCategory
                    {
                        CategoryName = "Tablete si accesorii",
                        Description = "",
                        ParentCategory = mobileCategory
                    },
                    new ProductCategory
                    {
                        CategoryName = "Resigilate",
                        Description = "",
                        ParentCategory = mobileCategory
                    },
                };

                await _context.AddRangeAsync(mobileSubcategories);
                await _context.SaveChangesAsync();

                var mobileSubSubcategories = new List<ProductCategory>()
                {
                    new ProductCategory
                    {
                        CategoryName = "Telefoane mobile",
                        Description = "",
                        ParentCategory = mobileSubcategories[0]
                    },
                    new ProductCategory
                    {
                        CategoryName = "Huse Telefoane",
                        Description = "",
                        ParentCategory = mobileSubcategories[1]
                    },
                    new ProductCategory
                    {
                        CategoryName = "Folii protectie telefoane",
                        Description = "",
                        ParentCategory = mobileSubcategories[1]
                    },
                    new ProductCategory
                    {
                        CategoryName = "Incarcatoare telefoane",
                        Description = "",
                        ParentCategory = mobileSubcategories[1]
                    },
                    new ProductCategory
                    {
                        CategoryName = "Tablete",
                        Description = "",
                        ParentCategory = mobileSubcategories[2]
                    },
                    new ProductCategory
                    {
                        CategoryName = "Huse tablete",
                        Description = "",
                        ParentCategory = mobileSubcategories[2]
                    },
                    new ProductCategory
                    {
                        CategoryName = "Folii protectie tablete",
                        Description = "",
                        ParentCategory = mobileSubcategories[2]
                    },
                    new ProductCategory
                    {
                        CategoryName = "Incarcatoare tablete",
                        Description = "",
                        ParentCategory = mobileSubcategories[2]
                    }
                };

                await _context.AddRangeAsync(mobileSubSubcategories);
                await _context.SaveChangesAsync();

            }

            if (pcCategory != null && !_context.ProductsCategories.Any(x => x.ParentCategoryId == pcCategory.Id))
            {
                var pcSubcategories = new List<ProductCategory>()
                {
                    new ProductCategory
                    {
                        CategoryName = "Calculatoare si Monitoare",
                        Description = "",
                        ParentCategory = pcCategory
                    },
                    new ProductCategory
                    {
                        CategoryName = "Componente Calculator",
                        Description = "",
                        ParentCategory = pcCategory
                    },
                    new ProductCategory
                    {
                        CategoryName = "Periferice",
                        Description = "",
                        ParentCategory = pcCategory
                    },
                    new ProductCategory
                    {
                        CategoryName = "Software",
                        Description = "",
                        ParentCategory = pcCategory
                    },
                };

                await _context.AddRangeAsync(pcSubcategories);
                await _context.SaveChangesAsync();

                var subSubcategories = new List<ProductCategory>()
                {
                    new ProductCategory
                    {
                        CategoryName = "Calculatoare",
                        Description = "",
                        ParentCategory = pcSubcategories[0]
                    },
                    new ProductCategory
                    {
                        CategoryName = "Monitoare",
                        Description = "",
                        ParentCategory = pcSubcategories[0]
                    },
                    new ProductCategory
                    {
                        CategoryName = "Placi video",
                        Description = "",
                        ParentCategory = pcSubcategories[1]
                    }
                    ,new ProductCategory
                    {
                        CategoryName = "Placi de baza",
                        Description = "",
                        ParentCategory = pcSubcategories[1]
                    },new ProductCategory
                    {
                        CategoryName = "Procesoare",
                        Description = "",
                        ParentCategory = pcSubcategories[1]
                    },new ProductCategory
                    {
                        CategoryName = "SSD",
                        Description = "",
                        ParentCategory = pcSubcategories[1]
                    },new ProductCategory
                    {
                        CategoryName = "HDD",
                        Description = "",
                        ParentCategory = pcSubcategories[1]
                    },new ProductCategory
                    {
                        CategoryName = "RAM",
                        Description = "",
                        ParentCategory = pcSubcategories[1]
                    },new ProductCategory
                    {
                        CategoryName = "Carcase",
                        Description = "",
                        ParentCategory = pcSubcategories[1]
                    },new ProductCategory
                    {
                        CategoryName = "Coolere",
                        Description = "",
                        ParentCategory = pcSubcategories[1]
                    },new ProductCategory
                    {
                        CategoryName = "Placi sunet",
                        Description = "",
                        ParentCategory = pcSubcategories[1]
                    },new ProductCategory
                    {
                        CategoryName = "Mouse",
                        Description = "",
                        ParentCategory = pcSubcategories[2]
                    },new ProductCategory
                    {
                        CategoryName = "Tastaturi",
                        Description = "",
                        ParentCategory = pcSubcategories[2]
                    },new ProductCategory
                    {
                        CategoryName = "Boxe",
                        Description = "",
                        ParentCategory = pcSubcategories[2]
                    },new ProductCategory
                    {
                        CategoryName = "Casti",
                        Description = "",
                        ParentCategory = pcSubcategories[2]
                    },new ProductCategory
                    {
                        CategoryName = "Microfoane",
                        Description = "",
                        ParentCategory = pcSubcategories[2]
                    },new ProductCategory
                    {
                        CategoryName = "Memorii USB",
                        Description = "",
                        ParentCategory = pcSubcategories[2]
                    },new ProductCategory
                    {
                        CategoryName = "Camere Web",
                        Description = "",
                        ParentCategory = pcSubcategories[2]
                    },new ProductCategory
                    {
                        CategoryName = "Sisteme operare",
                        Description = "",
                        ParentCategory = pcSubcategories[3]
                    },new ProductCategory
                    {
                        CategoryName = "Antivirusi",
                        Description = "",
                        ParentCategory = pcSubcategories[3]
                    },
                };

                await _context.AddRangeAsync(subSubcategories);
                await _context.SaveChangesAsync();
            }

            if (electronicsCategory != null && !_context.ProductsCategories.Any(x => x.ParentCategoryId == electronicsCategory.Id))
            {
                
            }
            
            if (fashionCategory != null && !_context.ProductsCategories.Any(x => x.ParentCategoryId == fashionCategory.Id))
            {
                
            }
        }
    }
}
