// --> Howard Jiang
/* This class describes a sequence of 2D points and uses the big five 
(copy and move constructor, copy and move assignment, and deconstructor) 
to manipulate data within it. 

Copy constructor and copy assignment passes in the previously created object as an L-value, 
which is why there is a const in front of it (we dont want to change those values).

Move constructor and move assignment passes in the previously created object as an R-Value,
We are merely getting the data of that object, not the object itself so there is no need for 
a const as there is no way for us to change the object's value.

The destructor deletes the dynamically allocated arrays and sets the pointers to nullptr (freeing up memory). 
*/
#ifndef CSCI335_HOMEWORK1_POINTS2_H_
#define CSCI335_HOMEWORK1_POINTS2_H_

#include <array>
#include <iostream>
#include <cstddef>
#include <string>
#include <sstream>

namespace teaching_project {
	//Place comments that provide a brief explanation of the class,
	//and its sample usage.
	template<typename Object>
	class Points2 {
		public:
			//Zero-parameter constructor. 
			//Set size to 0.
			//Ex: Points2<Object> a;
			Points2() : sequence_{ nullptr }, size_(0) {}

			//Copy-constructor.
			//Ex Points2<Object> c{a};
			//@result: c = a
			Points2(const Points2 &rhs) {
				//this.sequence_ = new std::array<Object, 2>{rhs.sequence_}; works for 1x1 but not 2d
				size_ = rhs.size_;
				sequence_ = new std::array<Object, 2>[size_];	//clears current sequence
				for (size_t i = 0; i < size_; i++) {
					sequence_[i] = rhs.sequence_[i];		//sets current sequence with param sequence.
				}
			}

			//Move-constructor. 
			//Ex: Points2<Object> e = move(c);
			//e takes c's values
			Points2(Points2 &&rhs){
				size_ = rhs.size_;
				sequence_ = rhs.sequence_;

				rhs.size_ = 0;	//deallocate rhs.
				rhs.sequence_ = nullptr;
			}

			// Copy-assignment. If you have already written
			// the copy-constructor and the move-constructor
			// you can just use:
			// {
			// Points2 copy = rhs; 
			// std::swap(*this, copy);
			// return *this;
			// }

			//Ex: Points2<Object> a = b;
			//a is equal to b
			Points2& operator=(const Points2 &rhs)
			{
				if (this != &rhs) {
					Points2 copy = rhs;
					std::swap(*this, copy);
				}
				return *this;
			}
			

			// Move-assignment.
			// Just use std::swap() for all variables.
			//Ex: a=move(e);
			//    a takes e's values
			Points2& operator=(Points2 &&rhs)
			{
				std::swap(size_, rhs.size_);
				std::swap(sequence_, rhs.sequence_);
				return *this;
			}

			//destructor
			//deletes the value and sets to nullptr;
			~Points2(){
				delete[] sequence_;	
				sequence_ = nullptr;
			}

			// End of big-five.




			// One parameter constructor.
			//Ex:const array<int, 2> a_point2{{7, 10}};
			//   Points2<int> d{ a_point2 };
			Points2(const std::array<Object, 2>& item) {
				/*std::array<Object, 2> temp_sequence_;
				temp_sequence_ = item;
				//std::cout << "Temp_sequence_" << temp_sequence_[0];
				sequence_ = temp_sequence_;
				size_ = item.size() / 2;*/		//Size is 2 here, so it messes up << function and prints an extra AoB number. 
				
				/*size_ = item.size() / 2;
				sequence_ = new std::array<Object, 2>[size_];
				sequence_ = item;*/



				size_ = item.size()/2;
				sequence_ = new std::array<Object, 2>[size_];
				sequence_[0] = item;
			}

			// Read a chain from standard input.
			void ReadPoints2() {
				std::string input_line;
				std::getline(std::cin, input_line); 
				std::stringstream input_stream(input_line);
				if (input_line.empty()) return;
				int size_of_sequence;
				input_stream >> size_of_sequence;

				// Allocate space for sequence.
				size_ = size_of_sequence;
				std::array<Object, 2> *temp_array_ = new std::array<Object, 2>[size_of_sequence];

				Object token;
				for (int i = 0 ;input_stream >> token; ++i) {
					temp_array_[i / 2][i % 2] = token;	//ints around down, increments counter every 2 input_streams.
				}

				//delete[] sequence_;	//delete old sequence
				sequence_ = temp_array_;
			}

			size_t size() const {
				return size_;
			}

			
			// @location: an index to a location in the sequence.
			// @returns the point at @location.
			// const version.
			// abort() if out-of-range.
			const std::array<Object, 2>& operator[](size_t location) const { 
				if(location > size_){	//AoB exception.
					abort();
				}
				else{
					return sequence_[location];
				}
			}

			//  @c1: A sequence.
			//  @c2: A second sequence.
			//  @return their sum. If the sequences are not of the same size, append the
			//    result with the remaining part of the larger sequence.
			friend Points2 operator+(const Points2 &c1, const Points2 &c2) {	//Finds the bigger sequence_ and copy assignments it into a temp_ Points2. loops through index of smaller sequence_ and adds onto corresponding index of bigger sequence_
				Points2 temp_;
				size_t smaller_sequence_;
				if(c1.size_ < c2.size_){
					temp_ = c2;
					smaller_sequence_ = c1.size_;
				}
				else if(c1.size_ > c2.size_ || c1.size_ == c2.size_){	//if the sizes are equal, doesnt matter which one is temp_ and which one is smaller_sequence_;
					temp_ = c1;
					smaller_sequence_ = c2.size_;
				}

				for (size_t i = 0; i < smaller_sequence_; i++) {		//adds values of both c1 and c2 sequence_'s up to the size_ of the smaller_sequence_
					temp_.sequence_[i][0] = c1.sequence_[i][0] + c2.sequence_[i][0];
					temp_.sequence_[i][1] = c1.sequence_[i][1] + c2.sequence_[i][1];
				}
				return temp_;
			}

			// Overloading the << operator.
			friend std::ostream &operator<<(std::ostream &out, const Points2 &some_points2) {
				if (some_points2.size_ == 0) {
					out << "()";
				}
				else {
					for (size_t i = 0; i < some_points2.size_;i++) {		//sorts through the array of sequences, printing out each number as it goes. 
						out << "(" << some_points2.sequence_[i][0] << "," << some_points2.sequence_[i][1] << ") ";
					}
				}
				out << std::endl;
				return out;
			}

		private:
			std::array<Object, 2> *sequence_;
			size_t size_;
	};	//end class
}  // namespace teaching_project
#endif // CSCI_335_HOMEWORK1_POINTS2_H_