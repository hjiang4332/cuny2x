You will use this exact Makefile for your Homework 1.

Failure to do so will result in deduction of points.


To compile on terminal type
  make clean
  make all

 
To delete executables and object file type
  make clean

To run:

./test_points2

^^
In that case you will provide input from standard input.


To run with a given file that is redirected to standard input:

./test_points2 < test_input_file.txt


/* copy pasted from the points2.h file
This class describes a sequence of 2D points and uses the big five 
(copy and move constructor, copy and move assignment, and deconstructor) 
	to manipulate data within it. 

	Copy constructor and copy assignment passes in the previously created object as an L-value, 
	which is why there is a const in front of it (we dont want to change those values).

	Move constructor and move assignment passes in the previously created object as an R-Value,
	We are merely getting the data of that object, not the object itself so there is no need for 
	a const as there is no way for us to change the object's value.

	The destructor deletes the dynamically allocated arrays and sets the pointers to nullptr (freeing up memory). 
*/

/* More in depth explanations
	copy constructor: makes sequence a 2d array so that it can hold multiple sequences. 
	copy assignment: Only copies if the parameter is different
	move constructor:  sets values to the parameter, deallocates parameter.
	move assignment: swaps all variables
	destructor: deletes dynamically allocated array and sets sequence_ to nullptr;

	one parameter constructor: Had a lot of difficulty with this one because my original plan was to do
		sequence_ = item. Problem is, i have to remove const from sequence_ later so creating a 2d array of 
		sequences the only solution i could think of.
	ReadPoints2: I am very proud of figuring out the i/2 portion of temp_array_[i/2][i%2]. Originally, there was an idea
		of doing a more complex counter by resetting back to 1 while checking if i=0 but i/2 works because ints round down when dividing.
	overload operator+: We already wrote copy assignment so i just used the bigger sequence_ as the main one and just added the smaller sequence_ onto it.
*/