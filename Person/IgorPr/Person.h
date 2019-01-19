#include<iostream>

using namespace std;

using namespace std;

class Person
{
private:
	char *name = nullptr;
	char *hobby = nullptr;
	int age;
public:
	Person(const char *n = "Guest", const char *h = "Nothing", const int a = 0)
	{
		ChangeName(n);
		ChangeHobby(h);
		ChangeAge(a);
		
	};
	explicit Person(const int a)
	{
		ChangeName("Guest");
		ChangeHobby("Nothing");
		age = a;
		
	}
	Person(const Person &per)
	{
		this->age = per.age;
		ChangeName(per.name);
		ChangeHobby(per.hobby);
		
	};
	
	void ChangeName(const char n[])
	{
		int size;
		delete[]name;
		name = new char[size=strlen(n) + 1];
		strcpy_s(name, size, n);
	};
	void ChangeHobby(const char h[])
	{
		int size;
		delete[]hobby;
		hobby = new char[size=strlen(h) + 1];
		strcpy_s(hobby, size, h);
	};
	void ChangeAge(const int a)
	{
		age = a;
	};
	void PersonOut()
	{
		cout << "Name: " << name << endl;
		cout << "Hobby: " << hobby << endl;
		cout << "Age: " << age << endl;
	};
	void In()
	{
		char word[20];
		int num;
		cout << "Name: ";
		cin.getline(word, 20);
		ChangeName(word);
		cout << "Hobby: ";
		cin.getline(word, 20);
		ChangeHobby(word);
		cout << "Age: ";
		cin >> num;
		ChangeAge(num);
	};

	Person &operator = (const Person &other)
	{
		if (this == &other)
			cerr << "Error" << endl;
		else
		{
			ChangeName(other.name);
			ChangeHobby(other.hobby);
			age = other.age;
		}
		return *this;
	}

	friend ostream & operator << (ostream &os, Person &per);
	friend istream & operator >> (istream &is, Person &per);
	~Person()
	{
		delete[]name;
		delete[]hobby;
	};
};
