using System;
using System.Collections.Generic;
using System.Linq;
using DddWorkshops.Common.Guard;
using DddWorkshops.Common.ModelFramework;
using DddWorkshops.Model.Meeting.Entities.Agenda;
using DddWorkshops.Model.Meeting.Entities.Material;
using DddWorkshops.Model.Meeting.Exceptions;
using DddWorkshops.Model.Meeting.ValueObjects;

namespace DddWorkshops.Model.Meeting
{
    public class Meeting : Entity, IAggregateRoot
    {
        private Meeting(string title)
        {
            Title = Title.Give(title);
            AttachedMaterials = new List<Material>();
        }

        public static Meeting Create(string title) => new Meeting(title);

        public DateTime? StartDate => MeetingTerm?.StartDate;

        public DateTime? EndDate => MeetingTerm?.EndDate;

        public string MeetingTitle => Title.ToString();

        internal Title Title { get; }

        internal Term? MeetingTerm { get; private set; }

        internal Agenda? Agenda { get; private set; }

        public void ScheduleOn(DateTime startDate, DateTime endDate) => 
            MeetingTerm = Term.Set(startDate, endDate);

        internal IList<Material> AttachedMaterials { get; private set; }

        public void AttachMaterial(string materialName, string url, MaterialType materialType)
        {
            AttachedMaterials.Add(Material.New(materialName, url, materialType));
        }

        public void AddAgenda(string meetingAgenda)
        {
            Guard.With<AgendaIsAlreadyDefinedException>().Against(AgendaIsDefined(), this);

            Agenda = Agenda.Create(meetingAgenda);
        }

        public string? ViewAgenda() => Agenda?.ToString();

        public void UpdateAgenda(string newMeetingAgenda)
        {
            Guard.With<AgendaNotDefinedException>().Against(!AgendaIsDefined(), this);

            Agenda!.Update(newMeetingAgenda);
        }

        public void RemoveMaterial(string materialName)
        {           
            Guard.With<MaterialDoesNotExistException>().Against(!MaterialExists(), materialName, this);
            var materialToRemove = AttachedMaterials.First(m => m.Name == materialName);
            AttachedMaterials.Remove(materialToRemove);

            bool MaterialExists() => AttachedMaterials.Any(m => m.Name == materialName);
        }

        private bool AgendaIsDefined() => Agenda != null;
    }
}