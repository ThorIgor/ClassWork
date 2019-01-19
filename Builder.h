#include<iostream>
#include<string>

using namespace std;

class House
{
	string roof;
	string wall;
	string basement;
public:
	void Roof(const string &r)
	{
		roof = r;
	}
	void Wall(const string &w)
	{
		wall = w;
	}
	void Basement(const string &b)
	{
		basement = b;
	}
	void print()
	{
		cout << "Roof: " << roof << endl;
		cout << "Wall: " << wall << endl;
		cout << "Basement: " << basement << endl;
	}
};

class Builder abstract
{
	House house;
public:
	void Basement()
	{
		house.Basement(" height - 50m width - 30m");
	};
	void Wall()
	{
		house.Wall(" height - 10m");
	};
	void Roof()
	{
		house.Roof(" wooden, height - 5m");
	};
	House getResult()
	{
		return house;
	};
};
