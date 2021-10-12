using System;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace LEotA.Engine.Web.ViewModels.EventParticipantViewModels
{
    public class EventParticipantCreateViewModel : IViewModel
    {
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
    }
}