using NominateAndVote.DataModel.Poco;
using NominateAndVote.DataTableStorage.Common;

namespace NominateAndVote.DataTableStorage.Entity
{
    public class AdministratorEntity : BaseEntity<Administrator>
    {
        public long UserId
        {
            get
            {
                long userId;
                if (!long.TryParse(PartitionKey, out userId))
                {
                    throw new DataStorageException("Data inconsistency: the stored user ID is not a number: " + PartitionKey) { DataElement = this };
                }
                return userId;
            }
        }

        public AdministratorEntity()
        {
        }

        public AdministratorEntity(Administrator poco)
            : base(poco)
        {
            PartitionKey = poco.UserId.ToString("D8");
            RowKey = "";
        }

        public override Administrator ToPoco()
        {
            return new Administrator { UserId = UserId };
        }
    }
}