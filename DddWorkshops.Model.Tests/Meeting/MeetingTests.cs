using System;
using DddWorkshops.Common;
using DddWorkshops.Model.Meeting.Entities.Material;
using DddWorkshops.Model.Meeting.Exceptions;
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

        // TODO during workshops: participants & organizers handling

        public void Dispose() => DateTimeProvider.ResetImplementations();

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
            Assert.Throws<ArgumentException>(
                () => testMeeting.ScheduleOn(
                    DateTimeProvider.UtcNow.AddHours(1),
                    DateTimeProvider.UtcNow)); // Arrange, Act, Assert

        [Fact]
        public void Meeting_OnCreation_ShouldBeTitled() => Assert.NotNull(testMeeting.MeetingTitle); // Arrange, Act, Assert

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Meeting_OnCreation_ThrowsExceptionWhenInvalidTitleIsProvided(string meetingTitle) =>
            Assert.Throws<ArgumentException>(
                () => Model.Meeting.Meeting.Create(meetingTitle)); // Arrange, Act, Assert


        [Fact]
        public void Meeting_OnCreating_MaterialListIsEmpty() => Assert.Empty(testMeeting.AttachedMaterials); // Assert

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

        [Fact]
        public void Meeting_OnRemovingMaterials_CanBeRemoved()
        {
            //Arrange
            const string materialName = "materialName";
            const string otherMaterialName = "thisWillBeRetained";
            testMeeting.AttachMaterial(materialName, "https://url.pl", MaterialType.Recording);
            testMeeting.AttachMaterial(otherMaterialName, "fakeUrl", MaterialType.Presentation);

            //Act
            testMeeting.RemoveMaterial(materialName);

            //Assert
            Assert.DoesNotContain(testMeeting.AttachedMaterials, material => material.Name == materialName);
        }

        [Fact]
        public void Meeting_OnRemovingMaterials_ThrowsExceptionIfMaterialOfSpecifiedNameWasNotFound()
        {
            // Arrange
            const string materialName = "Test material name";

            Assert.Throws<MaterialDoesNotExistException>(() => testMeeting.RemoveMaterial(materialName));
        }


        [Theory]
        [InlineData("", "fakeUrl", MaterialType.CodeSample)]
        [InlineData("testName", "", MaterialType.Notes)]
        [InlineData("materialName", "fakeUrl", (MaterialType) 5)]
        public void Meeting_OnAttachingMaterials_ThrowsExceptionWhenEitherUrlOrNameIsInvalid(
            string materialName,
            string url,
            MaterialType materialType) =>
            Assert.ThrowsAny<ArgumentException>(
                () =>
                    testMeeting.AttachMaterial(materialName, url, materialType));

        [Fact]
        public void Meeting_OnAddingAgenda_NewAgendaCanBeViewed()
        {
            // Arrange
            const string meetingAgenda = "Here should be some points";

            // Act
            testMeeting.AddAgenda(meetingAgenda);

            // Assert
            Assert.Equal(meetingAgenda, testMeeting.ViewAgenda());
        }

        [Fact]
        public void Meeting_OnAddingAgenda_ThrowsExceptionIfAgendaIsAlreadyDefined()
        {
            // Arrange
            const string meetingAgenda = "Here should be some points";
            testMeeting.AddAgenda(meetingAgenda);

            // Act, Assert
            Assert.Throws<AgendaIsAlreadyDefinedException>(() => testMeeting.AddAgenda(meetingAgenda));
        }

        [Fact]
        public void Meeting_OnModifyingAgenda_NewAgendaIsPersisted()
        {
            // Arrange
            const string oldMeetingAgenda = "Old agenda";
            const string newMeetingAgenda = "New and improved meeting agenda";
            testMeeting.AddAgenda(oldMeetingAgenda);

            // Act
            testMeeting.UpdateAgenda(newMeetingAgenda);

            // Assert
            Assert.Equal(newMeetingAgenda, testMeeting.ViewAgenda());
        }

        [Fact]
        public void Meeting_OnModifyingAgenda_ThrowsExceptionIfNoAgendaIsDefined()
        {
            // Arrange
            const string meetingAgenda = "Dummy meeting agenda";

            // Act, Assert
            Assert.Throws<AgendaIsNotDefinedException>(() => testMeeting.UpdateAgenda(meetingAgenda));
        }
    }
}