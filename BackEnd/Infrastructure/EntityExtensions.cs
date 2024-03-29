﻿using System.Linq;
using BackEnd.Data;

namespace BackEnd.Infrastructure
{
    public static class EntityExtensions
    {
        public static ConferenceDTO.SpeakerResponse MapSpeakerResponse(this Speaker speaker) =>
            new ConferenceDTO.SpeakerResponse
            {
                ID = speaker.ID,
                Name = speaker.Name,
                Bio = speaker.Bio,
                WebSite = speaker.WebSite,
                Sessions = speaker.SessionSpeakers?
                    .Select(ss =>
                        new ConferenceDTO.Session
                        {
                            ID = ss.SessionId,
                            Title = ss.Session.Title
                        })
                    .ToList()
            };

        public static ConferenceDTO.SessionResponse MapSessionResponse(this Session session) =>
            new ConferenceDTO.SessionResponse
            {
                ID = session.ID,
                Title = session.Title,
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                Tags = session.SessionTags?
                    .Select(st => new ConferenceDTO.Tag
                    {
                        ID = st.TagId,
                        Name = st.Tag.Name
                    })
                    .ToList(),
                Speakers = session.SessionSpeakers?
                    .Select(ss => new ConferenceDTO.Speaker
                    {
                        ID = ss.SpeakerId,
                        Name = ss.Speaker.Name
                    })
                    .ToList(),
                TrackId = session.TrackId,
                Track = new ConferenceDTO.Track
                {
                    TrackId = session?.TrackId ?? 0,
                    Name = session.Track?.Name
                },
                ConferenceID = session.ConferenceID,
                Abstract = session.Abstract
            };

        public static ConferenceDTO.AttendeeResponse MapAttendeeResponse(this Attendee attendee) =>
            new ConferenceDTO.AttendeeResponse
            {
                ID = attendee.ID,
                FirstName = attendee.FirstName,
                LastName = attendee.LastName,
                UserName = attendee.UserName,
                Sessions = attendee.SessionAttendees?
                    .Select(s =>
                        new ConferenceDTO.Session
                        {
                            ID = s.SessionId,
                            Title = s.Session.Title,
                            StartTime = s.Session.StartTime,
                            EndTime = s.Session.EndTime
                        })
                    .ToList(),
                Conferences = attendee.ConferenceAttendees?
                    .Select(ca =>
                        new ConferenceDTO.Conference
                        {
                            ID = ca.ConferenceId,
                            Name = ca.Conference.Name
                        })
                    .ToList(),
            };
    }
}