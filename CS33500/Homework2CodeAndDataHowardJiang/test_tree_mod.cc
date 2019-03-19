// Howard Jiang
// Main file for Part2(c) of Homework 2.

#include "avl_tree_modified.h"
#include "sequence_map.h"

#include <iostream>
#include <string>
#include <sstream>
#include <fstream> //for ifstream
#include <math.h> //for log2

using namespace std;

namespace {

	// @db_filename: an input database filename.
	// @seq_filename: an input sequences filename.
	// @a_tree: an input tree of the type TreeType. It is assumed to be empty.
	template <typename TreeType>
	void TestTree(const string &db_filename, const string &seq_filename, TreeType &a_tree) {
		//Part 1: construct tree (same as 2A)
		ifstream input_file(db_filename, ifstream::in); // same as ios::in
		string input_line, an_enz_acro, a_reco_seq;

		for (unsigned int i = 0; i < 10; i++) {	//Get rid of garbage text/lines
			getline(input_file, input_line);
		}

		while (getline(input_file, input_line))
		{
			int counter = 0;
			for (size_t i = 0; i < input_line.length(); i++)
			{
				if (input_line[i] == '/') counter++;
			}

			stringstream ss(input_line);
			getline(ss, an_enz_acro, '/');

			for (int i = 0; i < (int)counter - 2; i++)	//-2 just to ignore the last 2 slashes
			{
				getline(ss, a_reco_seq, '/');
				SequenceMap new_sequence_map(a_reco_seq, an_enz_acro);
				a_tree.insert(new_sequence_map);
			}
		}

		//a_tree.printTree();

		ifstream input_file2(seq_filename, ifstream::in);
		string sequences;
		while (getline(input_file2, sequences))
		{
			//take seqs and output Acronyms
			SequenceMap new_sequence_map2(sequences);
			if (a_tree.contains(new_sequence_map2) == true)
			{
				new_sequence_map2 = a_tree.find(new_sequence_map2);
				string Acronyms = new_sequence_map2.Acronyms();
				//cout << Acronyms << endl;
			}
		}

		//Part 2: Prints num_nodes
		int num_nodes = a_tree.Count_Nodes();
		cout << "2: " << num_nodes << endl;

		//Part 3a: Prints avg depth 
		float depth = a_tree.Get_Avg_Depth(a_tree.Count_Nodes());
		cout << "3a: " << depth << endl;

		//Part 3b: 
		cout << "3b: " << (float)depth / log2(num_nodes) << endl;

		//4a)Prints number of strings found (queries)
		int num_queries = 0, num_recursions = 0;
		ifstream sequence_file(seq_filename, ifstream::in);
		while (getline(sequence_file, sequences))
		{
			SequenceMap new_sequence_map3(sequences);
			if (a_tree.contains(new_sequence_map3) == true)
			{
				num_queries++;
				new_sequence_map3 = a_tree.find(new_sequence_map3, num_recursions);
			}
		}
		cout << "4a: " << num_queries << endl;

		//Prints the average number of recursion calls
		cout << "4b: " << (float)num_recursions / num_queries << endl;

		//Prints the total number of successful removes
		int num_queries2 = 0, num_recursions2 = 0;
		ifstream sequence_file2(seq_filename, ifstream::in);
		while (getline(sequence_file2, sequences))
		{
			SequenceMap new_sequence_map3(sequences);
			if (a_tree.contains(new_sequence_map3) == true)
			{
				num_queries2++;
				a_tree.remove(new_sequence_map3, num_recursions2);
			}
			//Skips every other sequence.
			getline(sequence_file2, sequences);
		}
		cout << "5a: " << num_queries2 << endl;

		//Prints avg # recursion calls, total number of recursion calls / number of remove calls
		//int removedcalls = num_nodes - a_tree.Count_Nodes();
		//cout << "5b: " << (float)num_recursions2 / removedcalls << endl;
		cout << "5b: " << (float)num_recursions2 / num_queries2 << endl;

		//Prints num_nodes
		int num_nodes2 = a_tree.Count_Nodes();
		cout << "6a: " << num_nodes2 << endl;

		//Prints the average depth
		float depth2 = a_tree.Get_Avg_Depth(a_tree.Count_Nodes());
		cout << "6b: " << depth2 << endl;

		//Prints the ratio of the average depth to log2n
		cout << "6c: " << (float)depth2 / log2(num_nodes2) << endl;
	}
}  // namespace

int main(int argc, char **argv) {
	if (argc != 3) {
		cout << "Usage: " << argv[0] << " <databasefilename> <queryfilename>" << endl;
		return 0;
	}
	const string db_filename(argv[1]);
	const string seq_filename(argv[2]);
	cout << "Input file is " << db_filename << ", and sequences file is " << seq_filename << endl;
	// Note that you will replace AvlTree<int> with AvlTree<SequenceMap>
	AvlTree<SequenceMap> a_tree;
	TestTree(db_filename, seq_filename, a_tree);
	return 0;
}