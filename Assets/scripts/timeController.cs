using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class timeController : MonoBehaviour
{
	#region variables
	public float normalTimeScale;
	public float menuTimeScale;
	public float battleTimeScale;
	public float pausedTimeScale = 0.000000001f;
	public float currentTimeScale;
	public int year;
	public Seasons season;
	public int[] numDaysInSeason;
	public int currentDayInSeason;
	public int month;
	public int maxMonths = 8;
	public string[] months;
	public int[] numDaysInMonth;
	public int currentDayInMonth;
//	Text monthText;
//	public int week;
//	Text weekText;
//	int maxWeeks = 5;
//	public int day;
//	public string[] days;
//	Text dayText;
//	int maxDays = 5;
	Text timeText;
	public int hour;
	public int maxHours = 23;
	public int minute;
	public int maxMinutes = 59;
	float timer;

	public calendarController calendarController;
	#endregion

	// Use this for initialization
	void Start()
	{
		currentTimeScale = normalTimeScale;
//		monthText = GameObject.Find("MonthText").GetComponent<Text>();
//		weekText = GameObject.Find("WeekText").GetComponent<Text>();
//		dayText = GameObject.Find("DayText").GetComponent<Text>();
		timeText = GameObject.Find("TimeText").GetComponent<Text>();

//		calendarController = GameObject.Find("calendar").GetComponent<calendarController>();

		timeText.text = string.Format ("{0:00}:{1:00}", hour, minute);
//		timeText.text = "";
//		dayText.text = days[day] + " (" + (currentDayInMonth + 1) + ")";
//		dayText.text = "";
//		weekText.text = "week: " + (week + 1);
//		monthText.text = months[month];
	}
	
	// Update is called once per frame
	void Update()
	{
		timer += Time.smoothDeltaTime * currentTimeScale;

		if (timer >= 99)
		{
//			minute++;
			increaseMinute();
			timer = 0;
			timeText.text = string.Format("{0:00}:{1:00}", hour, minute);
//			timeText.text = "";
		}

//		if (minute > maxMinutes)
//		{
//			hour++;
//			minute = 0;
//			timeText.text = string.Format("{0:00}:{1:00}", hour, minute);
//			timeText.text = "";
//		}
//
//		if (hour > maxHours)
//		{
////			day++;
////
////			if (day > maxDays)
////			{
////				week++;
////				day = 0;
////				dayText.text = days[day] + " (" + (currentDayInMonth + 1) + ")";
////				weekText.text = "week: " + (week + 1);
////			}
////
//			currentDayInMonth++;
//			hour = 0;
//			timeText.text = string.Format("{0:00}:{1:00}", hour, minute);
//			dayText.text = days[day] + " (" + (currentDayInMonth + 1) + ")";
//			timeText.text = "";
//			dayText.text = "";
//			if (calendarController.gameObject.activeInHierarchy)
//			{
//				calendarController.changeDate();
//			}
//		}
//
//		if (currentDayInMonth >= numDaysInMonth[month])
//		{
//			currentDayInMonth = 0;
//			month++;
//			if (month > maxMonths)
//			{
//				month = 0;
//				year++;
//			}
//
//			if (calendarController.gameObject.activeInHierarchy)
//			{
//				calendarController.changeDate();
//			}
////			dayText.text = days[day] + " (" + (currentDayInMonth + 1) + ")";
////			dayText.text = "";
////			monthText.text = months[month];
//		}

//		if (week > maxWeeks)
//		{
//			week = 0;
//			weekText.text = "week: " + (week + 1);
//		}
	}

	void increaseMinute()
	{
		minute++;
		if (minute > maxMinutes)
		{
			minute = 0;
			increaseHour();
		}
	}

	void increaseHour()
	{
		hour++;
		if (hour > maxHours)
		{
			hour = 0;
			increaseDay();
		}
	}

	void increaseDay()
	{
		currentDayInMonth++;
		if (currentDayInMonth >= numDaysInMonth[month])
		{
			currentDayInMonth = 0;
			increaseMonth();
		}

		currentDayInSeason++;
		if (currentDayInSeason >= numDaysInSeason[(int)season])
		{
			currentDayInSeason = 0;
			increaseSeason();
		}

		if (calendarController.gameObject.activeInHierarchy)
		{
			calendarController.changeDate();
		}
	}

	void increaseMonth()
	{
		month++;
		if (month > maxMonths)
		{
			month = 0;
			increaseYear();
		}
	}

	void increaseSeason()
	{
		season++;
		if (season > Seasons.Autumn)
		{
			season = Seasons.Winter;
		}
	}

	void increaseYear()
	{
		year++;
	}
}