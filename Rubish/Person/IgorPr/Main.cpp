#include<iostream>
#include<ctime>

#include "Class.h"

using namespace std;

void main()
{
	srand(time(0)); 
	
	Person a("Ann");
	cin >> a;
	cout << a << endl;
	system("pause");
}