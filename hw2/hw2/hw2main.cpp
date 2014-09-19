//
// C++ program to compute average scores
//
// Antonio Villarreal
// U. of Illinois, Chicago
// CS341, Fall 2014
// Homework 2
//
#include <sstream>
#include <vector>
#include <iostream>
#include <fstream>
#include <vector>
#include <algorithm>
#include <string>
#include <numeric>
#include <map>
#include <iomanip>

using namespace std;
//
// Store one review:
//
class Review
{
public:
	int movieID;
	int userID;
	int movieRating;
	Review(int movie, int user, int rating)
	{
		movieID = movie;
		userID = user;
		movieRating = rating;
	}

};
//
// Parses one line of the reviews file and returns a Review object:
//
Review ParseReview(string line)
{
	istringstream strstream(line);
	string movieIDstr, userIDstr, ratingstr;
	getline(strstream, movieIDstr, ',');
	getline(strstream, userIDstr, ',');
	getline(strstream, ratingstr, ',');
	int movieID = atoi(movieIDstr.c_str());
	int userID = atoi(userIDstr.c_str());
	int movieRating = atoi(ratingstr.c_str());
	return Review(movieID, userID, movieRating);
}
//
// Stores one movie:
//
class Movie
{
public:
	int movieID;
	double averageRate;
	int totalReviews;
	int five;
	int four;
	int three;
	int two;
	int one;
	string movieName;
	Movie(int movie, string name)
	{
		movieID = movie;
		movieName = name;
	}
};
//
// Parses one line of the movies file and returns a Movie object:
//
Movie ParseMovie(string line)
{
	size_t comma = line.find(',');
	int movieID = atoi(line.substr(0, comma).c_str());
	string movieName = line.substr(comma + 1);
	return Movie(movieID, movieName);
}

template<typename Vector>	//Template for generic type
void parseMovie(const string &fileName, Vector & v){	//Parses all the movies in the movie file
	ifstream masterFile(fileName);						//Opens file stream
	string line;
	
	while (getline(masterFile, line))					//Iterates through file
	{
		auto movie = ParseMovie(line);
		v.push_back(pair<Movie, vector<double> >(movie, vector<double>()));	//Pushes new movie onto vector
	}
}

template<typename Container, typename Container2>	//Template for generic containers
void parseReviews(const string & filename, Container & movies, Container2 & userReviews){
	ifstream masterFile(filename);	//Opens file
	string line;

	while (getline(masterFile, line))	//Iterates through file
	{
		auto review = ParseReview(line);
		auto reviewIter = find_if(userReviews.begin(), userReviews.end(), [=](pair<int, int> e){ return e.first == review.userID; });	//Determines if the user has already been added to the vector
		
		if (reviewIter != userReviews.end()){		//If the user has been added already increment 2nd pair to keep track of num reviews
			reviewIter->second++;
		}
		else{
			userReviews.push_back(pair<int, int>(review.userID, 1));		//If the user is not found then we push back a new pair into the vector
		}

		auto movieIter = find_if(movies.begin(), movies.end(), [=](pair<Movie, vector<double>> e){ return e.first.movieID == review.movieID; });	//Finds the specific movie that got reviewed
		
		if (movieIter != movies.end()){
			movieIter->second.push_back(review.movieRating);	//Pushes rating into 2nd pair which is a vector double which contains all the reviews
		}

	}
									//Sorts the userReviews in descending order
	sort(userReviews.begin(), userReviews.end(), [=](pair<int, int> a, pair<int, int> b){return a.second != b.second ? a.second > b.second : a.first < b.first; });

}
	//Stores the number of 1-5 star reviews for each movie as well as compute the averages
void topTenMovies(vector<pair<Movie, vector<double>>> &v){
	
	for (auto &vec : v){		//Goes through the vector of pairs
		double accum = accumulate(vec.second.begin(), vec.second.end(), 0);		
		accum /= vec.second.size();											//Find average of the 2nd pair i.e vector of doubles
		
		int one = count(vec.second.begin(),vec.second.end(),1);				//Counts how many 1,2,3,4,5 star reviews the movie has had
		int two = count(vec.second.begin(), vec.second.end(), 2);
		int three = count(vec.second.begin(), vec.second.end(), 3);
		int four = count(vec.second.begin(), vec.second.end(), 4);
		int five = count(vec.second.begin(), vec.second.end(), 5);
		
		vec.first.averageRate = accum;										//Saves the counts from earlier into the movie class
		vec.first.five = five;
		vec.first.four = four;
		vec.first.three = three;
		vec.first.two = two;
		vec.first.one = one;
		vec.first.totalReviews = vec.second.size();
	}
				//Sorts the vector by average rating of each movie in descending order
	sort(v.begin(), v.end(), [](pair<Movie, vector<double>> a, pair<Movie, vector<double>> b){return a.first.averageRate > b.first.averageRate; });
}
//Inifinte while loop prints out user specified movie info
void specificMovie(const vector<pair<Movie, vector<double>>> &v){
	int option = -1;
	cin >> option;
	//Reads in the user input and prints out specific movie
	while (option !=0){
		auto avgIter = find_if(v.begin(), v.end(), [=](pair<Movie, vector<double>> e){return e.first.movieID == option; });
		cout << "\t   Movie ID\t             MovieName\t         Movie Rating\n\n";
		cout << "\t" << avgIter->first.movieID << "\t   " << avgIter->first.movieName << "\t    " << avgIter->first.averageRate << endl;
		cout << "\t\t\tFive Stars: " << avgIter->first.five << endl;
		cout << "\t\t\tFour Stars: " << avgIter->first.four << endl;
		cout << "\t\t\tThree Stars: " << avgIter->first.three << endl;
		cout << "\t\t\tTwo Stars: " << avgIter->first.two << endl;
		cout << "\t\t\tOne Stars: " << avgIter->first.one << endl;
		cout << endl;
		cin >> option;
	}
	cout << setw(30) << "Exiting" << endl;
}
//Prints top 10 reviewers
void printReviews(const vector<pair<int, int>> & v){
	cout << "\t      Top 10 Reviewers!\n\tUser ID\t\tNumber of Reviews\n\n";

	int i = 0;
	for (auto & temp : v){
		if (i < 10){
			cout <<"\t"<< temp.first << "\t\t\t" << temp.second << endl;
			
		}
		else{
			break;
		}
		i++;
	}
	cout << endl<<endl;
}
//Prints top 10 movies
void printMovies(const vector<pair<Movie, vector<double>>> &v){
	
	int i = 0;
	cout << "\t          Top Ten Movies!\n\n\tMovie ID\t\tMovieName\t\tMovie Rating\n\n";
	for (auto & temp : v){
		if (i == 10){
			break;
		}
		cout << " " << setw(10) << temp.first.movieID << "\t";
		cout << " " << setw(10) << temp.first.movieName << "\t";
		cout << " " << setw(10) << temp.first.averageRate << "\t" << endl;
		i++;
	}
	cout << endl << endl;
}

int main(int argc, string *argv){
	string f1 = "movies.txt";
	string f2 = "reviews2.txt";
	vector<pair<Movie,vector<double>>> movieRatings;
	vector<pair<int, int>> userRatings;

	parseMovie<vector<pair<Movie, vector<double>>>>(f1, movieRatings);
	parseReviews<vector<pair<Movie, vector<double>>>, vector<pair<int, int>>>(f2, movieRatings, userRatings);
	
	topTenMovies(movieRatings);
	
	printMovies(movieRatings);
	printReviews(userRatings);
	
	specificMovie(movieRatings);
	
	system("pause");
}