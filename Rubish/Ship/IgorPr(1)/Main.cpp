#include"Ship.h"

void Task1()
{
	Car car("Fort", 120, 5);
	car.print();
	Ship ship("Mas", 80, 8);
	ship.print();
	Amphibia am(car, ship);
	am.print();
};

void Task2()
{

};

using namespace std;

void main()
{
	/*Ship ship("Titan", 1267);
	cout << ship << "It is ship" << endl;
	ship.~Ship();*/

	//Task1();

	//Task2();
	system("pause");
}