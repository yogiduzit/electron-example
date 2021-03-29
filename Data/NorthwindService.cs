using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectronServerBlazorEf.NW;
using Microsoft.EntityFrameworkCore;

namespace ElectronServerBlazorEf.Data
{
  public class NorthwindService
  {
    private readonly NorthwindContext _context;
    public NorthwindService(NorthwindContext context)
    {
      _context = context;
    }
    public async Task<List<object>> GetCategoriesByProductAsync()
    {
      var query = _context.Products
        .Include(c => c.Category)
        .GroupBy(p => p.Category.CategoryName)
        .Select(g => new
        {
          Name = g.Key,
          Count = g.Count()
        })
        .OrderByDescending(cp => cp.Count);

      return await query.ToListAsync<object>();
    }
  }
}
