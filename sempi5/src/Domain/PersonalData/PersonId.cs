using Newtonsoft.Json;
using Sempi5.Domain.Shared;

namespace Sempi5.Domain.PersonalData
{
    public class PersonId : EntityId, IValueObject
    {
        public PersonId() : base(null)
        {
        }

        public PersonId(long value) : base(value)
        {
        }

        public override string AsString()
        {
            return (string)base.ObjValue.ToString();
        }

        public long AsLong()
        {
            return (long)base.ObjValue;
        }

        protected override object createFromString(string text)
        {
            return long.Parse(text);
        }
    }
}