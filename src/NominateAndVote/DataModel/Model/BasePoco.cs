using System;

namespace NominateAndVote.DataModel.Model
{
    public abstract class BasePoco<TPoco> : IEquatable<TPoco> where TPoco : BasePoco<TPoco>
    {
        public abstract bool Equals(TPoco other);
    }
}