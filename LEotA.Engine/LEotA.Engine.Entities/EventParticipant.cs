using System;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace LEotA.Engine.Entities
{
    public class EventParticipant : Identity
    {
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
    }
}