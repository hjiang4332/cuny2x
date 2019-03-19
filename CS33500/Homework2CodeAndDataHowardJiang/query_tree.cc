// Howard Jiang
// Main file for Part2(a) of Homework 2.

#include "avl_tree.h"
//#include "avl_tree_modified.h"
#include "sequence_map.h"

#include <iostream>
#include <string>
#include <sstream>
#include <fstream> //for ifstream

using namespace std;

namespace {
	// @db_filename: an input filename.
	// @a_tree: an input tree of the type TreeType. It is assumed to be empty.
	template <typename TreeType>
	void QueryTree(const string &db_filename, TreeType &a_tree) 
	{
		ifstream input_file(db_filename, ifstream::in); // same as ios::in
		string input_line, an_enz_acro, a_reco_seq;
		
		for(unsigned int i = 0; i < 10; i++) {	//Get rid of garbage text/lines
			getline(input_file, input_line);
		}

		/*while (getline(input_file, input_line)) {
			counter = 0;
			for (unsigned i = 0; i < input_line.length(); i++) {
				if (input_line[i] == '/')
				{
					if (counter == 0) {
						an_enz_acro = input_line.substr(0, i);
						//cout << "an_enz_acro is: " << an_enz_acro << endl;
						counter = i + 1; //keeps track that the acronym is received, for else statement. +1 skips the /
					}
					else if (counter != 0 && input_line[i + 1] != '/') {	//get sequence
						a_reco_seq = input_line.substr(counter, i - counter);
						//cout << "Counter: " << counter << " i - counter: " << i - counter<< " substring: " << input_line.substr(counter, i-counter) << endl;
						//cout << "acro: " << an_enz_acro << " seq: " << a_reco_seq << endl;
						counter = i+1;
						SequenceMap new_sequence_map(an_enz_acro, a_reco_seq);
						a_tree.insert(new_sequence_map);
					}
					else if (counter != 0 && input_line[i + 1] == '/') {	//get sequence
						a_reco_seq = input_line.substr(counter, i - counter);
						//cout << "Counter: " << counter << " i - counter: " << i - counter << " substring: " << input_line.substr(counter, i - counter) << endl;
						//cout << "acro: " << an_enz_acro << " seq: " << a_reco_seq << endl;
						SequenceMap new_sequence_map(an_enz_acro, a_reco_seq);
						a_tree.insert(new_sequence_map);
						break;
					}
				}
			}
		}*/

		while (getline(input_file, input_line)) 
		{
			int counter = 0;
			for (size_t i = 0; i<input_line.length(); i++)
			{
				if (input_line[i] == '/') counter++;
			}

			stringstream ss(input_line);
			getline(ss, an_enz_acro, '/');
		
			for (int i=0; i<(int)counter-2; i++)	//-2 just to ignore the last 2 slashes
			{
				getline(ss, a_reco_seq, '/');
				SequenceMap new_sequence_map(a_reco_seq, an_enz_acro);
				a_tree.insert(new_sequence_map);
			}				
		}	

		a_tree.printTree();

		string sequences;
		while (cin >> sequences) {
			SequenceMap new_sequence_map2(sequences);
			if (a_tree.contains(new_sequence_map2)) {
				new_sequence_map2 = a_tree.find(new_sequence_map2);
				string Acronyms = new_sequence_map2.Acronyms();
				cout << Acronyms << endl;
			}
			else {
				cout << "Not Found" << endl;
			}
		}
	}	// end of QueryTree
}  // end of namespace

int main(int argc, char **argv) {
	if (argc != 2) {
		cout << "Usage: " << argv[0] << " <databasefilename>" << endl;
		return 0;
	}
	const string db_filename(argv[1]);
	cout << "Input filename is " << db_filename << endl;
	// Note that you will replace AvlTree<int> with AvlTree<SequenceMap>
	AvlTree<SequenceMap> a_tree;
	QueryTree(db_filename, a_tree);
	return 0;
}