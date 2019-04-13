#include "Button.h"

Button::Button(const string & n) : name(n)
{}

void Button::SetName(const string & n)
{
	name = n;
}

string Button::GetName() const
{
	return name;
}

void WinButton::Press() const
{
	cout << "WinButton is pressed" << endl;
}

void MacButton::Press() const
{
	cout << "MacButton is pressed"
}

CheckBox::CheckBox(const string & n, const bool & ch): name(n), checked(ch)
{}

void CheckBox::SetName(const string & n)
{
	name = n;
}

string CheckBox::GetName() const
{
	return name;
}

void CheckBox::Press()
{
	(checked == 0) ? checked = true : checked = false;
}

void WinCheckBox::Press()
{
	CheckBox::Press();
	cout << "WinCheckBox is pressed" << endl;
}

void MacCheckBox::Press()
{
	CheckBox::Press();
	cout << "MaxCheckBox is pressed" << endl;
}
