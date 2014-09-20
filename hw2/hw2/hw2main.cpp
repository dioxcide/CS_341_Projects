//
// C++ program to compute average scores
//
// Antonio Villarreal
// U. of Illinois, Chicago
// CS341, Fall 2014
// Homework 2
//
#include <sstream>
#include <iostream>
#include <fstream>
#include <algorithm>
#include <vector>
#include <string>
#include <numeric>
#include <iomanip>
#include <map>

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
	Movie(){
		movieID = 0;
		movieName = "Null";
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

template<typename Map>	//Template for generic type
void parseMovie(const string &fileName, Map & v){	//Parses all the movies in the movie file
	ifstream masterFile(fileName);						//Opens file stream
	string line;

	while (getline(masterFile, line))					//Iterates through file
	{
		auto movie = ParseMovie(line);
		v[movie.movieID] = pair<Movie, vector<double>>(movie, vector<double>());
	}
}

void parseReviews(const string & filename, map<int, pair<Movie, vector<double>>> & movies, map<int, int> & userReviews){
	ifstream masterFile(filename);	//Opens file
	string line;
	int temp = 0;
	cout << "Running.";

	while (getline(masterFile, line))//Iterates through file
	{
		auto review = ParseReview(line);

		userReviews[review.userID]++; //Increments the amount of ratings each user has

		movies[review.movieID].second.push_back(review.movieRating);	//Pushes all the ratings for each individual movie onto the vector
	}
}
//Stores the number of 1-5 star reviews for each movie as well as compute the averages
void topTenMovies(map<int, pair<Movie, vector<double>>> &v){
	map<double, Movie> avgMap;

	for (auto &vec : v){		//Goes through the vector of pairs
		double accum = accumulate(vec.second.second.begin(), vec.second.second.end(), 0);
		accum /= vec.second.second.size();											//Find average of the 2nd pair i.e vector of doubles

		vec.second.first.one = count(vec.second.second.begin(), vec.second.second.end(), 1);				//Counts how many 1,2,3,4,5 star reviews the movie has had
		vec.second.first.two = count(vec.second.second.begin(), vec.second.second.end(), 2);
		vec.second.first.three = count(vec.second.second.begin(), vec.second.second.end(), 3);
		vec.second.first.four = count(vec.second.second.begin(), vec.second.second.end(), 4);
		vec.second.first.five = count(vec.second.second.begin(), vec.second.second.end(), 5);

		vec.second.first.averageRate = accum;										//Saves the counts from earlier into the movie class
		vec.second.first.totalReviews = vec.second.second.size();

		avgMap[vec.second.first.averageRate] = vec.second.first;	//Stores the key as the average rating and the value as the movieID
	}															//With how maps work this will give us the top 10 movies at the bottom 
	//Of the map so we can just iterate backwards to get the top 10 movies from the map
	int i = 0;
	cout << "\t          Top Ten Movies!\n\n\tMovie ID\t\tMovieName\t\tMovie Rating\n\n";

	for (auto rit = avgMap.rbegin(); rit != avgMap.rend(); ++rit){	//Iterates backwards to get the top ten movies

		if (i == 10){								//Breaks the while loop when we printed out all 10 movies
			break;
		}

		cout << " " << setw(10) << rit->second.movieID << "\t";		//Printing movie info
		cout << " " << setw(10) << rit->second.movieName << "\t";
		cout << " " << setw(10) << rit->second.averageRate << "\t" << endl;
		i++;
	}

	cout << endl << endl;
}

//Inifinte while loop prints out user specified movie info
void specificMovie(const map<int, pair<Movie, vector<double>>> &v){
	int option = -1;
	cout << "Enter a movie ID you want info on: ";
	cin >> option;
	//Reads in the user input and prints out specific movie
	while (option != 0){
		if (option <= v.size() && option >= 1){
			Movie avgIter = v.at(option).first;
			cout << "\tMovie ID\t       MovieName\t         Movie Rating\n\n";
			cout << "\t" << avgIter.movieID << "\t      " << avgIter.movieName << "\t       " << avgIter.averageRate << endl << endl;
			cout << "\t\t\t   Five Stars: " << avgIter.five << endl;
			cout << "\t\t\t   Four Stars: " << avgIter.four << endl;
			cout << "\t\t\t   Three Stars: " << avgIter.three << endl;			//Printing Movie Info
			cout << "\t\t\t   Two Stars: " << avgIter.two << endl;
			cout << "\t\t\t   One Stars: " << avgIter.one << endl;
			cout << endl;
		}
		else if (option < 0 || option > v.size()){								//Error checking in case the user inputs a negative number
			cout << "Movie ID invalid! Please enter another one: ";
		}
		cin >> option;
	}
	cout << setw(30) << "Exiting" << endl;
}

void topTenUsers(map<int, int> & userMap){
	multimap<int, int> tempMap;				//Creates a multimap to take into account for users that have the same amount
	//of reviews
	for (auto user : userMap){
		tempMap.insert(pair<int, int>(user.second, user.first));	//Goes through the map populating the new map with the key being
	}															//Users amount of reviews and the value being the user ID

	cout << "\t      Top 10 Reviewers!\n\tUser ID\t\tNumber of Reviews\n\n";

	int i = 0;
	for (auto rit = tempMap.rbegin(); rit != tempMap.rend(); ++rit){		//Iterates through the map in reverse

		if (i == 10){														//Breaks the loop after the top 10 users
			break;
		}
		cout << "\t" << rit->second << "\t\t\t" << rit->first << endl;

		i++;
	}
	cout << endl << endl;
}

//Main delcaration of general functions and files.
int main(int argc, string *argv){
	string f1 = "movies.txt";
	string f2 = "reviews1.txt";

	map<int, pair<Movie, vector<double>>> movieRatings;
	map<int, int> userRatings;
	map<double, Movie> avgMovieRate;

	parseMovie<map<int, pair<Movie, vector<double>>>>(f1, movieRatings);
	parseReviews(f2, movieRatings, userRatings);

	topTenMovies(movieRatings);
	topTenUsers(userRatings);

	specificMovie(movieRatings);

	system("pause");
}