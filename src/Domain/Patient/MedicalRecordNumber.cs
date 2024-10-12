using System;
using Newtonsoft.Json;
using Sempi5.Domain.Shared;

namespace Sempi5.Domain.Patient
{
    public class MedicalRecordNumber : EntityId
    {
        
        public MedicalRecordNumber() : base(null)
        {
        }
        
        public MedicalRecordNumber(String value) : base(value)
        {
        }
        
        protected override object createFromString(String text)
        {
            return text;
        }

        override
        public String AsString()
        {
            return (string)ObjValue;
        }
    }
}