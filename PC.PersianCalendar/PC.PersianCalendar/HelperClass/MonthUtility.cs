using PC.PersianCalendar.HelperClass.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PC.PersianCalendar.HelperClass
{
    public class MonthUtility
    {
        public static string GetMonthName(MonthEnum month)
        {
            switch (month)
            {
                case MonthEnum.FARVARDIN:
                    return "فروردین";
                case MonthEnum.ORDIBEHESHT:
                    return "اردیبهشت";
                case MonthEnum.KHORDAD:
                    return "خرداد";
                case MonthEnum.TIR:
                    return "تیر";
                case MonthEnum.MORDAD:
                    return "مرداد";
                case MonthEnum.SHAHRIVAR:
                    return "شهریور";
                case MonthEnum.MEHR:
                    return "مهر";
                case MonthEnum.ABAN:
                    return "آبان";
                case MonthEnum.AZAR:
                    return "آذر";
                case MonthEnum.DEY:
                    return "دی";
                case MonthEnum.BAHMAN:
                    return "بهمن";
                case MonthEnum.ESFAND:
                    return "اسفند";
                default:
                    return "";
            }
        }

        public static int GetLastDayInMonth(MonthEnum month, int persianYear)
        {
            try
            {
                switch (month)
                {
                    case MonthEnum.FARVARDIN:
                        return 31;
                    case MonthEnum.ORDIBEHESHT:
                        return 31;
                    case MonthEnum.KHORDAD:
                        return 31;
                    case MonthEnum.TIR:
                        return 31;
                    case MonthEnum.MORDAD:
                        return 31;
                    case MonthEnum.SHAHRIVAR:
                        return 31;
                    case MonthEnum.MEHR:
                        return 30;
                    case MonthEnum.ABAN:
                        return 30;
                    case MonthEnum.AZAR:
                        return 30;
                    case MonthEnum.DEY:
                        return 30;
                    case MonthEnum.BAHMAN:
                        return 30;
                    case MonthEnum.ESFAND:
                        System.Globalization.PersianCalendar persianCalendar = new System.Globalization.PersianCalendar();
                        DateTime dtpc = persianCalendar.ToDateTime(persianYear, 1, 1, 0, 0, 0, 0);
                        bool isLeap = DateTime.IsLeapYear(dtpc.Year);
                        int d = isLeap ? 30 : 29;
                        return d;
                    default:
                        return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
