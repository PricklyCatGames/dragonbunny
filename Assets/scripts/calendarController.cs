using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class calendarController : MonoBehaviour
{
	#region variables
	public GameObject[] monthLayouts;
	public Text monthYearText;
	public Color currentDayColor = Color.blue;
	public Color holidayColor = Color.green;
	public Color eventColor = Color.yellow;
	Color originalDayColor = new Color32(50, 50, 50, 255);
	public Button decreaseButton;
	public GameObject notesPanel;
	public Text[] notesText;
	public GameObject reminderSelector;
	dayController dayController;
	public gameController gameController;
	timeController timeController;
	public int selectedYear;
	public int selectedMonth;
	public int currentYear;
	public int currentMonth;
	public int currentDay;
	RectTransform today;
	Text todayText;
	#endregion

	// Use this for initialization
	void Start()
	{
		timeController = GameObject.Find("TimeController").GetComponent<timeController>();

		reset();
//		currentDay = timeController.day + 1;
//		currentMonth = timeController.month;
//		currentYear = timeController.year;
//		selectedYear = currentYear;
//
//		if (currentYear % 2 == 0)
//		{
//			selectedMonth = currentMonth + 9;
//		}
//		else
//		{
//			selectedMonth = currentMonth;
//		}
//
//		monthLayouts[selectedMonth].SetActive(true);
//		monthYearText.text = timeController.months[currentMonth] + " " + currentYear;
//
//		if (selectedYear == 1 && selectedMonth == 0)
//		{
//			decreaseButton.interactable = false;
//		}
//
//		if (todayText)
//		{
//			todayText.color = new Color32(50, 50, 50, 255);
//		}
//		today = monthLayouts[selectedMonth].transform.Find(currentDay.ToString()) as RectTransform;
//		todayText = today.GetComponentInChildren<Text>();
//		todayText.color = currentDayColor;
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	public void increaseMonth()
	{
		monthLayouts[selectedMonth].SetActive(false);

		if (selectedMonth < monthLayouts.Length - 1)
		{
			selectedMonth++;
			if (selectedMonth == 9)
			{
				selectedYear++;
			}
		}
		else
		{
			selectedMonth = 0;
			selectedYear++;
		}

		if (selectedYear == currentYear)
		{
			todayText.color = currentDayColor;
		}
		else
		{
			todayText.color = originalDayColor;
		}

		monthLayouts[selectedMonth].SetActive(true);

		if (selectedMonth < 9)
		{
			monthYearText.text = timeController.months[selectedMonth] + " " + selectedYear;
		}
		else
		{
			monthYearText.text = timeController.months[selectedMonth - 9] + " " + selectedYear;
		}

		if (!decreaseButton.interactable)
		{
			decreaseButton.interactable = true;
		}
	}

	public void decreaseMonth()
	{
		monthLayouts[selectedMonth].SetActive(false);

		if (selectedMonth > 0)
		{
			selectedMonth--;
			if (selectedMonth == 8)
			{
				selectedYear--;
			}
		}
		else
		{
			selectedMonth = monthLayouts.Length - 1;
			selectedYear--;
		}

		if (selectedYear == currentYear)
		{
			todayText.color = currentDayColor;
		}
		else
		{
			todayText.color = originalDayColor;
		}

		monthLayouts[selectedMonth].SetActive(true);

		if (selectedMonth < 9)
		{
			monthYearText.text = timeController.months[selectedMonth] + " " + selectedYear;
		}
		else
		{
			monthYearText.text = timeController.months[selectedMonth - 9] + " " + selectedYear;
		}

		if (selectedYear == 1 && selectedMonth == 0)
		{
			decreaseButton.interactable = false;
		}
	}

	public void reset()
	{
		monthLayouts[selectedMonth].SetActive(false);
		notesPanel.SetActive(false);
		currentDay = timeController.currentDayInMonth + 1;
		currentMonth = timeController.month;
		currentYear = timeController.year;
		selectedYear = currentYear;

		if (currentYear % 2 == 0)
		{
			selectedMonth = currentMonth + 9;
		}
		else
		{
			selectedMonth = currentMonth;
		}

		monthLayouts[selectedMonth].SetActive(true);
		monthYearText.text = timeController.months[currentMonth] + " " + currentYear;

		if (selectedYear == 1 && selectedMonth == 0)
		{
			decreaseButton.interactable = false;
		}

		if (todayText)
		{
			todayText.color = originalDayColor;
		}
		today = monthLayouts[selectedMonth].transform.Find(currentDay.ToString()) as RectTransform;
		todayText = today.GetComponentInChildren<Text>();
		originalDayColor = todayText.color;
		todayText.color = currentDayColor;
	}

	public void changeDate()
	{
		currentDay = timeController.currentDayInMonth + 1;
		currentMonth = timeController.month;
		currentYear = timeController.year;
		todayText.color = originalDayColor;

		if (currentYear % 2 == 0)
		{
//			selectedMonth = currentMonth + 9;
			today = monthLayouts[currentMonth + 9].transform.Find(currentDay.ToString()) as RectTransform;
		}
		else
		{
//			selectedMonth = currentMonth;
			today = monthLayouts[currentMonth].transform.Find(currentDay.ToString()) as RectTransform;
		}
//		Debug.Log("currentDay = " + currentDay);
		todayText = today.GetComponentInChildren<Text>();
		todayText.color = currentDayColor;
	}

	public void openDate(int day)
	{
		notesPanel.SetActive(true);
		gameController.menuDepth = 3;
		
		notesText[0].text = "No notes.";
		notesText[1].text = "";

		if (isHoliday(day))
		{
			notesText[0].text = dayController.dayText;
//			notesText[1].text = "";
		}
//		else
//		{
//			notesText[0].text = "No notes.";
//			notesText[1].text = "";
//		}
		if (hasReminder(day))
		{
			if (isHoliday(day))
			{
				notesText[1].text = dayController.reminderText[dayController.reminderNumber];
			}
			else
			{
				notesText[0].text = dayController.reminderText[dayController.reminderNumber];
//				notesText[1].text = "";
			}
		}
	}

	public void addNote()
	{
//		gameController.menuDepth = 4;
	}

	public void selectReminder(int reminderNumber)
	{
		dayController.hasReminder = true;
		dayController.reminderNumber = reminderNumber;
	}

	public void cancelReminder()
	{
//		gameController.menuDepth = 3;
	}

	public void removeNote()
	{
		dayController.hasReminder = false;
		dayController.reminderNumber = 0;
	}

	public void closeNotes()
	{
		notesPanel.SetActive(false);
		gameController.menuDepth = 2;
	}

	public void cancelNote()
	{
//		gameController.menuDepth = 3;
	}

	bool isHoliday(int day)
	{
		bool holiday = false;
		Transform selectedDay = monthLayouts[selectedMonth].transform.Find(day.ToString());
		if (selectedDay.GetComponent<dayController>())
		{
			dayController = selectedDay.GetComponent<dayController>();
			holiday = dayController.isHoliday;
		}
		return holiday;
	}

	bool hasReminder(int day)
	{
		bool reminder = false;
		Transform selectedDay = monthLayouts[selectedMonth].transform.Find(day.ToString());
		if (selectedDay.GetComponent<dayController>())
		{
			dayController = selectedDay.GetComponent<dayController>();
			if (dayController.year == selectedYear)
			{
				reminder = dayController.hasReminder;
			}
		}
		return reminder;
	}
}