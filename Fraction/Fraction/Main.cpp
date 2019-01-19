#include <iostream>
#include "Fraction.h"

using namespace std;

int main()
{
	Fraction a(25, 7);
	a(10, 100);
	a.print();
	system("pause");
}