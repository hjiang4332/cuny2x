all: project3
project3: Date.o Holidays.o project3.o
	g++ -Wall -o project3 project3.o Date.o Holidays.o
project3.o: project3.cpp
	g++ -o project3.o -c project3.cpp
Date.o: Date.cpp main.h
	g++ -o Date.o -c Date.cpp
Holidays.o: Holidays.cpp main.h
	g++ -o Holidays.o -c Holidays.cpp
clean:
	rm lab12