using System;

namespace NominateAndVote.DataModel.Common
{
    public class DataException : Exception
    {
        public object DataElement { get; set; }

        public DataException(string message)
            : base(message)
        {
        }

        public DataException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}