#include <string>
using namespace std;
class Date
{
public:		
	Date(int yearValue, int monthValue, int dayValue);
	Date(int yearValue);	
	int getYear();
	int getMonth();
	int getDay();
	unsigned int modernTime();
	string getDayOfWeek();
	int operator-(Date date);
	//friend ostream& operator <<(ostream& os, Date& date);
	void changeMonthAndDay(int monthValue, int dayValue);
	void changeThanksgivingDay();
	void changeSchoolDay();
	void changeBirthday(int month, int day);
	const static string months[];
private:
	int year;
	int month;
	int day;
	void testDate();
};

class Holidays
{
public:
	Holidays(int year);
	Date getIndependenceDay();
	Date getNextYearIndependenceDay();
	Date getThanksgiving();
	Date getSchoolDay();
	Date getBirthday();
	Date getNextYearBirthday();
	void showNextHoliday(Date today);
	void update(int year, int month, int day);
	friend ostream& operator <<(ostream& os, Holidays& holidays);
private:
	Date independenceDay;
	Date nextYearIndependenceDay;
	Date thanksgiving;
	Date schoolDay;
	Date birthday;
	Date nextYearBirthday;
};

extern bool leapYear(int year);
