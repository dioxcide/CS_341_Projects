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
#include <iterator>
#include <fstream>
#include <vector>
#include <algorithm>
#include <string>
#include <numeric>
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

class AverageRating{
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

		AverageRating(int id, double rate, int reviews, string name, int num, int num2, int num3, int num4, int num5){
			movieID = id;
			averageRate = rate;
			totalReviews = reviews;
			movieName = name;
			five = num5;
			four = num4;
			three = num3;
			two = num2;
			one = num;
		}
};

template<typename Vector>
void parseMovie(const string &fileName, Vector & v){
	ifstream masterFile(fileName);
	string line;
	
	while (getline(masterFile, line))
	{
		auto movie = ParseMovie(line);
		v.push_back(pair<Movie, vector<double> >(movie, vector<double>()));
	}
}

template<typename Container, typename Container2>
void parseReviews(const string & filename, Container & c, Container2 & c2){
	ifstream masterFile(filename);
	string line;

	while (getline(masterFile, line))
	{
		auto review = ParseReview(line);
		auto reviewIter = find_if(c2.begin(), c2.end(), [=](pair<int,int> e){ return e.first == review.userID; });
		
		if (reviewIter != c2.end()){
			reviewIter->second++;
		}
		else{
			c2.push_back(pair<int, int>(review.userID, 1));
		}

		auto movieIter = find_if(c.begin(), c.end(), [=](pair<Movie, vector<double>> e){ return e.first.movieID == review.movieID; });
		
		if (movieIter != c.end()){
			movieIter->second.push_back(review.movieRating);
		}

	}
	
	sort(c2.begin(), c2.end(), [=](pair<int, int> a, pair<int, int> b){return a.second != b.second ? a.second > b.second : a.first < b.first; });

}

void topTenMovies(const vector<pair<Movie, vector<double>>> &v, vector<AverageRating> & v2){
	
	for (auto &vec : v){
		double accum = accumulate(vec.second.begin(), vec.second.end(), 0);
		accum /= vec.second.size();
		
		int one = count(vec.second.begin(),vec.second.end(),1);
		int two = count(vec.second.begin(), vec.second.end(), 2);
		int three = count(vec.second.begin(), vec.second.end(), 3);
		int four = count(vec.second.begin(), vec.second.end(), 4);
		int five = count(vec.second.begin(), vec.second.end(), 5);
		
		AverageRating movieAvg(vec.first.movieID, accum, vec.second.size(), vec.first.movieName, one, two, three, four, five);	
		v2.push_back(movieAvg);

	}

	sort(v2.begin(), v2.end(), [](AverageRating a, AverageRating b){return a.averageRate > b.averageRate; });
}

void specificMovie(const vector<AverageRating> & v){
	int option = -1;
	cin >> option;

	while (option !=0){
		auto avgIter = find_if(v.begin(), v.end(), [=](AverageRating e){return e.movieID == option; });
		cout << "\t" << avgIter->movieID << "\t    " << avgIter->movieName << "\t           " << avgIter->averageRate << endl;
		cout << "Five Stars: " << avgIter->five << endl;
		cout << "Four Stars: " << avgIter->four << endl;
		cout << "Three Stars: " << avgIter->three << endl;
		cout << "Two Stars: " << avgIter->two << endl;
		cout << "One Stars: " << avgIter->one << endl;
		cout << endl;
		cin >> option;
	}
}

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
	cout << endl;
}

void printMovies(const vector<AverageRating> &v){
	
	int i = 0;
	cout << "\t          Top Ten Movies!\n\n\tMovie ID\t\tMovieName\t\tMovie Rating\n\n";
	for (auto & temp : v){
		if (i == 10){
			break;
		}
		cout<<"\t" << temp.movieID <<"\t    " << temp.movieName << "\t           " << temp.averageRate << endl;
		i++;
	}
	cout << endl;
}

int main(int argc, string *argv){
	string f1 = "movies.txt";
	string f2 = "reviews1.txt";
	vector<pair<Movie,vector<double>>> movieRatings;
	vector<AverageRating> movieAvgRate;
	vector<pair<int, int>> userRatings;

	parseMovie<vector<pair<Movie, vector<double>>>>(f1, movieRatings);
	parseReviews<vector<pair<Movie, vector<double>>>, vector<pair<int, int>>>(f2, movieRatings, userRatings);
	
	topTenMovies(movieRatings, movieAvgRate);
	cout << "DONE" << endl;
	
	printMovies(movieAvgRate);
	printReviews(userRatings);
	specificMovie(movieAvgRate);
	
	system("pause");
}