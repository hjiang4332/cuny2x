/*
  Author: <Howard Jiang>
  Course: {135,136}
  Instructor: <Xiao Jie>
  Assignment: <Project 3>

	This program takes in inputs from the user and creates Date objects which are then placed into the holiday class for which
	the information is manipulated. This project makes clever use of classes and other files including the makefile in order to compile a 
	full program that outputs the specific holidays of the year, including the next holiday coming up.
*/
#include <iostream>
#include <string>
#include <cstdlib>
#include "main.h"

using namespace std;
const string Date::months[] = {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};

int main(){
	int todayYear, todayMonth, todayDay;
	int birthYear, birthMonth, birthDay;
	
	//cout << "Enter today's year" << endl;
	cin >> todayYear;
	//cout << "Enter today's month" << endl;
	cin >> todayMonth;
	//cout << "Enter today's day" << endl;
	cin >> todayDay;
	//cout << "Enter your birthday's year" << endl;
	cin >> birthYear;
	//cout << "Enter your birthday's month" << endl;
	cin >> birthMonth;
	//cout << "Enter your birthday's day" << endl;
	cin >> birthDay;
	
	Date today(todayYear, todayMonth, todayDay);
	Date birthday(todayYear, birthMonth, birthDay);
	/*if(leapYear(todayYear) && todayMonth==2 && birthDay==29){
		Date nextYearBirthDay(todayYear+1, birthMonth, 28);
	}else{
		Date nextYearBirthDay(todayYear+1, birthMonth, birthDay);
	}*/
	Holidays thisYearsHolidays(todayYear);
	thisYearsHolidays.Holidays::update(todayYear, birthMonth,birthDay);
	
	//cout << Date::months[0];
	//cout << independenceDay.Date::getDayOfWeek();
	cout << thisYearsHolidays;	
	thisYearsHolidays.Holidays::showNextHoliday(today);
	return 0;
}