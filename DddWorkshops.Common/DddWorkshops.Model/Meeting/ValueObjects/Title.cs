using System;
using DddWorkshops.Common.Guard;
using DddWorkshops.Common.ModelFramework;

namespace DddWorkshops.Model.Meeting.ValueObjects
{
    internal class Title : ValueObject<Title>
    {
        private Title(string name)
        {
            MeetingTitle = name;
        }

        public static Title Give(string name)
        {
            Guard.With<ArgumentException>()
                .Against(
                    string.IsNullOrWhiteSpace(name),
                    $"{nameof(name)} cannot be null, empty or whitespace!",
                    nameof(name));

            return new Title(name);
        }

        public string MeetingTitle { get; private set; }

        public override string ToString() => MeetingTitle;

        protected override bool InternalEquals(Title valueObject) => 
            valueObject.MeetingTitle == MeetingTitle;

        protected override int InternalGetHashCode() => HashCode.Combine(GetType(), MeetingTitle);
    }
}