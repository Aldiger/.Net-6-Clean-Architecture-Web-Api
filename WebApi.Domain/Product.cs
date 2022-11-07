using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WebApi.Domain;

[Table(nameof(Product))]
public class Product : BaseEntity
{
    public string Code { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    [AllowNull]
    public string Description { get; set; }

    public List<ProductHistory> History { get; set; }


    public void LogHistory()
    {
        this.History.Add(new ProductHistory
        {
            Code = Code,
            Name = Name,
            Price = Price,
            Description = Description
        });
    }
}