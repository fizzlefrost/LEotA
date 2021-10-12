using System;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace LEotA.Engine.Web.ViewModels.EventParticipantViewModels
{
    public class EventParticipantUpdateViewModel : ViewModelBase
    {
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
    }
}