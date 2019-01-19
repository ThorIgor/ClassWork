#include "Class.h"

ostream & operator <<(ostream & os, Person & per)
{
	os << "Name: " << per.name << endl << "Hobby: " << per.hobby << endl << "Age: " << per.age << endl;
	return os;
}

istream & operator>>(istream & is, Person & per)
{
	char dname[20], dhobby[20];
	is >> dname >> dhobby >> per.age;
	per.ChangeName(dname);
	per.ChangeHobby(dhobby);
	return is;
}
