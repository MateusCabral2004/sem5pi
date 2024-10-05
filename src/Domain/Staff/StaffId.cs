using Newtonsoft.Json;
using Sempi5.Domain.Shared;

namespace Sempi5.Domain.Staff
{
    public class StaffId : EntityId
    {
        // Parameterless constructor for EF Core
        public StaffId() : base(string.Empty) // or you can choose to set it to null if appropriate
        {
        }

        // Constructor to initialize StaffId with a string value
        [JsonConstructor]
        public StaffId(string value) : base(value)
        {
        }

        public override string AsString()
        {
            return (string)base.ObjValue;
        }

        protected override object createFromString(string text)
        {
            throw new NotImplementedException();
        }

        // Additional validation can be done here if necessary
    }
}
