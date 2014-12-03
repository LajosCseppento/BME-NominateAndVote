using System;

namespace NominateAndVote.DataTableStorage.Common
{
    public class DataStorageException : Exception
    {
        public object DataElement { get; set; }

        public DataStorageException(string message)
            : base(message)
        {
        }

        public DataStorageException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}