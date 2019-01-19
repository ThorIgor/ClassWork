#include <iostream>
#include <string>
using namespace std;
#include "Fraction.h"


Fraction::Fraction(int num, int denom)// 
{
	setNum(num);
	setDenom(denom);
	reduce();
}


void Fraction::print() const
{
	cout << num << "/" << denom << endl;
}
void Fraction::reduce()
{
	int dop1, dop2;
	if (num > denom)
	{
		dop1 = num;
		dop2 = denom;
	}
	else
	{
		dop1 = denom;
		dop2 = num;
	}
	while (true)
	{
		if (dop1%dop2 == 0)
		{
			num /= dop2;
			denom /= dop2;
			break;
		}
		else
		{
			int pr = dop1%dop2;
			dop1 = dop2;
			dop2 = pr;
		}
	}
	if (denom == 0)
	{
		denom = 1;
	}
	else if (denom < 0)
	{
		num *= -1;
		denom *= -1;
	}
}


Fraction Fraction::operator+(const Fraction & two) const
{
	int num = this->num * two.denom + this->denom * two.num;
	int denom = this->denom * two.denom;
	Fraction result(num, denom);
	result.reduce();
	return  result;
}
Fraction Fraction::operator-(const Fraction & two) const
{
	int num = this->num * two.denom - this->denom * two.num;
	int denom = this->denom * two.denom;
	Fraction result(num, denom);
	result.reduce();
	return result;
}
Fraction Fraction::operator*(const Fraction & two) const
{
	Fraction result(num * two.num, denom * two.denom);;
	result.reduce();
	return result;
}
Fraction Fraction::operator/(const Fraction & two) const
{
	Fraction result;
	if (two.denom != 0)
	{
		result = { num * two.denom, denom * two.num };
		result.reduce();
		return result;
	}
	else
		return result = { 0, 1 };
}


bool Fraction::operator==(const Fraction & two) const
{
	return this->num * two.denom == this->denom * two.num;
}
bool Fraction::operator<(const Fraction & two) const
{
	return this->num * two.denom < this->denom * two.num;
}
bool Fraction::operator>(const Fraction & two) const
{
	return this->num * two.denom > this->denom * two.num;
}


Fraction & Fraction::operator++() 
{
	this->num += denom;
	reduce();
	return *this;
}
Fraction Fraction::operator++(int)
{
	Fraction old(num, denom);
	++(*this);
	reduce();
	return old;
}


Fraction & Fraction::operator-()
{
	num *= -1;
	reduce();
	return *this;
}


int Fraction::operator[](const int & n) const 
{
	if (n == 1)
		return num;
	else if (n == 2)
		return denom;
	else
		cerr << "Error (only 1, 2)" << endl;
	return INT_MIN;
}
int Fraction::operator[](const string & n) const
{
	if (n == "first")
		return num;
	else if (n == "second")
		return denom;
	else
		cerr << "Error" << endl;
	return INT_MIN;
}


Fraction::operator int() const
{
	return num / denom;
}
Fraction::operator double() const
{
	return (double)num / denom;
}
Fraction::operator float() const
{
	return (float)num / denom;
}

void Fraction::operator()(const int & n, const int & d)
{
	setNum(n);
	setDenom(d);
}


Fraction::~Fraction()
{
}
