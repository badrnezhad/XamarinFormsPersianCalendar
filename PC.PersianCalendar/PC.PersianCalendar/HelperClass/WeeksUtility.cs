using PC.PersianCalendar.HelperClass.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PC.PersianCalendar.HelperClass
{
    public class WeeksUtility
    {
        public static string GetDayTitle(DaysOfWeekEnum day)
        {
            switch (day)
            {
                case DaysOfWeekEnum.SHANBE:
                    return "ش";
                case DaysOfWeekEnum.YEKSHANBE:
                    return "ی";
                case DaysOfWeekEnum.DOSHANBE:
                    return "د";
                case DaysOfWeekEnum.SESHANBE:
                    return "س";
                case DaysOfWeekEnum.CHAHARSHANBE:
                    return "چ";
                case DaysOfWeekEnum.PANJSHANBE:
                    return "پ";
                case DaysOfWeekEnum.JOME:
                    return "ج";
                default:
                    return "";
            }
        }
    }
}
