using PollAndNomination.DataModel.Model;
using System;

namespace PollAndNomination.DataModel
{
    public class SampleDataModel : DataModel
    {
        public SampleDataModel()
            : base()
        {
            // TODO Ágnes: create sample objects - use all the classes - you should not edit that classes
            News news1 = new News { ID = Guid.NewGuid(), Title = "First", Text = "Blah blah", PublicationDate = DateTime.Now.AddDays(-2) };
        }
    }
}