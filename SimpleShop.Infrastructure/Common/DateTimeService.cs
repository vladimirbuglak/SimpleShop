using SimpleShop.Application.Modify.Interfaces;

namespace SimpleShop.Infrastructure.Common
{
    public class DateTimeService : IDateTime
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
