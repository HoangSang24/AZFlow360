using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZFlow360.Application.Common.DTOs
{
    public record OrderDto(
        int Id,
        string OrderCode,
        DateTime OrderDate,
        decimal TotalAmount,
        string Status,
        UserBriefDto User,
        CustomerBriefDto? Customer,
        IReadOnlyCollection<OrderDetailDto> Details
    );

    public record OrderDetailDto(
        int Id,
        int VariantId,
        string Sku,
        string ProductName,
        int Quantity,
        decimal UnitPrice,
        decimal Subtotal
    );

    public record CustomerBriefDto(int Id, string FullName);
}