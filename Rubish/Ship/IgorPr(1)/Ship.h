#include<iostream>
#include<string>

using namespace std;

/*class Ship
{
private:
	string name;
	int year;
public:
	Ship(const string &N = "New ship", const int &Y = 2000);
	//Set
	void SetName(const string &N);
	void SetYear(const int &Y);
	//Get
	string GetName() const;
	int GetYear() const;

	void print() const;
	friend ostream &operator << (ostream &os, const Ship &sh);

	~Ship();
};*/

 class Vehicle 
{
protected:
	string vendor;
	double speed;
public:
	Vehicle(const string &v = "Noname", const double &s = 100) : vendor(v), speed(s){}
	void print() const;
};

class Ship : virtual public Vehicle
{
protected:
	int turbins;
public:
	Ship(const string &v = "Noname", const int &s = 100, const int &t = 2) : Vehicle(v, s), turbins(t) {}
	string getName() const;
	double getSpeed() const;
	int getTur() const;
	void SetTur(const int &t);
	void print() const;
};

class Car : virtual public Vehicle
{
protected:
	int power;
public:
	Car(const string &v = "Noname", const int &s = 100, const int &p = 2) : Vehicle(v, s), power(p) {}
	string getName() const;
	double getSpeed() const;
	int getPower() const;
	void setPower(const int &p);
	void print() const;
};

class Amphibia : public Car, public Ship
{
public:
	Amphibia(Car car, Ship ship) : Vehicle(car.getName()+ship.getName(), car.getSpeed){
		Car::setPower(car.getPower());
		Ship::SetTur(ship.getTur());
	}
	Amphibia(const string &v = "Noname", const int &s = 100, const int &ts = 2, const int &pc = 4) : Vehicle(v, s), Car(v, s, pc), Ship(v, s, ts) {}
	void print() const;

};