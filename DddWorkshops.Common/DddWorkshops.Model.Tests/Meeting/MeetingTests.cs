using System;
using DddWorkshops.Common;
using DddWorkshops.Model.Meeting.Entities.Material;
using Xunit;

namespace DddWorkshops.Model.Tests.Meeting
{
    [Trait("Category", TestCategory)]
    public class MeetingTests : IDisposable
    {
        private const string TestCategory = "Model tests: meeting";

        private readonly Model.Meeting.Meeting testMeeting;

        public MeetingTests()
        {
            testMeeting = Model.Meeting.Meeting.Create("Test Title");
            DateTimeProvider.SetUtcNowImplementation(() => DateTime.Parse("2020-09-24 17:00:00"));
        }

        [Fact]
        public void Meeting_WhenStartDateOccursEarlierThanEndDate_CanBeScheduled()
        {
            // Arrange
            var meetingEndTime = DateTimeProvider.UtcNow.AddHours(3);

            // Act
            testMeeting.ScheduleOn(DateTimeProvider.UtcNow, meetingEndTime);

            // Assert
            Assert.Equal(DateTimeProvider.UtcNow, testMeeting.StartDate);
            Assert.Equal(meetingEndTime, testMeeting.EndDate);
        }

        [Fact]
        public void Meeting_OnPassingEndDateEarlierThanStartDate_ThrowsException() =>
            Assert.Throws<ArgumentException>(() => testMeeting.ScheduleOn(
                DateTimeProvider.UtcNow.AddHours(1),
                DateTimeProvider.UtcNow)); // Arrange, Act, Assert

        [Fact]
        public void Meeting_OnCreation_ShouldBeTitled() => Assert.NotNull(testMeeting.MeetingTitle); // Arrange, Act, Assert

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Meeting_OnCreation_ThrowsExceptionWhenInvalidTitleIsProvided(string meetingTitle) =>
            Assert.Throws<ArgumentException>(() =>
                Model.Meeting.Meeting.Create(meetingTitle)); // Arrange, Act, Assert


        [Fact]
        public void Meeting_OnCreating_MaterialListIsEmpty()
        {
            // Assert
            Assert.Empty(testMeeting.AttachedMaterials);
        }

        [Fact]
        public void Meeting_OnAttachingMaterials_CanBeAttached()
        {
            // Arrange
            const string materialName = "materialName";

            // Act
            testMeeting.AttachMaterial(materialName, "https://url.pl", MaterialType.Recording);

            // Assert
            Assert.Contains(testMeeting.AttachedMaterials, material => material.Name == materialName);
        }

        [Theory]
        [InlineData("", "fakeUrl")]
        [InlineData("testName", "")]
        public void Meeting_OnAttachingMaterials_ThrowsExceptionWhenEitherUrlOrNameIsInvalid(string materialName, string url)
        {
            // TODO: implement this
        }


        public void Dispose() => DateTimeProvider.ResetImplementations();
    }
}