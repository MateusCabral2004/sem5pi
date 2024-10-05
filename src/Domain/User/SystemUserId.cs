using Newtonsoft.Json;
using Sempi5.Domain.Shared;

namespace Sempi5.Domain.User
{
    public class SystemUserId : EntityId
    {
        // Parameterless constructor for EF Core
        public SystemUserId() : base(string.Empty) // or you can choose to set it to null if appropriate
        {
        }

        // Constructor to initialize StaffId with a string value
        [JsonConstructor]
        public SystemUserId(string value) : base(value)
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
