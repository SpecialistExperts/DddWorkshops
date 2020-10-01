using System;

namespace DddWorkshops.Model.Meeting.Exceptions
{
    public class MaterialDoesNotExistException : Exception
    {
        public MaterialDoesNotExistException(string materialName, Meeting meeting)
            : base($"Material with name {materialName} was not attached to the {meeting}!")
        {
            
        }
    }
}