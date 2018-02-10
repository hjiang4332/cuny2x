#include <iostream>
#include "main.h"
#include <cstdlib>
using namespace std;

/*Date::Date(int yearValue, int monthValue, int dayValue){
	year = yearValue;
	month = monthValue;
	day = dayValue;
	testDate();
}*/
Date::Date(int yearValue, int monthValue, int dayValue): year(yearValue), month(monthValue), day(dayValue){
	testDate();
}

Date::Date(int yearValue){
	year = yearValue;
	month = 1;
	day = 1;
	testDate();
}

int Date::getYear(){
	return year;
}

int Date::getMonth(){
	return month;
}

int Date::getDay(){
	return day;
}

void Date::changeMonthAndDay(int monthValue, int dayValue){
	month = monthValue;
	day = dayValue;
}

void Date::changeThanksgivingDay(){
	for(int i=22; i<29;i++){
		int numdays;
		Date tempThanksgiving(Date::getYear(),11,i);
		numdays = tempThanksgiving.Date::modernTime();
		if(numdays%7==2){
			Date::changeMonthAndDay(11, i);
			break;
		}
	}
}

void Date::changeSchoolDay(){
	for(int i=25; i<32;i++){
		int numdays;
		Date tempSchoolDay(Date::getYear(),8,i);
		numdays = tempSchoolDay.Date::modernTime();
		if(numdays%7==6){
			Date::changeMonthAndDay(8,i);
			break;
		}
	}
}

void Date::changeBirthday(int month, int day){
	Date::changeMonthAndDay(month, day);
}

/*ostream& operator <<(ostream& os, Date date){
	os << "The month: " << date.Date::getMonth() << endl;
	return os;
}*/

ostream& operator << (ostream& os, Date& date)
{
    os << date.Date::getMonth() <<" " << date.Date::getDay() << endl;
    return os ;
}

bool leapYear(int year){
	if(year%4==0 && (year%100!=0 || year%400==0)){
		return true;
	}
	return false;
}

void Date::testDate()
{
	if(year<1901){
		cout << "Illegal year value!" << endl;
		exit(1);
	}
	else if(month<1 || month>12){
		cout << "Illegal month value!" << endl;
		exit(1);
	}			
	else if(day<1 || day>31){
		cout << "Illegal day value!" << endl;
		exit(1);
	}
	else if(((month%2==0 && month<8 && month!=2)|| (month%2!=0 && month>=8)) && day > 30){
		cout << "Illegal day value for this specific month" << endl;
		exit(1);
	}
	else if(leapYear(year) && month==2 && day>29){
		cout << "This is a leap year, february cant have more than 29 days!" << endl;
		exit(1);
	}
	else if(!leapYear(year) && month==2 && day>28){
		cout << "This is not a leap year, february cant have more than 28 days!" << endl;
		exit(1);
	}
}

unsigned int Date::modernTime(){
	Date beforeDate(1901,1,1);
	int numDaysPassed = 0;
	int leapyears = 0;
	
	for(int i=1901; i<year;i++){	//years passed
		numDaysPassed+=365;
		if(leapYear(i)){
			numDaysPassed++;
		}
	}
	
	for(int i=1;i<month;i++){	//month represents january 
		if((i%2!=0 && i<8) || (i%2==0&& i>=8)){
			numDaysPassed+=31;
		}			
		else if((i%2==0 && i<8 && i!=2)|| (i%2!=0 && i>=8)){
			numDaysPassed+=30;
		}
		else if(i==2 && leapYear(year)){
			numDaysPassed+=29;
		}
		else if(i==2 && !leapYear(year)){
			numDaysPassed+=28;
		}
	}
	
	numDaysPassed+=day-1;	//days

	return numDaysPassed;
}

string Date::getDayOfWeek(){
	int displacement = this -> modernTime()%7;
	switch(displacement){
		case 0: return "Tuesday";
			break;
		case 1: return "Wednesday";
			break;
		case 2: return "Thursday";
			break;
		case 3: return "Friday";
			break;
		case 4: return "Saturday";
			break;
		case 5: return "Sunday";
			break;
		case 6: return "Monday";
			break;
	}
}

int Date::operator-(Date before){
	int nowModernTime = this -> modernTime();
	int beforeModernTime = before.Date::modernTime();
	int difference = nowModernTime-beforeModernTime;
	return difference;
}
