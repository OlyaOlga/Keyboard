using KeyboardModel.Enums;

namespace KeyboardModel.Statistic
{
    /// <summary>
    /// Represent unique identifier in sense of three unemus <see cref="Complexity"/>, <see cref="Language"/>, <see cref="Time"/>
    /// </summary>
    public struct StatisticsIdentifier
    {
        public StatisticsIdentifier(Complexity complexityId, Language languageId, Time timeId)
        {
            ComplexityID = complexityId;
            LanguageID = languageId;
            TimeID = timeId;
        }

        public Complexity ComplexityID { get; }

        public Language LanguageID { get; }

        public Time TimeID { get; }

        public bool Equals(StatisticsIdentifier other)
        {
            return ComplexityID == other.ComplexityID && LanguageID == other.LanguageID && TimeID == other.TimeID;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is StatisticsIdentifier && Equals((StatisticsIdentifier) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) ComplexityID;
                hashCode = (hashCode*397) ^ (int) LanguageID;
                hashCode = (hashCode*397) ^ (int) TimeID;
                return hashCode;
            }
        }

        public static bool operator ==(StatisticsIdentifier left, StatisticsIdentifier right)
        {
            return left.ComplexityID == right.ComplexityID && left.LanguageID == right.LanguageID &&
                   left.TimeID == right.TimeID;
        }

        public static bool operator !=(StatisticsIdentifier left, StatisticsIdentifier right)
        {
            return !(left == right);
        }
    }
}