using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestSuite.Tests
{
    class ScheduleSerializationTest : ITest
    {
        string ITest.Title
        {
            get { return "Schedule Serialization Test"; }
        }

        void ITest.Run()
        {
            Schedule sch = new Schedule();
            sch.CurrentSchedule.Add(new ScheduleItem() { Date = DateTime.Today.AddDays(5), StartTime = new TimeSpan(11, 30, 00), EndTime = new TimeSpan(13, 45, 00) });
            sch.CurrentSchedule.Add(new ScheduleItem() { Date = DateTime.Today.AddDays(7), StartTime = new TimeSpan(11, 00, 00), EndTime = new TimeSpan(13, 00, 00) });
            sch.CurrentSchedule.Add(new ScheduleItem() { Date = DateTime.Today.AddDays(9), StartTime = new TimeSpan(15, 00, 00), EndTime = new TimeSpan(17, 45, 00) });
            sch.PreviousSchedule.Add(new ScheduleItem() { Date = DateTime.Today.AddDays(5), StartTime = new TimeSpan(11, 30, 00), EndTime = new TimeSpan(13, 45, 00) });
            sch.PreviousSchedule.Add(new ScheduleItem() { Date = DateTime.Today.AddDays(9), StartTime = new TimeSpan(15, 00, 00), EndTime = new TimeSpan(17, 45, 00) });

            string json = sch.ToString();
            Console.WriteLine("1 - " + json);
            Console.WriteLine("1.5 - Solid - " + sch.IsCurrentScheduleSolid(DateTime.Today.AddDays(5), DateTime.Today.AddDays(9)));
            sch.FillSchedule(new DateTime(2017, 11, 11, 11, 30, 0), new DateTime(2017, 11, 18, 13, 30, 0));
            sch.FillSchedule(new DateTime(2017, 11, 11, 11, 30, 0), new DateTime(2017, 11, 18, 13, 30, 0));
            Console.WriteLine("2 - " + sch.ToString());
            Console.WriteLine("2.5 - Solid - " + sch.IsCurrentScheduleSolid(new DateTime(2017, 11, 11, 11, 30, 0), new DateTime(2017, 11, 18, 13, 30, 0)));
            Console.WriteLine("2.75 - Equals - " + sch.CurrentAndPreviousEquals());
            sch.DeserializeFromJson(null);
            Console.WriteLine("3 - " + sch.ToString());
            sch.DeserializeFromJson(json);
            Console.WriteLine("4 - " + sch.ToString());
            foreach(ScheduleItem itm in sch.CurrentSchedule)
            {
                Console.WriteLine(string.Format("{0} - {1}", itm.GetStartDate(), itm.GetEndDate()));
                Console.WriteLine(string.Format("var dts = new Date({0});alert(dts); var dte = new Date({1}); alert(dte);", itm.GetStartDateJS(), itm.GetEndDateJS()));
            }
            Console.WriteLine(System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortDatePattern);
            Console.WriteLine(System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.DateSeparator);
        }

        public class ScheduleItem
        {
            public DateTime Date { get; set; }
            public TimeSpan StartTime { get; set; }
            public TimeSpan EndTime { get; set; }
            public DateTime GetStartDate()
            {
                return Date.Add(StartTime);
            }
            public double GetStartDateJS()
            {
                return GetJSDate(GetStartDate());
            }
            public double GetEndDateJS()
            {
                return GetJSDate(GetEndDate());
            }
            public DateTime GetEndDate()
            {
                return Date.Add(EndTime);
            }
            public override bool Equals(object obj)
            {
                ScheduleItem item = (ScheduleItem)obj;
                return (this.Date == item.Date && this.StartTime == item.StartTime && this.EndTime == item.EndTime);
            }
            public override int GetHashCode()
            {
                return this.Date.GetHashCode() ^ this.StartTime.GetHashCode() ^ this.EndTime.GetHashCode();
            }
            private double GetJSDate(DateTime date)
            {
                return date.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            }
        }
        public class Schedule
        {
            private List<ScheduleItem> _CurrentSchedule;
            public List<ScheduleItem> CurrentSchedule {
                get
                {
                    return _CurrentSchedule;
                }
                set
                {
                    PreviousSchedule = _CurrentSchedule;
                    _CurrentSchedule = value;
                }
            }
            public List<ScheduleItem> PreviousSchedule { get; set; }
            public Schedule()
            {
                _CurrentSchedule = new List<ScheduleItem>();
                PreviousSchedule = new List<ScheduleItem>();
            }
            private List<ScheduleItem> FillPeriod(DateTime startDate, DateTime endDate)
            {
                return Enumerable.Range(0, 1 + endDate.Subtract(startDate).Days)
                        .Select(offset => new ScheduleItem() { Date = startDate.AddDays(offset), StartTime = new TimeSpan(startDate.Hour, startDate.Minute, 0), EndTime = new TimeSpan(endDate.Hour, endDate.Minute, 0) }).ToList();
            }
            
            public void FillSchedule(DateTime startDate, DateTime endDate)
            {
                CurrentSchedule = FillPeriod(startDate, endDate);
            }
            public bool IsCurrentScheduleSolid(DateTime startDate, DateTime endDate)
            {
                return ColleactionsEqual(FillPeriod(startDate, endDate), CurrentSchedule);
            }
            public bool CurrentAndPreviousEquals()
            {
                return ColleactionsEqual(PreviousSchedule, CurrentSchedule);
            }
            private bool ColleactionsEqual(List<ScheduleItem> right, List<ScheduleItem> left)
            {
                if(right.Count != left.Count)
                {
                    return false;
                }
                for(int i = 0; i< left.Count -1; i++)
                {
                    if (!left[i].Equals(right[i]))
                    {
                        return false;
                    }
                }
                return true;
            }
            public string SerializeToJson()
            {
                return JsonConvert.SerializeObject(this);
            }
            public override string ToString()
            {
                return SerializeToJson();
            }
            public void DeserializeFromJson(string json)
            {
                Schedule newSchedule = null;
                if(null != json)
                {
                    newSchedule = JsonConvert.DeserializeObject<Schedule>(json); 
                }
                if(null == newSchedule)
                {
                    CurrentSchedule = new List<ScheduleItem>();
                    PreviousSchedule = new List<ScheduleItem>();
                }
                else
                {
                    CurrentSchedule = newSchedule.CurrentSchedule;
                    PreviousSchedule = newSchedule.PreviousSchedule;
                }
            }
        }
    }
}
