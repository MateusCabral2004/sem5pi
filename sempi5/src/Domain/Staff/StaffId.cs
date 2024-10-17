using Newtonsoft.Json;
using Sempi5.Domain.Shared;

namespace Sempi5.Domain.Staff
{
    public class StaffId : EntityId
    {

        public StaffId(string value) : base(value)
        {
        }
        
        override
        public string AsString()
        {
            return (string)ObjValue;
        }

        protected override object createFromString(string text)
        {
            return text;
        }
        
    }
}
