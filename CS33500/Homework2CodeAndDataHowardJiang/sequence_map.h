//Howard Jiang
#ifndef SEQUENCE_MAP_H
#define SEQUENCE_MAP_H

//#include "dsexceptions.h"
//#include <algorithm>
#include <iostream> 
#include <vector>
#include <string>

using namespace std;
class SequenceMap
{
	public:
		SequenceMap(const string &a_rec_seq, const string &an_enz_acro)
		{
			recognition_sequence_ = a_rec_seq;
			//the vector enzyme_acronyms_will contain just one element, the an_enz_acro
			enzyme_acronyms_.push_back(an_enz_acro);
		}
		
		SequenceMap(const string &a_rec_seq)
		{
			recognition_sequence_ = a_rec_seq;
			enzyme_acronyms_ = {};
		}	
	
  	// Zero-parameter constructor. 
  	SequenceMap() = default;
  	// Copy-constructor.
  	SequenceMap(const SequenceMap &rhs) = default;
  	// Copy-assignment. 
  	SequenceMap& operator=(const SequenceMap &rhs) = default;
  	// Move-constructor. 
  	SequenceMap(SequenceMap &&rhs) = default;
  	// Move-assignment.
  	SequenceMap& operator=(SequenceMap &&rhs) = default;
	// Deconstructor
  	~SequenceMap() = default;
  	
  	bool operator<(const SequenceMap &rhs)const
	{
		if (recognition_sequence_ < rhs.recognition_sequence_) return true;
		return false;
	}
	
	bool operator==(const SequenceMap &rhs)const
	{
		if (recognition_sequence_ == rhs.recognition_sequence_) return true;
		return false;
	}
	
 	// Overloading the << operator.
 	friend ostream &operator<<(ostream &out, const SequenceMap &some_sequence) 
 	{
 		out << "recognition_sequence_: " << some_sequence.recognition_sequence_ << "\n";
 		for (int i=0; i != (int)some_sequence.enzyme_acronyms_.size(); ++i)
			out << "enzyme_acronyms_: " << some_sequence.enzyme_acronyms_[i] << " ";
 		out << "\n";
 	
		return out;
 	}
 	
 	void Merge(const SequenceMap &other_sequence)
	{	
		enzyme_acronyms_.push_back(other_sequence.enzyme_acronyms_[0]);
 	}

	//vector<string> const &Get_Enzyme_Acronyms() const {return enzyme_acronyms_}	//private variable, cant access.
		
	string Acronyms() {
		string s;
		for (unsigned int i = 0; i < enzyme_acronyms_.size(); i++)
		{
			s = s + enzyme_acronyms_[i] + " ";
		}
		return s;
	}

	private:
		string recognition_sequence_;
		vector<string> enzyme_acronyms_;
};
#endif
