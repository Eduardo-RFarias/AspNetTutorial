using System.Runtime.Serialization;

namespace GarageRelation.Exceptions
{
    [Serializable]
    internal class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() : base("The entity was not found in the Database.")
        {
        }

        public EntityNotFoundException(string? message) : base(message)
        {
        }

        public EntityNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}