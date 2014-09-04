//
// C++ program to compute average scores
//
// Antonio Villarreal
// U. of Illinois, Chicago
// CS341, Fall 2014
// Homework 1
//
#include <iostream>
#include <iterator>
#include <fstream>
#include <vector>
#include <algorithm>
#include <string>
#include <numeric>

using namespace std;	//Simplifies the need of typing std::cout and other 
						//standard libraries
void parseFiles(vector<string> &files , string fileName){

		ifstream masterFile(fileName);
		istream_iterator<string> start(masterFile), end;
		// Read file & count the occurrences of the first digits:

		for_each(start, end, [&](string s)
		{
			files.push_back(s);
		});

}

void userInput(string &fileName){
	cout << "Enter master file name: " << endl;
	cin >> fileName;
}

int numStudents(vector<string> files){	
		ifstream masterFile(files[0]);
		istream_iterator<int> start(masterFile), end;
		int numberOfStudents = 0;
		
		for_each(start, end, [&](int s)
		{
			numberOfStudents++;
		});
		
		return numberOfStudents;
}

void parseGrades(vector<string> files ,vector<vector<double>> &grades ,int numRows ,int numColumns){

	for (int i = 0; i < numRows; i++){
		vector<double> tempColumns(numColumns, 0);
		int temp = 0;
		
		for (auto &file : files){

			ifstream masterFile(file);
			istream_iterator<double> start(masterFile);
			for (int j = 0; j < i; j++){
				start++;
			}
			tempColumns[temp] = *start;
			temp++;
		}
		
		grades[i] = tempColumns;
	}
}

void printGrades(vector<vector<double>> grades){
	int numStudents = 0;

	for (auto &student : grades){
		numStudents++;
		cout << "Student " << numStudents << " Grades: " << endl;

		for (auto &grade : student){
			cout << grade << endl;
		}
		cout << "\n" << endl;
	}
}

void dropLowestScore(vector<vector<double>> &grades){
	for (auto &student : grades){
		auto worstGrade = min_element(student.begin(), student.end()) - student.begin();
		student.erase(student.begin() + worstGrade);
	}
}

void averageGrades(vector<vector<double>> grades , vector<vector<double>> &finalGrades){
	int currStudent = 0;
	
	for(auto grade : grades){
		vector<double> avgStdGrade(1,0);
		double sumOfGrades = accumulate(grade.begin(), grade.end(), 0);
		double averagedGrade = sumOfGrades / grade.size();

		avgStdGrade[0] = averagedGrade;
		finalGrades[currStudent] = avgStdGrade;
		currStudent++;
	}
}

int main()
{
	int numColumns = 0;
	int numRows = 0;
	string masterFile;		//Stores main file name
	
	vector<string> files; // Stores all subfiles
	vector<vector<double>> studentGrades;
	vector<vector<double>> finalGrades;

	userInput(masterFile);
	parseFiles(files, masterFile);

	numColumns = files.size();
	numRows = numStudents(files);
	studentGrades.resize(numRows, vector<double>(numColumns, 0));
	finalGrades.resize(numRows, vector<double>(1, 0));

	parseGrades(files, studentGrades, numRows, numColumns);
	printGrades(studentGrades);
	
	dropLowestScore(studentGrades);
	printGrades(studentGrades);
	
	averageGrades(studentGrades, finalGrades);
	printGrades(finalGrades);

	system("pause");
	return 0;
}
