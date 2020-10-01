using System;
using System.Collections.Generic;
using DddWorkshops.Common.ModelFramework;
using DddWorkshops.Model.Meeting.Entities.Material;
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

        internal Term? MeetingTerm { get; private set; }

        internal Title Title { get; }

        public void ScheduleOn(DateTime startDate, DateTime endDate) => 
            MeetingTerm = Term.Set(startDate, endDate);

        internal IList<Material> AttachedMaterials { get; private set; }

        public void AttachMaterial(string materialName, string url, MaterialType materialType)
        {
            AttachedMaterials.Add(Material.New(materialName, url, materialType));
        }
    }
}