#include "Ship.h"

/*Ship::Ship(const string & N, const int & Y)
{
	cout << "Con. is working" << endl;
	name = N;
	year = Y;
}

//Set
void Ship::SetName(const string &N)
{
	name = N;
}
void Ship::SetYear(const int & Y)
{
	if (Y < 0)
		year = 0;
	else
		year = Y;
}

//Get
string Ship::GetName() const
{
	return name;
}
int Ship::GetYear() const
{
	return year;
}


void Ship::print() const
{
	cout << "Name: " << name << endl;
	cout << "Year: " << year << endl;
}


Ship::~Ship()
{
	cout << "Des. is working" << endl;
}


ostream &operator<<(ostream & os, const Ship & sh)
{
	os << "Name: " << sh.name << endl << "Year: " << sh.year << endl;
	return os;
}*/

void Vehicle::print() const
{
	cout << "Vendor: " << vendor << endl;
	cout << "Speed: " << speed << endl;
}

string Ship::getName() const
{
	return Vehicle::vendor;
}

double Ship::getSpeed() const
{
	return Vehicle::speed;
}

int Ship::getTur() const
{
	return turbins;
}

void Ship::SetTur(const int &t)
{
	turbins = t;
}

void Ship::print() const
{
	Vehicle::print();
	cout << "Turbins: " << turbins << endl;
}

string Car::getName() const
{
	return Vehicle::vendor;
}

double Car::getSpeed() const
{
	return Vehicle::speed;
}

int Car::getPower() const
{
	return power;
}

void Car::setPower(const int &p)
{
	power = p;
}

void Car::print() const
{
	Vehicle::print();
	cout << "Power: " << power << endl;
}

Amphibia::Amphibia(Car car, Ship ship): Vehicle
{

}

void Amphibia::print() const
{
	cout << "Car" << endl;
	Car::print();
	cout << "Ship" << endl;
	Ship::print();
}
