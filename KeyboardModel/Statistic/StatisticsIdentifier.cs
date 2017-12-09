using System.ComponentModel;
using System.Runtime.CompilerServices;
using KeyboardModel.Annotations;

namespace KeyboardModel.Statistic
{
    using System;
    using KeyboardModel.Enums;

    /// <summary>
    /// Represent unique identifier in sense of three unemus <see cref="Complexity"/>, <see cref="Language"/>, <see cref="Time"/>
    /// </summary>
    [Serializable]
    public struct StatisticsIdentifier:
        INotifyPropertyChanged
    {
        private Complexity complexityID;
        private Language languageID;
        private Time timeID;
        public StatisticsIdentifier(Complexity complexityId, Language languageId, Time timeId):this()
        {
            ComplexityID = complexityId;
            LanguageID = languageId;
            TimeID = timeId;
        }

        public Complexity ComplexityID
        {
            get { return complexityID; }
            set
            {
                complexityID = value;
                OnPropertyChanged(nameof(ComplexityID));
            }
        }

        public Language LanguageID
        {
            get { return languageID; }
            set
            {
                languageID = value;
                OnPropertyChanged(nameof(LanguageID));
            }
        }

        public Time TimeID
        {
            get { return timeID; }
            set
            {
                timeID = value;
                OnPropertyChanged(nameof(TimeID));
            }
        }

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

        public override string ToString()
        {
            return $"Language: {languageID} Time: {timeID} Complexity: {ComplexityID}";
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}