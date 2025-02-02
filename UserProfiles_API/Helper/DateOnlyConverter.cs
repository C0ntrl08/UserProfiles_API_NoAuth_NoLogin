using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace UserProfiles_API.Helper
{
    public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
    {
        public DateOnlyConverter() : base(
            d => d.ToDateTime(TimeOnly.MinValue),
            d => DateOnly.FromDateTime(d))
            { }
    }
}
