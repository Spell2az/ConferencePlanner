﻿using System.Collections;
using System.Collections.Generic;

namespace BackEnd.Data
{
    public class Attendee : ConferenceDTO.Attendee
    {
        public virtual  ICollection<ConferenceAttendee> ConferenceAttendees { get; set; }
        public virtual  ICollection<SessionAttendee> SessionAttendees { get; set; }
    }
}