using Newtonsoft.Json;
using Sempi5.Domain.Shared;

namespace Sempi5.Domain.Staff
{
    public class LicenseNumber : EntityId
    {
        public LicenseNumber() : base(string.Empty)
        {
        }

        [JsonConstructor]
        public LicenseNumber(string value) : base(value)
        {
        }

        public override string AsString()
        {
            return (string)ObjValue;
        }

        protected override object createFromString(string text)
        {
            return text;
        }
        
    }
}
