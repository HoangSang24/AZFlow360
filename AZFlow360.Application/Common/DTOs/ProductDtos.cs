using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZFlow360.Application.Common.DTOs
{
    public record ProductDto(
        int Id,
        string ProductName,
        string? Description,
        int CategoryId,
        string CategoryName,
        int? SupplierId,
        string? SupplierName,
        IReadOnlyCollection<ProductVariantDto> Variants
    );

    public record ProductVariantDto(
        int Id,
        string SKU,
        decimal SalePrice,
        int Stock
    );

    public record CategoryDto(int Id, string CategoryName, int? ParentCategoryID);
    public record SupplierDto(int Id, string SupplierName);
}
