using WIPFLI.Models.Resource;
using WIPFLI.Models.Response;

namespace WIPFLI.Infrastructure.Services
{
    public interface IBillingService
    {
        CartResponse CalculateDiscountAndGetFinalBill(CartResource resource);
    }
}
