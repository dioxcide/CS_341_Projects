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
	ifstream masterFile(fileName), e_o_f;
	
	while (!masterFile){				/// Debugging while loop if user inputs an invalid file
		cout << "Invalid File!\n Please enter another one: " << endl;	
		cin >> fileName;				//Takes in a new file name 
		masterFile.open(fileName);
	}

	istream_iterator<string> start(masterFile), end;	//Creates a new iterator to iterate through the master file
	// Read file & count the occurrences of the first digits:

	for_each(start, end, [&](string s)			//For each loop gets the info from the file
	{											//and pushes the file names onto the string vector
		files.push_back(s);
	});

}
//Function to retrieve master file from the user
void userInput(string &fileName){
	cout << "Enter master file name: " << endl;
	cin >> fileName;		//Stores the file name into a string
}
//Reports the total number of students represented in the files
int numStudents(vector<string> files){	
		ifstream masterFile(files[0]);			//Opens the first file from the vector of file names
		istream_iterator<double> start(masterFile), end;	//creates an iterator for the file
		int numberOfStudents = 0;
		
		for_each(start, end, [&](double s)	//for each loop goes through the iterator and increments
		{									// numberOfStudents depending on how many items are read in
			numberOfStudents++;
		});
		
		return numberOfStudents;			//Returns the total number of students
}
//Function to parse the grades from each individual file
void parseGrades(vector<string> files ,vector<vector<double>> &grades ,int numRows ,int numColumns){	
	//For loop goes through the total amount of students and stores their grades
	for (int i = 0; i < numRows; i++){
		vector<double> tempColumns(numColumns, 0);		//double vector to store individual students
		int temp = 0;									// grades and gets stored in the 2d vector
														// later on
		for (auto &file : files){				//Range for loop goes through each file

			ifstream masterFile(file);						//Opens file
			istream_iterator<double> start(masterFile);		//Creates an iterator
			for (int j = 0; j < i; j++){					//Increments the iterator depending on which 
				start++;									//student we are currently on so we store the grades
			}												//from the same row in each file in the same vector
			tempColumns[temp] = *start;						//Stores it into the vector
			temp++;
		}
		
		grades[i] = tempColumns;							//Stores the temp vector in the 2d vector
	}
}
// Function that prints out essentially the 2d vector of grades
void printGrades(vector<vector<double>> grades){
	int numStudents = 0;
		
	for (auto &student : grades){		//Goes through each student
		numStudents++;	
		cout << "Student " << numStudents << " Grades: ";

		for (auto &grade : student){		//Prints out the students grade
			cout << "\t" << grade << endl;
		}
		cout << "\n" << endl;
	}
}
// Function that drops the lowest score from each students grades
void dropLowestScore(vector<vector<double>> &grades){

	for (auto &student : grades){//Range for loop that goes through the 2d vector
		auto worstGrade = min_element(student.begin(), student.end()) - student.begin();	//Finds the lowest test score for each student
		student.erase(student.begin() + worstGrade);			//Erases the lowest grade
	}
}
//Function that computes the average grade
void averageGrades(vector<vector<double>> grades , vector<vector<double>> &finalGrades){
	int currStudent = 0;
	
	for(auto grade : grades){	//Range for loop goes through each student
		vector<double> avgStdGrade(1,0);	//Creates a new vector to be stored in final grades
		double sumOfGrades = accumulate(grade.begin(), grade.end(), 0);	//Adds all the grades of the student
		double averagedGrade = sumOfGrades / grade.size();				//Divides the grades by number of grades the student has

		avgStdGrade[0] = averagedGrade;					//Adds it to the temp vector
		finalGrades[currStudent] = avgStdGrade;			//Adds it to the 2d final average vector sir
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
	
	averageGrades(studentGrades, finalGrades);
	//printGrades(finalGrades);

	system("pause");
	return 0;
}
