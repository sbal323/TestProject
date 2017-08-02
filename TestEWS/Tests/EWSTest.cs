﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Exchange.WebServices.Data;

namespace TestEWS.Tests
{
    class EWSTest: ITest
    {
        void ITest.Run()
        {
            ExchangeService service;
            service = new ExchangeService(ExchangeVersion.Exchange2013);
            service.Credentials = new WebCredentials("sbalog@lanteria.net", "------");
            service.Url = new Uri("https://outlook.office365.com/EWS/Exchange.asmx");

            
            List<AttendeeInfo> attendees = new List<AttendeeInfo>();

            attendees.Add(new AttendeeInfo()
            {
                SmtpAddress = "sturin@lanteria.net",
                AttendeeType = MeetingAttendeeType.Required
            });

            attendees.Add(new AttendeeInfo()
            {
                SmtpAddress = "meetingroom@lanteria.net",
                AttendeeType = MeetingAttendeeType.Required
            });
            attendees.Add(new AttendeeInfo()
            {
                SmtpAddress = "ivan.deynega@lanteria.net",
                AttendeeType = MeetingAttendeeType.Required
            });
            attendees.Add(new AttendeeInfo()
            {
                SmtpAddress = "sbalog@lanteria.net",
                AttendeeType = MeetingAttendeeType.Required
            });

            // Specify options to request free/busy information and suggested meeting times.
            AvailabilityOptions availabilityOptions = new AvailabilityOptions();
            availabilityOptions.GoodSuggestionThreshold = 49;
            availabilityOptions.MaximumNonWorkHoursSuggestionsPerDay = 0;
            availabilityOptions.MaximumSuggestionsPerDay = 4;
            // Note that 60 minutes is the default value for MeetingDuration, but setting it explicitly for demonstration purposes.
            availabilityOptions.MeetingDuration = 180;
            availabilityOptions.MinimumSuggestionQuality = SuggestionQuality.Good;
            availabilityOptions.DetailedSuggestionsWindow = new TimeWindow(DateTime.Now.AddDays(4), DateTime.Now.AddDays(5));
            availabilityOptions.RequestedFreeBusyView = FreeBusyViewType.Detailed;


            // Return free/busy information and a set of suggested meeting times. 
            // This method results in a GetUserAvailabilityRequest call to EWS.
            GetUserAvailabilityResults results = service.GetUserAvailability(attendees,
                                                                             availabilityOptions.DetailedSuggestionsWindow,
                                                                             AvailabilityData.FreeBusyAndSuggestions,
                                                                             availabilityOptions);

            // Display suggested meeting times. 
            Console.WriteLine("Availability for {0} and {1}", attendees[0].SmtpAddress, attendees[1].SmtpAddress);
            Console.WriteLine();

            foreach (Suggestion suggestion in results.Suggestions)
            {
                Console.WriteLine("Suggested date: {0}\n", suggestion.Date.ToShortDateString());
                Console.WriteLine("Suggested meeting times:\n");
                foreach (TimeSuggestion timeSuggestion in suggestion.TimeSuggestions)
                {
                    Console.WriteLine("\t{0} - {1}\n",
                                      timeSuggestion.MeetingTime.ToShortTimeString(),
                                      timeSuggestion.MeetingTime.Add(TimeSpan.FromMinutes(availabilityOptions.MeetingDuration)).ToShortTimeString());



                }
            }

            int i = 0;

            //
            // Display free/busy times.
            //
            foreach (AttendeeAvailability availability in results.AttendeesAvailability)
            {
                Console.WriteLine("Availability information for {0}:\n", attendees[i].SmtpAddress);

                foreach (CalendarEvent calEvent in availability.CalendarEvents)
                {
                    Console.WriteLine("\tBusy from {0} to {1} {2}\n", calEvent.StartTime.ToString(), calEvent.EndTime.ToString(), calEvent.Details == null ? "" : string.Format("({0})", calEvent.Details.Subject));
                }

                i++;
            }
        }


        string ITest.Title
        {
            get { return "Exchange Web Services Test"; }
        }
    }
}
