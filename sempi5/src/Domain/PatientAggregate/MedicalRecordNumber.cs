using System;
using Newtonsoft.Json;
using Sempi5.Domain.Shared;

namespace Sempi5.Domain.PatientAggregate
{
    public class MedicalRecordNumber : EntityId
    {
        
        public MedicalRecordNumber(string value) : base(value)
        {
        }
        
        protected override object createFromString(string text)
        {
            return text;
        }

        override
        public string AsString()
        {
            return (string)ObjValue;
        }
    }
}