using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Domain;

[Owned]
public class ProductHistory : BaseEntity
{
    public string Code { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    [AllowNull]
    public string Description { get; set; }
}