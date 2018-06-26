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
            //sch.CurrentSchedule.Add(new ScheduleItem() { Date = DateTime.Today.AddDays(5), StartTime = new TimeSpan(11, 30, 00), EndTime = new TimeSpan(13, 45, 00) });
            //sch.CurrentSchedule.Add(new ScheduleItem() { Date = DateTime.Today.AddDays(7), StartTime = new TimeSpan(11, 00, 00), EndTime = new TimeSpan(13, 00, 00) });
            //sch.CurrentSchedule.Add(new ScheduleItem() { Date = DateTime.Today.AddDays(9), StartTime = new TimeSpan(15, 00, 00), EndTime = new TimeSpan(17, 45, 00) });
            //sch.PreviousSchedule.Add(new ScheduleItem() { Date = DateTime.Today.AddDays(5), StartTime = new TimeSpan(11, 30, 00), EndTime = new TimeSpan(13, 45, 00) });
            //sch.PreviousSchedule.Add(new ScheduleItem() { Date = DateTime.Today.AddDays(9), StartTime = new TimeSpan(15, 00, 00), EndTime = new TimeSpan(17, 45, 00) });

            //string json = sch.ToString();
            //Console.WriteLine("1 - " + json);
            //Console.WriteLine("1.5 - Solid - " + sch.IsCurrentScheduleSolid(DateTime.Today.AddDays(5), DateTime.Today.AddDays(9)));
            //sch.FillSchedule(new DateTime(2017, 11, 11, 11, 30, 0), new DateTime(2017, 11, 18, 13, 30, 0));
            //sch.FillSchedule(new DateTime(2017, 11, 11, 11, 30, 0), new DateTime(2017, 11, 18, 13, 30, 0));
            //Console.WriteLine("2 - " + sch.ToString());
            //Console.WriteLine("2.5 - Solid - " + sch.IsCurrentScheduleSolid(new DateTime(2017, 11, 11, 11, 30, 0), new DateTime(2017, 11, 18, 13, 30, 0)));
            //Console.WriteLine("2.75 - Equals - " + sch.CurrentAndPreviousEquals());
            //sch.DeserializeFromJson(null);
            //Console.WriteLine("3 - " + sch.ToString());
            //sch.DeserializeFromJson(json);
            //Console.WriteLine("4 - " + sch.ToString());
            //sch.CurrentSchedule = sch.DeserializeFromJs("1518559200000:true:9:0:12:0##1518645600000:false:9:35:12:45##1518732000000:true:9:0:12:0##");
            //Console.WriteLine(sch.ToString());
            //sch.DeserializeFromJson(sch.ToString());
            //Console.WriteLine(sch.SerializeToJs());
            //foreach (ScheduleItem itm in sch.DeserializeFromJs("1518300000000:true:10:0:13:0##1518386400000:true:8:0:13:0##1518472800000:false:8:0:13:0##1518559200000:false:8:0:13:0##1518645600000:false:8:0:13:0"))
            //{
            //    Console.WriteLine(string.Format("{0} - {1}", itm.Date, itm.Active));
            //}
            //foreach(ScheduleItem itm in sch.CurrentSchedule)
            //{
            //    Console.WriteLine(string.Format("{0} - {1}", itm.GetStartDate(), itm.GetEndDate()));
            //    Console.WriteLine(string.Format("var dts = new Date({0});alert(dts); var dte = new Date({1}); alert(dte);", itm.GetStartDateJS(), itm.GetEndDateJS()));
            //}
            //Console.WriteLine(System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortDatePattern);
            //Console.WriteLine(System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.DateSeparator);
            //TimeSpan ts = new TimeSpan(10, 5, 0);
            //Console.WriteLine(new DateTime(ts.Ticks).ToString("HH:mm"));
            //string testString = "1small test and the other part which is not so small 2small test and the other part which is not so small 3small test and the other part which is not so small 4small test and the other part which is not so small 5small test and the other part which is not so small ";
            //Console.WriteLine( (testString.Length < 255) ? testString : testString.Substring(0, 255));
            NotificationTask nt = new NotificationTask();
            nt.EntityID = 25;
            nt.ReminderRule = "7;#15";
            nt.NotificationType = "4#;90";
            nt.Recipients = "kva kva";
            nt.Title = "Holiday: Abraham, Aaubucek (03/14/2018 - 03/15/2018)";
            Console.WriteLine(JsonConvert.SerializeObject(nt));
        }
        [JsonObject(MemberSerialization.OptOut)]
        [Serializable()]
        public class NotificationTask: Item
        {
            [JsonProperty("NotificationType")]
            public string NotificationType { get; set; }
            public int EntityID { get; set; }
            public string ReminderRule { get; set; }
            public string Recipients { get; set; }
            public string Content { get; set; }
        }
        [JsonObject(MemberSerialization.OptIn)]
        [Serializable()]
        public class Item
        {
            
            //
            // SP Field Name
            //
            public const string FN_ID = "ID";
            public const string FN_TITLE = "Title";
            public const string FN_VERSION = "_UIVersionString";
            public const string FN_PATH = "FileDirRef";
            public const string FN_MODIFIED = "Modified";
            public const string FN_CREATED = "Created";
            public const string FN_MODIFIED_BY = "Editor";
            public const string FN_ATTACHMENT = "Attachments";
            public const string FN_CREATED_BY = "Author";

            public static List<string> ItemColumns
            {
                get
                {
                    List<string> _knownColumns = new List<string>();
                    _knownColumns.Add(Item.FN_ID);
                    _knownColumns.Add(Item.FN_TITLE);
                    _knownColumns.Add(Item.FN_VERSION);
                    _knownColumns.Add(Item.FN_PATH);
                    _knownColumns.Add(Item.FN_MODIFIED);
                    _knownColumns.Add(Item.FN_CREATED);
                    _knownColumns.Add(Item.FN_CREATED_BY);
                    return _knownColumns;
                }
            }
            public static List<string> KnownColumns
            {
                get { return Item.ItemColumns; }
            }

            //
            // Properties
            //
            [JsonProperty("I1")]
            public int ID { get; set; }
            [JsonProperty("I2")]
            public string Title { get; set; }
            [JsonProperty("I3")]
            public string Version { get; set; }
            [JsonProperty("I4")]
            public System.DateTime Modified { get; set; }
            [JsonProperty("I5")]
            public System.DateTime Created { get; set; }
            [JsonProperty("I6")]
            public string ModifiedBy { get; set; }
            
        }
        public class ScheduleItem
        { 
            public DateTime Date { get; set; }
            public TimeSpan StartTime { get; set; }
            public TimeSpan EndTime { get; set; }
            public bool Active { get; set; }
            //public DateTime GetStartDate()
            //{
            //    return Date.Add(StartTime);
            //}
            //public double GetStartDateJS()
            //{
            //    return GetJSDate(GetStartDate());
            //}
            //public double GetEndDateJS()
            //{
            //    return GetJSDate(GetEndDate());
            //}
            //public DateTime GetEndDate()
            //{
            //    return Date.Add(EndTime);
            //}
            public override bool Equals(object obj)
            {
                ScheduleItem item = (ScheduleItem)obj;
                return (this.Date == item.Date && this.StartTime == item.StartTime && this.EndTime == item.EndTime);
            }
            public override int GetHashCode()
            {
                return this.Date.GetHashCode() ^ this.StartTime.GetHashCode() ^ this.EndTime.GetHashCode();
            }
            public string SerializeToJs()
            {
                return Date.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds + ":" + Active.ToString().ToLower() + ":" + StartTime.Hours + ":" + StartTime.Minutes + ":" + EndTime.Hours + ":" + EndTime.Minutes;
                //return Date.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds + ":" + Active.ToString().ToLower() + ":" + StartTime.Hours + ":" + StartTime.Minutes + ":" + EndTime.Hours + ":" + EndTime.Minutes;
            }
            //private double GetJSDate(DateTime date)
            //{
            //    return date.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            //}
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
                        .Select(offset => new ScheduleItem() { Date = startDate.AddDays(offset), StartTime = new TimeSpan(startDate.Hour, startDate.Minute, 0), EndTime = new TimeSpan(endDate.Hour, endDate.Minute, 0), Active = true }).ToList();
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
            public List<ScheduleItem> DeserializeFromJs(string js)
            {
                List<ScheduleItem> res = new List<ScheduleItem>();
                DateTime baseDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                foreach (string el in js.Split("##".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                {
                    ScheduleItem itm = new ScheduleItem();
                    string[] arr = el.Split(":".ToCharArray());
                    DateTime jsDate = baseDate.AddMilliseconds(double.Parse(arr[0]));
                    itm.Date = jsDate;// new DateTime(jsDate.Year, jsDate.Month, jsDate.Day);
                    itm.Active = bool.Parse(arr[1]);
                    itm.StartTime = new TimeSpan(int.Parse(arr[2]), int.Parse(arr[3]), 0);
                    itm.EndTime = new TimeSpan(int.Parse(arr[4]), int.Parse(arr[5]), 0);
                    res.Add(itm);
                }
                return res;
            }
            public string SerializeToJs()
            {
                //System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ti
                return string.Join("##", CurrentSchedule.Select(x => x.SerializeToJs()).ToArray());
            }
        }
    }
}
