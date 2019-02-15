using PC.PersianCalendar.CustomControls;
using PC.PersianCalendar.HelperClass;
using PC.PersianCalendar.HelperClass.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using SG = System.Globalization;

namespace PC.PersianCalendar
{
    public class PCCalendar : StackLayout
    {
        #region Variables
        private static List<DaysOfWeekEnum> daysOfWeekList;
        private static int currentYear;
        private static int currentMonth;
        private static int currentDay;
        private static int startDayOfMonth;
        SG.PersianCalendar persianCalendar;

        private Button BtnNextMonth;
        private Button BtnPrevMonth;
        private PCLabel YearTitle;
        private PCLabel MonthTitle;
        private Grid PCDateGrid;
        private List<Exception> exceptions;
        #endregion

        #region Properties
        public Color BackgroundColor { get; set; }
        public Color HeaderBackgroundColor { get; set; }
        public Color HeaderTextColor { get; set; }
        #endregion

        #region Constructors
        public PCCalendar()
        {
            InitializeControls();
            InitializePersianDate();
            CalculatePersianDate();
        }
        #endregion

        #region Helper Methods
        private void InitializeControls()
        {
            exceptions = new List<Exception>();

            #region Header Grid
            var headerGird = new Grid()
            {
                ColumnDefinitions = new ColumnDefinitionCollection()
                {
                    new ColumnDefinition(){ Width = GridLength.Star },
                    new ColumnDefinition(){ Width = new GridLength(3, GridUnitType.Star)},
                    new ColumnDefinition(){ Width = GridLength.Star },
                },
                RowDefinitions = new RowDefinitionCollection()
                {
                    new RowDefinition(){ Height = GridLength.Star }
                },
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                BackgroundColor = HeaderBackgroundColor
            };

            BtnNextMonth = new Button()
            {
                Text = "⏪",
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            BtnNextMonth.Clicked += BtnNextMonth_Clicked;

            BtnPrevMonth = new Button()
            {
                Text = "⏩",
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            BtnPrevMonth.Clicked += BtnPrevMonth_Clicked;

            YearTitle = new PCLabel()
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                TextColor = HeaderTextColor
            };

            MonthTitle = new PCLabel()
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                TextColor = HeaderTextColor
            };

            headerGird.Children.Add(BtnNextMonth, 0, 0);

            headerGird.Children.Add(new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    YearTitle,
                    MonthTitle
                }
            }, 1, 0);

            headerGird.Children.Add(BtnPrevMonth, 2, 0);

            #endregion

            #region Main Grid
            PCDateGrid = new Grid()
            {
                FlowDirection = FlowDirection.RightToLeft,
                ColumnDefinitions = new ColumnDefinitionCollection()
                {
                    new ColumnDefinition(){ Width = GridLength.Star },
                    new ColumnDefinition(){ Width = GridLength.Star },
                    new ColumnDefinition(){ Width = GridLength.Star },
                    new ColumnDefinition(){ Width = GridLength.Star },
                    new ColumnDefinition(){ Width = GridLength.Star },
                    new ColumnDefinition(){ Width = GridLength.Star },
                    new ColumnDefinition(){ Width = GridLength.Star }
                },
                RowDefinitions = new RowDefinitionCollection()
                {
                    new RowDefinition(){ Height = GridLength.Star },
                    new RowDefinition(){ Height = new GridLength(2, GridUnitType.Star) },
                    new RowDefinition(){ Height = new GridLength(2, GridUnitType.Star) },
                    new RowDefinition(){ Height = new GridLength(2, GridUnitType.Star) },
                    new RowDefinition(){ Height = new GridLength(2, GridUnitType.Star) },
                    new RowDefinition(){ Height = new GridLength(2, GridUnitType.Star) },
                    new RowDefinition(){ Height = new GridLength(2, GridUnitType.Star) }
                },
                BackgroundColor = BackgroundColor
            };
            #endregion

            this.BackgroundColor = BackgroundColor;
            this.Children.Add(headerGird);
            this.Children.Add(PCDateGrid);
            this.Padding = 15;
            this.HorizontalOptions = LayoutOptions.StartAndExpand;
            this.VerticalOptions = LayoutOptions.StartAndExpand;
            this.Orientation = StackOrientation.Vertical;
        }
        private void InitializePersianDate()
        {
            try
            {
                persianCalendar = new SG.PersianCalendar();
                daysOfWeekList = Enum.GetValues(typeof(DaysOfWeekEnum)).Cast<DaysOfWeekEnum>().ToList();
                currentYear = persianCalendar.GetYear(DateTime.Now);
                currentMonth = persianCalendar.GetMonth(DateTime.Now);
                currentDay = persianCalendar.GetDayOfMonth(DateTime.Now);
                CalculateFirstDayOfMonth();
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
            }
        }
        private int CalculateFirstDayOfMonth()
        {
            try
            {
                var dtpc = persianCalendar.ToDateTime(currentYear, currentMonth, 1, 0, 0, 0, 0);
                startDayOfMonth = ((int)dtpc.DayOfWeek + 1) % 7;
                return startDayOfMonth;
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
                return 0;
            }
        }
        private StackLayout GenerateDayView(string day, bool isHeader = false, bool isHolyDay = false)
        {
            try
            {
                var stackLayout = new StackLayout();
                stackLayout.VerticalOptions = LayoutOptions.FillAndExpand;
                stackLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
                stackLayout.BackgroundColor = isHeader ? Color.FromHex("555555") : Color.FromHex("EEEEEE");

                string tColor = "333333";
                if (isHolyDay)
                    tColor = "EA4335";
                if (isHeader)
                    tColor = "FFFFFF";

                var label = new PCLabel();
                label.Text = day;
                label.VerticalOptions = LayoutOptions.CenterAndExpand;
                label.HorizontalOptions = LayoutOptions.CenterAndExpand;
                label.TextColor = Color.FromHex(tColor);
                stackLayout.Children.Add(label);
                return stackLayout;
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
                return null;
            }
        }
        private void SetCalendarTitleViewDate()
        {
            try
            {
                MonthEnum month = (MonthEnum)currentMonth;
                YearTitle.Text = currentYear.ToString();
                MonthTitle.Text = MonthUtility.GetMonthName(month);
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
            }
        }
        #endregion

        #region Event Methods
        private void BtnNextMonth_Clicked(object sender, EventArgs e)
        {
            try
            {
                bool isLastMonth = false;
                if (currentMonth == 12)
                    isLastMonth = true;
                currentMonth = isLastMonth ? 1 : currentMonth + 1;
                currentYear = isLastMonth ? currentYear + 1 : currentYear;
                currentDay = 1;
                CalculateFirstDayOfMonth();
                CalculatePersianDate();
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
            }
        }
        private void BtnPrevMonth_Clicked(object sender, EventArgs e)
        {
            try
            {
                bool isFirstMonth = false;
                if (currentMonth == 1)
                    isFirstMonth = true;
                currentMonth = isFirstMonth ? 12 : currentMonth - 1;
                currentYear = isFirstMonth ? currentYear - 1 : currentYear;
                currentDay = 1;
                CalculateFirstDayOfMonth();
                CalculatePersianDate();
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
            }
        }
        #endregion

        #region Main Methods
        private void CalculatePersianDate()
        {
            try
            {
                SetCalendarTitleViewDate();
                RenderView();
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
            }
        }
        private void RenderView()
        {
            try
            {
                PCDateGrid.Children.Clear();
                RenderHeaderView();
                int d = 0;
                for (int i = 0; i <= 6; i++)
                {
                    for (int j = 0; j <= 6; j++)
                    {
                        if ((j >= startDayOfMonth && i == 0) || i > 0)
                        {
                            d++;
                            PCDateGrid.Children.Add(GenerateDayView(d.ToString(), false, (j == 6)), j, (i + 1));
                        }
                        else
                        {
                            PCDateGrid.Children.Add(GenerateDayView(""), j, (i + 1));
                        }
                        if (d + 1 > MonthUtility.GetLastDayInMonth((MonthEnum)currentMonth, currentYear))
                            break;
                    }
                    if (d + 1 > MonthUtility.GetLastDayInMonth((MonthEnum)currentMonth, currentYear))
                        break;
                }
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
            }
        }

        private void RenderHeaderView()
        {
            try
            {
                for (int i = 0; i <= 6; i++)
                {
                    PCDateGrid.Children.Add(GenerateDayView(WeeksUtility.GetDayTitle((DaysOfWeekEnum)i), true), i, 0);
                }
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
            }
        }
        #endregion
    }
}