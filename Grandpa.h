#include<iostream>
#include<string>

using namespace std;

class Grandpa
{
	string name;
public:
	Grandpa(const string &n)
	{
		name = n;
	}
	void SetName(const string &n)
	{
		name = n;
	}
	string GetName() const 
	{
		return name;
	}
	virtual void sleep() const
	{
		cout << typeid(*this).name()+6 << " " << name << " is sleeping!" << endl;
	}
};

class Daddy : public Grandpa
{
public:
	Daddy(const string &n) : Grandpa(n)
	{}
	virtual void walk() const
	{
		cout << typeid(*this).name()+6 << " " << Grandpa::GetName() << " is walking!" << endl;
	}
};

class Son : public Daddy
{
public:
	Son(const string &n) : Daddy(n)
	{}
	virtual void play() const 
	{
		cout << typeid(*this).name()+6 << " " << Grandpa::GetName() << " is playing!" << endl;
	}
};