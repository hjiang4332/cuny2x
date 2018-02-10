#include <iostream>
#include "main.h"

Holidays::Holidays(int year): independenceDay(year, 7, 4), nextYearIndependenceDay(year+1,7,4), schoolDay(year,8,25), thanksgiving(year,11,22), birthday(year), nextYearBirthday(year+1)
	{}

Date Holidays::getThanksgiving(){
	return thanksgiving;
}

Date Holidays::getSchoolDay(){
	return schoolDay;
}

Date Holidays::getBirthday(){
	return birthday;
}

Date Holidays::getNextYearBirthday(){
	return nextYearBirthday;
}

Date Holidays::getIndependenceDay(){
	return independenceDay;
}

Date Holidays::getNextYearIndependenceDay(){
	return nextYearIndependenceDay;
}

void Holidays::update(int birthYear, int birthMonth, int birthDay){
	thanksgiving.Date::changeThanksgivingDay();
	schoolDay.Date::changeSchoolDay();
	birthday.Date::changeBirthday(birthMonth,birthDay);
	if(leapYear(birthYear+1) && birthMonth==2 && birthDay==29){
		nextYearBirthday.Date::changeBirthday(birthMonth,birthDay);
	}else if(!leapYear(birthYear+1) && birthMonth==2 && birthDay==29){
		nextYearBirthday.Date::changeBirthday(birthMonth,birthDay-1);
	}else{
		nextYearBirthday.Date::changeBirthday(birthMonth,birthDay);
	}
}

//ostream& operator <<(ostream& os, Holidays thisYearsHolidays){
	//os << "independence Day will occur on " << thisYearsHolidays.Holidays::getIndependenceDay().Date::getMonth() << " " << thisYearsHolidays.Holidays::getIndependenceDay().Date::getDay() << ", " << thisYearsHolidays.Holidays::getIndependenceDay().Date::getYear() << ", which is day " << thisYearsHolidays.Holidays::getIndependenceDay().Date::modernTime() << " in modern time." << endl;
	//os << "school Day will occur on, " << thisYearsHolidays.Holidays::getSchoolDay().Date::getMonth() << " " << thisYearsHolidays.Holidays::getSchoolDay().Date::getDay() << ", " << thisYearsHolidays.Holidays::getSchoolDay().Date::getYear() << ", which is day " << thisYearsHolidays.Holidays::getSchoolDay().Date::modernTime() << " in modern time." << endl;
	//os << "thanksgiving will occur on, " << thisYearsHolidays.Holidays::getThanksgiving().Date::getMonth() << " " << thisYearsHolidays.Holidays::getThanksgiving().Date::getDay() << ", " << thisYearsHolidays.Holidays::getThanksgiving().Date::getYear() << ", which is day " << thisYearsHolidays.Holidays::getThanksgiving().Date::modernTime() << " in modern time." << endl;
	//os << "Birth Day will occur on, " << thisYearsHolidays.Holidays::getBirthday().Date::getMonth() << " " << thisYearsHolidays.Holidays::getBirthday().Date::getDay() << ", " << thisYearsHolidays.Holidays::getBirthday().Date::getYear() << ", which is day " << thisYearsHolidays.Holidays::getBirthday().Date::modernTime() << " in modern time." << endl;
	//return os;
//}

//ostream& operator <<(ostream& os, Holidays& thisYearsHolidays){
//	os << "independence Day will occur on " << thisYearsHolidays.Holidays::getIndependenceDay().Date::getMonth() << " " << thisYearsHolidays.Holidays::getIndependenceDay().Date::getDay() << ", " << thisYearsHolidays.Holidays::getIndependenceDay().Date::getYear() << ", which is day " << thisYearsHolidays.Holidays::getIndependenceDay().Date::modernTime() << " in modern time." << endl;
//	return os;
//}

ostream& operator << (ostream& os, Holidays& holidays)
{
    //os << "Independence Day will occur on " << holidays.Holidays::getIndependenceDay().Date::getMonth() <<" " << holidays.Holidays::getIndependenceDay().Date::getDay() << endl;
    os << "Independence Day will occur on " << Date::months[holidays.Holidays::getIndependenceDay().Date::getMonth()-1] << " " << holidays.Holidays::getIndependenceDay().Date::getDay() << ", " << holidays.Holidays::getIndependenceDay().Date::getYear() << ", which is day " << holidays.Holidays::getIndependenceDay().Date::modernTime() << " in modern time." << endl;
	os << "School Day will occur on " << Date::months[holidays.Holidays::getSchoolDay().Date::getMonth()-1] << " " << holidays.Holidays::getSchoolDay().Date::getDay() << ", " << holidays.Holidays::getSchoolDay().Date::getYear() << ", which is day " << holidays.Holidays::getSchoolDay().Date::modernTime() << " in modern time." << endl;
	os << "Thanksgiving will occur on " << Date::months[holidays.Holidays::getThanksgiving().Date::getMonth()-1] << " " << holidays.Holidays::getThanksgiving().Date::getDay() << ", " << holidays.Holidays::getThanksgiving().Date::getYear() << ", which is day " << holidays.Holidays::getThanksgiving().Date::modernTime() << " in modern time." << endl;
	os << "Your birthday will occur on " << Date::months[holidays.Holidays::getBirthday().Date::getMonth()-1] << " " << holidays.Holidays::getBirthday().Date::getDay() << ", " << holidays.Holidays::getBirthday().Date::getYear() << ", which is day " << holidays.Holidays::getBirthday().Date::modernTime() << " in modern time." << endl;
	return os ;
}

void Holidays::showNextHoliday(Date today){
	string name;
	int numDays;
	if(thanksgiving.modernTime()<today.modernTime()){	//today is after thanksgiving
		if(birthday.modernTime()<today.modernTime()){	//Go to next year
			if(birthday.modernTime()<independenceDay.modernTime()){		//next year birthday is the earliest holiday
				name = "your birthday";
				numDays = nextYearBirthday - today;
			}
			else if(birthday.modernTime()>independenceDay.modernTime()){//next year independence day is the earliest holiday
				name = "Independence day";
				numDays = nextYearIndependenceDay-today;
			}
			else if(birthday.modernTime() == independenceDay.modernTime()){		//both birthday and independence day are on the same day, and are the earliest holiday
				name = "Independence day and your birthday";
				numDays = nextYearBirthday - today;
			}
		}
		else{	//after thanksgiving, birthday is either today or after today in the same year
			name = "your birthday";
			numDays = birthday-today;
		}
	}
	else if(schoolDay.modernTime()<today.modernTime()){		//same year code, date is after school day 
		name = "Thanksgiving";
		numDays = thanksgiving-today;
	}
	else if(independenceDay.modernTime() < today.modernTime()){	//date is after independence day
		name = "school day";
		numDays = schoolDay-today;
	}
	else if(today.modernTime()<independenceDay.modernTime()){	//date is before independence day 
		name = "Independence Day";
		numDays = independenceDay-today;
	}
	if(0 < birthday-today && birthday-today < numDays){
		name = "your birthday";
		numDays = birthday-today;
	}
	//cout << today.Date::getYear() << endl;
	cout << "The next holiday is " << name << ", which is " << numDays << " days from today!" << endl;
}
